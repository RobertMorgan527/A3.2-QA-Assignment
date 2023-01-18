using System.Text.RegularExpressions;

namespace ActivityPostCourse
{
    public class Address
    {
        public string number { get; set; }
        public string addressLine { get; set; }
        public string postCode { get; set; }
        public string city { get; set; }

        public bool isValidUKPostCode()
        {
            string regXString = @"^([A-Z]{1,2})([0-9][0-9A-Z]?) ([0-9])([ABDEFGHJLNPQRSTUWXYZ]{2})$";
            Regex r = new Regex(regXString);
            return r.IsMatch(postCode.Trim());
        }
        public bool isValidHouseNumber()
        {
            string regXString = @"^[1-9]\d*(?:[-\s]?\w+)?$";
            Regex r = new Regex(regXString);
            return r.IsMatch(number.Trim());
            //The Regex here is incorrect allowing multiple trailing letters instead of just 1
            //It should also not allow numbers over 1000
            //A correct regex would be: ^[1-9]\d{0,2}[a-zA-Z]?$

        }
        public bool isValidCity(IUserAccount userAccountDb)
        {
            return userAccountDb.getCityNames().Contains(city);
        }
    }
}