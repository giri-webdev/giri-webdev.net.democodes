using giri_webdev_livedemo.Models.CodeFirst;
using System.Data.Entity;

namespace giri_webdev_livedemo.DbContexts
{
    public class NorthWindContext:DbContext
    {
        public NorthWindContext():base("giriwebdevConnection")
        {

        }

        public DbSet<Product> Products { get; set; }
    }
}