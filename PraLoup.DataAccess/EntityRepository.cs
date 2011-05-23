using System.Data.Entity;
using PraLoup.DataAccess.Entities;
using PraLoup.DataAccess.Interfaces;

namespace PraLoup.DataAccess
{
    public class EntityRepository : DbContext 
    {
        public DbSet<Event> Events { get; set; }
        public DbSet<Invitation> Invitations { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<MetroArea> MetroAreas { get; set; }
        // NOTE: Enter new entities here        

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // TODO: replace this with DI framework call
            IDataGenerator generator = new TestSeedDataGenerator();
            generator.Execute();
        }     
    }
}