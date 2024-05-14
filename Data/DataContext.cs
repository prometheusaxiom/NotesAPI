

using Microsoft.EntityFrameworkCore;

namespace NotesAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Notes> Notes => Set<Notes>(); 
    }
}
