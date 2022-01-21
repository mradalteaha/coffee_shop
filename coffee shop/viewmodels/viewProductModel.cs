using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using coffee_shop.Models;
namespace coffee_shop.viewmodels
{
    public class viewProductModel
    {
        public product myprod { get; set; }

        public List<product> products { get; set; }
    }
}