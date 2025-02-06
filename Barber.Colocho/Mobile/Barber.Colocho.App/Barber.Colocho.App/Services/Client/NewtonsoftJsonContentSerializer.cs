using Newtonsoft.Json;
using Refit;
using System.Reflection;
using System.Text;

namespace Barber.Colocho.App.Services.Client
{
    public class NewtonsoftJsonContentSerializer : IHttpContentSerializer
    {
        /// <summary>
        /// The <see cref="Lazy{T}"/> instance providing the JSON serialization settings to use
        /// </summary>
        readonly Lazy<JsonSerializerSettings> jsonSerializerSettings;

        /// <summary>
        /// Creates a new <see cref="NewtonsoftJsonContentSerializer"/> instance
        /// </summary>
        public NewtonsoftJsonContentSerializer() : this(null) { }

        /// <summary>
        /// Creates a new <see cref="NewtonsoftJsonContentSerializer"/> instance with the specified parameters
        /// </summary>
        /// <param name="jsonSerializerSettings">The serialization settings to use for the current instance</param>
        public NewtonsoftJsonContentSerializer(JsonSerializerSettings jsonSerializerSettings)
        {
            this.jsonSerializerSettings = new Lazy<JsonSerializerSettings>(() => jsonSerializerSettings
                                                                                 ?? JsonConvert.DefaultSettings?.Invoke()
                                                                                 ?? new JsonSerializerSettings());
        }

        /// <inheritdoc/>
        public HttpContent ToHttpContent<T>(T item)
        {
            var content = new StringContent(JsonConvert.SerializeObject(item, jsonSerializerSettings.Value), Encoding.UTF8, "application/json");

            return content;
        }

        /// <inheritdoc/>
        public async Task<T> FromHttpContentAsync<T>(HttpContent content, CancellationToken cancellationToken = default)
        {
            var serializer = JsonSerializer.Create(jsonSerializerSettings.Value);

            var stream = await content.ReadAsStreamAsync().ConfigureAwait(false);
            var reader = new StreamReader(stream);
            var jsonTextReader = new JsonTextReader(reader);

            return serializer.Deserialize<T>(jsonTextReader);
        }

        public string GetFieldNameForProperty(PropertyInfo propertyInfo)
        {
            if (propertyInfo is null)
                throw new ArgumentNullException(nameof(propertyInfo));

            return propertyInfo.GetCustomAttributes<JsonPropertyAttribute>(true)
                              .Select(a => a.PropertyName)
                              .FirstOrDefault();
        }
    }
}
