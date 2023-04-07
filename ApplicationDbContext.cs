using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ConsoleBank;
using ConsoleBank_1.Models;

namespace Migration49
{
    /// <summary>
    /// Контекст базы данных приложения
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Person> Persons { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<HistoryOfTransaction> HistoryOfTransactions { get; set; }
        public DbSet<Service> Services { get; set; }
       

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(@"C:\Users\USER\Desktop\C#\classwork\Console Bank\ConsoleBank-4\ConsoleBank-1");
            builder.AddJsonFile("appSettings.json");

            var config = builder.Build();
            var connectionString = config.GetConnectionString("DefaultConnection");

            optionsBuilder.UseSqlServer(connectionString);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Client>()
                .HasIndex(x => x.PhoneNumber)
                .IsUnique();

            modelBuilder
                .Entity<Client>()
                .HasIndex(x => x.Login)
                .IsUnique();

            modelBuilder
                .Entity<Admin>()
                .HasIndex(x => x.Login)
                .IsUnique();
        }
    }
}
