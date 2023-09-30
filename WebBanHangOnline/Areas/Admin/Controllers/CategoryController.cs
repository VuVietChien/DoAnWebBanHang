using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHangOnline.Models;
using WebBanHangOnline.Models.EF;

namespace WebBanHangOnline.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        // GET: Admin/Category
        ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            var items = db.Categories;
            return View(items);
        }

        public ActionResult Add()   
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Category model)
        {
            if(ModelState.IsValid)            
            {
                model.CreatedDate = DateTime.Now;
                model.ModifiedDate = DateTime.Now;
                model.Alias = WebBanHangOnline.Models.Common.Filter.RemoveSign4VietnameseString(model.Title);
                db.Categories.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var item = db.Categories.Find(id);
            return View(item);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category model)
        {
            if (ModelState.IsValid)
            {
                db.Categories.Attach(model);
                model.ModifiedDate = DateTime.Now;
                model.Alias = WebBanHangOnline.Models.Common.Filter.RemoveSign4VietnameseString(model.Title);

                db.Entry(model).Property(m => m.Title).IsModified = true;
                db.Entry(model).Property(m => m.Description).IsModified = true;
                db.Entry(model).Property(m => m.Alias).IsModified = true;
                db.Entry(model).Property(m => m.SeoDescription).IsModified = true;
                db.Entry(model).Property(m => m.SeoKeywords).IsModified = true;
                db.Entry(model).Property(m => m.SeoTitle).IsModified = true;
                db.Entry(model).Property(m => m.Position).IsModified = true;
                db.Entry(model).Property(m => m.ModifiedBy).IsModified = true;
                db.Entry(model).Property(m => m.ModifiedDate).IsModified = true;
                db.Entry(model).Property(m => m.IsActive).IsModified = true;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var item = db.Categories.Find(id);
            if(item!=null)
            {
                db.Categories.Remove(item);
                db.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });

        }


        [HttpPost]
        public ActionResult IsActive(int Id)
        {
            var item = db.Categories.Find(Id);
            if (item != null)
            {
                item.IsActive = !item.IsActive;
                db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                return Json(new { success = true, isActive = item.IsActive });

            }
            return Json(new { success = false });

        }
    }
}