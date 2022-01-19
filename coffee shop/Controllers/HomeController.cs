using coffee_shop.Dal;
using coffee_shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace coffee_shop.Controllers
{
    public class HomeController : Controller
    {

        private UserDal db = new UserDal();
        public ActionResult Index()
        {
            return View();
        }

    

        public ActionResult Register()
        {

            return View("Register");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "Id,FirstName,LastName,username,email,PhoneNumber,Password,isAdmin,isBarista")] UserModel userModel)
        {

            try
            {
                if (ModelState.IsValid)
                {

                    UserModel testforexisting = db.Users.Where(x => x.username == userModel.username).FirstOrDefault();
                    if (testforexisting != null)
                    {
                        ViewData["JavaScriptFunction"] = "alreadyexist();";
                        return View("Register");
                    }
                    db.Users.Add(userModel);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

            }
            catch (Exception ex) { 
            }
            return View("Register");
        }


        public ActionResult Login()
        {

            return View("Login");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login( UserModel input)
        {
            UserModel user = db.Users.Where(x => x.username == input.username && x.Password == input.Password).FirstOrDefault();
            try
            {
                if (ModelState.IsValid)
                {
                    if (user == null)
                    {
                        ViewBag.massage = "Password or username is not correct.";
                        return View("Login");
                    }
                    else if (user.isAdmin)
                    {
                        Session["name"] = user.username;
                        return RedirectToAction("Admin", "UserModels");
                    }
                    else if (user.isBarista)
                    {
                        Session["name"] = user.username;
                        return RedirectToAction("Barista", "UserModels");
                    }
                    else {
                        Session["name"] = user.username;
                        return RedirectToAction("Index", "UserModels");
                    }

                  
                }

            }
            catch (Exception ex)
            {
            }
            return  View("Login");
        }

    }
}
