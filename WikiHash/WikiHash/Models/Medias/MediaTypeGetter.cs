using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;


namespace WikiHash.Models.Medias
{
    public static class MediaTypeGetter
    {
        public static string GetMediaType(string fileName)
        {
            const string pattern = @"(?<=\.)\w+$";

            var match = Regex.Match(fileName, pattern);

            if (!match.Success)
                throw new Exception("Filename have not a valid extension.");

            var extension = match.Value.ToLower();

            switch(extension)
            {
                case "jpg":
                case "png":
                case "jpeg":
                case "gif":
                case "apng":
                case "svg":
                case "bmp":
                    return "image";
                case "mp4":
                    return "video";
                default:
                    return "unsupported";
            }
        }
    }
}