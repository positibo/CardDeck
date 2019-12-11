using CardDeck.Entities;
using Microsoft.EntityFrameworkCore;

namespace CardDeck.Model
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            :base(options)
        {
        }

        public DbSet<Card> Cards { get; set; }
        public DbSet<Suit> Suits { get; set; }
        public DbSet<CardRank> CardRanks { get; set; }
    }
}
