using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using ProjectMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectMvc.Controllers
{
    public class RoleController : Controller
    {  // GET: Role
        [HttpGet]
        public ActionResult AddRole()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddRole(string RoleName)
        {
            if (RoleName != null)
            {
                ApplicationDbContext context = new ApplicationDbContext();
                RoleStore<IdentityRole> store = new RoleStore<IdentityRole>(context);
                RoleManager<IdentityRole> roleManager = new RoleManager<IdentityRole>(store);
                IdentityRole Role = new IdentityRole();
                Role.Name = RoleName;
                IdentityResult result = roleManager.Create(Role);

            }
            return View();
        }
    }
}