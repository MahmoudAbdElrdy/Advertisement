using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AuthDomain.Entities.Auth;
using Common;
using Common.Interfaces;
using Common.Options;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;


namespace Infrastructure
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<User> _userManager;
        private readonly IAuditService _auditService;
        private readonly Common.Interfaces.IUrlHelper _urlHelper;
        private readonly RoleManager<Role> _roleManager;

        private readonly JwtOption _jwtOption;
        private readonly IAppDbContext _context;

        public IdentityService(UserManager<User> userManager,
          IAuditService auditService,
          Common.Interfaces.IUrlHelper urlHelper,
          RoleManager<Role> roleManager,
          JwtOption jwtOption,
          IAppDbContext context
        )
        {
            _userManager = userManager;
            _auditService = auditService;
            _roleManager = roleManager;
            _jwtOption = jwtOption;
            _context = context;
            _urlHelper = urlHelper;
        }

        public Task<Result> RegisterAsync(string email, string password)
        {
            throw new NotImplementedException();
        }

        public async Task<Result> LoginAsync(string email, string password, string lang, string WebToken)
        {
            var user = await _userManager.Users.Include(x => x.Avatar).FirstOrDefaultAsync(x => x.Email.ToLower() == email.ToLower() || x.UserName.ToLower() == email.ToLower());

            if (user == null || user.IsDeleted == true)
            {
                throw new ApiException(ApiExeptionType.NotFound);
            }           

            var userHasValidPassword = await _userManager.CheckPasswordAsync(user, password);

            if (!userHasValidPassword)
                throw new ApiException(ApiExeptionType.InvalidLogin);

           
            user.WebToken = WebToken;
            user.UserLang = lang;

            await _userManager.UpdateAsync(user);
            //_auditService.UserLanguage = lang;
            //_auditService.WebToken = WebToken;
            return await GenerateAuthenticationResultForUserAsync(user);
        }

        public Task<Result> RefreshTokenAsync(string token, string refreshToken)
        {
            throw new NotImplementedException();
        }

        public Task<Result> LoginWithFacebookAsync(string accessToken)
        {
            throw new NotImplementedException();
        }

        private async Task<Result> GenerateAuthenticationResultForUserAsync(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            //var token = tokenHandler.CreateToken(tokenDescriptor);
            var refreshToken = new RefreshToken
            {
                JwtId = Guid.NewGuid().ToString(),
                UserId = user.Id,
                CreationDate = DateTime.UtcNow,
                ExpiryDate = DateTime.UtcNow.AddMonths(6),
                //lang = user.UserLang
            };         

            await _context.Set<RefreshToken>().AddAsync(refreshToken);
            await _context.SaveChangesAsync();
            return Result.Successed(new
            {
                Token = await GenerateJwtToken(user.Email, user),
                RefreshToken = refreshToken.Token,
                User = new
                {
                    user.Id,
                    user.FullName,
                    user.Avatar?.Imageurl                    
                }
            });
        }

        private async Task<string> GenerateJwtToken(string email, User appUser)
        {
            var claims = new List<Claim> {
        new Claim(ClaimTypes.Name, email),
        //new Claim(JwtRegisteredClaimNames.Sub, email),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        new Claim(ClaimTypes.NameIdentifier, appUser.Id),
        new Claim("UserId", appUser.Id),
      };
            claims.AddRange(await _userManager.GetClaimsAsync(appUser));
            var userRoles = await _userManager.GetRolesAsync(appUser);
            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim("Role", userRole));
                claims.Add(new Claim(ClaimTypes.Role, userRole));
                var role = await _roleManager.FindByNameAsync(userRole);
                if (role != null)
                {
                    var roleClaims = await _roleManager.GetClaimsAsync(role);
                    foreach (Claim roleClaim in roleClaims)
                    {
                        claims.Add(roleClaim);
                    }
                }
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOption.Key));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_jwtOption.ExpireDays));

            var token = new JwtSecurityToken(
              _jwtOption.Issuer,
              _jwtOption.Issuer,
              claims,
              expires: expires,
              signingCredentials: cred
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<Result> ChangePasswordAsync(string passworrd, string newPassword)
        {
            var user = await _userManager.FindByEmailAsync(_auditService.UserName);
            if (user == null)
            {
                return Result.Failure(new[] { "User does not exist" });
            }

            var result = await _userManager.ChangePasswordAsync(user, passworrd, newPassword);


            if (result.Succeeded)
            {
                return Result.Successed();
            }

            return Result.Failure(result.Errors.Select(s => s.Description));
        }

        public async Task<Result> ForgetPasswordAsync(string Email)
        {
            var user = await _userManager.FindByEmailAsync(Email);
            if (user == null)
            {
                return Result.Failure(new[] { "User does not exist" });
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var url = $" auth / reset - password ? token = {token} +  & email =  + {user.Email}";


            var callback = _urlHelper.GetCurrentUrl(url);

            return Result.Successed();
        }

        public async Task<Result> ResetPasswordAsync(string token, string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return Result.Failure(new[] { "User does not exist" });
            }

            var resetPassResult = await _userManager.ResetPasswordAsync(user, token, password);
            if (resetPassResult.Succeeded)
            {
                return Result.Successed();
            }

            return Result.Failure(resetPassResult.Errors.Select(s => s.Description));
        }
    }
}