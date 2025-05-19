using System.Resources;

namespace Barber.Colocho.Transversal.Resources
{
    public class BarberResources
    {
        readonly ResourceManager resource;
        public BarberResources()
        {
            resource = BarberColocho.ResourceManager;
            var culture = Thread.CurrentThread.CurrentUICulture;
            string label = resource?.GetString("LABEL_USER_EMPTY", culture);
        }

        public string GetString(string key)
        {
            var culture = Thread.CurrentThread.CurrentUICulture;
            string label = resource?.GetString(key, culture);
            return label!;
        }
    }
}
