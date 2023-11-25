import { Injectable } from '@angular/core';

import { environment } from '../../../environments/environment';

@Injectable()
export class EnvironmentService {

    constructor() { }

    public getToDoApiBaseUrl(): string {
        return environment.todoApiBaseUrl;
    }
}
