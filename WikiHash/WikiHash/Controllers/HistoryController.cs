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