using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Model.Entity;
using Model.Service;
using Model.Dto;
using Model.Repository;
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

        [Route("Order/UploadPaymentSlip/{id}")]
        [HttpGet]
        public ActionResult UplodPaymentSlip(int id)
        {
            return View(_orderService.GetByPaymentId(id).Payment);
        }

        [Route("Order/UploadPaymentSlip")]
        [HttpPost]
        public ActionResult UplodPaymentSlip(HttpPostedFileBase upload, int id)
        {
            const string imagePath = "~/Public/Images";
            string fileName = null;

            try
            {
                var order = _orderService.GetByPaymentId(id);
                if (!order.Username.Equals(User.Identity.GetUserName()))
                {
                    TempData["ErrorMessage"] = "You attempt to change someone else's payment slip";
                    return RedirectToAction("Index");
                }

                if (upload != null && upload.ContentLength > 0)
                {

                    using (var reader = new System.IO.BinaryReader(upload.InputStream))
                    {
                        byte[] content = reader.ReadBytes(upload.ContentLength);
                        string timestamp = DateTime.Now.ToString("HH:mm:ss.fff",
                            System.Globalization.DateTimeFormatInfo.InvariantInfo);
                        string name = upload.FileName + timestamp;
                        string ext = Path.GetExtension(upload.FileName);
                        fileName = name.GetHashCode() + ext;
                        string path = Path.Combine(Server.MapPath(imagePath), fileName);
                        upload.SaveAs(path);
                    }
                }
                else
                {
                    TempData["ErrorMessage"] = "Upload slip fail, no file specified";
                    return RedirectToAction("UplodPaymentSlip", new {id = id});
                }

                order.Payment.Attachment = fileName;
                _orderService.UpdatePaymentAttachment(order.Id, fileName);
                TempData["SuccessMessage"] = "Upload slip success";
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                TempData["ErrorMessage"] = "Upload slip fail";
                return RedirectToAction("Index");
            }
            
           
        }
    }
}
