using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebshopServer.Dtos;

namespace WebshopServer.Interfaces
{
    public interface IUserService
    {
        List<UserDto> GetAllUsers();
        UserDto GetUserById(long id);
        UserDto RegisterUser(UserDto userDto);
        UserDto UpdateUser(long id, UserDto userDto);
        LoginResponseDto LoginUser(LoginRequestDto requestDto);
        UserDto VerifyUser(VerifyDto verifyDto);
    }
}
