namespace Catalog.Api.Constant.CollectionConstant
{
    public class CollectionConstant
    {
        public const string ValidateTokenFacebookUrl = "https://graph.facebook.com/me?fields=name,email&access_token={0}";
        public const string ValidateTokenGoogleUrl = "https://www.googleapis.com/oauth2/v3/userinfo?access_token={0}";
        public const int pageNumberDefault = 1;
        public const int pageSizeDefault = 10;
        public const string sortByDefault = "Name";
        public const string sortOrderDefault = "asc";
        public const string searchInputDefault = "";
    }
}
