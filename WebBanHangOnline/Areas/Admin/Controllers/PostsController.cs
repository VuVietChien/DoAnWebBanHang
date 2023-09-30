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
    [Authorize(Roles = "Admin,Employee")]
    public class PostsController : Controller
    {
        
        // GET: Admin/Posts
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index(string Searchtext ,int? page)
        {
            var pageSize = 8;
            if(page == null)
            {
                page = 1;
            }
            IEnumerable<Post> items = db.Posts.OrderByDescending(m => m.Id);
            if (!string.IsNullOrEmpty(Searchtext))
            {
                items = items.Where(m => m.Alias.Contains(Searchtext) || m.Title.Contains(Searchtext));
            }
            var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;

            items = items.ToPagedList(pageIndex,pageSize);
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
        public ActionResult Add(Post model)
        {
            if (ModelState.IsValid)
            {
                model.CreatedDate = DateTime.Now;
                model.CategoryId = 1;
                model.ModifiedDate = DateTime.Now;
                model.Alias = WebBanHangOnline.Models.Common.Filter.RemoveSign4VietnameseString(model.Title);
                db.Posts.Add(model);
                db.SaveChanges();
                return RedirectToAction("index");
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(int Id)
        {
            var model = db.Posts.Find(Id);
            return View(model);
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Post model)
        {
            if (ModelState.IsValid)
            {
                db.Posts.Attach(model);
                model.CreatedDate = DateTime.Now;
                model.CategoryId = 1;
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
            var item = db.Posts.Find(Id);
            if (item != null)
            {
                db.Posts.Remove(item);
                db.SaveChanges();

                return Json(new { success = true });

            }
            return Json(new { success = false });

        }


        [HttpPost]
        public ActionResult IsActive(int Id)
        {
            var item = db.Posts.Find(Id);
            if (item != null)
            {
                item.IsActive = !item.IsActive;
                db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                return Json(new { success = true, isActive = item.IsActive });

            }
            return Json(new { success = false });

        }


        [HttpPost]
        public ActionResult DeleteAll(string ids)
        {

            if (!string.IsNullOrEmpty(ids))
            {
                var items = ids.Split(',');

                if (items != null && items.Any())
                {
                    foreach (var item in items)
                    {
                        var obj = db.Posts.Find(Convert.ToInt32(item));
                        db.Posts.Remove(obj);
                        db.SaveChanges();
                    }
                }
                return Json(new { success = true });

            }
            return Json(new { success = false });

        }

    }
}