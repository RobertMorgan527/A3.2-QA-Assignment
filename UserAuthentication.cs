namespace ActivityPostCourse
{
    public class UserAuthentication
    {
        public static bool isValidPasswordFormat(string password)
        {
            password = password.Trim();
            return password.Length >= 8 &&
                    Utils.hasOneDigit(password) &&
                    Utils.hasExtraPasswordChars(password);
        }

        public static bool isValidUserNameFormat(string userName)
        {
            //Should include Trim() like password does
            //username = username.Trim();
            return //Should check that length is less than 50
                userName.Length > 1 && // userName.Length < 50 &&
                Utils.isAlphabetic(userName) &&
                !Utils.hasIllegalChars(userName);
        }
    }
}