using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace MercadoPagoWeb.Models
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

        public virtual DbSet<DatosPago> DatosPago { get; set; }
    }
}
