using System;
using System.Collections.Generic;
using MyChat.DTO;
using MyChat.Domain.Models;
using MyChat.Domain.Mappers;
using MyChat.Domain.Common.UnitOfWork;
using MyChat.Domain.Services.Interfaces;
using MyChat.Domain.Events;

namespace MyChat.Domain.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public UserDto Add(UserDto entity)
        {
            if (!_unitOfWork.Users.IsUserNameAvailable(entity.UserName))
                throw new ArgumentException("Username is not available.");

            var user = new User(
                id: Guid.NewGuid(),
                userName: entity.UserName,
                password: entity.Password,
                emailAddress: entity.EmailAddress,
                firstName: entity.FirstName,
                lastName: entity.LastName);

            user.DomainEvents.Add(
                new UserCreatedEvent(user));

            _unitOfWork.Users.Add(user);
            _unitOfWork.Commit();

            return user.ToDto();
        }

        public void Delete(Guid id)
        {
            var user = _unitOfWork.Users.Get(id);
            if (user == null)
                throw new ArgumentException($"No User with given id({id}).");

            _unitOfWork.Users.Add(user);
            _unitOfWork.Commit();
        }

        public UserDto GetUserByUserName(string username)
            => _unitOfWork.Users.GetUserByUserName(username).ToDto();

        public IEnumerable<UserDto> GetUsersByKeyword(string keyword)
            => _unitOfWork.Users.GetByKeyword(keyword).ToDto();

        public bool IsUserNameAvailable(string username)
              => _unitOfWork.Users.IsUserNameAvailable(username);

        public bool IsValidLogin(string username, string password)
             => _unitOfWork.Users.IsValidLogin(username, password);
    }
}
