using System.Security.Claims;
using System.Threading.Tasks;
//using Microsoft.AspNet.Identity.Owin;
//using Microsoft.Owin;
using Microsoft.Owin.Security;

namespace Powell.Vehicles.Managers
{
    using Identity.Domain;

    public class ApplicationSignInManager : SignInManagerBase<User>
    {
        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(User user)
        {
            return user.GenerateUserIdentityAsync((ApplicationUserManager) UserManager);
        }

        //// TODO: TBD: probably not needing this one at all...
        //public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
        //{
        //    return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
        //}
    }
}
