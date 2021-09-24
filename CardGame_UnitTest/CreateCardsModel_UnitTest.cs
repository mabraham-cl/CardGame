using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Moq;
using CardGame.BusinessLogic;
using CardGame.Pages;

namespace CardGame_UnitTest
{
    class CreateCardsModel_UnitTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CreateCardsModel_OnPost_Success()
        {
            Mock<ICardValidator> mockCardValidator = new Mock<ICardValidator>();
            mockCardValidator.Setup(x => x.ValidateInput(new string[] { "TC"})).Returns(true);

            Mock<ICardCreator> mockCardCreator = new Mock<ICardCreator>();
            mockCardCreator.Setup(x=>x.CreateCards(new string[] { "TC" })).Returns(true);
            mockCardCreator.Setup(x=>x.RetrieveTotalPoints()).Returns(2);

            mockCardCreator.Setup(x => x.Cards).Returns(new List<Card>() { new Card() { DisplayName = "TC", Suit = Suit.C, Value = 1 } });

            CreateCardsModel model = new CreateCardsModel(mockCardCreator.Object, mockCardValidator.Object);

            model.Cards = "TC";

            model.OnPost();

            Assert.AreEqual(2, model.TotalPoints);
        }

        [Test]
        public void CreateCardsModel_OnPost_ValidationFailed()
        {
            Mock<ICardValidator> mockCardValidator = new Mock<ICardValidator>();
            mockCardValidator.Setup(x => x.ValidateInput(new string[] { "TC" })).Returns(false);
            mockCardValidator.Setup(x => x.ErrorMessage).Returns("Invalid input string.");

            Mock<ICardCreator> mockCardCreator = new Mock<ICardCreator>();
            CreateCardsModel model = new CreateCardsModel(mockCardCreator.Object, mockCardValidator.Object);

            model.Cards = "T%C";

            model.OnPost();

            Assert.AreEqual("Invalid input string.", model.ErrorMessage);
        }

        [Test]
        public void CreateCardsModel_OnPost_InputEmpty()
        {
            Mock<ICardValidator> mockCardValidator = new Mock<ICardValidator>();

            Mock<ICardCreator> mockCardCreator = new Mock<ICardCreator>();
            CreateCardsModel model = new CreateCardsModel(mockCardCreator.Object, mockCardValidator.Object);

            model.Cards = " ";

            model.OnPost();

            Assert.AreEqual("Please enter the cards.", model.ErrorMessage);

            model.Cards = "";

            model.OnPost();

            Assert.AreEqual("Please enter the cards.", model.ErrorMessage);
        }

        [Test]
        public void CreateCardsModel_OnPost_Exception()
        {
            Mock<ICardValidator> mockCardValidator = new Mock<ICardValidator>();
            mockCardValidator.Setup(x => x.ValidateInput(new string[] { "TC" })).Throws(new Exception());

            Mock<ICardCreator> mockCardCreator = new Mock<ICardCreator>();
            CreateCardsModel model = new CreateCardsModel(mockCardCreator.Object, mockCardValidator.Object);

            model.Cards = "TC";
            model.OnPost();

            Assert.That(!string.IsNullOrEmpty(model.ErrorMessage));
        }

        [Test]
        public void CreateCardsModel_OnPost_CreateCard_Failed()
        {
            Mock<ICardValidator> mockCardValidator = new Mock<ICardValidator>();
            mockCardValidator.Setup(x => x.ValidateInput(new string[] { "1C" })).Returns(true);

            Mock<ICardCreator> mockCardCreator = new Mock<ICardCreator>();
            mockCardCreator.Setup(x => x.CreateCards(new string[] { "1C" })).Returns(false);
            mockCardCreator.Setup(x => x.RetrieveTotalPoints()).Returns(0);           

            CreateCardsModel model = new CreateCardsModel(mockCardCreator.Object, mockCardValidator.Object);

            model.Cards = "1C";

            model.OnPost();

            Assert.AreEqual("Card not recognised.", model.ErrorMessage);
        }
    }
}
