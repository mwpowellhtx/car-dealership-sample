using Microsoft.Owin.Security;

namespace Powell.Vehicles.Managers
{
    public class ManagerHelper
    {
        public ApplicationUserManager UserManager { get; }

        public IAuthenticationManager AuthManager { get; }

        public ManagerHelper(ApplicationUserManager userManager, IAuthenticationManager authManager)
        {
            UserManager = userManager;
            AuthManager = authManager;
        }
    }
}
