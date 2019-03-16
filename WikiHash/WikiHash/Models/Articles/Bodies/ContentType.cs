using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WikiHash.Models.Articles.Bodies
{
    public enum ContentType
    {
        Text,
        Media
    }

    public static class ContentTypeParser
    {
        private const string TextString = "text";
        private const string MediaString = "media";

        public static string ToString(ContentType contentType)
        {
            switch(contentType)
            {
                case ContentType.Text:
                    return TextString;
                default:
                    return MediaString;
            }
        }

        public static ContentType Parse(string text)
        {
            switch(text)
            {
                case TextString:
                    return ContentType.Text;
                default:
                    return ContentType.Media;
            }
        }
    }
}