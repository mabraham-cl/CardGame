using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using CardGame.BusinessLogic;
using NUnit.Framework;

namespace CardGame_UnitTest
{
    public class CardValidator_UnitTest
    {
        [SetUp]
        public void Setup()
        {
            _iCardValidator = new CardValidator();
        }

        [Test]
        [TestCaseSource(typeof(StringArrayTestValidationDataFalse))]
        public void CardValidator_ValidateInput_ReturnFalse(string[] cards)
        {
            var result = _iCardValidator.ValidateInput(cards);
            Assert.IsFalse(result);
        }

        [Test]
        public void CardValidator_ValidateInput_InvalidCard()
        {
            var result = _iCardValidator.ValidateInput(new string[] {"AAA"});
            Assert.AreEqual("Invalid Card.", _iCardValidator.ErrorMessage);
        }

        [Test]
        public void CardValidator_ValidateInput_MoreThanTwoJokers()
        {
            var result = _iCardValidator.ValidateInput(new string[] {"JR" ,"JR","JR"});
            Assert.AreEqual("A hand cannot contain more than two Jokers.", _iCardValidator.ErrorMessage);
        }

        [Test]
        public void CardValidator_ValidateInput_DuplicateCards()
        {
            var result = _iCardValidator.ValidateInput(new string[] {"TC","TC" });
            Assert.AreEqual("Cards cannot be duplicated.", _iCardValidator.ErrorMessage);
        }

        [Test]
        public void CardValidator_ValidateInput_InvalidInputString()
        {
            var result = _iCardValidator.ValidateInput(new string[] { "A%" });
            Assert.AreEqual("Invalid input string.", _iCardValidator.ErrorMessage);
        }

        [Test]
        [TestCaseSource(typeof(StringArrayTestValidationDataTrue))]
        public void CardValidator_ValidateInput_ReturnTrue(string[] cards)
        {
            var result = _iCardValidator.ValidateInput(cards);
            Assert.IsTrue(result);
        }

        private ICardValidator _iCardValidator;
    }

    public class StringArrayTestValidationDataFalse : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            yield return new string[] { "TCC", "TD", "JR", "TH", "TS", "JR" };
            yield return new string[] { "3", "1B", "5" };
            yield return new string[] { "6", "8", "10" };
            yield return new string[] { "JR", "JR", "JR" };
            yield return new string[] { "3H", "3H", "JR" };
            yield return new string[] { "3%H" };
        }
    }

    public class StringArrayTestValidationDataTrue : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            yield return new string[] { "TC", "TD", "JR", "TH", "TS", "JR" };
            yield return new string[] { "JR", "2C", "JR" };
            yield return new string[] { "JR", "JR" };
            yield return new string[] { "2C", "3C", "4C", "5C", "6C", "7C", "8C", "9C", "TC", "JC", "QC", "KC", "AC" };
        }
    }

}
