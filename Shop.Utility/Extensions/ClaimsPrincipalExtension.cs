using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Shop.Utility.Extensions
{
    public static class ClaimsPrincipalExtension
    {
        public static string GetFullName(this ClaimsPrincipal claimsPrincipal)
        {
            var claim = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.GivenName);
            return claim?.Value;
        }
    }
}
