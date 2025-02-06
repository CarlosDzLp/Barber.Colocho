namespace Barber.Colocho.Core.Helpers
{
    public class Response<T>
    {
        public T Result { get; set; }
        public string Message { get; set; }
        public int Count { get; set; }
    }
}
