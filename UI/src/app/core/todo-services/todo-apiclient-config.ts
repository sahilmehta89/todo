import { Injectable } from '@angular/core';

import { EnvironmentService } from '../environment/environment.service';

@Injectable()
export class TodoApiClientConfiguration {

    constructor(private environmentService: EnvironmentService) {
    }

    public getBaseUrl(): string {
        return this.environmentService.getToDoApiBaseUrl();
    }

}
