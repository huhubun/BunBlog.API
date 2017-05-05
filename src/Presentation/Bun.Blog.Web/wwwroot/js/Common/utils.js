/// <reference path="../../typings/index.d.ts" />
var Bun;
(function (Bun) {
    var Blog;
    (function (Blog) {
        var Common;
        (function (Common) {
            var Utils;
            (function (Utils) {
                var Notification = (function () {
                    function Notification() {
                    }
                    Notification.success = function (message) {
                        this._initNotificationElement();
                        this._notificationElement.success(message);
                    };
                    Notification.error = function (message) {
                        this._initNotificationElement();
                        this._notificationElement.error(message);
                    };
                    Notification._initNotificationElement = function () {
                        if (!this._notificationElement) {
                            var notification = $("#Notifications");
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
                    };
                    return Notification;
                }());
                Utils.Notification = Notification;
            })(Utils = Common.Utils || (Common.Utils = {}));
        })(Common = Blog.Common || (Blog.Common = {}));
    })(Blog = Bun.Blog || (Bun.Blog = {}));
})(Bun || (Bun = {}));
//# sourceMappingURL=utils.js.map