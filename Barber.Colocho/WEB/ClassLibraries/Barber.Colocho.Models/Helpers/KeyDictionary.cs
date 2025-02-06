namespace Barber.Colocho.Models.Helpers
{
    public class KeyDictionary
    {
#if DEBUG
        public const string URL_BASE_SAVE_FILE = @"C:\User\BarberColocho";
        public const string URL_BASE_IMAGE = @"C:\User\BarberColocho";
#else
            public const string URL_BASE_SAVE_FILE = @"C:\Inetpub\vhosts\barber.rent-app.com.mx\Barber";
            public const string URL_BASE_IMAGE = "https://www.barber.rent-app.com.mx/Barber/";
#endif

        public const string URL_BASE = "https://www.barber.rent-app.com.mx/";
    }
}
