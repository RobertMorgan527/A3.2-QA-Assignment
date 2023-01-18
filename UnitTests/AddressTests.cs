using ActivityPostCourse;
using Moq;

namespace UnitTests
{
    public class AddressTests
    {
        private IUserAccount userAccountDbMock;

        [SetUp]
        public void Setup()
        {
            Mock<IUserAccount> mockDb = new Mock<IUserAccount>();
            mockDb.Setup(x => x.getCityNames()).Returns(new List<string>
            {
                "London","Manchester","Birmingham","Leeds","Southampton",
                "Liverpool","Newcastle"
            });
            this.userAccountDbMock = mockDb.Object;
        }

        [Test]
        public void isValidUKPostCodeTestForValid()
        {
            var validAddress = new Address()
            {
                postCode = "AB12 1AB"
            };
            Assert.True(validAddress.isValidUKPostCode());
        }

        [Test]
        public void isValidUKPostCodeTestForInvalid()
        {
            var invalidAddress = new Address()
            {
                postCode = "Not a Postcode"
            };
            Assert.False(invalidAddress.isValidUKPostCode());
        }

        [Test]
        public void isValidHouseNumberTestForValid()
        {
            var validAddress = new Address()
            {
                number = "1"
            };
            Assert.True(validAddress.isValidHouseNumber());
        }

        //No Validation exists for address line meaning anything
        //can be used, or it can be excluded entirely which is a problem

        [Test]
        public void isValidHouseNumberTestForMultipleTrailingLetters()
        {
            var invalidAddress = new Address()
            {
                number = "123abcdef"
            };
            Assert.False(invalidAddress.isValidHouseNumber());
            //Regex is incorrect for house number validation
            //allowing for multiple trailing letters when it
            //should only allow 1
            //Correct regex: ^[1-9]\d{0,2}[a-zA-Z]?$
        }

        [Test]
        public void isValidHouseNumberTestForNumberOver1000()
        {
            var invalidAddress = new Address()
            {
                number = "12345a"
            };
            Assert.False(invalidAddress.isValidHouseNumber());
            //Validation is meant to return false for house
            //numbers over 1000
            //Correct regex: ^[1-9]\d{0,2}[a-zA-Z]?$
        }

        [Test]
        public void isValidCityTestForValid()
        {
            var validAddress = new Address()
            {
                city = "London"
            };
            Assert.True(validAddress.isValidCity(userAccountDbMock));
        }

        [Test]
        public void isValidCityTestForInvalid()
        {
            var validAddress = new Address()
            {
                city = "NotACity"
            };
            Assert.False(validAddress.isValidCity(userAccountDbMock));
        }

    }
}