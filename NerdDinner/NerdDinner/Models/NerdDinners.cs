using System.Data.Entity;

namespace NerdDinner.Models
{
    public class NerdDinners : DbContext
    {
        public NerdDinners()
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<NerdDinners>());
        }

        public DbSet<Dinner> Dinners { get; set; }
        public DbSet<RSVP> RSVPs { get; set; }
    }
}