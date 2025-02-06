using System.Globalization;
using System.Text.RegularExpressions;

namespace Barber.Colocho.App.RulesValidations
{
    public class Validations
    {
        public static bool IsPhone(string telefono)
        {
            try
            {

                Regex exreg = new Regex("^\\d{10,10}$");
                return exreg.IsMatch(telefono);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static bool IsEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper, RegexOptions.None, TimeSpan.FromMilliseconds(200));
                string DomainMapper(Match match)
                {
                    var idn = new IdnMapping();
                    string domainName = idn.GetAscii(match.Groups[2].Value);
                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException e)
            {
                return false;
            }
            catch (ArgumentException e)
            {
                return false;
            }
            try
            {
                return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        public static bool IsPassword(string password)
        {
            try
            {
                Regex validateEmailRegex = new Regex("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-.]).{8,}$");
                if (validateEmailRegex.IsMatch(password)) return true;
                else return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static bool IsRFC(string rFC)
        {
            try
            {
                Regex validateEmailRegex = new Regex("^[A-Za-zñÑ&]{3,4}\\d{6}\\w{3}$");
                if (validateEmailRegex.IsMatch(rFC)) return true;
                else return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
