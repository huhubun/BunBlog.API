using CommonMark;
using CommonMark.Syntax;
using System.IO;

namespace Bun.Blog.WebApi.Formatters
{
    public class PostFormatter : CommonMark.Formatters.HtmlFormatter
    {
        public PostFormatter(TextWriter target, CommonMarkSettings settings) : base(target, settings)
        {
        }

        protected override void WriteInline(Inline inline, bool isOpening, bool isClosing, out bool ignoreChildNodes)
        {
            if (inline.Tag == InlineTag.LineBreak && !this.RenderPlainTextInlines.Peek())
            {
                ignoreChildNodes = false;

                this.Write("</p><p>");
            }
            else
            {
                base.WriteInline(inline, isOpening, isClosing, out ignoreChildNodes);
            }
        }
    }
}
