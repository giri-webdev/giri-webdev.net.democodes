using System.Web.Mvc;

namespace giri_webdev_livedemo.Utilities
{
    /// <summary>
    /// JUNE-22-2015
    /// </summary>
    public static class Print
    {
        //Custom external helper
        public static MvcHtmlString PrintMessage(this HtmlHelper helper, string message)
        {
            TagBuilder tagBuilder = new TagBuilder("h1");
            if (message.Length <= 5)
            {
                tagBuilder.Attributes["style"] = "color:green";
                tagBuilder.SetInnerText(string.Format("Hello {0} !! from external helper.", message));
            }
            else
            {
                tagBuilder.Attributes["style"] = "color:red";
                tagBuilder.SetInnerText(string.Format("Hello {0} !! from external helper.", message));
            }

            return new MvcHtmlString(tagBuilder.ToString());
        }
    }
}