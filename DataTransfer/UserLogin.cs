using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthAPI.DataTransfer
{
    public class UserLogin
    {
        public int UserLoginId { get; set; }
        public int UserId { get; set; }
        public int FailedLoginAttempts { get; set; }
        public bool LockedOut { get; set; }
        public DateTime? UnlockDate { get; set; }
    }
}
