import { Injectable } from '@angular/core';

import { TodoApiClientConfiguration } from './todo-apiclient-config';

@Injectable()
export class TodoApiClientBase {

    constructor(private todoApiClientConfiguration: TodoApiClientConfiguration) {
    }

    protected transformOptions(options: any) {
        return Promise.resolve(options);
    }

    public getBaseUrl(url: string): string {
        return this.todoApiClientConfiguration.getBaseUrl();
    }

}