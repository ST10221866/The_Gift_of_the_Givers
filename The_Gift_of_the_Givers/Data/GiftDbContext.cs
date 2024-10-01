using Microsoft.EntityFrameworkCore;
using The_Gift_of_the_Givers.Models;

namespace The_Gift_of_the_Givers.Data
{
    public class GiftDbContext : DbContext
    {
        public GiftDbContext(DbContextOptions<GiftDbContext> options) : base(options) { }

        public DbSet<Users> Users { set; get; }
        public DbSet<IncidentReport> IncidentReports { get; set; }
        public DbSet<Donation> donations {  set; get; }
        public DbSet<Volunteer> Volunteers { get; set; }
        public DbSet<Admin> Admins { get; set; }
    }
}
