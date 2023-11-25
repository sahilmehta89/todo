import { Injectable } from '@angular/core';

import { AlertingConfig } from './alerting-config.model';
import { AlertingConstants } from '../constants/alerting.constants';

@Injectable({ providedIn: 'root' })
export class AlertingConfigService {

    //Get All Data
    static getAlertingConfigForErrorInGetTodos(): AlertingConfig {
        const alertingConfig: AlertingConfig = {
            message: AlertingConstants.Error_GetTodos,
        }
        return alertingConfig;
    }

    static getAlertingConfigForErrorInAddTodoItem(): AlertingConfig {
        const alertingConfig: AlertingConfig = {
            message: AlertingConstants.Error_AddTodoItem,
        }
        return alertingConfig;
    }

    static getAlertingConfigForErrorInEditTodoItem(): AlertingConfig {
        const alertingConfig: AlertingConfig = {
            message: AlertingConstants.Error_EditTodoItem,
        }
        return alertingConfig;
    }

    static getAlertingConfigForErrorInMarkDoneTodoItem(): AlertingConfig {
        const alertingConfig: AlertingConfig = {
            message: AlertingConstants.Error_MarkDoneTodoItem,
        }
        return alertingConfig;
    }

    static getAlertingConfigForErrorInDeleteTodoItem(): AlertingConfig {
        const alertingConfig: AlertingConfig = {
            message: AlertingConstants.Error_DeleteTodoItem,
        }
        return alertingConfig;
    }
}
