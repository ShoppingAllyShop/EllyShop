using Comman.Domain.Models;
using System.Collections;

namespace ContentManagement.API.Models.Responses
{
    public class MainPageResponse
    {
        public IEnumerable<News>? NewsList { get; set; }
        public IEnumerable<NewsMedia>? NewsMediaList { get; set; }
        public IEnumerable<Branch>? BranchList { get; set; }
        public IEnumerable<Prize>? PrizeList { get; set; }

    }
}
