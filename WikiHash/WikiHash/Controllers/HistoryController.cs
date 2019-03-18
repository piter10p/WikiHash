using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WikiHash.Models.Modifications;

namespace WikiHash.Controllers
{
    public class HistoryController : Controller
    {
        // GET: History
        public ActionResult Index(string link)
        {
            try
            {
                if (!Models.Permissions.PermissionChecker.CheckPermission(User.Identity.Name, Models.Permissions.PermissionTarget.ReadingArticles))
                    return RedirectToAction("Login", "Account", new { returnUrl = Url.Action("Index") });

                var model = ModificationViewList.Create(link);
                return View(model);
            }
            catch(KeyNotFoundException)
            {
                return View("Error", null, "No article with specified title found.");
            }
            catch
            {
                return View("Error");
            }
        }
    }
}