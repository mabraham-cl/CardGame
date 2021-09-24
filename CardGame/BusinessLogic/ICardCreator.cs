using System.Collections.Generic;

namespace CardGame.BusinessLogic
{
    /// <summary>
    /// Public interface to CardCreator
    /// </summary>
    public interface ICardCreator
    {
        /// <summary>
        /// Stores the list of cards in a game
        /// </summary>
        List<Card> Cards { get; }

        /// <summary>
        /// Method that clear the list
        /// </summary>
        void ClearCards();

        /// <summary>
        /// Method that create the card and add to the Cards list
        /// </summary>
        /// <param name="cardsDisp">List of display names</param>
        /// <returns>true if success otherwise returns false</returns>
        bool CreateCards(string[] cardsDisp);

        /// <summary>
        /// This method retrieve the Total points for the winner
        /// </summary>
        /// <returns>Total points of all cards</returns>
        int RetrieveTotalPoints();
    }
}