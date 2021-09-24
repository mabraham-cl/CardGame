namespace CardGame.BusinessLogic
{
    /// <summary>
    /// Public interface for CardValidator
    /// </summary>
    public interface ICardValidator
    {
        /// <summary>
        /// Error message
        /// </summary>
        string ErrorMessage { get; }

        /// <summary>
        /// Validates the input
        /// </summary>
        /// <param name="cardsDisp">array of display names</param>
        /// <returns>true if validation is successfull otherwise false</returns>
        bool ValidateInput(string[] cardsDisp);
    }
}