using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Database
{
    /// <summary>
    /// Db context class which interacts with database to perform operations.
    /// </summary>
    public class ToDoServiceDBContext : DbContext
    {

        public ToDoServiceDBContext()
        {

        }
        public DbSet<ToDoList> ToDoLists { get; set; }
        public DbSet<ToDoItem> ToDoItems { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Label> Labels { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile(@Directory.GetCurrentDirectory() + "/appsettings.json").Build();
            string connectionString = configuration.GetConnectionString("ToDoServiceDb");
            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BaseToDoItem>()
            .ToTable("ToDoItems");
            modelBuilder.Entity<BaseToDoList>()
           .ToTable("ToDoLists");
            modelBuilder.Ignore<BaseToDoItem>();
            modelBuilder.Ignore<BaseToDoList>();


        }
    }
}
