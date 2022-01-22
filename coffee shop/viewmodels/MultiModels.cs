using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using coffee_shop.Models;
using coffee_shop.viewmodels;
namespace coffee_shop.viewmodels
{
    public class MultiModels
    {
        public MultiModels()
        {
            CoffeeShopEntities enit = new CoffeeShopEntities();
            ViewProductModel pvm = new ViewProductModel();
            List<product> products = enit.products.ToList<product>();
            ShoppingCartModel mycart = new ShoppingCartModel();
            List<seatModel> seat = new List <seatModel>();
            pvm.myprod = new product();
            pvm.products = products;
           

        }
        public product myprod { get; set; }
        public List<product> products { get; set; }
        public ShoppingCartModel mycart { get; set; }
        public UserModel model { set; get; }
        public List<seat> seats { get; set; }


        public void updateprice(product prod)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["UserDal"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {

                SqlCommand cmd = new SqlCommand("updateandswapPrice", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;


                decimal oldprice = prod.productPrice;


                SqlParameter parampId = new SqlParameter();
                parampId.ParameterName = "@Id";
                parampId.Value = prod.productId;
                cmd.Parameters.Add(parampId);


                SqlParameter newPricee = new SqlParameter();
                newPricee.ParameterName = "@newPrice";
                newPricee.Value = prod.productPrice;
                cmd.Parameters.Add(newPricee);


                con.Open();
                cmd.ExecuteNonQuery();

            }
        }

        
    }
}