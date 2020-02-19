using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthAPI.Services
{
    public interface IAuthenticationService
    {
        public string GetToken(int userId);
    }
}
