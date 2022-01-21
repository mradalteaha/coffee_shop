using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using coffee_shop.Models;
using coffee_shop.ViewModels;
using System.IO;
namespace coffee_shop.Controllers
{
    public class ProductController : Controller
    {
        public ActionResult Enter()
        {
            ProductsDBEntities1 enit = new ProductsDBEntities1();
            ProductViewModel pvm = new ProductViewModel();
            List<product> products = enit.products.ToList<product>();
            pvm.myprod = new product();
            pvm.products = products;
            return View(pvm);
        }


        [HttpPost]
        public ActionResult Submit()
        {
            ProductViewModel pvm = new ProductViewModel();
            product myprod = new product()
            {
                ProductID = Request.Form["myprod.ProductID"].ToString(),
                ProductName = Request.Form["myprod.ProductName"].ToString(),
                // Description = Request.Form["myprod.Description"].ToString(),
                Price = Convert.ToDecimal(Request.Form["myprod.Price"]),
                imagepath = Request.Files["myprod.imagepath"].FileName,
                imgfile = Request.Files["myprod.imagepath"],


            };
            string fileName = Path.GetFileNameWithoutExtension(myprod.imgfile.FileName);
            string extension = Path.GetExtension(myprod.imgfile.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            myprod.Description = "~/assets/Images/" + fileName;
            fileName = Path.Combine(Server.MapPath("~/assets/Images/"), fileName);
            myprod.imagepath = fileName;
            myprod.imgfile.SaveAs(fileName);

            ProductsDBEntities1 enit = new ProductsDBEntities1();
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
            ProductsDBEntities1 enit = new ProductsDBEntities1();
            ProductViewModel pvm = new ProductViewModel();
            List<product> products = enit.products.ToList<product>();
            pvm.myprod = new product();
            pvm.products = products;
            return View(pvm);
        }
    }
}