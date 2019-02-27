using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WikiHash.Models.Medias;

namespace WikiHash.Controllers
{
    public class MediasController : Controller
    {
        public ActionResult Show(string link)
        {
            var media = MediasManager.GetMedia(link);
            return View(media);
        }
    }
}