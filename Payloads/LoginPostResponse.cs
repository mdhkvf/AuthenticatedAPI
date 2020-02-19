using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthAPI.Payloads
{
    public class LoginPostResponse
    {
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
        public LoginPostResponseBody Body { get; set; }
    }

    public class LoginPostResponseBody
    {
        public string AuthToken { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string EmailAddress { get; set; }
    }
}
