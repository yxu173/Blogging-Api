using Ganss.Xss;
using Markdig;

namespace Application.Services;

public class PostServices
{
    public string ConvertMarkdownToHtml(string markdownContent)
    {
        var pipeline = new MarkdownPipelineBuilder().Build();

        var result = Markdown.ToHtml(markdownContent, pipeline);

        var sanitize = new HtmlSanitizer();

        return sanitize.Sanitize(result);
    }
}