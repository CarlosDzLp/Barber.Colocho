namespace Barber.Colocho.Application.Interface.Response
{
    public class ResponseApplication<T> where T : class
    {
        public T? Result { get; set; }
        public int Count { get; set; }
        public string? Message { get; set; }
    }
}
