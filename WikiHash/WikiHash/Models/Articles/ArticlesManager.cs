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
                var query = from a in context.Articles where a.Link == link select a;
                var result = query.First();

                return result;
            }
            catch(ArgumentNullException e)
            {
                throw new Exception("Argument is null.", e);
            }
            catch(InvalidOperationException e)
            {
                throw new Exception("No Article with specified link found.", e);
            }
        }
    }
}