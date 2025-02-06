using Maui.GoogleMaps;
using System.Reflection;

namespace Barber.Colocho.App.Services.GoogleMpas
{
    public static class StyleMap
    {
        public static MapStyle Style()
        {
            try
            {
                var assembly = typeof(StyleMap).GetTypeInfo().Assembly;
                var stream = assembly.GetManifestResourceStream("Barber.Colocho.App.Style.MapStyle.json");
                string styleFile;
                using (var reader = new System.IO.StreamReader(stream))
                {
                    styleFile = reader.ReadToEnd();
                }

                return MapStyle.FromJson(styleFile);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al cargar el estilo del mapa: {ex.Message}");
                return null;
            }
        }
    }
}
