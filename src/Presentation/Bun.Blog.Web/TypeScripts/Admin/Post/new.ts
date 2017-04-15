/// <reference path="../../../typings/index.d.ts" />

namespace Bun.Blog.Admin.Post {
    export class NewOptions {
        publishUrl: string;
    }

    export class New {
        options: NewOptions;

        public constructor(options: NewOptions) {
            this.options = options;
            this.init();
        }

        private init() {
            $("#EditorToolbar").kendoToolBar({
                items: [
                    {
                        type: "buttonGroup",
                        buttons: [
                            { text: "文本", togglable: true, group: "position", selected: true },
                            { text: "预览", togglable: true, group: "position" }
                        ]
                    },
                    { type: "separator" },
                    { template: `<button class="bun-editor-toolbar-btn" type="button"><span class="fa fa-bold"></span></button>` },
                    { template: `<button class="bun-editor-toolbar-btn" type="button"><span class="fa fa-italic"></span></button>` },

                    { template: `<button class="bun-editor-toolbar-btn" type="button"><span class="fa fa-quote-left"></span></button>` },
                    { template: `<button class="bun-editor-toolbar-btn" type="button"><span class="fa fa-code"></span></button>` },
                    { template: `<button class="bun-editor-toolbar-btn" type="button"><span class="fa fa-link"></span></button>` },

                    { template: `<button class="bun-editor-toolbar-btn" type="button"><span class="fa fa-list"></span></button>` },
                    { template: `<button class="bun-editor-toolbar-btn" type="button"><span class="fa fa-list-ol"></span></button>` },
                ]
            });

            $("#BreadcrumbMenu .publish-post").click(e => {
                var publishData = {
                    title: $("#Title").val(),
                    content: $("#Content").val()
                };

                var p = $.post(this.options.publishUrl, publishData, (data) => {
                    debugger;
                    return data;
                });

                p.done((a, b, c, d, e, f, g) => { debugger; });

                //$.ajax({
                //    url: this.options.publishUrl,
                //    method: "POST",
                //    data: publishData
                //})
            });

        }
    }
}