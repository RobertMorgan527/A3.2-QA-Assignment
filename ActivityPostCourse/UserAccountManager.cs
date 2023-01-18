using ActivityPostCourse;

public interface IUserAccount
{
    bool register(string userName, string password, Address address);
    bool isRegisteredUser(string userName, string password);
    List<String> getCityNames();
    bool isAnExistingUser(string userName);
}

public class UserAccountManager
{
    int noOfTries = 0;
    IUserAccount userRegistrationDb;

    public UserAccountManager(IUserAccount userRegistrationDb)
    {
        this.userRegistrationDb = userRegistrationDb;
    }

    public bool login(String userName, String password)
    {
        if (userRegistrationDb.isRegisteredUser(userName, password))
        {
            return true;
        }
        else
        {
            if (++noOfTries > 3)
            {
                throw new TooManyLoginTriesException();
            }
            throw new LoginException("Not a valid username or password");
        }
    }

    public bool register(String userName, String password, Address address)
    {
        if (userRegistrationDb.isAnExistingUser(userName))
        {
            throw new LoginException("User name already exists");
        }
        if (!UserAuthentication.isValidUserNameFormat(userName))
        {
            throw new LoginException("User name format is not valid");
        }
        if (!UserAuthentication.isValidPasswordFormat(password))
        {
            throw new LoginException("Password not in the correct format");
        }

        if (!address.isValidHouseNumber())
        {
            throw new InvalidUKAddressException("Not a valid house number");
        }
        if (!address.isValidUKPostCode())
        {
            throw new InvalidUKAddressException("Invalid UK post code");
        }

        if (!address.isValidCity(this.userRegistrationDb))
        {
            throw new InvalidUKAddressException("Not a valid UK city name or we do not operate in this city");
        }

        userRegistrationDb.register(userName, password, address);
        return true;
    }
}
