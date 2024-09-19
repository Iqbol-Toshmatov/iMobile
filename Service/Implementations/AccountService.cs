using iMobile.DAL.Interfaces;
using iMobile.Object.Entity;
using iMobile.Object.Response;
using iMobile.Object.ViewModels.Account;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using iMobile.Object.Response;
using iMobile.Object.Enum;
using iMobile.Object.Helpers;

namespace iMobile.Service.Implementations
{
    public class AccountService
    {

        private ClaimsIdentity Authenticate(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType,user.Name),
                new Claim(ClaimsIdentity.DefaultNameClaimType,user.Role.ToString()),

            };

            return new ClaimsIdentity(claims,"ApplicationCookie",
                ClaimsIdentity.DefaultNameClaimType,ClaimsIdentity.DefaultRoleClaimType);
        }

        private readonly IBaseRepository<User> _userRepository;
        private readonly ILogger<AccountService> _logger;

        public AccountService(IBaseRepository<User> userRepository,
            ILogger<AccountService> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task<BaseResponse<ClaimsIdentity>> Register(RegisterViewModel model)
        {
            try
            {
                var user = _userRepository.Select().FirstOrDefault(x => x.Name == model.Name);
                if (user != null)
                {
                    return new BaseResponse<ClaimsIdentity>()
                    {
                        Description = "This name is not vacant"
                    };
                }

                user = new User()
                {
                    Name= model.Name,
                    Role=Role.User,
                    Password=HashPasswordHelper.HashPassword(model.Password),
                };

                _userRepository.Create(user);
                var result = Authenticate(user);

                return new BaseResponse<ClaimsIdentity>()
                {
                    Data = result,
                    Description="User added",
                    StatusCode=StatusCode.Ok
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[Register]: {ex.Message}");
                return new BaseResponse<ClaimsIdentity>()
                {
                    Description=ex.Message,
                    StatusCode=StatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<ClaimsIdentity>> Login(LoginViewModel model)
        {
            try
            {
                var user = _userRepository.Select().FirstOrDefault(x => x.Name == model.Name);
                if (user == null)
                {
                    return new BaseResponse<ClaimsIdentity>()
                    {
                        Description = "User not found"
                    };
                }

                if (user.Password != HashPasswordHelper.HashPassword(model.Password))
                {
                    return new BaseResponse<ClaimsIdentity>()
                    {
                        Description = "Wrong password"
                    };
                }

                var result = Authenticate(user);

                return new BaseResponse<ClaimsIdentity>()
                {
                    Data = result,
                    StatusCode = StatusCode.Ok
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[Login]: {ex.Message}");

                return new BaseResponse<ClaimsIdentity>()
                {
                    Description=ex.Message,
                    StatusCode=StatusCode.InternalServerError
                };
            }
        }

    }
}
