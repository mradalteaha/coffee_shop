using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using coffee_shop.Models;
using coffee_shop.viewmodels;
using System.IO;
using System.Net;
using System.Data.Entity;

namespace coffee_shop.Controllers
{
    public class ProductController : Controller
    {


        public ActionResult Enter()
        {
            CoffeeShopEntities enit = new CoffeeShopEntities();
            ViewProductModel pvm = new ViewProductModel();
            List<product> products = enit.products.ToList<product>();
            pvm.myprod = new product();
            pvm.products = products;
            return View(pvm);
        }


        [HttpPost]
        public ActionResult Submit()
        {
            ViewProductModel pvm = new ViewProductModel();
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
            return View("Enter", pvm);

        }


        // GET: Items/Edit/5
        public ActionResult Edit(int? id)
        {
            CoffeeShopEntities enit = new CoffeeShopEntities();
            if (id == null)
            {
                return View("Enter");
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product myprod = enit.products.Find(id);
            if (myprod == null)
            {
                return HttpNotFound();
            }
            return View(myprod);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ItemDescription,price,images,availability,sales,age,isonSale,newPrice")] product myprod)
        {
            CoffeeShopEntities enit = new CoffeeShopEntities();
            
            if (ModelState.IsValid)
            {
                enit.Entry(myprod).State = EntityState.Modified;
                enit.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(myprod);
        }

        public ActionResult ProView()
        {
            CoffeeShopEntities enit = new CoffeeShopEntities();
            ViewProductModel pvm = new ViewProductModel();
            List<product> products = enit.products.ToList<product>();
            pvm.myprod = new product();
            pvm.products = products;
            return View(pvm);
        }
    }
}