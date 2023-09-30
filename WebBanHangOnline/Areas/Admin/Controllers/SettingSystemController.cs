using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHangOnline.Common;
using WebBanHangOnline.Models;
using WebBanHangOnline.Models.EF;

namespace WebBanHangOnline.Areas.Admin.Controllers
{
    public class SettingSystemController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin/SettingSystem
        public ActionResult Index()
        {
            
            return View();
        }

        public ActionResult Partial_Setting()
        {
            //ViewBag.settingTitle = SettingHelper.GetValue("SettingTitle");
            var itemTitle = db.SystemSettings.SingleOrDefault(x => x.SettingKey == "SettingTitle");
            ViewBag.settingTitle = itemTitle.SettingValue;

            var itemLogo = db.SystemSettings.SingleOrDefault(x => x.SettingKey == "SettingLogo");
            ViewBag.settingLogo = itemLogo.SettingValue;

            var itemHotline = db.SystemSettings.SingleOrDefault(x => x.SettingKey == "SettingHotline");
            ViewBag.settingHotline = itemHotline.SettingValue;

            var itemEmail = db.SystemSettings.SingleOrDefault(x => x.SettingKey == "SettingEmail");
            ViewBag.settingEmail = itemEmail.SettingValue;

            var itemTitleSeo = db.SystemSettings.SingleOrDefault(x => x.SettingKey == "SettingTitleSeo");
            ViewBag.settingTitleSeo = itemTitleSeo.SettingValue;

            var itemDesSeo = db.SystemSettings.SingleOrDefault(x => x.SettingKey == "SettingDesSeo");
            ViewBag.settingDesSeo = itemDesSeo.SettingValue;

            var itemKeySeo = db.SystemSettings.SingleOrDefault(x => x.SettingKey == "SettingKeySeo");
            ViewBag.settingKeySeo = itemKeySeo.SettingValue;

            return PartialView();
        }

        [HttpPost]
        public ActionResult AddSetting(SettingSystemViewModel req)
        {
            SystemSetting set = null;
            var checkTitle = db.SystemSettings.FirstOrDefault(x => x.SettingKey.Contains("SettingTitle"));
            if (checkTitle == null)
            {
                set = new SystemSetting();
                set.SettingKey = "SettingTitle";
                set.SettingValue = req.SettingTitle;
                db.SystemSettings.Add(set);
               
            }
            else
            {
                checkTitle.SettingValue = req.SettingTitle;
                db.Entry(checkTitle).State = System.Data.Entity.EntityState.Modified;
            }

            //logo

            var checkLogo = db.SystemSettings.FirstOrDefault(x => x.SettingKey.Contains("SettingLogo"));
            if (checkLogo == null)
            {
                set = new SystemSetting();
                set.SettingKey = "SettingLogo";
                set.SettingValue = req.SettingLogo;
                db.SystemSettings.Add(set);
            }
            else
            {
                checkLogo.SettingValue = req.SettingLogo;
                db.Entry(checkLogo).State = System.Data.Entity.EntityState.Modified;
            }

            //email

            var checkEmail = db.SystemSettings.FirstOrDefault(x => x.SettingKey.Contains("SettingEmail"));
            if (checkEmail == null)
            {
                set = new SystemSetting();
                set.SettingKey = "SettingEmail";
                set.SettingValue = req.SettingEmail;
                db.SystemSettings.Add(set);
            }
            else
            {
                checkEmail.SettingValue = req.SettingEmail;
                db.Entry(checkEmail).State = System.Data.Entity.EntityState.Modified;
            }

            //hotline

            var checkHotline = db.SystemSettings.FirstOrDefault(x => x.SettingKey.Contains("SettingHotline"));
            if (checkHotline == null)
            {
                set = new SystemSetting();
                set.SettingKey = "SettingHotline";
                set.SettingValue = req.SettingHotline;
                db.SystemSettings.Add(set);
            }
            else
            {
                checkHotline.SettingValue = req.SettingHotline;
                db.Entry(checkHotline).State = System.Data.Entity.EntityState.Modified;
            }

            //TitleSEO

            var checkTitleSeo = db.SystemSettings.FirstOrDefault(x => x.SettingKey.Contains("SettingTitleSeo"));
            if (checkTitleSeo == null)
            {
                set = new SystemSetting();
                set.SettingKey = "SettingTitleSeo";
                set.SettingValue = req.SettingTitleSeo;
                db.SystemSettings.Add(set);
            }
            else
            {
                checkTitleSeo.SettingValue = req.SettingTitleSeo;
                db.Entry(checkTitleSeo).State = System.Data.Entity.EntityState.Modified;
            }


            //Desception SEO

            var checkDesSeo = db.SystemSettings.FirstOrDefault(x => x.SettingKey.Contains("SettingDesSeo"));
            if (checkDesSeo == null)
            {
                set = new SystemSetting();
                set.SettingKey = "SettingDesSeo";
                set.SettingValue = req.SettingDesSeo;
                db.SystemSettings.Add(set);
            }
            else
            {
                checkDesSeo.SettingValue = req.SettingDesSeo;
                db.Entry(checkDesSeo).State = System.Data.Entity.EntityState.Modified;
            }


            //keyword SEO

            var checkKeySeo = db.SystemSettings.FirstOrDefault(x => x.SettingKey.Contains("SettingKeySeo"));
            if (checkKeySeo == null)
            {
                set = new SystemSetting();
                set.SettingKey = "SettingKeySeo";
                set.SettingValue = req.SettingKeySeo;
                db.SystemSettings.Add(set);
            }
            else
            {
                checkKeySeo.SettingValue = req.SettingKeySeo;
                db.Entry(checkKeySeo).State = System.Data.Entity.EntityState.Modified;
            }

            db.SaveChanges();

            
            return RedirectToAction("index");
        }



    }
}