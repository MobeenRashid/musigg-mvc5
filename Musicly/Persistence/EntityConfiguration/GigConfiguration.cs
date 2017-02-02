using System.Data.Entity.ModelConfiguration;
using Musicly.Core.Models;

namespace Musicly.Persistence.EntityConfiguration
{
    public class GigConfiguration:EntityTypeConfiguration<Gig>
    {
        public GigConfiguration()
        {
            Property(g => g.ArtistId).IsRequired();

            Property(g => g.GenreId).IsRequired();

            Property(g => g.Venue).IsRequired().HasMaxLength(255);

            Property(g => g.DateTime).IsRequired();

            HasMany(g => g.Attendances).WithRequired(a => a.Gig).WillCascadeOnDelete(false);
        }
    }
}