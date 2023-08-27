using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHangOnline.Models;
using WebBanHangOnline.Models.EF;

namespace WebBanHangOnline.Areas.Admin.Controllers
{
    public class ProductImageController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin/ProductImages
        public ActionResult Index(int Id)
        {
            ViewBag.productId = Id;
            var item = db.ProductImages.Where(x => x.ProductId == Id).ToList();
            return View(item);
        }

        [HttpPost]
        public ActionResult AddImage (int productId, string url)
        {
            var items = db.ProductImages.Where(x=>x.ProductId == productId).ToList();
            if(items.Count == 0)
            {
                db.ProductImages.Add(new ProductImage
                {
                    ProductId = productId,
                    Image = url,
                    IsDefault = true,

                });
            }
            else
            {
                db.ProductImages.Add(new ProductImage
                {
                    ProductId = productId,
                    Image = url,
                    IsDefault = false,

                });
            }
            db.SaveChanges();
            return Json(new { Success = true });
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var item = db.ProductImages.Find(id);
            db.ProductImages.Remove(item);
            db.SaveChanges();
            return Json(new { success = true });
        }

        [HttpPost]
        public ActionResult IsDefault(int ProductId, int id)
        {
            var item1 = db.ProductImages.Where(x => x.ProductId == ProductId).ToList();
            foreach(var i in item1)
            {
                i.IsDefault = false;
            }
            
            var item = db.ProductImages.Find(id);
            item.IsDefault = true;
            db.SaveChanges();
            return Json(new { success = true });
        }
    }
}