using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;

namespace ProjectMvc.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        
        public ICollection<ShippingDetails> ShippingDetailsList { get; set; }
        public virtual ICollection<Review>ReviewList { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {

            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
         
     
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base(@"Data Source=DESKTOP-9HH57Q0\MSSQLSEVEREX;Initial Catalog=ProjectMvc;Integrated Security=True", throwIfV1Schema: false)
        {
        }
        
        public virtual DbSet<category> Category { get; set; }
        
       
        public virtual DbSet<Product> Product { get; set; }
   
        public virtual DbSet<ShippingDetails> ShippingDetails { get; set; }
        public virtual DbSet<SliderImage> SliderImage { get; set; }
        public virtual DbSet<ImagesProduct> ImagesProduct { get; set; }
        public virtual DbSet<Sub> Sub { get; set; }
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

           }
}