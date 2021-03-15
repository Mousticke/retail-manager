using RMDesktopUI.Library.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RMDesktopUI.Library.Api
{
    public interface IUserEndpoint
    {
        Task<List<UserModel>> GetAll();
        Task AddUserToRole(string userId, string roleName);
        Task RemoveFromRole(string userId, string roleName);
        Task<Dictionary<string, string>> GetAllRoles();
    }
}