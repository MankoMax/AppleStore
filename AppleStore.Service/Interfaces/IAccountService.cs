using AppleStore.Domain.Response;
using AppleStore.Domain.ViewModels.Account;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AppleStore.Service.Interfaces
{
    public interface IAccountService
    {
        Task<BaseResponse<ClaimsIdentity>> Register(RegisterViewModel model);

        Task<BaseResponse<ClaimsIdentity>> Login(LoginViewModel model);
    }
}