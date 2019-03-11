using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WikiHash.Models.Articles
{
    public class ArticleListModel
    {
        public string Title { get; private set; }
        public string Link { get; private set; }

        private ArticleListModel() { }

        public static ArticleListModel FromArticle(Article article)
        {
            var model = new ArticleListModel();
            model.Title = article.Title;
            model.Link = article.Link;
            return model;
        }
    }
}