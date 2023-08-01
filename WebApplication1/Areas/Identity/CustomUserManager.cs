
using Entities;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;


namespace Game_ECommerce.Areas.Identity
{
    public class CustomUserManager : UserManager<IdentityUser>
    {
        private readonly ApplicationDbContext _dbContext;

        public CustomUserManager(IUserStore<IdentityUser> store, IOptions<IdentityOptions> optionsAccessor, IPasswordHasher<IdentityUser> passwordHasher,
            IEnumerable<IUserValidator<IdentityUser>> userValidators, IEnumerable<IPasswordValidator<IdentityUser>> passwordValidators,
            ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<IdentityUser>> logger,
            ApplicationDbContext dbContext)
            : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
            _dbContext = dbContext;
        }

        public override async Task<IdentityResult> CreateAsync(IdentityUser user)
        {
            var result = await base.CreateAsync(user);

            if (result.Succeeded)
            {
                var shoppingCart = new ShoppingCart
                {
                    UserId = user.Id,
                    // Altre proprietà del carrello, se necessario
                    // ...
                };

                _dbContext.ShoppingCarts.Add(shoppingCart);
                await _dbContext.SaveChangesAsync();
            }

            return result;
        }
    }
}
