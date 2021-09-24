using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CardGame.BusinessLogic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CardGame.Pages
{
    /// <summary>
    /// Cards Game UI 
    /// </summary>
    public class CreateCardsModel : PageModel
    {
        /// <summary>
        /// Constructor where the dependant classes are injected via interfaces
        /// </summary>
        /// <param name="cardCreator">card creator object</param>
        /// <param name="cardValidator">card validator object</param>
        public CreateCardsModel(ICardCreator cardCreator, ICardValidator cardValidator)
        {
            _cardCreator = cardCreator;
            _cardValidator = cardValidator;
        }

        /// <summary>
        /// User input
        /// </summary>
        [BindProperty]
        public string Cards { get; set; }

        /// <summary>
        /// Error message to be displayed for user
        /// </summary>
        [BindProperty]
        public string ErrorMessage { get; set; }

        /// <summary>
        /// Total points/score calculated
        /// </summary>
        [BindProperty]
        public int TotalPoints { get; set; }

        /// <summary>
        /// Post method
        /// </summary>
        public void OnPost()
        {
            try
            {
                _cardsDisplay = new string[] { };

                if (string.IsNullOrEmpty(Cards) || string.IsNullOrWhiteSpace(Cards))
                    ErrorMessage = "Please enter the cards.";
                else
                {
                    _cardsDisplay = Cards.Trim().TrimEnd(',').TrimStart(',').ToUpper().Split(',').Select(x => x.Trim()).ToArray();

                    // Validate Input

                    if (!_cardValidator.ValidateInput(_cardsDisplay))
                    {
                        ErrorMessage = _cardValidator.ErrorMessage;
                    }
                    else
                    {
                        _cardCreator.ClearCards();

                        // Create card objects

                        if (!_cardCreator.CreateCards(_cardsDisplay))
                            ErrorMessage = "Card not recognised.";
                        else
                            TotalPoints = _cardCreator.RetrieveTotalPoints();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }

        }

        /// <summary>
        /// ICardCreator object
        /// </summary>
        private readonly ICardCreator _cardCreator;

        /// <summary>
        /// ICardValidator object
        /// </summary>
        private readonly ICardValidator _cardValidator;

        /// <summary>
        /// Stores the user input that is split to an array
        /// </summary>
        private string[] _cardsDisplay;
    }
}
