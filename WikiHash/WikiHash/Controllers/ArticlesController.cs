using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WikiHash.Models.Articles;

namespace WikiHash.Controllers
{
    public class ArticlesController : Controller
    {
        public ActionResult Read(string link)
        {
            try
            {
                if (!Models.Permissions.PermissionChecker.CheckPermission(User.Identity.Name, Models.Permissions.PermissionTarget.ReadingArticles))
                    return RedirectToAction("Login", "Account", new { returnUrl = Url.Action("Read") });

                var article = ArticlesManager.GetArticle(link);

                var viewModel = ArticleViewModel.FromArticle(article);
                return View(viewModel);
            }
            catch (KeyNotFoundException e)
            {
                return View("Error", null, "No matching article found.");
            }
            catch
            {
                return View("Error");
            }
        }
    }
}