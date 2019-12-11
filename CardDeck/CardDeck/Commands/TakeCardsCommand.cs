using CardDeck.Dto;
using CardDeck.Entities;
using CardDeck.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CardDeck.Commands
{
    public class TakeCardsCommand : IRequest<List<CardDto>>
    {
        public int NumberOfCards { get; set; }

        public TakeCardsCommand(int numberOfCards)
        {
            this.NumberOfCards = numberOfCards;
        }

        private class TakeCardsHandler : IRequestHandler<TakeCardsCommand, List<CardDto>>
        {
            private DataContext context;

            public TakeCardsHandler(DataContext context)
            {
                this.context = context;
            }

            public async Task<List<CardDto>> Handle(TakeCardsCommand request, CancellationToken cancellationToken)
            {
                var result = await context.Cards.ToListAsync();
                var cards = new List<CardDto>();
                if (result != null)
                {
                    foreach (var item in result)
                    {
                        var suit = await context.Suits.FirstOrDefaultAsync(s => s.SuitId == item.SuitId);
                        cards.Add(new CardDto
                        {
                            CardId = item.CardId,
                            CardNumber = item.CardNumber,
                            SuitName = suit != null ? suit.SuitName : string.Empty
                        });
                    }
                }

                var takeCards = cards.Take(request.NumberOfCards);
                //cards.RemoveAll(takeCards.Contains);

                foreach (var entity in result)
                {
                    if (takeCards.Any(c => c.CardId == entity.CardId))
                        context.Cards.Remove(entity);
                }

                await context.SaveChangesAsync();

                return takeCards.ToList();
            }

            private Task<Card> DoAsyncResult(Card entity)
            {
                Task.Delay(1000);
                return Task.FromResult(entity);
            }
        }
    }
}
