using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebshopServer.Dtos;

namespace WebshopServer.Interfaces
{
    public interface IUserService
    {
        List<UserResponseDto> GetAllUsers();
        UserResponseDto GetUserById(long id);
        UserResponseDto RegisterUser(RegisterRequestDto requestDto);
        UserResponseDto UpdateUser(long id, UserRequestDto requestDto);
        LoginResponseDto LoginUser(LoginRequestDto requestDto);
        VerificationResponseDto VerifyUser(long id, VerificationRequestDto requestDto);
    }
}
