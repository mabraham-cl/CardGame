using System.Collections;
using CardGame.BusinessLogic;
using NUnit.Framework;
using Moq;
using System.Collections.Generic;

namespace CardGame_UnitTest
{
    public class CardCreator_UnitTest
    {
        [SetUp]
        public void Setup()
        {
            _cardCreator = new CardCreator();
        }

        [Test]
        [TestCaseSource(typeof(StringArrayTestDataFalse))]
        public void CardCreator_CreateCards_ReturnFalse(string[] cards)
        {
            var result = _cardCreator.CreateCards(cards);
            Assert.IsFalse(result);
        }

        [Test]
        [TestCaseSource(typeof(StringArrayTestDataTrue))]
        public void CardCreator_CreateCards_ReturnTrue(string[] cards)
        {
            var result = _cardCreator.CreateCards(cards);
            Assert.IsTrue(result);
        }

        [Test]
        public void CardCreator_CreateCards_ClearCards()
        {
            _cardCreator.ClearCards();
            Assert.AreEqual(0, _cardCreator.Cards.Count);
        }

        [Test]
        public void CardCreator_CreateCards_RetrieveTotalPoints()
        {
            var result = _cardCreator.CreateCards(new string[] { "TC", "TD", "JR", "TH", "TS", "JR" });
            int score = _cardCreator.RetrieveTotalPoints();
            Assert.AreEqual(6, _cardCreator.Cards.Count);
            Assert.AreEqual(400, score);
        }

        private ICardCreator _cardCreator;
    }

    public class StringArrayTestDataFalse : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            yield return new string[] { "AA", "BB", "CC" };
            yield return new string[] { "3", "1B", "5" };
            yield return new string[] { "6", "8", "10" };
            yield return new string[] { "1S" };
            yield return new string[] { "1S", "2S" };
        }
    }

    public class StringArrayTestDataTrue : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            yield return new string[] { "TC", "TD", "JR", "TH", "TS", "JR" };           
            yield return new string[] { "JR", "2C", "JR" };
            yield return new string[] { "JR", "JR" };
            yield return new string[] { "2C", "3C","4C","5C","6C","7C","8C","9C","TC","JC","QC","KC","AC" };
        }
    }
}