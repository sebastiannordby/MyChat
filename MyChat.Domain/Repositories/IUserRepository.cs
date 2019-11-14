using MyChat.Domain.Common;
using MyChat.Domain.Models;
using System.Collections.Generic;

namespace MyChat.Domain.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        User GetUserByUserName(string username);
        bool IsUserNameAvailable(string username);
        bool IsValidLogin(string username, string password);
        IEnumerable<User> GetByKeyword(string keyword);
    }
}
