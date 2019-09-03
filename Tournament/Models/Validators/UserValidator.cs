using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;

namespace Tournament.Models
{
    public class CustomUserValidator : UserValidator<ApplicationUser>
    {
        public CustomUserValidator(ApplicationUserManager mgr)
            : base(mgr)
        {
            AllowOnlyAlphanumericUserNames = false;
        }
        //public override async Task<IdentityResult> ValidateAsync(ApplicationUser user)
        //{
        //    IdentityResult result = await base.ValidateAsync(user);
        //    if (user.UserName.ToLower().Contains("admin"))
        //    {
        //        var errors = result.Errors.ToList();
        //        errors.Add("User nickname can't contains 'admin'");
        //        result = new IdentityResult(errors);
        //    }
        //    return result;
        //}
    }
}