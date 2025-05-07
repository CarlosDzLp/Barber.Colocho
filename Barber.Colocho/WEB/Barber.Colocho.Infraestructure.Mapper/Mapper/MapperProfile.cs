using AutoMapper;

namespace Barber.Colocho.Transversal.Mapper.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            //AutoRegisterMappings();
            //CreateMap<Mediical.Tepeyac.Infrastructure.Data.Tables.User, Mediical.Tepeyac.Domain.Entity.User.Login.UserDomainDto>().ReverseMap();
        }

        private void AutoRegisterMappings()
        {
            // Carga los ensamblados de entidades y DTOs
            //var infraEstructureAssembly = typeof(Barber.Colocho.Infraestructure.Data.Tables.User).Assembly;
            //var domainAssembly = typeof(Barber.Colocho.Domain.Entity.User.Login.UserDomainDto).Assembly;

            //var entityTypes = infraEstructureAssembly.GetExportedTypes()
            //.Where(t => t.IsClass && !t.IsAbstract).ToList();

            //var dtoTypes = domainAssembly.GetExportedTypes()
            //    .Where(t => t.IsClass && t.Name.EndsWith("Dto")).ToList();

            //foreach (var dtoType in dtoTypes)
            //{
            //    var entityName = dtoType.Name.Replace("Dto", "");
            //    var entityType = entityTypes.FirstOrDefault(t => t.Name == entityName);

            //    if (entityType != null)
            //    {
            //        CreateMap(entityType, dtoType).ReverseMap();
            //    }
            //}
        }
    }
}
