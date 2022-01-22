using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace coffee_shop.viewmodels
{
    public class seatModel
    {

        public int seatId { get; set; }
        public string reserver { get; set; }
        public string occupied { get; set; }
        public bool available { get; set; }
        public string place { get; set; }
    }
}