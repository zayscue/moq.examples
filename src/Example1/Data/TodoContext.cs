using Example1.Data.Seed;
using Example1.Models;
using Microsoft.EntityFrameworkCore;

namespace Example1.Data
{
    public sealed class TodoContext : DbContext
    {
        public TodoContext() : base() {}

        public TodoContext(DbContextOptions<TodoContext> options) : base(options)
        {
            Database.EnsureCreated();
            this.EnsureSeedData();
        }

        public DbSet<Reminder> Reminders { get; set; }
    }
}
