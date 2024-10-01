using Comman.Domain.Models;

namespace ShowRoom.Api.Interfaces
{
    public interface IShowRoom
    {
        Task<IEnumerable<Branch>> GetAll();
    }
}
