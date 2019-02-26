using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WikiHash.Models.Articles.Bodies
{
    public enum FrameWidth
    {
        W1, W2, W3, W4, W5, W6, AUTO
    }

    public static class FrameWidthParser
    {
        public static FrameWidth Parse(string text)
        {
            switch(text)
            {
                case "1":
                    return FrameWidth.W1;
                case "2":
                    return FrameWidth.W2;
                case "3":
                    return FrameWidth.W3;
                case "4":
                    return FrameWidth.W4;
                case "5":
                    return FrameWidth.W5;
                case "6":
                    return FrameWidth.W6;
                case "auto":
                    return FrameWidth.AUTO;

            }

            throw new Exception("Failed to parse Frame Width string.");
        }

        public static string ToBootstrapString(FrameWidth frameWidth)
        {
            switch(frameWidth)
            {
                case FrameWidth.W1:
                    return "-2";
                case FrameWidth.W2:
                    return "-4";
                case FrameWidth.W3:
                    return "-6";
                case FrameWidth.W4:
                    return "-8";
                case FrameWidth.W5:
                    return "-10";
                case FrameWidth.W6:
                    return "-12";
                case FrameWidth.AUTO:
                    return "";
            }

            return null;
        }
    }
}