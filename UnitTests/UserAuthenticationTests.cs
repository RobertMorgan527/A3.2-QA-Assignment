using ActivityPostCourse;
using Moq;

namespace UnitTests
{
    public class UserAuthenticationTests
    {
        [Test]
        public void isValidPasswordTestForValid()
        {
            Assert.True(UserAuthentication.isValidPasswordFormat("Password1$"));
            //Only @ $ £ are valid symbols, all symbols should be valid
            //Using other symbols such as !%^&*() will cause the method to return false
        }

        [Test]
        public void isValidPasswordTestForTrimWhiteSpace()
        {
            Assert.True(UserAuthentication.isValidPasswordFormat("    Password1$   "));
        }

        [Test]
        public void isValidPasswordTestForLessThan8Chars()
        {
            Assert.False(UserAuthentication.isValidPasswordFormat("Pass1$"));
        }

        [Test]
        public void isValidPasswordTestForNoDigit()
        {
            Assert.False(UserAuthentication.isValidPasswordFormat("Password$"));
        }

        [Test]
        public void isValidPasswordTestForNoSpecialChar()
        {
            Assert.False(UserAuthentication.isValidPasswordFormat("Password1"));
        }

        [Test]
        public void isValidUserNameFormatTestForValid()
        {
            Assert.True(UserAuthentication.isValidUserNameFormat("Username"));
        }

        [Test]
        public void isValidUserNameFormatTestForTrimWhiteSpace()
        {
            Assert.True(UserAuthentication.isValidUserNameFormat("    Username    "));
            //isValidPasswordFormat uses.Trim() to remove whitespace
            //isValidUserNameFormat should do the same but currently doesnt
        }

        [Test]
        public void isValidUserNameFormatTestForLessThan2Char()
        {
            Assert.False(UserAuthentication.isValidUserNameFormat("a"));
        }

        [Test]
        public void isValidUserNameFormatTestForMoreThan50Char()
        {
            Assert.False(UserAuthentication.isValidUserNameFormat(
                "aaaaaaaaaa" + //10 chars
                "aaaaaaaaaa" + //20 chars
                "aaaaaaaaaa" + //30 chars
                "aaaaaaaaaa" + //40 chars
                "aaaaaaaaaa" + //50 chars
                "aaaaaaaaaa"   //60 chars
           ));
            //Method is meant to return false for usernames over 50 characters
            //But validation for this doesnt currently exist
        }

        [Test]
        public void isValidUserNameFormatTestForNonAlphabetic()
        {
            Assert.False(UserAuthentication.isValidUserNameFormat("Username1"));
        }
    }
}