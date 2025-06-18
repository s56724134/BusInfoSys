using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using api.Models.LineModel;


namespace api.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Bus> Buses { get; set; }
        public DbSet<Stop> Stops { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<Remind> Reminds { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // 若要處理 JSON 欄位可以加轉換器，或用 ValueObject
            modelBuilder.Entity<Remind>().OwnsOne(r => r.RemindTime, rt =>
            {
                rt.OwnsOne(rtv => rtv.BusShowUpTime);
            });
        }
    }
}
