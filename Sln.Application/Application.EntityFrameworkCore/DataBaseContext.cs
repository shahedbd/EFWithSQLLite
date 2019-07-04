using Application.Core;
using Microsoft.EntityFrameworkCore;

namespace Application.EntityFrameworkCore
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options) { }

        public DbSet<PersonalInfo> PersonalInfo { get; set; }
    }
}
