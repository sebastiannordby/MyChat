
namespace MyChat.Domain.Common.UnitOfWork
{
    public interface IUnitOfWorkRepository
    {
        void Execute(IOperation operation);
    }
}
