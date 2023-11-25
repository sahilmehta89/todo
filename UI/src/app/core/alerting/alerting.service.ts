import { Injectable } from '@angular/core';

import { ToastrService } from 'ngx-toastr';

import { AlertingConfig } from './alerting-config.model';

@Injectable({ providedIn: 'root' })
export class AlertingService {

    constructor(private _toastrService: ToastrService) {
    }

    error(alertingConfig: AlertingConfig) {
        this._toastrService.error(alertingConfig.message, alertingConfig.title, alertingConfig.override);
    }

    info(alertingConfig: AlertingConfig) {
        this._toastrService.info(alertingConfig.message, alertingConfig.title, alertingConfig.override);
    }

    success(alertingConfig: AlertingConfig) {
        this._toastrService.success(alertingConfig.message, alertingConfig.title, alertingConfig.override);
    }

    warning(alertingConfig: AlertingConfig) {
        this._toastrService.warning(alertingConfig.message, alertingConfig.title, alertingConfig.override);
    }

    clear(toastId?: number) {
        this._toastrService.clear(toastId);
    }

}
