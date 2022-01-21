using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using coffee_shop.Dal;
using coffee_shop.Models;
using coffee_shop.viewmodels;
using System.IO;
namespace coffee_shop.Controllers
 
{
    public class UserModelsController : Controller
    {
        private UserDal db = new UserDal();

        // GET: UserModels
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        // GET: UserModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserModel userModel = db.Users.Find(id);
            if (userModel == null)
            {
                return HttpNotFound();
            }
            return View(userModel);
        }

        // GET: UserModels/Create
        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Admin()
        {
            CoffeeShopEntities enit = new CoffeeShopEntities();
            viewProductModel pvm = new viewProductModel();
            List<product> products = enit.products.ToList<product>();
            pvm.myprod = new product();
            pvm.products = products;
            return View(pvm);
        }
        [HttpPost]
        public ActionResult Submit()
        {
            viewProductModel pvm = new viewProductModel();
            product myprod = new product()
            {
                productId = Convert.ToInt32(Request.Form["myprod.ProductID"]),
                productName = Request.Form["myprod.ProductName"].ToString(),
                productDesc = Request.Form["myprod.productDesc"].ToString(),
                productPrice = Convert.ToDecimal(Request.Form["myprod.productPrice"]),
                productOldP = Convert.ToDecimal(Request.Form["myprod.productPrice"]),
                imagePath = Request.Files["myprod.imagepath"].FileName,
                imgfile = Request.Files["myprod.imagepath"],


            };
            string fileName = Path.GetFileNameWithoutExtension(myprod.imgfile.FileName);
            string extension = Path.GetExtension(myprod.imgfile.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            myprod.imagePath = "~/assets/Images/" + fileName;
            fileName = Path.Combine(Server.MapPath("~/assets/Images/"), fileName);

            myprod.imgfile.SaveAs(fileName);

            CoffeeShopEntities enit = new CoffeeShopEntities();
            if (ModelState.IsValid)
            {
                enit.products.Add(myprod);
                enit.SaveChanges();
                pvm.myprod = new product();
            }
            else
            {
                pvm.myprod = myprod;
            }
            pvm.products = enit.products.ToList<product>();
            return View("Admin", pvm);

        }
        public ActionResult Barista()
        {
            return View();
        }

        // POST: UserModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,username,email,PhoneNumber,Password,isAdmin,isBarista")] UserModel userModel)
        {
            userModel.isVip = false;
            userModel.cupcounter = 0;
            if (ModelState.IsValid)
            {
                db.Users.Add(userModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(userModel);
        }

        // GET: UserModels/Edit/5
        

        // POST: UserModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,username,email,PhoneNumber,Password,isAdmin,isBarista")] UserModel userModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(userModel);
        }

        // GET: UserModels/Delete/5


        
        public ActionResult Edit(int? id )
        {
            viewProductModel pvm = new viewProductModel();
            product prod = new product();
            CoffeeShopEntities enit = new CoffeeShopEntities();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            prod = enit.products.Find(id);
            if (prod == null)
            {
                return HttpNotFound();
            }
            decimal temp = prod.productPrice;
            prod.productPrice = prod.productOldP;
            prod.productOldP = temp;

            enit.Entry(prod).State = EntityState.Modified;
            enit.SaveChanges();


            pvm.products = enit.products.ToList<product>();
            return View("Admin", pvm);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditConfirmed(int id )
        {
            viewProductModel pvm = new viewProductModel();
            CoffeeShopEntities enit = new CoffeeShopEntities();
            product prod = enit.products.Find(id);

            decimal temp = prod.productPrice;
            prod.productPrice = prod.productOldP;
            prod.productOldP = temp;
            enit.Entry(prod).State = EntityState.Modified;

            enit.SaveChanges();
            return RedirectToAction("Index");
        }



        public ActionResult Delete(int? id)
        {
            viewProductModel pvm = new viewProductModel();
            product myprod = new product();
            CoffeeShopEntities enit = new CoffeeShopEntities();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            myprod = enit.products.Find(id);
            if (myprod == null)
            {
                return HttpNotFound();
            }
            
                enit.products.Remove(myprod);
                enit.SaveChanges();

            
            pvm.products = enit.products.ToList<product>();
            return View("Admin", pvm);
        }



        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            viewProductModel pvm = new viewProductModel();
            CoffeeShopEntities enit = new CoffeeShopEntities();
            product prod = enit.products.Find(id);
            enit.products.Remove(prod);
            enit.SaveChanges();
            return RedirectToAction("Index");
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
