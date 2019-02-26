using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WikiHash.Models.Articles.Bodies
{
    public class ContentFrame
    {
        public ContentFrame(string text, string width)
        {
            Text = text;
            Width = FrameWidthParser.Parse(width);
        }

        public string Text { get; private set; }
        public FrameWidth Width { get; private set; }
    }
}