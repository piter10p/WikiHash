using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;


namespace WikiHash.Models
{
    public static class PathsGenerator
    {
        private const string ArticlesFolderPath = @"~/Content/Articles/";
        private const string FileExtension = ".xml";

        private const string MediasFolderPath = @"~/Content/Medias/";

        public static string Article(string link)
        {
            return HostingEnvironment.MapPath(ArticlesFolderPath + link + FileExtension);
        }

        public static string Media(string fileName)
        {
            return HostingEnvironment.MapPath(MediasFolderPath + fileName);
        }
    }
}