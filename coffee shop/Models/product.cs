//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace coffee_shop.Models
{
    using System;
    using System.Collections.Generic;
    using System.Web;
    public partial class product
    {
        public int productId { get; set; }
        public string productName { get; set; }
        public decimal productPrice { get; set; }
        public string productDesc { get; set; }
        public decimal productOldP { get; set; }
        public string imagePath { get; set; }

        public HttpPostedFileBase imgfile { get; set; }
    }
}