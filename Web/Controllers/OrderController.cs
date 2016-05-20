using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Model.Entity;
using Model.Service;
using Model.Dto;
using Web.Service;

namespace Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly OrderService _orderService;
        private readonly ShoppingCartService _shoppingCartService;

        public OrderController(OrderService orderService, ShoppingCartService shoppingCartService)
        {
            _orderService = orderService;
            _shoppingCartService = shoppingCartService;
        }

        // GET: Order
        public ActionResult Index()
        {
            return View(_orderService.GetAll());
        }

        // GET: Order/Details/5
        public ActionResult Details(int id)
        {
            return View(_orderService.GetById(id));
        }

        // GET: Order/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Order/Create
        [HttpPost]
        public ActionResult Create(Order order)
        {
            try
            {
               _orderService.Create(order);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Order/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Order/Edit/5
        [HttpPost]
        public ActionResult Edit(Order order)
        {
            try
            {
                _orderService.Update(order);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Order/Delete/5
        public ActionResult Delete(int id)
        {
            _orderService.Delete(id);
            return RedirectToAction("Index");
        }

        // POST: Order/Delete/5
        [HttpPost]
        public ActionResult Delete(FormCollection collection)
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

        [Route("Order/Checkout")]
        [HttpGet]
        public ActionResult CheckOut()
        {
            var checkoutDetail = Session["CheckoutDetail"] ?? new CheckOutViewModel();
            ViewBag.CheckoutDetail = checkoutDetail;
            return View(checkoutDetail);
        }

        [Route("Order/Checkout")]
        [HttpPost]
        public ActionResult CheckOut(CheckOutViewModel checkoutDetail)
        {
            Session["CheckoutDetail"] = checkoutDetail;

            if (!ModelState.IsValid) return View(checkoutDetail);

            if (checkoutDetail == null) return View("CheckOut");

            var cart = _shoppingCartService.GetShoppingCart();
            var payment = new Payment
            {
                Attachment = checkoutDetail.Attachment,
                Paid = false,
                PaymentMethod = checkoutDetail.PaymentMethod
            };
            _orderService.MakeOrder(
                cart.SelectedProducts,
                User.Identity.GetUserName(),
                checkoutDetail.TransportationType,
                payment,
                checkoutDetail.Note
            );
            return RedirectToAction("Index");
        }
    }
}
