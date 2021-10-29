using App.Domain.Dtos;
using App.Domain.Entities;

namespace App.Domain.Persistence.Repositories
{
    public interface IUserRepository
    {
        User RegisterUser(RegisterUserModel model);

        //LoginWithTokenModel LoginUser(LoginModel user);
        UserWithTokenDto LoginUser(LoginModel user);

    }
}
