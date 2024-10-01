using Comman.Domain.Models;
using ContentManagement.API.Models.Responses;

namespace ContentMainPage.Api.Interfaces
{
    public interface IContentMainPage
    {
        Task<IEnumerable<Branch>> GetAll();
        MainPageResponse GetContentMainPage();
    }
}
