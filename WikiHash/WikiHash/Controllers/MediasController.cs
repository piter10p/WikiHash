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
            try
            {
                if (!Models.Permissions.PermissionChecker.CheckPermission(User.Identity.Name, Models.Permissions.PermissionTarget.ReadingArticles))
                    return RedirectToAction("Login", "Account", new { returnUrl = Url.Action("Show") });

                var media = MediasManager.GetMedia(link);
                var viewMedia = MediaViewModel.FromMedia(media);
                return View(viewMedia);
            }
            catch(KeyNotFoundException e)
            {
                return View("Error", null, "No matching media found.");
            }
            catch
            {
                return View("Error");
            }
        }

        [HttpGet]
        public ActionResult New()
        {
            if (!Models.Permissions.PermissionChecker.CheckPermission(User.Identity.Name, Models.Permissions.PermissionTarget.CreatingNewMedias))
                return RedirectToAction("Login", "Account", new { returnUrl = Url.Action("New") });

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult New(MediaCreationModel model)
        {
            try
            {
                if (!Models.Permissions.PermissionChecker.CheckPermission(User.Identity.Name, Models.Permissions.PermissionTarget.CreatingNewMedias))
                    return RedirectToAction("Login", "Account", new { returnUrl = Url.Action("New") });

                if (!ModelState.IsValid)
                    return View(model);

                MediasManager.AddMedia(model);
                var path = Models.PathsGenerator.Media(model.File.FileName);
                model.File.SaveAs(path);

                return RedirectToAction("MediasUploadingComplete");
            }
            catch(Models.EntryExistsException e)
            {
                return View("Error", null, "Media with the selected title already exists.");
            }
            catch
            {
                return View("Error");
            }
        }

        public ActionResult Explore()
        {
            return View();
        }

        public ActionResult MediasUploadingComplete()
        {
            return View("MessagePage", new WikiHash.Models.MessageViewModel { Title = "Media uploaded", Message = "Media has been uploaded successfully." });
        }

        public string GetMediaUrl(string link)
        {
            var media = MediasManager.GetMedia(link);
            var viewMedia = MediaViewModel.FromMedia(media);
            var json = JsonConvert.SerializeObject(viewMedia);
            return json;
        }

        public string GetMediasAJAX(string filter = "")
        {
            var list = MediasManager.GetFilteredMedias(filter);
            return JsonConvert.SerializeObject(list);
        }
    }
}