//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System.Web;
namespace coffee_shop.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class product
    {
        public string ProductName { get; set; }
        public string ProductID { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string imagepath { get; set; }
        public int Catgoryid { get; set; }

        public HttpPostedFileBase imgfile { get; set; }
    }
}