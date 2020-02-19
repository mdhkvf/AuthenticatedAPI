using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthAPI.Database;
using AuthAPI.Payloads;
using AuthAPI.Models;
using AuthAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace AuthAPI.Controllers
{
    [Route("api/registration")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly IRegistrationService _registrationService;
        private readonly IAuthenticationService _authService;

        public RegistrationController(IRegistrationService registrationService, IAuthenticationService authService)
        {
            _registrationService = registrationService;
            _authService = authService;
        }

        [HttpPost]
        public IActionResult Post([FromBody] RegistrationPost registrationData) 
        {
            LoginPostResponse response = new LoginPostResponse();

            DataTransfer.User registeredUser = _registrationService.Register(registrationData);

            if (registeredUser != null)
            {
                response.Success = true;
                response.Body = new LoginPostResponseBody()
                {
                    UserName = registeredUser.UserName,
                    EmailAddress = registeredUser.EmailAddress,
                    FirstName = registeredUser.FirstName,
                    LastName = registeredUser.LastName,
                    AuthToken = _authService.GetToken(registeredUser.UserId)
                };
            }
            else
            {
                response.Success = false;
                response.ErrorMessage = "Unable to create account.";
            }

            return Ok(response);
        }
    }
}