using AuthAPI.Payloads;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthAPI.Services
{
    public interface IRegistrationService
    {
        public DataTransfer.User Register(RegistrationPost registrationData);
    }
}
