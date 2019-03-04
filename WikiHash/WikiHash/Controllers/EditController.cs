﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WikiHash.Models.Articles;

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
    }
}