using System;
using System.Data;

namespace Microservice.User.Infrastructure.Interfaces.UnitOfWork
{
    ///<summary>
    /// Represents a singular unit of work. Used to carry out
    /// the philosophies of the unit of work design pattern.
    /// 
    /// This contract is specific to units of work for database
    /// engine connections
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        ///<summary>
        /// Creates the command.
        /// </summary>
        /// <returns>System.Data.IDbCommand</returns>
        IDbCommand CreateCommand();

        /// <summary>
        /// Saves the changes.
        /// </summary>
        void Commit();

        /// <summary>
        /// Rollback the instance
        /// </summary>
        void Undo();
    }
}
