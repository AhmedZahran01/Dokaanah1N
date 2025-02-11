using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Dokaanah.Models
{
    public class DokkanahDBContex: IdentityDbContext<Customer>
    { 
        #region Constractor  Region
          
        public DokkanahDBContex(DbContextOptions<DokkanahDBContex> options)
             : base(options)
        {
            // Ensure the base constructor is called with the options
        }

        #endregion

        #region Dbsets Region
        public virtual DbSet<IdentityRole> Roles { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Cart> Carts { get; set; }
        public virtual DbSet<Cart_Product> Cart_Products { get; set; }

         
        #endregion

        #region   On Configuring Comment Region

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    #region Comment for Connection String Region

        //    //UseSqlServer("Server=DESKTOP-0HPU58A\\SQLEXPRESS;Database=DokkanahDataBase_2fff;Encrypt=false;Trusted_Connection=True;TrustServerCertificate=True");

        //    //optionsBuilder.UseSqlServer(
        //    //    //"server=localhost;database=DokkanahDataBas2e;uid=root;pwd=new_password;"
        //    //      //("Server=DESKTOP-NRGEJ6B\\SQLEXPRESS;Database=DokSql;Trusted_Connection=True;")
        //    //      ("Server=localhost\\SQLEXPRESS;Database=YourDatabase;Trusted_Connection=True;TrustServerCertificate=True;") 
        //    //); 
        //    #endregion
        //}

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        optionsBuilder.UseSqlServer("Server=DESKTOP-NRGEJ6B\\SQLEXPRESS;Database=YourDatabase;Trusted_Connection=True;TrustServerCertificate=True;");
        //    }
        //}

        #endregion
         
        #region  On Model Creating Region

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
             
            modelBuilder.Entity<Cart_Product>()
                .HasKey(e => new { e.Prid, e.Caid });

             

        }

        #endregion

    }
}
