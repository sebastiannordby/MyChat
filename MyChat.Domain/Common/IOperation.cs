using MyChat.Domain.Common.UnitOfWork;

namespace MyChat.Domain.Common
{
    public interface IOperation
    {
        OperationType OperationType { get; }
        IEntity Entity { get; }
        IUnitOfWorkRepository Repository { get; }

        void Execute();
    }
}
