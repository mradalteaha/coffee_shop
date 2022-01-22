using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace coffee_shop.Models
{

   // [Table("Users")]
    public partial class UserModel

    {
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string username { get; set; }
        
        [Required]
        public string email { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        

        public string Password { get; set; }
        public bool isAdmin { get; set; }
        public bool isBarista { get; set; }
        public bool isVip { get; set; }
        public int cupcounter { get; set; }



    }
}