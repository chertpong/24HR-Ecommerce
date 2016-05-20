using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Service;

namespace Web.Controllers
{
    public class ShoppingController : Controller
    {
        private readonly ShoppingCartService _shoppingCartService;

        public ShoppingController(ShoppingCartService shoppingCartService)
        {
            this._shoppingCartService = shoppingCartService;
        }

        // GET: Shopping
        public ActionResult Index()
        {
            ViewBag.ErrorMessage = TempData["ErrorMessage"] ?? "";
            ViewBag.SuccessMessage = TempData["SuccessMessage"] ?? "";
            return View(_shoppingCartService.GetShoppingCart());
        }

        // GET: Shopping/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        [HttpPost]
        [Route("Shopping/AddToShoppingCart")]
        public ActionResult AddToShoppingCart(int productId, int amount)
        {
            try
            {
                var success = _shoppingCartService.AddToShoppingCart(productId, amount);
                if (success)
                {
                    TempData["SuccessMessage"] = "Add item to shoppingcart successfully";
                }
                else
                {
                    TempData["ErrorMessage"] = "Add item to fail";
                }
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                TempData["ErrorMessage"] = "Add product to shopping cart fail: "+e.Message;
                return RedirectToAction("Index");
            }
        }

        // POST: Shopping/Create
        [HttpPost]
        [Route("Shopping/UpdateShoppingCart")]
        public ActionResult UpdateShoppingCart(int productId, int amount)
        {
            try
            {
                var success = _shoppingCartService.UpdateShoppingCart(productId, amount);
                if (success)
                {
                    TempData["SuccessMessage"] = "Update shoppingcart successfully";
                }
                else
                {
                    TempData["ErrorMessage"] = "Update shopping cart fail";
                }
                return RedirectToAction("Index");
            }
            catch(Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                TempData["ErrorMessage"] = "Update shopping cart fail: "+e.Message;
                return RedirectToAction("Index");
            }
        }


        // POST: Shopping/Delete/5
        [HttpPost]
        public ActionResult Delete(int productId)
        {
            try
            {
                var success =_shoppingCartService.RemoveProductFromShoppingCart(productId);
                if (success)
                {
                    TempData["SuccessMessage"] = "Remove product id:" + productId + " successfully";
                }
                else
                {
                    TempData["ErrorMessage"] = "Remove product id:" + productId + " fail";
                }
                return RedirectToAction("Index");
            }
            catch
            {
                TempData["ErrorMessage"] = "Remove product id:" + productId + " fail";
                return RedirectToAction("Index");
            }
        }
    }
}
