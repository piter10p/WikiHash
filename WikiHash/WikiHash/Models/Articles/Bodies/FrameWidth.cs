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
        const string W1String = "1";
        const string W2String = "2";
        const string W3String = "3";
        const string W4String = "4";
        const string W5String = "5";
        const string W6String = "6";
        const string WAutoString = "auto";

        public static FrameWidth Parse(string text)
        {
            switch(text)
            {
                case W1String:
                    return FrameWidth.W1;
                case W2String:
                    return FrameWidth.W2;
                case W3String:
                    return FrameWidth.W3;
                case W4String:
                    return FrameWidth.W4;
                case W5String:
                    return FrameWidth.W5;
                case W6String:
                    return FrameWidth.W6;
                case WAutoString:
                    return FrameWidth.AUTO;

            }

            throw new Exception("Failed to parse Frame Width string.");
        }

        public static string ToString(FrameWidth width)
        {
            switch(width)
            {
                case FrameWidth.W1:
                    return W1String;
                case FrameWidth.W2:
                    return W2String;
                case FrameWidth.W3:
                    return W3String;
                case FrameWidth.W4:
                    return W4String;
                case FrameWidth.W5:
                    return W5String;
                case FrameWidth.W6:
                    return W6String;
                case FrameWidth.AUTO:;
                    return WAutoString;
            }

            throw new Exception("Failed to convert from Frame Width to string.");
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