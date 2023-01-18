using ActivityPostCourse;
using Moq;

namespace UnitTests
{
    public class UserAccountManagerTests
    {
        private UserAccountManager uam;
        private readonly Address validAddress = new Address
        {
            number = "1",
            //No Validation for address line which is a problem
            postCode = "AB12 1AB",
            city = "London"
        };

        [SetUp]
        public void Setup()
        {
            Mock<IUserAccount> mockDb = new();
            mockDb.Setup(x => 
                x.register(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Address>()))
                .Returns(true);
            mockDb.Setup(x => x.isRegisteredUser("registered", It.IsAny<string>()))
                .Returns(true);
            mockDb.Setup(x => x.isRegisteredUser("notregistered", It.IsAny<string>()))
                .Returns(false);
            mockDb.Setup(x => x.getCityNames()).Returns(new List<string>
            {
                "London","Manchester","Birmingham","Leeds","Southampton",
                "Liverpool","Newcastle"
            });
            mockDb.Setup(x => x.isAnExistingUser("existing"))
                .Returns(true);
            mockDb.Setup(x => x.isAnExistingUser("notexisting"))
                .Returns(false);

            this.uam = new UserAccountManager(mockDb.Object);
        }

        [Test]
        public void loginTestForRegisteredUser()
        {
            Assert.True(uam.login("registered","Password1$"));
        }

        [Test]
        public void loginTestForNonRegisteredUser()
        {
            var ex = Assert.Throws<LoginException>(() =>
                uam.login("notregistered", "Password1$"));
            Assert.That(ex.Message, Is.EqualTo("Not a valid username or password"));
        }

        [Test]
        public void loginTestForTooManyAttempts()
        {
            for (int i = 0; i < 3; i++)
            {
                try
                {
                    uam.login("notregistered", "Password1$");
                }
                catch (LoginException) { }
            }

            var ex = Assert.Throws<TooManyLoginTriesException>(() => 
            uam.login("notregistered", "Password1$"));
            Assert.That(ex.Message, Is.EqualTo("Login failed more than 3 times"));
        }

        [Test]
        public void registerTestForSuccessful()
        {
            Assert.True(uam.register("notexisting", "Password1$", validAddress));
        }

        [Test]
        public void registerTestForExistingUser()
        {
            var ex = Assert.Throws<LoginException>(() =>
            uam.register("existing", "Password1$", validAddress));
            Assert.That(ex.Message, Is.EqualTo("User name already exists"));
        }

        [Test]
        public void registerTestForInvalidUsernameFormat()
        {
            var ex = Assert.Throws<LoginException>(() =>
            uam.register("existing1!", "Password1$", validAddress));
            Assert.That(ex.Message, Is.EqualTo("User name format is not valid"));
        }

        [Test]
        public void registerTestForInvalidPasswordFormat()
        {
            var ex = Assert.Throws<LoginException>(() =>
            uam.register("notexisting", "P", validAddress));
            Assert.That(ex.Message, Is.EqualTo("Password not in the correct format"));
        }

        [Test]
        public void registerTestForInvalidHouseNumber()
        {
            var invalidHouseNumberAddress = validAddress;
            invalidHouseNumberAddress.number = "0";
            var ex = Assert.Throws<InvalidUKAddressException>(() =>
            uam.register("notexisting", "Password1$", invalidHouseNumberAddress));
            Assert.That(ex.Message, Is.EqualTo("Not a valid house number"));
        }

        [Test]
        public void registerTestForInvalidUKPostcode()
        {
            var invalidPostcodeAddress = validAddress;
            invalidPostcodeAddress.postCode = "Invalid";
            var ex = Assert.Throws<InvalidUKAddressException>(() =>
            uam.register("notexisting", "Password1$", invalidPostcodeAddress));
            Assert.That(ex.Message, Is.EqualTo("Invalid UK post code"));
        }

        [Test]
        public void registerTestForInvalidCity()
        {
            var invalidCityAddress = validAddress;
            invalidCityAddress.city = "fakeCity";
            var ex = Assert.Throws<InvalidUKAddressException>(() =>
            uam.register("notexisting", "Password1$", invalidCityAddress));
            Assert.That(ex.Message, Is.EqualTo("Not a valid UK city name or we do not operate in this city"));
        }
    }
}
