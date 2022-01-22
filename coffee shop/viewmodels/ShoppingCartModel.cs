using coffee_shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace coffee_shop.viewmodels
{
    public class ShoppingCartModel
    {
       
            public string itemid { get; set; }

            public decimal Quantity { get; set; }

            public decimal unitprice { get; set; }

            public decimal total { get; set; }

            public string imagepath { set; get; }

        

    }
}