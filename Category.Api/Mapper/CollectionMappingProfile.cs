using AutoMapper;
using Catalog.Api.Models.CollectionModel.Request;
using Comman.Domain.Elly_Catalog;
namespace Catalog.Api.Mapper
{
    public class CollectionMappingProfile: Profile
    {
        public CollectionMappingProfile()
        {
            CreateMap<CollectionRequest, Collection>();
        }
    }
}
