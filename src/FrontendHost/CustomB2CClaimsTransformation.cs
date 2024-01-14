using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace poc_bff
{
    public class CustomB2CClaimsTransformation : IClaimsTransformation
    {
        public Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            ClaimsIdentity claimsIdentity = new ClaimsIdentity();
            var myCustomClaim = "mycustom_claim";
            if (!principal.HasClaim(claim => claim.Type == myCustomClaim))
            {
                string myCustomValue= $"hello world";
                claimsIdentity.AddClaim(new Claim(myCustomClaim, myCustomValue));
            }

            principal.AddIdentity(claimsIdentity);
            return Task.FromResult(principal);
        }
    }
}