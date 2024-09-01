using Microsoft.AspNetCore.Html;
using System.Text.RegularExpressions;

namespace MVC_UI.Extensions
{
    public static  class FromExtensions
    {
        public static async Task<TimeSpan> CalcualteReadingTime(this string value)
        {
            return TimeSpan.FromMinutes((value.Length / 10) + 1);
        }

        public static async Task<byte[]> StringToByteArrayAsync(this IFormFile formFile)
        {
            using MemoryStream memory = new MemoryStream();
            await formFile.CopyToAsync(memory);
            return memory.ToArray();
        }



        public static Task<IHtmlContent> FormatTextAreaAsync(this string content)
        {
            // If the content is null or empty, return an empty HtmlString
            if (string.IsNullOrWhiteSpace(content))
            {
                return Task.FromResult<IHtmlContent>(new HtmlString(string.Empty));
            }

            // Replace line breaks with <br> tags
            var formattedContent = Regex.Replace(content, @"\r\n|\n", "<br>").Trim();

            // Split the content into paragraphs
            var paragraphs = formattedContent.Split(new[] { "<br>" }, StringSplitOptions.RemoveEmptyEntries);

            var result = new HtmlContentBuilder();

            // Append each paragraph wrapped in <p> tags
            foreach (var paragraph in paragraphs)
            {
                if (!string.IsNullOrWhiteSpace(paragraph))
                {
                    result.AppendHtml($"<p>{paragraph.Trim()}</p>");
                }
            }

            // Return the constructed HtmlContentBuilder as IHtmlContent
            return Task.FromResult<IHtmlContent>(result);
        }

        public static string ToHtmlString(this IHtmlContent content)
        {
            using (var writer = new StringWriter())
            {
                content.WriteTo(writer, System.Text.Encodings.Web.HtmlEncoder.Default);
                return writer.ToString();
            }
        }
    }
}
