import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';

import { TodoApiClient } from './todo-apiclient.service';
import { TodoApiClientBase } from './todo-apiclient.extension';
import { TodoApiClientConfiguration } from './todo-apiclient-config';

@NgModule({
    imports: [
        HttpClientModule
    ],
    providers: [
        TodoApiClient,
        TodoApiClientBase,
        TodoApiClientConfiguration,
    ]
})
export class TodoApiClientModule {
}
