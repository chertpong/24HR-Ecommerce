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
        [Route("/Image/{id}")]
        public ActionResult Index(int id)
        {
            // TODO : FIX SECURITY FLAW HERE!!
            var dir = Server.MapPath("~/Public/Images");
            var path = Path.Combine(dir, Convert.ToString(id));
            return File(path, "image/jpeg");
        }
    }
}