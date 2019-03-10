using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;

namespace WikiHash.Models.Articles
{
    public static class HTMLSecurityTagsRemover
    {
        private const string ScriptTagPattern = "<[Ss][Cc][Rr][Ii][Pp][Tt]";

        public static string RemoveUnsecureTags(string html)
        {
            return RemoveScriptTags(html);
        }

        private static string RemoveScriptTags(string html)
        {
            return Regex.Replace(html, ScriptTagPattern, "");
        }
    }
}