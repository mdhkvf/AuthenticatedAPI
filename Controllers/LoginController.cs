using AuthAPI.Entities;
using AuthAPI.Payloads;
using AuthAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace AuthAPI.Controllers
{
    [Route("api/login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;

        public LoginController(IRegistrationService registrationService, IAuthenticationService authService, ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost]
        public IActionResult Post([FromBody] LoginPost loginData)
        {
            LoginPostResponse response = new LoginPostResponse();

            LoginAttempt loginAttempt = _loginService.Login(loginData.UserName, loginData.Password);

            if (loginAttempt.IsSuccessful)
            {
                response.Success = true;
                response.Body = new LoginPostResponseBody()
                {
                    UserName = loginAttempt.UserName,
                    EmailAddress = loginAttempt.EmailAddress,
                    FirstName = loginAttempt.FirstName,
                    LastName = loginAttempt.LastName,
                    AuthToken = loginAttempt.AuthToken
                };
            }
            else
            {
                response.Success = false;
                response.ErrorMessage = loginAttempt.ErrorMessage;
            }

            return Ok(response);
        }
    }
}