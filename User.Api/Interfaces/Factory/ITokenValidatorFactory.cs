using static CommonLib.Constants.AppEnums;

namespace User.Api.Interfaces.Factory
{
    public interface ITokenValidatorFactory
    {
        ITokenValidator CreateValidator(AuthProvider provider);
    }
}
