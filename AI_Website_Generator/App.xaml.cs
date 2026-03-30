using System.Windows;
using Microsoft.EntityFrameworkCore;
using Orak.WebPro.Data.Context;

namespace Orak.WebPro.Admin
{
    public partial class App : Application
    {
        public static OrakWebProDbContext CreateDbContext()
        {
            var options = new DbContextOptionsBuilder<OrakWebProDbContext>()
                .UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=OrakWebProDb;Trusted_Connection=True;TrustServerCertificate=True;")
                .Options;

            return new OrakWebProDbContext(options);
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            using var db = CreateDbContext();
            db.Database.EnsureCreated();
        }
    }
}