using AutoMapper;
using Catalog.Api.Interfaces;
using Catalog.Api.Models.CatalogModel.Response;
using Catalog.Api.Models.CategoryModel.Response;
using Catalog.Api.Models.CollectionModel.Response;
using Catalog.Api.Models.ProductModel.Respone;
using Category.Api.Implements;
using Category.Api.Interfaces;
using Comman.Domain.Elly_Catalog;
using Common.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Api.Implements
{
    public class CatalogService : ICatalog
    {
        private readonly IUnitOfWork<Elly_CatalogContext> _unitOfWork;
        private readonly ILogger<CatalogService> _logger;
        private readonly IMapper _mapper;
        private readonly IProduct _product;
        private readonly ICollections _collection;
        private readonly ICategory _category;
        public CatalogService(IUnitOfWork<Elly_CatalogContext> unitOfWork, IProduct product, ICollections collection, ICategory category, ILogger<CatalogService> logger,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _product = product;
            _collection = collection;
            _category = category;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<CatalogDataResponse> GetMainPageContentAsync()
        {
            var productList = _product.GetMainPageProduct();
            var collectionsList = await _collection.GetCollection();
            var categoryList = await _category.GetAllAsync();


            var CatalogContent = new CatalogDataResponse()
            {
                ProductList = productList,
                CollectionsList = collectionsList,
                CategoryList = categoryList,
            };
            return CatalogContent;
        }
    }
}
