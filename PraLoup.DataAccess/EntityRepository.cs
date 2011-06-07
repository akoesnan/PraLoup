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

        public IDatabaseInitializer<EntityRepository> DataGenerator { get; private set; }
        

        public EntityRepository(IDatabaseInitializer<EntityRepository> dataGenerator, string nameOrConnectionString)
            : base(nameOrConnectionString) 
        {
            this.DataGenerator = dataGenerator;
        }

        public EntityRepository()
        {
            // TODO: Complete member initialization
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Drop the db when there is changes on the model
            // TODO: we do not want this in production, for test machine we need to make seed data
            Database.SetInitializer<EntityRepository>(this.DataGenerator);
        }     
    }
}