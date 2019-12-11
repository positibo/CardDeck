using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CardDeck.Entities
{
    public class Card
    {
        [Key]
        public int CardId { get; set; }
        public int SuitId { get; set; }
        public int CardNumber { get; set; }
    }
}
