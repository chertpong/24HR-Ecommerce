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
                _shoppingCartService.AddToShoppingCart(productId, amount);
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
                _shoppingCartService.UpdateShoppingCart(productId, amount);
                return RedirectToAction("Index");
            }
            catch(Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                TempData["ErrorMessage"] = "Update shopping cart fail: "+e.Message;
                return RedirectToAction("Index");
            }
        }

        // GET: Shopping/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Shopping/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Shopping/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Shopping/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
