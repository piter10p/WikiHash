using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace WikiHash.Models
{
    public static class TitleFunctions
    {
        //Title can be made of characters: unicode letters, digits, :, ?, " and spaces.
        //Link is generated from lowered title, without :, ? and " characters, and with spaces replaced by - character.

        public static string GenerateLink(string title)
        {
            if (!Validate(title))
                throw new Exception("Title is not valid");

            var output = title.ToLower();
            output = output.Replace(' ', '-');
            output = Regex.Replace(output, "[:?\"]", string.Empty);//matches :, ? and " characters
            return output;
        }

        public static bool Validate(string title)
        {
            var match = Regex.Match(title, "([\\p{L} \":?\\d]+)");//matches all string made from unicode letters, digits, :, ?, " and spaces.

            if (match.Value == title)
                return true;
            return false;
        }
    }
}