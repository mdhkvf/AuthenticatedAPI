using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AuthAPI.DataTransfer
{
    public class User
    {
        public int UserId { get; set; }

        [Column(TypeName = "VARCHAR(50)")]
        [StringLength(50)]
        public string UserName { get; set; }

        [Column(TypeName = "VARCHAR(250)")]
        [StringLength(50)]
        public string Password { get; set; }

        [Column(TypeName = "VARCHAR(50)")]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Column(TypeName = "VARCHAR(50)")]
        [StringLength(50)]
        public string LastName { get; set; }

        [Column(TypeName = "VARCHAR(50)")]
        [StringLength(50)]
        public string EmailAddress { get; set; }
        public UserLogin LoginInfo { get; set; }
    }
}
