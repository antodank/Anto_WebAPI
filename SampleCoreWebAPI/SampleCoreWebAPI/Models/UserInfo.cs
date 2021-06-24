using System;
using System.Collections.Generic;

namespace SampleCoreWebAPI.Models
{
    public partial class UserInfo : BaseEntity
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
