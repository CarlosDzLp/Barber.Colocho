namespace Barber.Colocho.Infraestructure.Repository.Response
{
    public class ResponseInfraestructure<T>
    {
        public T Result { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
