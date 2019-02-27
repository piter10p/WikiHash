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
    }
}