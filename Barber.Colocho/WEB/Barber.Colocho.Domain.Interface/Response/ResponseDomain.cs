namespace Barber.Colocho.Domain.Interface.Response
{
    public class ResponseDomain<T>
    {
        public T Result { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
