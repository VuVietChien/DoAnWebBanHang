using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHangOnline.Models;

namespace WebBanHangOnline.Areas.Admin.Controllers
{
    public class RoleController : Controller
    {
        // GET: Admin/Role

        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            var items = db.Roles.ToList();
            return View();
        }


        public ActionResult Create()
        {
            return View();
        }
    }
}