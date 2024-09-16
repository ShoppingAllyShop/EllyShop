namespace User.Api.Constant
{
    public static class UserConstant
    {
        public const string ValidateTokenFacebookUrl = "https://graph.facebook.com/debug_token?input_token={0}&access_token={1}\r\n";
        public const string ValidateTokenGoogleUrl = "https://www.googleapis.com/oauth2/v3/userinfo?access_token={0}";
    }
}
