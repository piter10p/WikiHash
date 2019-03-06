using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WikiHash.Models.Medias;
using Newtonsoft.Json;

namespace WikiHash.Controllers
{
    public class MediasController : Controller
    {
        public ActionResult Show(string link)
        {
            var media = MediasManager.GetMedia(link);
            var viewMedia = MediaViewModel.FromMedia(media);
            return View(viewMedia);
        }

        [HttpGet]
        public ActionResult New()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult New(MediaCreationModel model)
        {
            if(!ModelState.IsValid)
                return View(model);

            //TODO: Implement saving

            throw new NotImplementedException();

            return View();
        }

        public string GetMediaUrl(string link)
        {
            var media = MediasManager.GetMedia(link);
            var viewMedia = MediaViewModel.FromMedia(media);
            var json = JsonConvert.SerializeObject(viewMedia);
            return json;
        }
    }
}