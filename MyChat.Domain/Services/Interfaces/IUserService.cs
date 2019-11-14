using MyChat.Domain.Common;
using MyChat.DTO;
using System.Collections.Generic;

namespace MyChat.Domain.Services.Interfaces
{
    public interface IUserService : IEntityService<UserDto>
    {
        UserDto Add(UserDto userDto);
        UserDto GetUserByUserName(string username);
        bool IsUserNameAvailable(string username);
        bool IsValidLogin(string username, string password);
        IEnumerable<UserDto> GetUsersByKeyword(string keyword);
    }
}
