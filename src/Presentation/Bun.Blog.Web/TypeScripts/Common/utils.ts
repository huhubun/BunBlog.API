/// <reference path="../../typings/index.d.ts" />

namespace Bun.Blog.Common.Utils {
    export class Notification {
        public static success(message: string) {
            this._initNotificationElement();
            this._notificationElement.success(message);
        }

        public static error(message: string) {
            this._initNotificationElement();
            this._notificationElement.error(message);
        }

        private static _notificationElement: kendo.ui.Notification;

        private static _initNotificationElement() {
            if (!this._notificationElement) {
                let notification = $("#Notifications");

                this._notificationElement = $("#Notifications").kendoNotification({
                    appendTo: "#Notifications",
                    hideOnClick: true,
                    autoHideAfter: 0,
                    templates: [{
                        type: "success",
                        template: $("#KendoSuccessNotificationTemplate").html()
                    }, {
                        type: "error",
                        template: $("#KendoErrorNotificationTemplate").html()
                    }]
                }).data("kendoNotification");
            }
        }
    }
}