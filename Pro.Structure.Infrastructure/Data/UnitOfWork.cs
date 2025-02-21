using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Pro.Structure.Core.Interfaces;

namespace Pro.Structure.Infrastructure.Data;

/// <summary>
/// Implementation of the Unit of Work pattern for managing database transactions.
/// This class ensures that multiple database operations are executed atomically.
/// 
/// Note: This implementation was developed with significant assistance from AI,
/// particularly for the transaction management logic and error handling patterns.
/// The AI helped ensure proper transaction lifecycle management and resource cleanup.
/// </summary>
public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    private IDbContextTransaction? _currentTransaction;

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Begins a new database transaction. Throws if a transaction is already in progress.
    /// Implementation assisted by AI for proper transaction state management.
    /// </summary>
    public async Task BeginTransactionAsync(
        IsolationLevel isolationLevel = IsolationLevel.ReadCommitted
    )
    {
        if (_currentTransaction != null)
        {
            throw new InvalidOperationException("A transaction is already in progress");
        }

        _currentTransaction = await _context.Database.BeginTransactionAsync(isolationLevel);
    }

    /// <summary>
    /// Commits the current transaction and saves all changes to the database.
    /// If any step fails, the transaction is automatically rolled back.
    /// Implementation assisted by AI for proper error handling and resource cleanup.
    /// </summary>
    public async Task CommitAsync()
    {
        try
        {
            // First save all changes to ensure the unit of work is complete
            await _context.SaveChangesAsync();

            // Then commit the transaction if one exists
            if (_currentTransaction != null)
            {
                await _currentTransaction.CommitAsync();
            }
        }
        catch
        {
            // If anything fails, roll back the transaction
            await RollbackAsync();
            throw;
        }
        finally
        {
            // Clean up the transaction object
            if (_currentTransaction != null)
            {
                _currentTransaction.Dispose();
                _currentTransaction = null;
            }
        }
    }

    /// <summary>
    /// Rolls back the current transaction, undoing all changes made within it.
    /// Implementation assisted by AI for proper error handling and cleanup.
    /// </summary>
    public async Task RollbackAsync()
    {
        try
        {
            if (_currentTransaction != null)
            {
                await _currentTransaction.RollbackAsync();
            }
        }
        finally
        {
            // Always clean up the transaction object
            if (_currentTransaction != null)
            {
                _currentTransaction.Dispose();
                _currentTransaction = null;
            }
        }
    }

    /// <summary>
    /// Executes an operation within a transaction and returns its result.
    /// Automatically handles transaction lifecycle and rollback on failure.
    /// Implementation fully assisted by AI for robust transaction management.
    /// </summary>
    public async Task<T> ExecuteInTransactionAsync<T>(
        Func<Task<T>> operation,
        IsolationLevel isolationLevel = IsolationLevel.ReadCommitted
    )
    {
        try
        {
            await BeginTransactionAsync(isolationLevel);
            T result = await operation();
            await CommitAsync();
            return result;
        }
        catch
        {
            await RollbackAsync();
            throw;
        }
    }

    /// <summary>
    /// Executes an operation within a transaction without returning a result.
    /// Automatically handles transaction lifecycle and rollback on failure.
    /// Implementation fully assisted by AI for robust transaction management.
    /// </summary>
    public async Task ExecuteInTransactionAsync(
        Func<Task> operation,
        IsolationLevel isolationLevel = IsolationLevel.ReadCommitted
    )
    {
        try
        {
            await BeginTransactionAsync(isolationLevel);
            await operation();
            await CommitAsync();
        }
        catch
        {
            await RollbackAsync();
            throw;
        }
    }

    public void Dispose()
    {
        _currentTransaction?.Dispose();
    }
}
