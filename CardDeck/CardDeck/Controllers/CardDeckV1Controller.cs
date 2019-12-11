using CardDeck.Commands;
using CardDeck.Dto;
using CardDeck.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CardDeck.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class CardDeckV1Controller : ControllerBase
    {
        private IMediator mediator;

        public CardDeckV1Controller(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [Route("[action]")]
        [HttpGet]
        public async Task<ActionResult<List<CardDto>>> ShuffleCard()
        {
            return Ok(await mediator.Send(new ShuffleCardCommand()));
        }

        [Route("[action]")]
        [HttpGet]
        public async Task<ActionResult<CardDto>> TakeCard()
        {
            return Ok(await mediator.Send(new TakeCardCommand()));
        }

        [Route("[action]/{numberOfCards}")]
        [HttpGet]
        public async Task<ActionResult<List<CardDto>>> TakeCards(int numberOfCards)
        {
            return Ok(await mediator.Send(new TakeCardsCommand(numberOfCards)));
        }

        [Route("[action]")]
        [HttpGet]
        public async Task<ActionResult<List<CardDto>>> ResetCard()
        {
            return Ok(await mediator.Send(new ResetCardQuery()));
        }

        [Route("[action]")]
        [HttpGet]
        public async Task<ActionResult<CardDto>> GetAllCardRanks()
        {
            return Ok(await mediator.Send(new GetAllCardRanksQuery()));
        }

        [Route("[action]")]
        [HttpGet]
        public async Task<ActionResult<CardDto>> GetAllSuitsQuery()
        {
            return Ok(await mediator.Send(new GetAllSuitsQuery()));
        }
    }
}