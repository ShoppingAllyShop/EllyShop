using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Helpers.Interfaces
{
    public interface IApiServices
    {
        Task<TResponse?> GetAsync<TResponse>(string url);

        Task<TResponse?> PostAsync<TRequest, TResponse>(string url, TRequest data);
    }
}
