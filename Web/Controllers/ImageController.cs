using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class ImageController : Controller
    {
        // GET: Image
        public ActionResult Index(string imageName)
        {
            // TODO : FIX SECURITY FLAW HERE!!
            var dir = Server.MapPath("~/Public/Images");
            var path = Path.Combine(dir, imageName);
            return File(path, "image/jpeg");
        }
    }
}