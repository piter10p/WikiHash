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

        public static void SaveArticle(Article article, bool overwrite = false)
        {
            try
            {
                if (article == null)
                    throw new ArgumentNullException();

                var context = DAL.ApplicationDbContext.Create();

                if (overwrite)
                {
                    if(!IsArticleExisting(article.Title))//TODO: Add data updating if existing
                        context.Articles.Add(article);
                }
                else
                {
                    if (!IsArticleExisting(article.Title))
                        context.Articles.Add(article);
                    else
                        throw new Exception("Article exists already.");
                }

                context.SaveChanges();
            }
            catch(Exception e)
            {
                throw new Exception("Failed to save article.", e);
            }
        }

        private static bool IsArticleExisting(string title)
        {
            var context = DAL.ApplicationDbContext.Create();
            var query = from a in context.Articles where a.Title == title select a;

            if (query.Count() != 0)
                return true;
            return false;
        }
    }
}