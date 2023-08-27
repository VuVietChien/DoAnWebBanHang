using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHangOnline.Models;

namespace WebBanHangOnline.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index(int? id)
        {
            var items = db.Products.ToList();
            if (id != null)
            {
                items = db.Products.Where(x => x.ProductCategoryID == id).ToList();
            }
            return View(items);
        }

        public ActionResult ProductCategory(String alias , int? id)
        {
            var items = db.Products.ToList();
            if (id > 0)
            {
                items = db.Products.Where(x => x.ProductCategoryID == id).ToList();
            }
            var cate = db.ProductCategories.Find(id);
            if(cate!=null)
            {
                ViewBag.CateName = cate.Title;
            }
            ViewBag.CateId = id;
            return View(items);
        }

        public ActionResult Partial_ItemsByCateId()
        {
            var items = db.Products.Where(x => x.IsHome && x.IsActive == true).Take(12).ToList();
            return PartialView(items);
        }

        public ActionResult Partial_ProductSale()
        {
            var items = db.Products.Where(x => x.IsSale && x.IsActive == true).Take(12).ToList();
            return PartialView(items);
        }
    }
}