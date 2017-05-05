/// <reference path="../../../typings/index.d.ts" />
/// <reference path="../../common/enums.ts" />

namespace Bun.Blog.Admin.Post {
    export class Edit {
        public constructor() {
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

            $("#BreadcrumbMenu .update-post").click(e => $("#UpdatePost").submit());
        }
    }
}