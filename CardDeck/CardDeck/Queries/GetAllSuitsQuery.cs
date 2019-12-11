using CardDeck.Dto;
using CardDeck.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CardDeck.Queries
{
    public class GetAllSuitsQuery : IRequest<List<SuitDto>>
    {
        private class GetAllSuitsQueryHandler : IRequestHandler<GetAllSuitsQuery, List<SuitDto>>
        {
            private DataContext context;

            public GetAllSuitsQueryHandler(DataContext context)
            {
                this.context = context;
            }

            public async Task<List<SuitDto>> Handle(GetAllSuitsQuery request, CancellationToken cancellationToken)
            {
                var result = await context.Suits.ToListAsync();

                var suits = new List<SuitDto>();
                if (result != null)
                {
                    foreach (var item in result)
                    {
                        suits.Add(new SuitDto
                        {
                           SuitId = item.SuitId,
                           SuitName = item.SuitName
                        });
                    }
                }

                return suits;
            }
        }
    }
}
