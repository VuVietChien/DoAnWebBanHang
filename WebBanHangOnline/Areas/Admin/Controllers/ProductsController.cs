using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHangOnline.Models;
using WebBanHangOnline.Models.EF;

namespace WebBanHangOnline.Areas.Admin.Controllers
{


    public class ProductsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/Products
        public ActionResult Index(string Searchtext, int? page)
        {
            var pageSize = 8;
            if (page == null)
            {
                page = 1;
            }
            IEnumerable<Product> items = db.Products.OrderByDescending(m => m.Id);
            if (!string.IsNullOrEmpty(Searchtext))
            {
                items = items.Where(m => m.Alias.Contains(Searchtext) || m.Title.Contains(Searchtext));
            }
            var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;

            items = items.ToPagedList(pageIndex, pageSize);
            ViewBag.page = page;
            ViewBag.pageSize = pageSize;
            return View(items);
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Product model)
        {
            if (ModelState.IsValid)
            {
                model.CreatedDate = DateTime.Now;
                model.ModifiedDate = DateTime.Now;
                model.Alias = WebBanHangOnline.Models.Common.Filter.RemoveSign4VietnameseString(model.Title);
                db.Products.Add(model);
                db.SaveChanges();
                return RedirectToAction("index");
            }
            return View(model);
        }
    }
}