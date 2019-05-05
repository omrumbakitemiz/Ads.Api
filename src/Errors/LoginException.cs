using System;

namespace Ads.Api.Errors
{
    public class LoginException : Exception
    {
        public LoginException(string message) : base(message)
        {
        }
    }
}