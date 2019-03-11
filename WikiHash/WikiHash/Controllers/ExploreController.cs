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
    }
}