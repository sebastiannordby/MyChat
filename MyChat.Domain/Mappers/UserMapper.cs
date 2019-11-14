using MyChat.Domain.Models;
using MyChat.DTO;
using System.Collections.Generic;

namespace MyChat.Domain.Mappers
{
    public static class UserMapper
    {
        public static UserDto ToDto(this User user)
        {
            return new UserDto()
            {
                Id = user.Id,
                UserName = user.UserName,
                EmailAddress = user.EmailAddress,
                FirstName = user.FirstName,
                LastName = user.LastName
            };
        }

        public static IEnumerable<UserDto> ToDto(this IEnumerable<User> users)
        {
            var result = new List<UserDto>();

            foreach(var user in users)
            {
                result.Add(user.ToDto());
            }

            return result;
        }
    }
}
