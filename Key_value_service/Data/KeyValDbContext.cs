using Key_value_service.Models;
using Microsoft.EntityFrameworkCore;

namespace Key_value_service.Data
{
    public class KeyValDbContext : DbContext
    {
        public KeyValDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<KeyValue> keyValues { get; set; }
    }
}
