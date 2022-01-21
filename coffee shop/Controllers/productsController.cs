using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using coffee_shop.Models;
using coffee_shop.viewmodels;
using System.IO;
using System.Net;

namespace coffee_shop.Controllers
{
    public class ProductController : Controller
    {


        public ActionResult Enter()
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
            return View("Enter", pvm);

        }

 
        public ActionResult ProView()
        {
            CoffeeShopEntities enit = new CoffeeShopEntities();
            viewProductModel pvm = new viewProductModel();
            List<product> products = enit.products.ToList<product>();
            pvm.myprod = new product();
            pvm.products = products;
            return View(pvm);
        }
    }
}