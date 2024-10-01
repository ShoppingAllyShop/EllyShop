using Comman.Domain.Models;
using Common.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using ShowRoom.Api.Interfaces;

namespace ShowRoom.Api.Implements
{
    public class ShowRoomService : IShowRoom
    {
        private readonly IUnitOfWork<EllyShopContext> _unitOfWork;
        private readonly ILogger<ShowRoomService> _logger;

        public ShowRoomService(IUnitOfWork<EllyShopContext> unitOfWork, ILogger<ShowRoomService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<IEnumerable<Branch>> GetAll()
        {
            var result = await _unitOfWork.Repository<Branch>().AsNoTracking().ToListAsync();
            return result;
        }
    }
}
