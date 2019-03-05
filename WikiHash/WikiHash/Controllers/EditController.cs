using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WikiHash.Models.Articles;
using WikiHash.Models.Articles.BodiesWriter;

namespace WikiHash.Controllers
{
    public class EditController : Controller
    {
        public ActionResult Edit(string link)
        {
            var article = ArticlesManager.GetArticle(link);

            var viewModel = ArticleViewModel.FromArticle(article);
            return View(viewModel);
        }

        [ValidateInput(false)]
        [HttpPost]
        public string Save(string articleJson)
        {
            try
            {
                var articleViewModel = ArticleJsonParser.Parse(articleJson);
                var article = Article.FromViewModel(articleViewModel);
                ArticlesManager.UpdateArticle(article);
                var writer = new BodyWriter();
                writer.Write(articleViewModel.Body, articleViewModel.Link);

                return "ok";
            }
            catch
            {
                return "failed";
            }
        }
    }
}