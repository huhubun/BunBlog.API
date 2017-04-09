/// <reference path="../../../typings/index.d.ts" />

namespace Bun.Blog.Admin.Post {
    export class List {
        public constructor() {
            this.init();
        }

        private init() {
            $("#DatePicker").kendoDatePicker();

            var p = $("#DatePicker").data("kendoDatePicker");
            p.value("2017-01-02");
        }
    }
}