using coffee_shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace coffee_shop.Controllers
{
    public class RegisterController : Controller
    {
        private ApplicationDBContext _context = null;


        public RegisterController()
        {
            _context = new ApplicationDBContext();
        }
        // GET: Register
        public ActionResult Register()
        {
            
            return View();
        }
        [HttpPost]
        public ActionResult Create(UserModel model1)
        {
            try
            {
                if (_context.Users.Any(x => x.Username.Equals(model1.Username)))
                {
                    TempData["userMSG"] = "Choose another username";
                }
                if (_context.Users.Any(x => x.Email.Equals(model1.Email)))  
                {
                    TempData["emailMSG"] = "Email Aleady Exist";
                }

                if (_context.Users.Any(x => x.Id.Equals(model1.Id)))
                {
                    TempData["idMSG"] = "ID Aleady Exist";
                }
                if (TempData["userMSG"] != null || TempData["emailMSG"] != null || TempData["idMSG"] != null)
                {
                    return View(model1);
                }
                else
                {
                    _context.Users.Add(model1);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["exp"] = ex.ToString();
            }

            return View(model1);
        }
        public ActionResult RegisterCheck(UserModel user)
        {
            return View() ;
        }
    }
}