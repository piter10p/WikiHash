using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WikiHash.Models.Articles
{
    public static class ArticlesManager
    {
        public static Article GetArticle(string link)
        {
            
            try
            {
                if (link == null)
                    throw new ArgumentNullException();

                var context = DAL.ApplicationDbContext.Create();
                var query = from a in context.Articles select a;
                
                foreach(var article in query)
                {
                    if (article.Link == link)
                        return article;
                }

                throw new KeyNotFoundException();
            }
            catch
            {
                throw;
            }
        }

        public static Article UpdateArticle(Article article)
        {
            try
            {
                if (article == null)
                    throw new ArgumentNullException();

                var context = DAL.ApplicationDbContext.Create();

                var query = from a in context.Articles where a.Title == article.Title select a;

                if (query.Count() != 1)
                    throw new KeyNotFoundException();

                var targetArticle = query.First();
                targetArticle.Title = article.Title;
                targetArticle.CreationDate = article.CreationDate;

                context.SaveChanges();

                return targetArticle;
            }
            catch
            {
                throw;
            }
        }

        public static ArticleListModel[] GetFilteredArticles(string filter, string category)
        {
            try
            {
                if (filter == null || category == null)
                    throw new ArgumentNullException();

                var context = DAL.ApplicationDbContext.Create();

                IQueryable query;

                if(category == "all")
                {
                    query = from a in context.Articles where a.Title.Contains(filter) select a;
                }
                else
                {
                    query = from a in context.Articles.Include("Category")
                            where a.Title.Contains(filter) && a.Category.CategoryName == category select a;
                }

                var articlesList = new List<ArticleListModel>();

                foreach(Article article in query)
                {
                    var listModel = ArticleListModel.FromArticle(article);
                    articlesList.Add(listModel);
                }

                return articlesList.ToArray();
            }
            catch
            {
                throw;
            }
        }

        public static Article AddArticle(ArticleCreationModel model)
        {
            try
            {
                if (model == null)
                    throw new ArgumentNullException();

                var context = DAL.ApplicationDbContext.Create();
                var article = Article.FromCreationModel(model);

                var query = from a in context.Articles where a.Title == article.Title select a;

                if (query.Count() != 0)
                    throw new EntryExistsException();

                article = context.Articles.Add(article);
                context.SaveChanges();

                return article;
            }
            catch
            {
                throw;
            }
        }
    }
}