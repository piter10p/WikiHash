using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WikiHash.Models.Articles;
using WikiHash.Models.Modifications;

namespace WikiHash.Controllers
{
    public class EditController : Controller
    {
        public ActionResult Edit(string link)
        {
            try
            {
                var article = ArticlesManager.GetArticle(link);

                var viewModel = ArticleViewModel.FromArticle(article);
                return View(viewModel);
            }
            catch (KeyNotFoundException e)
            {
                return View("Error", null, "No matching article found");
            }
            catch
            {
                return View("Error");
            }
        }

        [ValidateInput(false)]
        [HttpPost]
        public string Save(string articleJson)
        {
            try
            {
                var articleViewModel = ArticleJsonParser.Parse(articleJson);
                var article = Article.FromViewModel(articleViewModel);
                article = ArticlesManager.UpdateArticle(article);

                var modification = new Modification() { Article = article, AuthorIp = Request.UserHostAddress };
                if (User.Identity.Name != "")
                    modification.UserEmail = User.Identity.Name;
                modification = ModificationsManager.AddModification(modification);

                ModificationWriter.Write(modification, articleViewModel);

                return "ok";
            }
            catch(Exception e)
            {
                return "failed";
            }
        }
    }
}