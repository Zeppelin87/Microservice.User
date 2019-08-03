using Microservice.User.Infrastructure.Interfaces.UnitOfWork;
using System.Data;

namespace Microservice.User.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private IDbConnection _connection;
        private IDbTransaction _transaction;
        private bool _hasCommitted;
        private bool _hasUndone;
        private bool _hasClosedConnection;

        public UnitOfWork(IDbConnection connection)
        {
            _connection = connection;
            _connection.Open();
            _transaction = connection.BeginTransaction();
            _hasCommitted = false;
            _hasUndone = false;
            _hasClosedConnection = false;
        }

        public void Commit()
        {
            if (_hasCommitted)
            {
                throw new System.InvalidOperationException(
                        "The unit of work has already been committed. Cannot commit a unit of work more than once.");
            }

            _transaction.Commit();
            _hasCommitted = true;
            _transaction = null;
        }

        public IDbCommand CreateCommand()
        {
            IDbCommand command = _connection.CreateCommand();
            command.Transaction = _transaction;
            return command;
        }

        public void Undo()
        {
            if (_hasCommitted || _hasUndone) { return; }

            _transaction.Rollback();
            _hasUndone = true;
            _transaction = null;
        }

        #region IDisposable Support

        private bool _hasDisposed = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!_hasDisposed && disposing)
            {
                if (!_hasCommitted && !_hasUndone)
                {
                    Undo();
                }

                if (!_hasClosedConnection)
                {
                    _connection.Close();
                    _hasClosedConnection = true;
                    _connection = null;
                }

                _hasDisposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }

        #endregion
    }
}
