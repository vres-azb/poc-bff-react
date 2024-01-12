using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace poc_bff
{
    public class CustomB2CClaimsTransformation : IClaimsTransformation
    {
        public Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            ClaimsIdentity claimsIdentity = new ClaimsIdentity();
            var bffLogoutForB2CClaimType = "bff:logout_url";
            if (!principal.HasClaim(claim => claim.Type == bffLogoutForB2CClaimType))
            {
                claimsIdentity.AddClaim(new Claim(bffLogoutForB2CClaimType, $"bff/logout?sid={0}"));
            }

            principal.AddIdentity(claimsIdentity);
            return Task.FromResult(principal);
        }
    }
}