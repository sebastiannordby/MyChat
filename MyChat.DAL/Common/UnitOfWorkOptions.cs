namespace MyChat.Infrastructure.Common
{
    public class UnitOfWorkOptions
    {
        public string ConnectionString { get; private set; }

        public UnitOfWorkOptions(string connectionString)
        {
            this.ConnectionString = connectionString;
        }
    }
}
