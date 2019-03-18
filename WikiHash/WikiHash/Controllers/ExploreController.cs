using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WikiHash.Models.Articles;

namespace WikiHash.Controllers
{
    public class ExploreController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public string GetArticlesAJAX(string filter, string category)
        {
            var articles = ArticlesManager.GetFilteredArticles(filter, category);
            return JsonConvert.SerializeObject(articles);
        }

        [HttpGet]
        public ActionResult New()
        {
            if (!Models.Permissions.PermissionChecker.CheckPermission(User.Identity.Name, Models.Permissions.PermissionTarget.CreatingNewArticles))
                return RedirectToAction("Login", "Account", new { returnUrl = Url.Action("New") });

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult New(ArticleCreationModel model)
        {
            try
            {
                if (!Models.Permissions.PermissionChecker.CheckPermission(User.Identity.Name, Models.Permissions.PermissionTarget.CreatingNewArticles))
                    return RedirectToAction("Login", "Account", new { returnUrl = Url.Action("New") });

                if (!ModelState.IsValid)
                    return View(model);

                var article = ArticlesManager.AddArticle(model);
                NewArticleGenerator.Create(article);
                return RedirectToAction("ArticleCreated");
            }
            catch(Models.EntryExistsException e)
            {
                return View("Error", null, "Article with this title already exists.");
            }
            catch
            {
                return View("Error");
            }
        }

        public ActionResult ArticleCreated()
        {
            return View("MessagePage", new Models.MessageViewModel { Title = "Article created", Message = "Article was created successfully." });
        }
    }
}