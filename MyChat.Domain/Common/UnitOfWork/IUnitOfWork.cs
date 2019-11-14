using MyChat.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyChat.Domain.Common.UnitOfWork
{
    public interface IUnitOfWork
    {
        IUserRepository Users { get; }

        void Commit();
    }
}
