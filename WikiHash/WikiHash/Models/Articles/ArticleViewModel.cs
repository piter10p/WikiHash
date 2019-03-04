using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WikiHash.Models.Articles
{
    public class ArticleViewModel: LinkableViewModel
    {
        public static ArticleViewModel FromArticle(Article article)
        {
            var output = new ArticleViewModel();
            output.Title = article.Title;
            output.Body = article.Body;
            output.CreationDate = article.CreationDate;
            return output;
        }

        public Bodies.Body Body { get; set; }
    }
}