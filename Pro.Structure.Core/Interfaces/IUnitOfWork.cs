using System.Data;

namespace Pro.Structure.Core.Interfaces;

/// <summary>
/// Interface for managing database transactions across multiple operations.
/// This ensures data consistency and provides rollback capabilities.
/// </summary>
public interface IUnitOfWork : IDisposable
{
    /// <summary>
    /// Begins a new database transaction with specified isolation level.
    /// </summary>
    /// <param name="isolationLevel">The isolation level for the transaction (default: ReadCommitted)</param>
    Task BeginTransactionAsync(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted);

    /// <summary>
    /// Commits all changes made within the current transaction to the database.
    /// </summary>
    Task CommitAsync();

    /// <summary>
    /// Rolls back all changes made within the current transaction.
    /// </summary>
    Task RollbackAsync();

    /// <summary>
    /// Executes an operation within a transaction and returns a result.
    /// If the operation fails, the transaction is rolled back.
    /// </summary>
    /// <typeparam name="T">The type of result to return</typeparam>
    /// <param name="operation">The operation to execute</param>
    /// <param name="isolationLevel">The isolation level for the transaction</param>
    Task<T> ExecuteInTransactionAsync<T>(
        Func<Task<T>> operation,
        IsolationLevel isolationLevel = IsolationLevel.ReadCommitted
    );

    /// <summary>
    /// Executes an operation within a transaction without returning a result.
    /// If the operation fails, the transaction is rolled back.
    /// </summary>
    /// <param name="operation">The operation to execute</param>
    /// <param name="isolationLevel">The isolation level for the transaction</param>
    Task ExecuteInTransactionAsync(
        Func<Task> operation,
        IsolationLevel isolationLevel = IsolationLevel.ReadCommitted
    );
}
