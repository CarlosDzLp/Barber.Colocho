using Barber.Colocho.Infraestructure.Repository.Response;

namespace Barber.Colocho.Infraestructure.Repository.Version
{
    public interface IVersionInfraestructure
    {
        Task<ResponseInfraestructure<Data.Tables.Version>> LastVersion();
    }
}
