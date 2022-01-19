using coffee_shop.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace coffee_shop.Dal
{
        public class UserDal : DbContext
        {
            protected override void OnModelCreating(DbModelBuilder modelBuilder)
            {
                base.OnModelCreating(modelBuilder);
                modelBuilder.Entity<UserModel>().ToTable("User");

            }

            public DbSet<UserModel> Users { get; set; }

        }

}