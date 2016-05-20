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
    [Authorize]
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
            ViewBag.ErrorMessage = TempData["ErrorMessage"] ?? "";
            ViewBag.SuccessMessage = TempData["SuccessMessage"] ?? "";

            var orders = _orderService
                                    .GetAll()
                                    .Where(o => o.Username.Equals(User.Identity.GetUserName()))
                                    .ToList();
            return View(orders == null ? new List<Order>() : orders );
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult AdminIndex()
        {
            ViewBag.ErrorMessage = TempData["ErrorMessage"] ?? "";
            ViewBag.SuccessMessage = TempData["SuccessMessage"] ?? "";
            return View(_orderService.GetAll());
        }

        // GET: Order/Details/5
        public ActionResult Details(int id)
        {
            var order = _orderService.GetById(id);
            if (!order.Username.Equals(User.Identity.GetUserName()) || User.IsInRole("Admin"))
            {
                TempData["ErrorMessage"] = "Sorry, it's someone else people order";
                return RedirectToAction("Index");
            }
            return View(order);
        }

        // GET: Order/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            var order = _orderService.GetById(id);
            return View(order);
        }

        // POST: Order/Edit/5
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(Order order)
        {
            try
            {
                order.Updated = DateTime.Now;
                _orderService.Update(order);
                return RedirectToAction("Index");
            }
            catch
            {
                TempData["ErrorMessage"] = "Update fail";
                return View(order);
            }
        }

        // POST: Order/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Delete(int orderId)
        {
            try
            {
                _orderService.Delete(orderId);
                TempData["SuccessMessage"] = "Delete order id: "+orderId+" success";
                return RedirectToAction("Index");
            }
            catch
            {
                TempData["ErrorMessage"] = "Delete order id: " + orderId + " fail";
                return RedirectToAction("Index");
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
