using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
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
            try
            {
                var role = _context.Roles.Find(id);
                var userList = new List<ApplicationUser>();
                role.Users.ToList()
                    .Select(u => u.UserId).ToList()
                    .ForEach(userId =>
                        userList.Add(
                            _context.Users.First(
                                u => u.Id.Equals(userId)
                                )
                            )
                    );
                ViewBag.UserList = userList;
                return View(role);
            }
            catch (Exception e)
            {
                TempData["ErrorMessage"] = "Role not found";
                return RedirectToAction("Index");
            }
            
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
            var users = _context.Users.ToList();
            var roles = _context.Roles.ToList();
            return View(new RoleViewModels.ShowRolesAndUsersViewModels {Users = users, Roles = roles });
        }

        [HttpPost]
        public ActionResult AddRoleToUser(RoleViewModels.AddRoleToUserViewModel model)
        {
            var role = this.RoleManager.FindByName(model.RoleName);
            var user = this.UserManager.FindByEmail(model.Username);
   
            var result = this.UserManager.AddToRoles(user.Id, new string[] { role.Name });
            if (result.Succeeded)
            {
                TempData["SuccessMessage"] = "Add user " + model.Username + " to role " + role.Name + "success";
                return RedirectToAction("Index");
            }
            else {
                TempData["ErrorMessage"] = "Add user to role error";
                return RedirectToAction("Index");
            }

        }

        [HttpPost]
        public ActionResult RemoveRoleFromUser(RoleViewModels.RemoveRoleFromUserViewModel model)
        {
            var role = this.RoleManager.FindByName(model.RoleName);
            var user = this.UserManager.FindByEmail(model.Username);

            try
            {
                var result = this.UserManager.RemoveFromRoles(user.Id, new string[] {role.Name});
                if (result.Succeeded)
                {
                    TempData["SuccessMessage"] = "Remove role " + role.Name + " from user " + model.Username + "success";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["ErrorMessage"] = "Remove role fail";
                    return RedirectToAction("Index");
                }
            }
            catch(Exception e)
            {
                TempData["ErrorMessage"] = "Remove role fail";
                return RedirectToAction("Index");
            }

        }
    }
}
