using BookStore.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace BookStore.Repository
{
    public interface IAccountRepository
    {
        Task<IdentityResult> SignupAsync(SignUpModel signupModel);
        Task<string> LoginAsync(SigninModel signinModel);
    }
}
