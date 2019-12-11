using CardDeck.Entities;
using CardDeck.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace CardDeck.Data
{
    public class DataGenerator
    {
       public static void Initialize(IServiceProvider serviceProvider)
       {
            using (var context = new DataContext(
                serviceProvider.GetRequiredService<DbContextOptions<DataContext>>()))
            {
                if (context.Cards.Any())
                    return;

                SeedCardRanks(context);
                SeedSuit(context);

                context.SaveChanges();
            }
       }

        private static void SeedSuit(DataContext context)
        {
            context.Suits.AddRange(new Suit { SuitName = "Club" });
            context.Suits.AddRange(new Suit { SuitName = "Diamond" });
            context.Suits.AddRange(new Suit { SuitName = "Heart" });
            context.Suits.AddRange(new Suit { SuitName = "Spades" });
        }

        private static void SeedCardRanks(DataContext context)
        {
            context.CardRanks.AddRange(new CardRank { CardNumber = 1, CardName = "Ace" });
            context.CardRanks.AddRange(new CardRank { CardNumber = 2, CardName = "Two" });
            context.CardRanks.AddRange(new CardRank { CardNumber = 3, CardName = "Three" });
            context.CardRanks.AddRange(new CardRank { CardNumber = 4, CardName = "Four" });
            context.CardRanks.AddRange(new CardRank { CardNumber = 5, CardName = "Five" });
            context.CardRanks.AddRange(new CardRank { CardNumber = 6, CardName = "Six" });
            context.CardRanks.AddRange(new CardRank { CardNumber = 7, CardName = "Seven" });
            context.CardRanks.AddRange(new CardRank { CardNumber = 8, CardName = "Eight" });
            context.CardRanks.AddRange(new CardRank { CardNumber = 9, CardName = "Nine" });
            context.CardRanks.AddRange(new CardRank { CardNumber = 10, CardName = "Ten" });
            context.CardRanks.AddRange(new CardRank { CardNumber = 11, CardName = "Jack" });
            context.CardRanks.AddRange(new CardRank { CardNumber = 12, CardName = "Queen" });
            context.CardRanks.AddRange(new CardRank { CardNumber = 13, CardName = "King" });
        }
    }
}
