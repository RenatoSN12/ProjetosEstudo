﻿using Blog.Models;
using System.Security.Claims;

namespace Blog.Extensions
{
    public static class RoleClaimsExtension
    {
        public static IEnumerable<Claim> GetClaims(this User user)
        {
            var result = new List<Claim>
            {
                new(ClaimTypes.Name, user.Email)
            };

            result.AddRange(user.Roles.Select(x => new Claim(ClaimTypes.Role, x.Slug)));
            return result;
        }
    }
}
