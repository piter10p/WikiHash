using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WikiHash.Models.Articles.Bodies
{
    public class ContentFrame
    {
        public ContentFrame(string content, string width)
        {
            Content = content;
            Width = FrameWidthParser.Parse(width);
        }

        public string Content { get; private set; }
        public FrameWidth Width { get; private set; }
    }
}