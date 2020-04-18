using System;
using System.IO;
using Microsoft.EntityFrameworkCore;

namespace EfCoreSqliteExample.Entity
{
    public class SampleContext: DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<Category> Categories { get; set; }
        
        public SampleContext()
        {
            this.Database.OpenConnection();
            this.Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string dbPath = string.Empty; 

            if (Xamarin.Forms.Device.RuntimePlatform == Xamarin.Forms.Device.Android)
            {
                dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "Sample.db");
            }
            else
            {
                dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "..", "Library", "Sample.db");
            }

            optionsBuilder.UseSqlite($"filename={dbPath}");
        }
    }
}