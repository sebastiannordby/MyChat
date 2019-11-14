using MyChat.Domain.Common;
using MyChat.Domain.Common.UnitOfWork;
using MyChat.Infrastructure.Common;
using MyChat.Infrastructure.UnitOfWork;
using System;
using System.Data.SqlClient;

namespace MyChat.Infrastructure.Repositories.SQL
{
    public abstract class SQLRepository<T> : IRepository<T> where T : IEntity
    {
        protected SQLUnitOfWork _unitOfWork;

        public SQLRepository(SQLUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Add(T entity)
        {
            _unitOfWork.RegisterOperation(new Operation(
                operationType: OperationType.Add,
                entity: entity,
                repository: this
            ));
        }

        public void Update(T entity)
        {
            _unitOfWork.RegisterOperation(new Operation(
                operationType: OperationType.Update,
                entity: entity,
                repository: this
            ));
        }

        public void Delete(T entity)
        {
            _unitOfWork.RegisterOperation(new Operation(
                operationType: OperationType.Delete,
                entity: entity,
                repository: this
            ));
        }

        public abstract T Get(Guid id);
        public abstract void Execute(IOperation operation);
    }
}
