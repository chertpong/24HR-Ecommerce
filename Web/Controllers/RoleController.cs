using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Web.Infrastructure;
using Web.Models;

namespace Web.Controllers
{
    public class RoleController : Controller
    {
        private readonly ApplicationDbContext _context;
        private ApplicationUserManager _userManager;
        private ApplicationRoleManager _roleManager;

        public ApplicationUserManager UserManager
        {
            get { return _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>(); }
            private set { _userManager = value; }
        }

        public ApplicationRoleManager RoleManager
        {
            get { return _roleManager ?? Request.GetOwinContext().GetUserManager<ApplicationRoleManager>(); }
            private set { _roleManager = value; }
        }

        public RoleController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: Role
        public ActionResult Index()
        {
            ViewBag.ErrorMessage = TempData["ErrorMessage"] ?? "";
            ViewBag.SuccessMessage = TempData["SuccessMessage"] ?? "";
            return View(_context.Roles.ToList());
        }

        // GET: Role/Details/5
        public ActionResult Details(string id)
        {
            return View(_context.Roles.Find(id));
        }

        // GET: Role/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Role/Create
        [HttpPost]
        public ActionResult Create(RoleViewModels.CreateRoleViewModel role)
        {
            try
            {
                _context.Roles.Add(new IdentityRole() { Name = role.Name });
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // POST: Role/Delete/5
        [HttpPost]
        public ActionResult Delete(string id)
        {
            try
            {
                var role = _context.Roles.First(r => r.Id.Equals(id));
                _context.Roles.Remove(role);
                return RedirectToAction("Index");
            }
            catch (Exception exception)
            {
                TempData["ErrorMessage"] = "Delete error";
                System.Diagnostics.Debug.WriteLine("[!] Error: "+exception.Message);
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public ActionResult AddRoleToUser()
        {
            ViewData["users"] = _context.Users.ToList();
            ViewData["roles"] = _context.Roles.ToList();
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> AddRoleToUser(RoleViewModels.AddRoleToUserViewModel model)
        {
            var role = await this.RoleManager.FindByNameAsync(model.RoleName);
            var user = await this.UserManager.FindByEmailAsync(model.Username);
            System.Diagnostics.Debug.WriteLine(role.Name);
            var result = await this.UserManager.AddToRolesAsync(user.Id, new string[] { role.Name });
            if (result.Succeeded)
            {
                TempData["SuccessMessage"] = "Add user " + model.Username + " to role " + role.Name + "success";
                RedirectToAction("Index");
            }
            TempData["ErrorMessage"] = "Add user to role error";
            return RedirectToAction("Index");
        }
    }
}
