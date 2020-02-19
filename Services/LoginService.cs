using AuthAPI.Database;
using AuthAPI.Entities;
using AuthAPI.Models;
using Microsoft.Extensions.Options;
using System;

namespace AuthAPI.Services
{
    public class LoginService : ILoginService
    {
        private IAuthenticationService _authService;
        private IPasswordHasher _passwordHasher;
        private UserContext _database;

        public LoginService(IAuthenticationService authService)
        {
            HashingOptions opts = new HashingOptions();
            opts.Iterations = 1;

            IOptions<HashingOptions> test = Options.Create<HashingOptions>(opts);
            _passwordHasher = new PasswordHasher(test);

            _authService = authService;
            _database = new UserContext();
        }

        public LoginAttempt Login(string username, string password)
        {
            LoginAttempt attemptResult = new LoginAttempt();

            if (IsUserLockedOut(username))
            {
                // Failed -> locked out
                attemptResult.IsSuccessful = false;
                attemptResult.ErrorMessage = "User is locked out";
                return attemptResult;
            }

            DataTransfer.User matchedUser = CheckMatch(username, password);
            if (matchedUser == null)
            {
                // Failed
                FailLoginAttempt(username);
                attemptResult.IsSuccessful = false;
                attemptResult.ErrorMessage = "Username or password did not match.";
                return attemptResult;
            }
            else
            {
                // Success
                ResetUserLockout(matchedUser);

                attemptResult.IsSuccessful = true;
                attemptResult.UserName = matchedUser.UserName;
                attemptResult.EmailAddress = matchedUser.EmailAddress;
                attemptResult.FirstName = matchedUser.FirstName;
                attemptResult.LastName = matchedUser.LastName;
                attemptResult.AuthToken = _authService.GetToken(matchedUser.UserId);
                return attemptResult;
            }
        }

        private bool IsUserLockedOut(string username)
        {
            DataTransfer.User user = null;
            foreach (DataTransfer.User userCheck in _database.GetUsers())
            {
                if (userCheck.UserName == username || userCheck.EmailAddress == username)
                {
                    user = userCheck;
                    break;
                }
            }

            if (user == null)
            {
                return false;
            }

            if (user.LoginInfo.LockedOut)
            {
                if (DateTime.Compare(user.LoginInfo.UnlockDate.Value, DateTime.Now) > 0)
                {
                    return true;
                }
            }

            return false;
        }
        private void FailLoginAttempt(string username)
        {
            DataTransfer.User user = null;
            foreach(DataTransfer.User userCheck in _database.GetUsers())
            {
                if (userCheck.UserName == username || userCheck.EmailAddress == username)
                {
                    user = userCheck;
                    break;
                }
            }

            if (user == null)
            {
                return;
            }

            user.LoginInfo.FailedLoginAttempts++;
            if (user.LoginInfo.FailedLoginAttempts >= 5)
            {
                user.LoginInfo.LockedOut = true;
                user.LoginInfo.UnlockDate = DateTime.Now.AddMinutes(15);
            }

            _database.Commit();
        }

        private void ResetUserLockout(DataTransfer.User user)
        {
            if (user.LoginInfo.LockedOut)
            {
                if (DateTime.Compare(user.LoginInfo.UnlockDate.Value, DateTime.Now) > 0)
                {
                    return;
                }
            }

            user.LoginInfo.LockedOut = false;
            user.LoginInfo.UnlockDate = null;
            user.LoginInfo.FailedLoginAttempts = 0;
            _database.Commit();
        }

        private DataTransfer.User CheckMatch(string username, string password)
        {
            foreach (DataTransfer.User userCheck in _database.GetUsers())
            {
                if (username == userCheck.UserName || username == userCheck.EmailAddress)
                {
                    var (verified, needsUpgrade) = _passwordHasher.Check(userCheck.Password, password);
                    if (verified)
                    {
                        return userCheck;
                    }
                    else
                    {
                        return null;
                    }
                }
            }

            return null;
        }
    }
}
