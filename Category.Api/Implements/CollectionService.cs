using AutoMapper;
using Catalog.Api.Interfaces;
using Category.Api.Implements;
using Comman.Domain.Elly_Catalog;
using Common.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace Catalog.Api.Implements
{
    public class CollectionService : ICollections
    {
        private readonly IUnitOfWork<Elly_CatalogContext> _unitOfWork;
        private readonly ILogger<CollectionService> _logger;
        public CollectionService(IUnitOfWork<Elly_CatalogContext> unitOfWork, ILogger<CollectionService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public async Task<IEnumerable<Collection>> GetCollectionAsync()
        {
            var result = await _unitOfWork.Repository<Collection>().AsNoTracking().ToListAsync();
            return result;
        }

    }
}
