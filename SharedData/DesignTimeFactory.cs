using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedData
{
    public class DesignTimeFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var cfg = new ConfigurationBuilder()
                .AddJsonFile("appsettings.Development.json", optional: true)
                .AddEnvironmentVariables()
                .Build();

            var cs = cfg.GetConnectionString("RealTimeDb")
                ?? "Data Source=LabVM1846780\\SQLEXPRESS;Initial Catalog=OrderSystem;Integrated Security=True;Encrypt=True;Trust Server Certificate=True;";

            var opts = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlServer(cs)
                .Options;

            return new AppDbContext(opts);
        }
    }
}
