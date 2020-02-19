using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthAPI.Entities
{
    public class LoginAttempt
    {
        public bool IsSuccessful { get; set; }
        public string ErrorMessage { get; set; }
        public string UserName { get; set; }
        public string EmailAddress { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AuthToken { get; set; }
    }
}
