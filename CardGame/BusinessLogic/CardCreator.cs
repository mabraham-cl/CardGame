using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardGame.BusinessLogic
{
    /// <summary>
    /// This class is responsible for the creation of card
    /// </summary>
    public class CardCreator : ICardCreator
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public CardCreator()
        {
            _cards = new List<Card>();
        }

        /// <summary>
        /// Stores the list of cards in a game
        /// </summary>
        public List<Card> Cards {
            get 
            { 
                return _cards; 
            } 
            private set 
            {
                _cards = value;
            }
        }

        /// <summary>
        /// Method that create the card and add to the Cards list
        /// </summary>
        /// <param name="cardsDisp">List of display names</param>
        /// <returns>true if success otherwise returns false</returns>
        public bool CreateCards(string[] cardsDisp)
        {            
            foreach (string displayName in cardsDisp)
            {
                if (displayName.Equals("JR"))
                    _cards.Add(new Card() { DisplayName = "JR", Suit = null, Value = 0 });
                else 
                {
                    if (displayName.Length == 1)
                        return false;

                    // 1st char denotes face
                    string face = displayName.Substring(0, 1);

                    // 2nd characted denotes suit
                    string suit = displayName.Substring(1, 1);

                    int value = GetVal(face, suit);

                    // If the card is not a recognized one, return false
                    if (value == -1 || value < 0)
                        return false;

                    Enum.TryParse(suit, out Suit enumSuit);

                    _cards.Add(new Card() { DisplayName = displayName, Suit = enumSuit, Value = value });
                }
            }
            return true;
        }

        /// <summary>
        /// This method retrieve the Total points for the winner
        /// </summary>
        /// <returns>Total points of all cards</returns>
        public int RetrieveTotalPoints()
        {
            int points = _cards.Sum(x => x.Value);

            int jockerCount = _cards.GroupBy(x => x).Where(y => y.Key.DisplayName == "JR").Count();

            if (jockerCount > 0)
                points = points * 2 * jockerCount;

            return points;
        }

        /// <summary>
        /// Method that clear the list
        /// </summary>
        public void ClearCards()
        {
            if (_cards != null)
                _cards.Clear();
        }

        /// <summary>
        /// Gets the value based on face and suit
        /// </summary>
        /// <param name="face">1st character of displayname that represents the cards value</param>
        /// <param name="suit">2nd character represents suit</param>
        /// <returns>Value for a card</returns>
        private int GetVal(string face, string suit)
        {
            switch (suit)
            {
                case "C":
                    return 1 * GetFaceValue(face);
                case "D":
                    return 2 * GetFaceValue(face);
                case "H":
                    return 3 * GetFaceValue(face);
                case "S":
                    return 4 * GetFaceValue(face);
                default:
                    return -1;
            }
        }

        /// <summary>
        /// Calculates the face value
        /// </summary>
        /// <param name="face">2nd character of display name</param>
        /// <returns>calculated value</returns>
        private int GetFaceValue(string face)
        {
            switch (face)
            {
                case "2":
                    return 2;
                case "3":
                    return 3;
                case "4":
                    return 4;
                case "5":
                    return 5;
                case "6":
                    return 6;
                case "7":
                    return 7;
                case "8":
                    return 8;
                case "9":
                    return 9;
                case "T":
                    return 10;
                case "J":
                    return 11;
                case "Q":
                    return 12;
                case "K":
                    return 13;
                case "A":
                    return 14;
                default:
                    return -1;
            }
        }

        /// <summary>
        /// Stores cards details
        /// </summary>
        private List<Card> _cards; 
    }
}
