using System;
using System.Collections.Generic;

namespace MovieStore.Core.Models.Response
{
    public class UserProfileResponseModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Email { get; set; }
        
        public string PhoneNumber { get; set; }
        public bool? TwoFactorEnabled { get; set; }
        public DateTime? LockoutEndDate { get; set; }
        public DateTime? LastLoginDateTime { get; set; }

        public IEnumerable<AccountRoleResponseModel> Roles { get; set; }
    }
}