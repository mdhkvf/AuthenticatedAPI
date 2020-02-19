using AuthAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthAPI.Services
{
    public interface ILoginService
    {
        public LoginAttempt Login(string username, string password);
    }
}
