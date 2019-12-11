using CardDeck.Dto;
using CardDeck.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace CardDeck.Commands
{
    public class TakeCardCommand : IRequest<CardDto>
    {
        private class TakeCardCommandHandler : IRequestHandler<TakeCardCommand, CardDto>
        {
            private DataContext context;

            public TakeCardCommandHandler(DataContext context)
            {
                this.context = context;
            }

            public async Task<CardDto> Handle(TakeCardCommand request, CancellationToken cancellationToken)
            {
                var result = await context.Cards.FirstOrDefaultAsync();
                var card = new CardDto();
                if (result != null)
                {
                    var suit = await context.Suits.FirstOrDefaultAsync(s => s.SuitId == result.SuitId);
                    card.CardId = result.CardId;
                    card.CardNumber = result.CardNumber;
                    card.SuitName = suit != null ? suit.SuitName : string.Empty;
                }

                context.Cards.Remove(result);
                await context.SaveChangesAsync();

                return card;
            }
        }
    }
}
