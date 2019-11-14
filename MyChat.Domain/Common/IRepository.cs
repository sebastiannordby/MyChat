using MyChat.Domain.Common.UnitOfWork;
using System;

namespace MyChat.Domain.Common
{
    public interface IRepository<T> : IUnitOfWorkRepository
        where T : IEntity
    {
        T Get(Guid id);

        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
