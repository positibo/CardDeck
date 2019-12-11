using CardDeck.Dto;
using CardDeck.Entities;
using CardDeck.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CardDeck.Queries
{
    public class ResetCardQuery : IRequest<List<CardDto>>
    {
        private class ResetCardQueryHandler : IRequestHandler<ResetCardQuery, List<CardDto>>
        {
            private DataContext context;

            public ResetCardQueryHandler(DataContext context)
            {
                this.context = context;
            }

            public async Task<List<CardDto>> Handle(ResetCardQuery request, CancellationToken cancellationToken)
            {
                var cards = Enumerable.Range(1, 4)
               .SelectMany(s => Enumerable.Range(1, 13)
                                   .Select(c => new Card()
                                   {
                                       SuitId = s,
                                       CardNumber = c
                                   })
                           )
                  .ToList();

                var cardList = new List<CardDto>();
                if (cards != null)

                {
                    foreach (var entity in context.Cards)
                        context.Cards.Remove(entity);

                    foreach (var item in cards)
                    {
                        context.Cards.Add(new Card { SuitId = item.SuitId, CardNumber = item.CardNumber });
                        var suit = await context.Suits.FirstOrDefaultAsync(s => s.SuitId == item.SuitId);
                        cardList.Add(new CardDto
                        {
                            CardId = item.CardId,
                            CardNumber = item.CardNumber,
                            SuitName = suit != null ? suit.SuitName : string.Empty
                        });
                    }
                }

                await context.SaveChangesAsync();

                return cardList;
            }
        }
    }
}
