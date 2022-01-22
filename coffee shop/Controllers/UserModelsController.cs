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

using System.Linq;
namespace coffee_shop.Controllers
 
{
    public class UserModelsController : Controller
    {
        private UserDal db = new UserDal();
        public static int tablesID = 1;
        private CoffeeShopEntities enit;
        private MultiModels pvm;
        private List<product> products;
        private List<seat> seats;
        private List<ShoppingCartModel> listofshoppingprod;
        private ShoppingCartModel shopcart;


        public UserModelsController() {
            enit = new CoffeeShopEntities();
            pvm = new MultiModels();
             products = enit.products.ToList<product>();
            seats = enit.seats.ToList<seat>();
             listofshoppingprod= new List<ShoppingCartModel>() ;
            shopcart = new ShoppingCartModel();

            pvm.seats = new List<seat>();




        }

        // GET: UserModels



        public ActionResult Index()

        {


            pvm.products = products;
            pvm.seats = seats;
           
            return View(pvm);
        }


        [HttpPost]
        public JsonResult Index(string itemid)
        {
            List<ShoppingCartModel> listofshoppingprod = new List<ShoppingCartModel>();
            ShoppingCartModel shopcart = new ShoppingCartModel();
           
            product objprod = enit.products.Single(model => model.productId.ToString() == itemid);
            if (Session["CartCounter"] != null)
            {
                listofshoppingprod = Session["Cartitem"] as List<ShoppingCartModel>;
            }
            if (listofshoppingprod.Any(model => model.itemid.ToString() == itemid))
            {
                shopcart = listofshoppingprod.Single(model => model.itemid == itemid);
                shopcart.Quantity = shopcart.Quantity + 1;
                shopcart.total = shopcart.Quantity * shopcart.unitprice;
            }
            else
            {
                shopcart.itemid = itemid;
                shopcart.imagepath = objprod.imagePath;
                shopcart.Quantity = 1;
                shopcart.total = objprod.productPrice;
                shopcart.unitprice = objprod.productPrice;

                listofshoppingprod.Add(shopcart);

            }
            Session["CartCounter"] = listofshoppingprod.Count;
            Session["Cartitem"] = listofshoppingprod;
            return Json(data: new { success = true, Counter = listofshoppingprod.Count }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult AddOrder()
        {
            int Orderid = 0;
            int cnt = 0;
            listofshoppingprod = Session["Cartitem"] as List<ShoppingCartModel>;
            Order order = new Order()
            {
                OrderDate = DateTime.Now,
                OrderNumber = string.Format("{0:ddmmyyyyHHmmss}", DateTime.Now),

            };
            int oid = Convert.ToInt32(string.Format("{0:ddmmHH}", DateTime.Now)) + Convert.ToInt32(shopcart.Quantity);

            while (enit.Orders.Any(ord => ord.Orderid == oid))
            {
                oid++;
            }

            order.Orderid = oid;
            Orderid = order.Orderid;
            enit.Orders.Add(order);
            enit.SaveChanges();


            foreach (var item in listofshoppingprod)
            {
                cnt += 1;
                OrderDetail detail = new OrderDetail();
                detail.Total = item.total;
                detail.itemid = item.itemid;
                detail.Quantetity = item.Quantity;
                detail.UnitPrice = item.unitprice;
                detail.Orderid = Orderid;
                detail.OrderDetails = Orderid + cnt;
                
                enit.OrderDetails.Add(detail);
                enit.SaveChanges();

            }
            Session["CartCounter"] = null;
            Session["Cartitem"] = null;

            return RedirectToAction("Index");
        }
        public ActionResult shoping()
        {
            listofshoppingprod = Session["Cartitem"] as List<ShoppingCartModel>;
            return View(listofshoppingprod);

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
           
            pvm.myprod = new product();
            pvm.products = products;
            pvm.seats = seats;
            return View(pvm);
        }
        [HttpPost]
        public ActionResult Submit()
        {
          
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
       [HttpGet]
        public ActionResult Edit(int? id)
        {

            
            
            product myprod = new product();
            pvm.products = enit.products.ToList<product>();

            myprod = enit.products.Find(id);
            if (myprod == null)
            {
                return HttpNotFound();
            }
          

            return View(myprod);
        }

        
        // POST: UserModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(product myprod)
        {

            

            if (ModelState.IsValid)
            {
                pvm.updateprice(myprod);
                return RedirectToAction("Admin");
            }
            

          
           
            pvm.products = enit.products.ToList<product>();
        
            return View("Admin", pvm);
        }


        // GET: UserModels/Delete/5





        public ActionResult Delete(int? id)
        {
            product myprod = new product();
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
            
            product prod = enit.products.Find(id);
            enit.products.Remove(prod);
            enit.SaveChanges();
            return RedirectToAction("Index");
        }


       
        [HttpPost, ActionName("AddTinside")]
        public ActionResult AddTinside()
        {
            int temp = 0;
            foreach (seat chair in enit.seats)
            {

                temp = chair.seatId;

            }
            temp += 1;
              
            seat tab = new seat()
            {
                place = "inside",
                available = true,
                occupied = "lightgreen",
                reserver = "none",
                seatId = temp


            };
            
            enit.seats.Add(tab);
            
            enit.SaveChanges();
            return RedirectToAction("Admin");
        }
        [HttpPost, ActionName("removeTinside")]
        public ActionResult removeTinside()
        {
            
            foreach(seat chair in enit.seats) {

                if (chair.available && chair.place.Trim() == ("inside"))
                {

                    enit.seats.Remove(chair);
                  
                    
                    break;
                }

            }
               
              enit.SaveChanges();
            

           
            return RedirectToAction("Admin");
        }


        [HttpPost, ActionName("AddTout")]
        public ActionResult AddTout()
        {

            int temp = 0;
            foreach (seat chair in enit.seats)
            {

                temp = chair.seatId;

            }
            temp += 1;
            seat tab = new seat()
            {
                place = "out",
                available = true,
                occupied = "lightgreen",
                reserver = "none",
                seatId = temp


            };
            
            enit.seats.Add(tab);

            enit.SaveChanges();
            return RedirectToAction("Admin");
        }
        [HttpPost, ActionName("removeTout")]
        public ActionResult removeTout()
        {
            
            foreach (seat chair in enit.seats)
            {

                if (chair.available && chair.place.Trim() == ("out"))
                {

                    enit.seats.Remove(chair);
                    
                  
                    break;
                }

            }
        enit.SaveChanges();


            return RedirectToAction("Admin");
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
