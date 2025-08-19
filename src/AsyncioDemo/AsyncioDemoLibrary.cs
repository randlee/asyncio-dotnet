using System.Runtime.CompilerServices;

namespace AsyncioDemo;

/// <summary>
/// Demonstration library for Python.NET integration showing various async patterns:
/// - Promise-based tasks
/// - Task-returning methods  
/// - Cancellable IAsyncEnumerable patterns
/// </summary>
public class AsyncioDemoLibrary
{
    #region Promise-based Tasks (using TaskCompletionSource)
    
    /// <summary>
    /// Creates a promise-based task that completes after the specified delay and returns the delay value.
    /// </summary>
    /// <param name="delayMilliseconds">Delay in milliseconds</param>
    /// <returns>TaskCompletionSource with Task that returns the delay value</returns>
    public async Task<int> CreatePromiseReturningInt(int delayMilliseconds)
    {
        // Start the async operation without blocking
        await Task.Delay(delayMilliseconds);
        return delayMilliseconds;
    }
    
    /// <summary>
    /// Creates a promise-based task that completes after the specified delay and returns a wrapped delay value.
    /// </summary>
    /// <param name="delayMilliseconds">Delay in milliseconds</param>
    /// <returns>TaskCompletionSource with Task that returns IntWrapper containing the delay value</returns>
    public async Task<IntWrapper> CreatePromiseReturningWrapper(int delayMilliseconds)
    {
        await Task.Delay(delayMilliseconds);
        return new IntWrapper(delayMilliseconds);
    }
    
    #endregion
    
    #region Task-returning Methods
    
    /// <summary>
    /// Returns a Task that completes after the specified delay and returns the delay value.
    /// </summary>
    /// <param name="delayMilliseconds">Delay in milliseconds</param>
    /// <returns>Task that returns the delay value</returns>
    public Task<int> GetTaskReturningInt(int delayMilliseconds)
    {
        return Task.Run(async () =>
        {
            await Task.Delay(delayMilliseconds);
            return delayMilliseconds;
        });
    }
    
    /// <summary>
    /// Returns a Task that completes after the specified delay and returns a wrapped delay value.
    /// </summary>
    /// <param name="delayMilliseconds">Delay in milliseconds</param>
    /// <returns>Task that returns IntWrapper containing the delay value</returns>
    public Task<IntWrapper> GetTaskReturningWrapper(int delayMilliseconds)
    {
        return Task.Run(async () =>
        {
            await Task.Delay(delayMilliseconds);
            return new IntWrapper(delayMilliseconds);
        });
    }
    
    #endregion
    
    #region Cancellable IAsyncEnumerable
    
    /// <summary>
    /// Returns an async enumerable that yields ascending integers with delays between each yield.
    /// </summary>
    /// <param name="count">Number of values to yield</param>
    /// <param name="delayMilliseconds">Delay between each yield in milliseconds</param>
    /// <param name="cancellationToken">Cancellation token to stop enumeration</param>
    /// <returns>IAsyncEnumerable that yields integers from 0 to count-1</returns>
    public async IAsyncEnumerable<int> GetAsyncEnumerableInt(
        int count, 
        int delayMilliseconds,
        [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        for (int i = 0; i < count; i++)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (i > 0) // Don't delay before the first item
                await Task.Delay(delayMilliseconds, cancellationToken);
            yield return i;
        }
    }
    
    /// <summary>
    /// Returns an async enumerable that yields wrapped ascending integers with delays between each yield.
    /// </summary>
    /// <param name="count">Number of values to yield</param>
    /// <param name="delayMilliseconds">Delay between each yield in milliseconds</param>
    /// <param name="cancellationToken">Cancellation token to stop enumeration</param>
    /// <returns>IAsyncEnumerable that yields IntWrapper objects containing integers from 0 to count-1</returns>
    public async IAsyncEnumerable<IntWrapper> GetAsyncEnumerableWrapper(
        int count, 
        int delayMilliseconds,
        [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        for (int i = 0; i < count; i++)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (i > 0) // Don't delay before the first item
                await Task.Delay(delayMilliseconds, cancellationToken);
            yield return new IntWrapper(i);
        }
    }
    
    #endregion
    
    #region Utility Methods for Python.NET Integration
    
    /// <summary>
    /// Helper method to create a CancellationTokenSource for Python integration.
    /// </summary>
    /// <param name="timeoutMilliseconds">Optional timeout in milliseconds</param>
    /// <returns>CancellationTokenSource that can be used with async enumerables</returns>
    public CancellationTokenSource CreateCancellationTokenSource(int? timeoutMilliseconds = null)
    {
        if (timeoutMilliseconds.HasValue)
        {
            return new CancellationTokenSource(timeoutMilliseconds.Value);
        }
        return new CancellationTokenSource();
    }
    
    /// <summary>
    /// Helper method to demonstrate async enumerable consumption from Python.
    /// Collects all values from an async enumerable into a list.
    /// </summary>
    /// <param name="source">The async enumerable source</param>
    /// <param name="cancellationToken">Optional cancellation token</param>
    /// <returns>List containing all yielded values</returns>
    public async Task<List<int>> CollectAsyncEnumerableInt(
        IAsyncEnumerable<int> source,
        CancellationToken cancellationToken = default)
    {
        var results = new List<int>();
        await foreach (var item in source.WithCancellation(cancellationToken))
        {
            results.Add(item);
        }
        return results;
    }
    
    /// <summary>
    /// Helper method to demonstrate async enumerable consumption from Python.
    /// Collects all values from an async enumerable into a list.
    /// </summary>
    /// <param name="source">The async enumerable source</param>
    /// <param name="cancellationToken">Optional cancellation token</param>
    /// <returns>List containing all yielded wrapper values</returns>
    public async Task<List<IntWrapper>> CollectAsyncEnumerableWrapper(
        IAsyncEnumerable<IntWrapper> source,
        CancellationToken cancellationToken = default)
    {
        var results = new List<IntWrapper>();
        await foreach (var item in source.WithCancellation(cancellationToken))
        {
            results.Add(item);
        }
        return results;
    }
    
    #endregion
}
