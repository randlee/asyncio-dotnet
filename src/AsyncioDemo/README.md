# AsyncioDemo C# Library

A .NET 9 class library designed for Python.NET integration, demonstrating various async patterns for bridging C# and Python asynchronous programming.

## Classes

### `AsyncioDemoLibrary`
Main class containing demonstration methods for different async patterns.

### `IntWrapper`
Simple wrapper class around an integer value, used to demonstrate reference type returns.

## API Reference

### Promise-based Tasks (TaskCompletionSource)

#### `CreatePromiseReturningInt(int delayMilliseconds)`
- **Returns**: `TaskCompletionSource<int>`
- **Description**: Creates a promise that completes after the specified delay, returning the delay value

#### `CreatePromiseReturningWrapper(int delayMilliseconds)`
- **Returns**: `TaskCompletionSource<IntWrapper>`
- **Description**: Creates a promise that completes after the specified delay, returning a wrapped delay value

### Task-returning Methods

#### `GetTaskReturningInt(int delayMilliseconds)`
- **Returns**: `Task<int>`
- **Description**: Returns a Task that completes after the specified delay, returning the delay value

#### `GetTaskReturningWrapper(int delayMilliseconds)`
- **Returns**: `Task<IntWrapper>`
- **Description**: Returns a Task that completes after the specified delay, returning a wrapped delay value

### Cancellable IAsyncEnumerable

#### `GetAsyncEnumerableInt(int count, int delayMilliseconds, CancellationToken cancellationToken = default)`
- **Returns**: `IAsyncEnumerable<int>`
- **Description**: Yields ascending integers (0 to count-1) with specified delays between each yield

#### `GetAsyncEnumerableWrapper(int count, int delayMilliseconds, CancellationToken cancellationToken = default)`
- **Returns**: `IAsyncEnumerable<IntWrapper>`
- **Description**: Yields wrapped ascending integers (0 to count-1) with specified delays between each yield

### Utility Methods

#### `CreateCancellationTokenSource(int? timeoutMilliseconds = null)`
- **Returns**: `CancellationTokenSource`
- **Description**: Helper to create cancellation token sources for Python integration

#### `CollectAsyncEnumerableInt(IAsyncEnumerable<int> source, CancellationToken cancellationToken = default)`
- **Returns**: `Task<List<int>>`
- **Description**: Collects all values from an async enumerable into a list

#### `CollectAsyncEnumerableWrapper(IAsyncEnumerable<IntWrapper> source, CancellationToken cancellationToken = default)`
- **Returns**: `Task<List<IntWrapper>>`
- **Description**: Collects all wrapped values from an async enumerable into a list

## Building

```bash
dotnet build
```

## Usage from Python.NET

```python
import clr
clr.AddReference("AsyncioDemo")

from AsyncioDemo import AsyncioDemoLibrary, IntWrapper
import asyncio

# Create library instance
lib = AsyncioDemoLibrary()

# Promise-based example
promise = lib.CreatePromiseReturningInt(1000)
result = await promise.Task  # Will return 1000 after 1 second

# Task-based example
task = lib.GetTaskReturningInt(500)
result = await task  # Will return 500 after 0.5 seconds

# Async enumerable example
async_enum = lib.GetAsyncEnumerableInt(5, 200)
async for value in async_enum:
    print(value)  # Prints 0, 1, 2, 3, 4 with 200ms delays
```

## Features

- ✅ Promise-based tasks using TaskCompletionSource
- ✅ Standard Task-returning methods
- ✅ Cancellable IAsyncEnumerable implementations
- ✅ Both value types (int) and reference types (IntWrapper) examples
- ✅ Proper cancellation token support
- ✅ Helper methods for Python.NET integration
- ✅ .NET 9 target framework
- ✅ Nullable reference types enabled
- ✅ Comprehensive XML documentation
