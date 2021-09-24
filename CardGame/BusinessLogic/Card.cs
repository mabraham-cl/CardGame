using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardGame.BusinessLogic
{
    /// <summary>
    /// Class that stores details about a card
    /// </summary>
    public class Card
    {
        /// <summary>
        /// Name displayed on the card
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// e.g: Club, Diamond, Heart, Spade
        /// </summary>
        public Suit? Suit { get; set; }

        /// <summary>
        /// Value that the card holds
        /// </summary>
        public int Value { get; set; }

    }
}
