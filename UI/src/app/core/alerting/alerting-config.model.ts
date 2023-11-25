
export class AlertingConfig {

    message?: string | undefined;
    title?: string | undefined;
    override?: any | undefined;

    constructor(message: string = '', title: string = '', override: any = null) {
        this.message = message;
        this.title = title;
        this.override = override;
    }

    static fromGenericError(error: any): AlertingConfig {
        const alertingConfig = new AlertingConfig();
        alertingConfig.message = 'Some error occured. Please try again';
        return alertingConfig;
    }

}
