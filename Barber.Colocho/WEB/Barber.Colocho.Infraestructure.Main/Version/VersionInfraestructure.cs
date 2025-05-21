using Barber.Colocho.Infraestructure.Repository.Repository;
using Barber.Colocho.Infraestructure.Repository.Response;
using Barber.Colocho.Infraestructure.Repository.Version;

namespace Barber.Colocho.Infraestructure.Main.Version
{
    public class VersionInfraestructure : IVersionInfraestructure
    {

        #region Constructor
        private readonly IGenericRepository<Data.Tables.Version> version;
        public VersionInfraestructure(IGenericRepository<Data.Tables.Version> version)
        {
            this.version = version;
        }
        #endregion

        public async Task<ResponseInfraestructure<Data.Tables.Version>> LastVersion()
        {
            var result = await version.FindAsync(c => c.IsActive);
            return new ResponseInfraestructure<Data.Tables.Version>
            {
                Count = (result != null) ? 1 : 0,
                Message = null,
                Result = result
            };
        }
    }
}
