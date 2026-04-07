using System;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using Orak.WebPro.Data.Context;

namespace Orak.WebPro.Admin
{
    public partial class App : System.Windows.Application
    {
        public static OrakWebProDbContext CreateDbContext()
        {
            string dbPath = System.IO.Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "orakwebpro.db");

            var options = new DbContextOptionsBuilder<OrakWebProDbContext>()
                .UseSqlite($"Data Source={dbPath}")
                .Options;

            return new OrakWebProDbContext(options);
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            try
            {
                using var db = CreateDbContext();
                db.Database.EnsureCreated();
            }
            catch
            {
                // DB unavailable — app continues without it
            }

            ShutdownMode = ShutdownMode.OnExplicitShutdown;

            var login = new Orak.WebPro.Admin.user.LoginWindow();
            if (login.ShowDialog() == true)
            {
                ShutdownMode = ShutdownMode.OnMainWindowClose;
                var main = new MainWindow();
                main.Show();
            }
            else
            {
                Shutdown();
            }
        }
    }
}