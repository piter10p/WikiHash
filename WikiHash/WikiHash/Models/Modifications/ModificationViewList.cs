using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WikiHash.Models.Modifications
{
    public class ModificationViewList
    {
        public string ArticleTitle { get; private set; }
        public ModificationViewModel[] Modifications { get; private set; }

        private ModificationViewList() { }

        public static ModificationViewList Create(string link)
        {
            try
            {
                var article = Articles.ArticlesManager.GetArticle(link);
                var modifications = ModificationsManager.GetArticleModificationHistory(link);

                var result = new ModificationViewList();
                result.ArticleTitle = article.Title;
                result.Modifications = modifications;
                return result;
            }
            catch
            {
                throw;
            }
        }
    }
}