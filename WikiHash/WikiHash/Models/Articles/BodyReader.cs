using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace WikiHash.Models.Articles
{
    public static class BodyReader
    {
        public static string Read(string link)
        {
            try
            {
                var path = ArticlesPathGenerator.Generate(link);
                var text = "";

                var fileInfo = new FileInfo(path);
                if (!fileInfo.Exists)
                    throw new Exception("File not exists.");

                using (var sr = new StreamReader(path))
                {
                    text = sr.ReadToEnd();
                }

                return text;
            }
            catch(Exception e)
            {
                throw new Exception("Failed to read article body.", e);
            }
        }
    }
}