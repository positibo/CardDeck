using System.ComponentModel.DataAnnotations;

namespace CardDeck.Entities
{
    public class CardRank
    {
        [Key]
        public int CardRankId { get; set; }
        public int CardNumber { get; set; }
        public string CardName { get; set; }
    }
}
