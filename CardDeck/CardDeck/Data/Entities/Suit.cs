using System.ComponentModel.DataAnnotations;

namespace CardDeck.Entities
{
    public class Suit
    {
        [Key]
        public int SuitId { get; set; }
        public string SuitName { get; set; }
    }
}
