using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHangOnline.Models;
using WebBanHangOnline.Models.EF;

namespace WebBanHangOnline.Areas.Admin.Controllers
{
    public class ProductCategoryController : Controller
    {
        // GET: Admin/ProductCategory
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            var item = db.ProductCategories;
            return View(item);
        }

        public ActionResult Add()
        {
            return View();
        }


        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Add(ProductCategory model)
        {
            if (ModelState.IsValid)
            {
               
                model.CreatedDate = DateTime.Now;
                model.ModifiedDate = DateTime.Now;
                model.Alias = WebBanHangOnline.Models.Common.Filter.RemoveSign4VietnameseString(model.Title);
                db.ProductCategories.Add(model);
                db.SaveChanges();
                return RedirectToAction("index");
            }
            return View();
        }


        public ActionResult Edit(int Id)
        {
            var model = db.ProductCategories.Find(Id);
            return View(model);
        }



        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Edit(ProductCategory model)
        {
            if(ModelState.IsValid)
            {
                db.ProductCategories.Attach(model);
                model.CreatedDate = DateTime.Now;
                model.ModifiedDate = DateTime.Now;
                model.Alias = WebBanHangOnline.Models.Common.Filter.RemoveSign4VietnameseString(model.Title);

                db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("index");
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(int Id)
        {
            var item = db.ProductCategories.Find(Id);
            if (item != null)
            {
                db.ProductCategories.Remove(item);
                db.SaveChanges();
                return Json(new { success = true });
            }
            else
                return Json(new { success = false });
        }



    }
}