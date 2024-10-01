using Comman.Domain.Models;
using Common.Infrastructure.Interfaces;
using ContentMainPage.Api.Interfaces;
using ContentManagement.API.Models.Responses;
using Microsoft.EntityFrameworkCore;

namespace ContentMainPage.Api.Implements
{
    public class ContentMainPageService : IContentMainPage
    {
        private readonly IUnitOfWork<EllyShopContext> _unitOfWork;
        private readonly ILogger<ContentMainPageService> _logger;

        public ContentMainPageService(IUnitOfWork<EllyShopContext> unitOfWork, ILogger<ContentMainPageService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public async Task<IEnumerable<Branch>> GetAll()
        {
            var result = await _unitOfWork.Repository<Branch>().AsNoTracking().ToListAsync();
            return result;
        }
        public MainPageResponse GetContentMainPage()
        {
            var newsList = _unitOfWork.Repository<News>().AsNoTracking().AsEnumerable();
            var newsMediaList = _unitOfWork.Repository<NewsMedia>().AsNoTracking().AsEnumerable();
            var branchList = _unitOfWork.Repository<Branch>().AsNoTracking().AsEnumerable();
            var prizeList = _unitOfWork.Repository<Prize>().AsNoTracking().AsEnumerable();
            return new MainPageResponse()
            {
                NewsList = newsList,
                NewsMediaList = newsMediaList,
                BranchList = branchList,
                PrizeList = prizeList,

            };
        }
    }
}
