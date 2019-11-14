using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using MyChat.Domain.Repositories;
using MyChat.Infrastructure.Common;
using MyChat.Domain.Common.UnitOfWork;
using MyChat.Infrastructure.Repositories.SQL;

namespace MyChat.Infrastructure.UnitOfWork
{
    public class SQLUnitOfWork : IUnitOfWork
    {
        public IUserRepository Users { get; private set; }

        private SqlTransaction _transaction;
        private SqlConnection _connection;

        private string _connectionString;
        private List<Operation> _operations;
        private readonly IDomainEventDispatcher _dispatcher;

        public SQLUnitOfWork(UnitOfWorkOptions options, IDomainEventDispatcher dispatcher)
        {
            _connectionString = options.ConnectionString;
            _operations = new List<Operation>();
            _dispatcher = dispatcher;

            this.Users = new SQLUserRepository(this);
        }

        private void ExecuteOperations()
        {
            foreach(var operation in _operations)
            {
                operation.Execute();
                
                foreach(var domainEvent in operation.Entity.DomainEvents)
                {
                    _dispatcher.DispatchEvent(domainEvent);
                }
            }
        }

        public void RegisterOperation(Operation operation)
        {
            _operations.Add(operation);
        }

        public SqlConnection Connection()
        {
            return new SqlConnection(_connectionString);
        }

        public SqlCommand Command(string command)
        {
            if (_connection == null)
            {
                _connection = new SqlConnection(_connectionString);
                _connection.Open();
            }

            if (_transaction == null)
                _transaction = _connection.BeginTransaction();

            return new SqlCommand(command, _connection, _transaction);
        }

        public void Commit()
        {
            try
            {
                ExecuteOperations();
                _transaction?.Commit();
                _connection?.Close();

                _transaction = null;
                _connection = null;
            }
            catch(Exception e)
            {
                _transaction?.Rollback();

                throw e;
            }
        }
    }
}
