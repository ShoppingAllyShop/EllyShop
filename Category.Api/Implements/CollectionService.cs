using AutoMapper;
using Catalog.Api.Constant.CollectionConstant;
using Catalog.Api.Interfaces;
using Catalog.Api.Models.CollectionModel.Request;
using Catalog.Api.Models.CollectionModel.Response;
using Category.Api.Implements;
using Comman.Domain.Elly_Catalog;
using Comman.Domain.Elly_User;
using Common.Infrastructure.Interfaces;
using CommonLib.Constants;
using CommonLib.Exceptions;
using CommonLib.Models.Base;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Xml.Linq;

namespace Catalog.Api.Implements
{
    public class CollectionService : ICollections
    {
        private readonly IUnitOfWork<Elly_CatalogContext> _unitOfWork;
        private readonly ILogger<CollectionService> _logger;
        private readonly IMapper _mapper;
        public CollectionService(IUnitOfWork<Elly_CatalogContext> unitOfWork, ILogger<CollectionService> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<string> AddCollectionAsync(CollectionRequest request )
        {
            var isExistCate = await _unitOfWork.Repository<Collection>()
                .AsNoTracking()
                .AnyAsync(x => x.Name == request.Name);
            if (isExistCate) throw new BusinessException("Bộ sưu tập đã tồn tại");

            var newCollection = new Collection()
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Description = request.Description
            };
            await _unitOfWork.Repository<Collection>().AddAsync(newCollection);
            var saveResult = await _unitOfWork.SaveChangesAsync();

            return newCollection?.Name;
        }

        public async Task<DeleteCollectionReponse> DeleteCollectionAsync(DeleteCollectionRequest model)
        {
            var collection = await _unitOfWork.Repository<Collection>()
               .AsNoTracking()
               .FirstOrDefaultAsync(x => x.Id == model.Id);
            if (collection == null) throw new BusinessException("bộ sưu tập này không tồn tại");

            _unitOfWork.Repository<Collection>().Remove(collection);
            await _unitOfWork.SaveChangesAsync();

            var pagingCollectionList = await SearchCollectionAsync(model.PageNumber, model.PageSize, model.SortBy, model.SortOrder, model.SearchInput);

            var result = new DeleteCollectionReponse
            {
                PagingCollectionList = pagingCollectionList,
            };

            _logger.LogInformation($"Remove category successfully.Id: {collection.Id}");
            return result;

           
        }

        public async Task<Collection> EditCollectionAsync(CollectionRequest request)
        {
            var collection = await _unitOfWork.Repository<Collection>()
              .AsNoTracking()
              .FirstOrDefaultAsync(x => x.Id == request.Id);
            if (collection == null) throw new BusinessException("Bộ sưu tập này không tồn tại");

            var updatedCategory = _mapper.Map<Collection>(request);
            _unitOfWork.UpdateEntity(updatedCategory);
            await _unitOfWork.SaveChangesAsync();
            return updatedCategory;
        }

        public async Task<IEnumerable<Collection>> GetCollectionAsync()
        {
            var result = await _unitOfWork.Repository<Collection>().AsNoTracking().ToListAsync();
            return result;
        }

        public async Task<DataCollectionResponse> GetDataAdminCollectionPageAsync()
        {
            //var positions = _unitOfWork.Repository<Position>().AsNoTracking().AsEnumerable();
            //var departments = _unitOfWork.Repository<Department>().AsNoTracking().AsEnumerable();
            //var roles = _unitOfWork.Repository<Roles>().AsNoTracking().AsEnumerable();
            var data = await SearchCollectionAsync();
            var result = new DataCollectionResponse
            {
                //ContentPageData = new ContentPageData
                //{
                //    Roles = roles,
                //    Departments = departments,
                //    Positions = positions,
                //},
                CollectionData = data
            };

            return result;
        }

        public async Task<SearchCollectionResponse> SearchCollectionAsync(int? pageNumber = null, int? pageSize = null, string? sortBy = null,string? sortOrder = null, string? searchInput = null)
        {
            int pageNumberValue = pageNumber ?? CollectionConstant.pageNumberDefault;
            int pageSizeValue = pageSize ?? CollectionConstant.pageSizeDefault;
            string sortByValue = sortBy ?? CollectionConstant.sortByDefault;
            string sortOrderValue = sortOrder ?? CollectionConstant.sortOrderDefault;
            string searchInputValue = searchInput ?? CollectionConstant.searchInputDefault;

            var query = _unitOfWork.Repository<Collection>().AsNoTracking()
                .Select(x => new Collectioninfo
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                }).AsQueryable();

            if (!string.IsNullOrEmpty(searchInputValue)) query = query.Where(x => x.Name.Contains(searchInputValue) || x.Description.Contains(searchInputValue));
            query = sortOrderValue.ToLower() == ConmonConstants.DescendingShortcut ? query.OrderByDescending(e => EF.Property<object>(e, sortByValue)) : query.OrderBy(e => EF.Property<object>(e, sortByValue));
            var totalRecords = await query.CountAsync();
            var collectioninfoList = query.Skip((pageNumberValue - 1) * pageSizeValue).Take(pageSizeValue);

            var result = new SearchCollectionResponse
            {
                CollectionList = collectioninfoList,
                Paging = new PagingResponseBase
                {
                    PageNumber = pageNumberValue,
                    PageSize = pageSizeValue,
                    TotalItems = totalRecords,
                    SortBy = sortByValue,
                    SortOrder = sortOrderValue
                }
            };
            return result;
        }
    }
}
