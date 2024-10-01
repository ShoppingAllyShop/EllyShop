using AutoMapper;
using Category.Api.Models.Request;
using Comman.Domain.Models;
using CategoryEntity = Comman.Domain.Models.Category;

namespace Category.Api.Mapper
{
    public class CategoryMappingProfile: Profile
    {
        public CategoryMappingProfile() 
        {
            CreateMap<CategoryRequest, CategoryEntity>();
        }
    }
}
