using CommonLib.Helpers.Implements;
using CommonLib.Helpers.Interfaces;
using User.Api.Implements.TokenValidator;

namespace User.Api.UnitTest
{
    public static class UnitTestUtils
    {
        public static ServiceProvider CreateServiceProvider()
        {
            var serviceCollection = new ServiceCollection();

            //Add other services
            serviceCollection.AddHttpClient();
            serviceCollection.AddTransient<IApiServices, ApiServices>();

            // Đăng ký các validator vào ServiceCollection
            serviceCollection.AddTransient<GoogleTokenValidator>();
            serviceCollection.AddTransient<FacebookTokenValidator>();

            // Tạo ServiceProvider từ ServiceCollection
            var result = serviceCollection.BuildServiceProvider();
            return result;
        }
    }
}
