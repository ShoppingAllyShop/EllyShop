using System.Collections;
using System.Threading.Tasks;
using Comman.Domain.Models;
using ContentManagement.API.Models.Responses;

namespace ContentManagement.API.Interfaces
{
    public interface IContentManagement
    {
        MainPageResponse GetContentMainPage();
    }
}
