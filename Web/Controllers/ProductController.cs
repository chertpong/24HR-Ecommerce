using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.Dto;
using Model.Entity;
using Model.Service;

namespace Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductService _productService;

        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

        // GET: Product
        public ActionResult Index()
        {
            ViewBag.ErrorMessage = TempData["ErrorMessage"] ?? "";
            ViewBag.SuccessMessage = TempData["SuccessMessage"] ?? "";
            return View(_productService.GetAll());
        }

        // GET: Product/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                return View(_productService.GetById(id));
            }
            catch (Exception)
            {
                
                throw;
                return RedirectToAction("Index");
            }
        }

        // GET: Default/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        public ActionResult Create(ProductViewModels.ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                Product p = new Product
                {
                    Name = model.Name,
                    Price = model.Price,
                    Amount = model.Amount,
                    Description = model.Description,
                    Thumbnail = model.Thumbnail
                };
                _productService.Create(p);
                return RedirectToAction("Index", "Home");
            }

            return View();
        }


        // GET: Default/Edit/5
        public ActionResult Edit(int id)
        {
            return View(_productService.GetById(id));
        }

        // POST: Product/Edit/5
        [HttpPost]
        public ActionResult Edit(ProductViewModels.ProductViewModel model)
        {
            var p = new Product
            {
                Id = model.Id,
                Name = model.Name,
                Price = model.Price,
                Amount = model.Amount,
                Description = model.Description,
                Thumbnail = model.Thumbnail
            };
            if (ModelState.IsValid)
            {
                _productService.Update(p);
                TempData["SuccessMessage"] = "Add product success";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["ErrorMessage"] = "Add product error";
                return RedirectToAction("Index");
            }

        }



        // GET: Default/Delete/5
        public ActionResult Delete(int id)
        {
            _productService.Delete(id);

            return RedirectToAction("Index");
        }
        
        // POST: Default/Delete/5
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
