using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WikiHash.Models.Articles
{
    public class ArticleViewModel
    {
        public static ArticleViewModel FromArticle(Article article)
        {
            var output = new ArticleViewModel();
            output.Title = article.Title;
            output.Body = article.Body;
            return output;
        }

        [MaxLength(256)]
        public string Title { get; set; }

        public string Body { get; set; }
    }
}