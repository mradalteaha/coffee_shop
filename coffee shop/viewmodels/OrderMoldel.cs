using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace coffee_shop.viewmodels
{
    public class OrderMoldel
    {

        public int OrderId { set; get; }
        public DateTime orderdate { set; get; }
        public string OrderNumber { set; get; }
    }
}