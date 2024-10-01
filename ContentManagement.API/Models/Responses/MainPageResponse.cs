using Comman.Domain.Models;
using System.Collections;

namespace ContentManagement.API.Models.Responses
{
    public class MainPageResponse
    {
        public LayoutData? LayoutData { get; set; }
        public MainPageData? MainPageData { get; set; }
    }
    public class LayoutData
    {
        public HeaderData? HeaderData { get; set; }
        public FooterData? FooterData { get; set; }
    }
    public class HeaderData
    {
        public IEnumerable<Navigation>? Navigation { get; set; }
    }
    public class FooterData
    {
        public IEnumerable<SocialMedia>? SocialMedias { get; set; }
        public IEnumerable<Policy>? Policies { get; set; }
        public IEnumerable<GeneralInfomation>? GeneralInfomations { get; set; }
    }
    public class MainPageData
    {
        public IEnumerable<News>? NewsList { get; set; }
        public IEnumerable<NewsMedia>? NewsMediaList { get; set; }
        public IEnumerable<Branch>? BranchList { get; set; }
        public IEnumerable<Prize>? PrizeList { get; set; }
    }
}
