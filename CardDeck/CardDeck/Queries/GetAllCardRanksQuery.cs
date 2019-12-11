using CardDeck.Dto;
using CardDeck.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CardDeck.Queries
{
    public class GetAllCardRanksQuery : IRequest<List<CardRankDto>>
    {
        private class GetAllCardRanksQueryHandler : IRequestHandler<GetAllCardRanksQuery, List<CardRankDto>>
        {
            private DataContext context;

            public GetAllCardRanksQueryHandler(DataContext context)
            {
                this.context = context;
            }

            public async Task<List<CardRankDto>> Handle(GetAllCardRanksQuery request, CancellationToken cancellationToken)
            {
                var result = await context.CardRanks.ToListAsync();

                var cardRanks = new List<CardRankDto>();
                if (result != null)
                {
                    foreach (var item in result)
                    {
                        cardRanks.Add(new CardRankDto
                        {
                            CardRankId = item.CardRankId,
                            CardNumber = item.CardNumber,
                            CardName = item.CardName
                        });
                    }
                }

                return cardRanks;
            }
        }
    }
}
