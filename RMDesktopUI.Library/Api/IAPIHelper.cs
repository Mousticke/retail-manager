using RMDesktopUI.Library.Models;
using System.Threading.Tasks;

namespace RMDesktopUI.Library.Api
{
    public interface IAPIHelper
    {
        Task<AuthenticatedUser> AuthenticateAsync(string username, string password);
        Task GetLoggedInUserInfoAsync(string token);
    }
}