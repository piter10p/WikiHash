using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;

namespace WikiHash.Models.Articles.Bodies
{
    public class ContentFrame
    {
        public ContentFrame(string content, string width)
        {
            if (content == null || width == null)
                throw new ArgumentNullException();

            Content = content;
            Width = FrameWidthParser.Parse(width);
        }

        public ContentFrame(string content, FrameWidth width)
        {
            Content = content ?? throw new ArgumentNullException();
            Width = width;
        }

        public static ContentFrame FromPrototype(ContentFramePrototype prototype)
        {
            if (prototype == null)
                throw new ArgumentNullException();

            return new ContentFrame(prototype.Content, prototype.Width);
        }

        public string Content { get; private set; }
        public FrameWidth Width { get; private set; }
    }
}