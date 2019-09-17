using DieboldNixdorfEntryTest.Entity;
using JetBrains.Annotations;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using DieboldNixdorfEntryTest.Core;
using Microsoft.Extensions.DependencyInjection;
using DieboldNixdorfEntryTest.Utility;

namespace DieboldNixdorfEntryTest.Data
{
    class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        private string databasePath = Path.Combine(ConfigurationManager.AppSettings["DatabaseDirectory"], ConfigurationManager.AppSettings["DatabaseFilename"]);

        public AppDbContext()
        {
            bool databaseExisted = File.Exists(databasePath);
            Database.EnsureCreated();
            if (!databaseExisted)
            {
                CreateSampleData(20);
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            Directory.CreateDirectory(ConfigurationManager.AppSettings["DatabaseDirectory"]);
            var builder = new SqliteConnectionStringBuilder() { DataSource = databasePath };
            optionsBuilder.UseSqlite(builder.ConnectionString);
        }

        private void CreateSampleData(int count)
        {
            var random = new Random();
            var cipher = Container.ServiceProvider.GetService<ICipher>();
            string[] usernames = { "bubbles", "shimmer", "angelic", "bubbly", "glimmer", "baby", "pink", "little", "butterfly", "sparkly", "doll", "sweet" };
            for (int i = 0; i < count; i++)
            {
                string username = usernames[random.Next(0, usernames.Length)] + "_" + random.Next(1, 100).ToString("D2");
                string password = cipher.Encrypt(username);
                Users.Add(new User(username, password));
            }
            SaveChanges();
        }
    }
}
