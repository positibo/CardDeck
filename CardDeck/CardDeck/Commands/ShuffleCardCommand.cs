using CardDeck.Dto;
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
    public class ShuffleCardCommand : IRequest<List<CardDto>>
    {
        private class ShuffleCardCommandHandler : IRequestHandler<ShuffleCardCommand, List<CardDto>>
        {
            private DataContext context;

            public ShuffleCardCommandHandler(DataContext context)
            {
                this.context = context;
            }

            public async Task<List<CardDto>> Handle(ShuffleCardCommand request, CancellationToken cancellationToken)
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

                    cards = cards.OrderBy(c => Guid.NewGuid()).ToList();
                }

                return cards;
            }
        }
    }
}
