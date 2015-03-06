using Cookishly.Data.Entities;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Cookishly.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext() : base("DefaultConnection", false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}