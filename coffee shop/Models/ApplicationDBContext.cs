using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace coffee_shop.Models
{
    public partial class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext()
            : base("name=ApplicationDBContext")
        {
        }

        public virtual DbSet<UserModel> Users { get; set; }



        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
