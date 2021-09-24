using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CardGame.BusinessLogic
{
    /// <summary>
    /// Class that validates the user input
    /// </summary>
    public class CardValidator : ICardValidator
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public CardValidator()
        { }

        /// <summary>
        /// Error message
        /// </summary>
        public string ErrorMessage { get { return _errorMessage; } private set { _errorMessage = value; } }

        /// <summary>
        /// Validates the input
        /// </summary>
        /// <param name="cardsDisp">array of display names</param>
        /// <returns>true if validation is successfull otherwise false</returns>
        public bool ValidateInput(string[] cardsDisp)
        {
            if(cardsDisp.Where(x=>x.Length == 1).FirstOrDefault() !=null || cardsDisp.Where(x => x.Length > 2).FirstOrDefault() != null)
            {
                _errorMessage = $"Invalid Card.";
                return false;
            }
            if (cardsDisp.GroupBy(x => x).Where(x => x.Key == "JR" && x.Count() > 2).FirstOrDefault() != null)
            {
                _errorMessage = "A hand cannot contain more than two Jokers.";
                return false;
            }


            if (cardsDisp.GroupBy(x => x).Where(x => x.Key != "JR" && x.Count() > 1).FirstOrDefault() != null)
            {
                _errorMessage = "Cards cannot be duplicated.";
                return false;
            }

            if (cardsDisp.Where(x => !Regex.IsMatch(x, "^[a-zA-Z0-9,]*$")).FirstOrDefault() != null)
            {
                _errorMessage = "Invalid input string.";
                return false;
            }

            return true;
        }

        /// <summary>
        /// Stores error message
        /// </summary>
        private string _errorMessage;
    }
}
