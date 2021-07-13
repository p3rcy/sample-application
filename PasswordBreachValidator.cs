using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using sample_application.ApiClients;
using sample_application.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sample_application
{
    public class PasswordBreachValidator<TUser> : IPasswordValidator<TUser>
    where TUser : IdentityUser
    {
        IPasswordBreachClient PasswordBreachClient { get; set; }
        ISHAEncryptionHelper ShaEncryptionHelper { get; set; }
        public ILogger<PasswordBreachValidator<TUser>> Logger { get; }

        public PasswordBreachValidator(IPasswordBreachClient passwordBreachClient, ISHAEncryptionHelper shaEncryptionHelper, ILogger<PasswordBreachValidator<TUser>> _logger)
        {
            PasswordBreachClient = passwordBreachClient;
            ShaEncryptionHelper = shaEncryptionHelper;
            Logger = _logger;
        }

        public async Task<IdentityResult> ValidateAsync(UserManager<TUser> manager, TUser user, string password)
        {
            var passwordHash = this.ShaEncryptionHelper.HashSHA1(password);

            try
            {
                bool breach = await this.PasswordBreachClient.HasPasswordBreached(passwordHash.Substring(0, 5), passwordHash);

                if (breach)
                {
                    return IdentityResult.Failed(new IdentityError
                    {
                        Code = "PasswordHasBeenBreached",
                        Description = "The password provided is an insecure password. Please try a different password."
                    });
                }
            }
            catch (Exception ex)
            {
                // Only thing that can throw exception is API. 
                this.Logger.LogError("Password breach api error: " + ex);

                return IdentityResult.Failed(new IdentityError
                {
                    Code = "PasswordBreachApiFailure",
                    Description = "This service is currently unavailable. Please try again later."
                });
            }

            return IdentityResult.Success;
        }
    }
}
