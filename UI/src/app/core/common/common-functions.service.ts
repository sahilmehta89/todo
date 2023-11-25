import { Injectable } from '@angular/core';

@Injectable({ providedIn: 'root' })
export class CommonFunctionsService {

    constructor() { }

    isApiResult(value: any): boolean {
        let result = false;
        try {
            result = (typeof value !== undefined && value !== null && value.success !== undefined)
        } catch (error) {
            result = false;
        }
        return result;
    }

    getDOB(value: any): string {
        let dob = new Date(value);
        dob.setUTCDate(dob.getDate());
        dob.setUTCHours(0, 0, 0, 0);
        return dob.toISOString();
    }

    getNotNullDateValue(value: any): string {
        let result: string = '';
        try {
            if (this.isObjectNotNull(value)) {
                let dob = new Date(value);
                dob.setUTCDate(dob.getDate());
                dob.setUTCHours(0, 0, 0, 0);
                result = dob.toISOString();
            }
            else {
                result = '';
            }
        } catch (error) {
            result = '';
        }
        return result;
    }

    getNotNullArrayValue(value: any[]): any[] {
        let result: any[] = [];

        try {
            result = this.isArrayNotEmpty(value) ? value : [];
        } catch (error) {
            result = [];
        }
        return result;
    }


    getNotNullStringValue(value: string): string {
        let result: string = '';

        try {
            result = this.isStringNotNullAndWhiteSpace(value) ? value : '';
        } catch (error) {
            result = '';
        }
        return result;
    }

    getRefreshParamValue(): number {
        return new Date().getTime();
    }

    isArrayNotEmpty(value: any): boolean {
        let result: boolean = false;
        try {
            result = (typeof value !== "undefined" && value !== null && Array.isArray(value) && value.length > 0)
        } catch (error) {
            result = false;
        }
        return result;
    }

    isArrayWithData(value: any[]): boolean {
        let result: boolean = false;
        try {
            result = value.length > 0;
        } catch (error) {
            result = false;
        }
        return result;
    }

    isNumberNotNull(value: any): boolean {
        let result: boolean = false;
        try {
            result = (typeof value !== "undefined" && value != null && (typeof value === 'number' || value instanceof Number) && value.toString().trim() !== '' && !Number.isNaN(value))
        } catch (error) {
            result = false;
        }
        return result;
    }

    isObjectNotNull(value: any): boolean {
        let result: boolean = false;
        try {
            result = (typeof value !== "undefined" && value !== null)
        } catch (error) {
            result = false;
        }
        return result;
    }

    isObjectNull(value: any): boolean {
        let result: boolean = true;
        try {
            result = (typeof value === "undefined" || value === null);
        } catch (error) {
            result = true;
        }
        return result;
    }

    isStringNullOrWhiteSpace(value: any): boolean {
        let result: boolean = true;
        try {
            result = (typeof value === "undefined" || value === null || value.trim() === '')
        } catch (error) {
            result = false;
        }
        return result;
    }

    isStringNotNullAndWhiteSpace(value: any): boolean {
        let result: boolean = false;
        try {
            result = (typeof value !== "undefined" && value != null && (typeof value === 'string' || value instanceof String) && value.trim() !== '')
        } catch (error) {
            result = false;
        }
        return result;
    }

    getFullName(firstName: string, lastName: string): string {
        return firstName + ' ' + lastName;
    }

    getNumber(value: string, defaultValue: number): number {
        let result: number = defaultValue;
        try {
            result = this.isStringNullOrWhiteSpace(value) ? defaultValue : parseInt(value);
        }
        catch (error) {
            result = defaultValue;
        }
        return result;
    }

    sortByProperty<T>(array: T[], propName: keyof T, order: 'ASC' | 'DESC'): void {
        array.sort((a, b) => {
            if (a[propName] < b[propName]) {
                return -1;
            }

            if (a[propName] > b[propName]) {
                return 1;
            }
            return 0;
        });

        if (order === 'DESC') {
            array.reverse();
        }
    }
}
