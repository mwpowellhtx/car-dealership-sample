using System;
using Microsoft.AspNet.Identity;
using Powell.Identity.Domain;

namespace Powell.Vehicles.Managers
{
    using Mvc.Identity;
    using Mvc.Services;
    using UserValidatorType = UserValidator<User, Guid>;
    using EmailTokenProviderType = EmailTokenProvider<User, Guid>;

    // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.
    public class ApplicationUserManager : UserManagerBase<User>
    {
        // ReSharper disable once SuggestBaseTypeForParameter
        public ApplicationUserManager(ICompositeUserRepository repository
            , IEmailIdentityMessageService emailService
            , IBasicUserTokenProvider userTokenProvider)
            : base(repository)
        {
            Initialize(emailService, userTokenProvider);
        }

        // ReSharper disable once SuggestBaseTypeForParameter
        private void Initialize(IEmailIdentityMessageService emailService, IBasicUserTokenProvider userTokenProvider)
        {
            // Configure validation logic for usernames.
            UserValidator = new UserValidatorType(this)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords.
            PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };

            // Configure user lockout defaults.
            UserLockoutEnabledByDefault = true;
            DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            MaxFailedAccessAttemptsBeforeLockout = 5;

            //// TODO: TBD: not a big fan of the "requirement" that users should "have a phone". Smacks of globalist technocratic rule.
            //// Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            //RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<ApplicationUser>
            //{
            //    MessageFormat = "Your security code is {0}"
            //});

            // TODO: TBD: could be more verbose, formatted, etc, etc...
            // You can write your own provider and plug it in here.
            RegisterTwoFactorProvider("Email Code", new EmailTokenProviderType
            {
                Subject = "Security Code",
                BodyFormat = "Your security code is {0}"
            });

            // Connect the services with the underlying mananger.
            EmailService = emailService;
            //manager.SmsService = new SmsService();

            // TODO: be careful of DI container life cycles...
            UserTokenProvider = userTokenProvider;
        }
    }

    // Configure the application sign-in manager which is used in this application.
}
