using iMobile.Object.Entity;
using iMobile.Object.Response;
using iMobile.Object.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iMobile.Service.Interfaces
{
    public interface IUserService
    {
        Task<IBaseResponse<User>> Create(UserViewModel user);
        BaseResponse<Dictionary<int, string>> GetRoles();
        Task<BaseResponse<IEnumerable<UserViewModel>>> GetUsers();
        Task<BaseResponse<bool>> DeleteUser(int id);
    }
}
