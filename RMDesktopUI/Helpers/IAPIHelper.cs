using RMDesktopUI.Models;
using System.Threading.Tasks;

namespace RMDesktopUI.Helpers
{
    public interface IAPIHelper
    {
        Task<AuthenticatedUser> AuthenticateAsync(string username, string password);
    }
}