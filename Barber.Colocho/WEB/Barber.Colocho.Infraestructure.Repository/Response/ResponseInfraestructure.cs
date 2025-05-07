namespace Barber.Colocho.Infraestructure.Repository.Response
{
    public class ResponseInfraestructure<T> where T : class
    {
        public T? Result { get; set; }
        public int Count { get; set; }
        public string? Message { get; set; }
    }
}
