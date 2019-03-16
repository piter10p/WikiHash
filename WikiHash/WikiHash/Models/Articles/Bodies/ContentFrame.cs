using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;

namespace WikiHash.Models.Articles.Bodies
{
    public class ContentFrame
    {
        public ContentFrame(string content, string width, string contentType)
        {
            if (content == null || width == null || contentType == null)
                throw new ArgumentNullException();

            Content = content;
            Width = FrameWidthParser.Parse(width);
            ContentType = ContentTypeParser.Parse(contentType);
        }

        public ContentFrame(string content, FrameWidth width, ContentType contentType)
        {
            Content = content ?? throw new ArgumentNullException();
            Width = width;
            ContentType = contentType;
        }

        public static ContentFrame FromPrototype(ContentFramePrototype prototype)
        {
            if (prototype == null)
                throw new ArgumentNullException();

            return new ContentFrame(prototype.Content, prototype.Width, prototype.ContentType);
        }

        public ContentType ContentType { get; private set; }
        public string Content { get; private set; }
        public FrameWidth Width { get; private set; }
    }
}