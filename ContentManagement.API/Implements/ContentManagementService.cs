using Comman.Domain.Elly_ContentManagement;
using Common.Infrastructure.Interfaces;
using ContentManagement.API.Interfaces;
using ContentManagement.API.Models.Responses;
using Microsoft.EntityFrameworkCore;

namespace ContentManagement.API.Implements
{
    public class ContentManagementService : IContentManagement
    {
        private readonly IUnitOfWork<Elly_ContentManagementContext> _unitOfWork;
        private readonly ILogger<ContentManagementService> _logger;

        public ContentManagementService(IUnitOfWork<Elly_ContentManagementContext> unitOfWork, ILogger<ContentManagementService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public MainPageResponse GetContentMainPage()
        {
            var layoutContent = GetLayOut();
            var mainPageContent = GetMainPageData();

            return new MainPageResponse()
            {
                LayoutData = layoutContent,
                MainPageData = mainPageContent
            };
        }

        private MainPageData GetMainPageData()
        {
            var newsList = _unitOfWork.Repository<News>().AsNoTracking().AsEnumerable();
            var newsMediaList = _unitOfWork.Repository<NewsMedia>().AsNoTracking().AsEnumerable();
            var branchList = _unitOfWork.Repository<Branch>().AsNoTracking().AsEnumerable();
            var prizeList = _unitOfWork.Repository<Prize>().AsNoTracking().AsEnumerable();
            var silderList = _unitOfWork.Repository<Silde>().AsNoTracking().AsEnumerable();
           

            var mainPageContent = new MainPageData()
            {
                NewsList = newsList,
                NewsMediaList = newsMediaList,
                BranchList = branchList,
                PrizeList = prizeList,
                SilderList = silderList,
               
            };
            return mainPageContent;
        }

        private LayoutData GetLayOut()
        {
            var header = GetHeaderData();
            var footer = GetFooterData();

            return new LayoutData()
            {
                FooterData = footer,
                HeaderData = header,
            };
        }

        private FooterData GetFooterData()
        {
            var socialMediasList = _unitOfWork.Repository<SocialMedia>().AsNoTracking().AsEnumerable();
            var policyList = _unitOfWork.Repository<Policy>().AsNoTracking().AsEnumerable();
            var generalInfomationList = _unitOfWork.Repository<GeneralInfomation>().AsNoTracking().AsEnumerable();
            var footer = new FooterData()
            {
                GeneralInfomations = generalInfomationList,
                Policies = policyList,
                SocialMedias = socialMediasList,
            };
            return footer;
        }

        private HeaderData GetHeaderData()
        {
            var navigationList = _unitOfWork.Repository<Navigation>().AsNoTracking().AsEnumerable();
            return new HeaderData()
            {
                Navigation = navigationList,
            };
        }
    }
}
