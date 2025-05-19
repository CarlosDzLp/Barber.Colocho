using System.Diagnostics.CodeAnalysis;

namespace Barber.Colocho.Application.Interface.Response
{
    public class RequestApplication<T>
    {
        public T Request { get; set; }
    }
}
