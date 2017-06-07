using Microsoft.EntityFrameworkCore;

namespace TokenApi.Models
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

		public DbSet<TokenItem> TokenItems { get; set; }
		public DbSet<MessageItem> MessageItem { get; set; }

    }
}