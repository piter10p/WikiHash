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

                throw new Exception("No article with specified link found.");
            }
            catch(Exception e)
            {
                throw new Exception("Failed to get article.", e);
            }
        }

        public static void UpdateArticle(Article article)
        {
            try
            {
                if (article == null)
                    throw new ArgumentNullException();

                var context = DAL.ApplicationDbContext.Create();

                var query = from a in context.Articles where a.Title == article.Title select a;

                if (query.Count() != 1)
                    throw new Exception("No article with specified title found.");

                var targetArticle = query.First();
                targetArticle.Title = article.Title;
                targetArticle.CreationDate = article.CreationDate;

                context.SaveChanges();
            }
            catch
            {
                throw;
            }
        }
    }
}