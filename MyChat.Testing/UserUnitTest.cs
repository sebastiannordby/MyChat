using MyChat.Domain.Common.UnitOfWork;
using MyChat.Domain.Models;
using MyChat.Infrastructure.UnitOfWork;
using System;
using Xunit;

namespace MyChat.Testing
{
    public class UserUnitTest
    {
        //[Fact]
        //public void FullTest()
        //{
        //    string connectionString = "Data Source=WL_LT_180412\\SQLEXPRESS;Database=CMDDemo;User Id=sa;Password=WLC0Mpwd;";
        //    IUnitOfWork unitOfWork = new SQLUnitOfWork(connectionString);

        //    var user = new User(Guid.NewGuid(), "Sebastian", "vannmelon", "sebastianbjornstad@hotmail.com", "Sebastian", "Nordby Bjørnstad");

        //    unitOfWork.Users.Add(user);
        //    unitOfWork.Commit();

        //    var createdUser = unitOfWork.Users.Get(user.Id);

        //    Assert.True(createdUser != null);

        //    unitOfWork.Users.Delete(createdUser);
        //    unitOfWork.Commit();

        //    var deletedUser = unitOfWork.Users.Get(user.Id);

        //    Assert.True(deletedUser == null);
        //}
    }
}
