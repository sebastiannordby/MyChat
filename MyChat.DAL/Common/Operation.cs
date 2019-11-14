using MyChat.Domain.Common;
using MyChat.Domain.Common.UnitOfWork;

namespace MyChat.Infrastructure.Common
{
    public class Operation : IOperation
    {
        public OperationType OperationType { get; private set; }
        public IEntity Entity { get; private set; }
        public IUnitOfWorkRepository Repository { get; private set; }

        public Operation(
            OperationType operationType,
            IEntity entity,
            IUnitOfWorkRepository repository)
        {
            this.OperationType = operationType;
            this.Entity = entity;
            this.Repository = repository;
        }

        public void Execute()
        {
            Repository.Execute(this);
        }
    }
}
