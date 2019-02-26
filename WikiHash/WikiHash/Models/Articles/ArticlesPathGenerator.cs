using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace WikiHash.Models.Articles
{
    public static class ArticlesPathGenerator
    {
        private const string ArticlesFolderPath = @"~/Content/Articles/";
        private const string FileExtension = ".txt";

        public static string Generate(string link)
        {
            return HostingEnvironment.MapPath(ArticlesFolderPath + link + FileExtension);
        }
    }
}