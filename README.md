# asyncio-dotnet

A Python library that brings asyncio-style programming to .NET integration scenarios.

## Project Structure

```
├── src/                           # C# .NET 9 source code
│   ├── AsyncioDemo.sln           # Visual Studio solution file
│   ├── global.json               # .NET SDK version configuration
│   └── AsyncioDemo/              # C# class library project
│       ├── AsyncioDemo.csproj    # Project file
│       ├── AsyncioDemoLibrary.cs # Main async demo library
│       ├── IntWrapper.cs         # Reference type wrapper
│       └── README.md             # C# library documentation
└── README.md                     # This file
```

## C# Library Features

The `AsyncioDemoLibrary` provides examples of:

- **Promise-based tasks** using TaskCompletionSource
- **Task-returning methods** with Task.Run
- **Cancellable IAsyncEnumerable** implementations
- Both value types (int) and reference types (IntWrapper)
- Proper cancellation token support
- Python.NET integration helpers

## Building the C# Library

```bash
cd src
dotnet restore
dotnet build
```

## Usage from Python.NET

```python
import clr
clr.AddReference("path/to/AsyncioDemo.dll")

from AsyncioDemo import AsyncioDemoLibrary, IntWrapper
import asyncio

async def main():
    lib = AsyncioDemoLibrary()
    
    # Promise-based example
    promise = lib.CreatePromiseReturningInt(1000)
    result = await promise.Task  # Returns 1000 after 1 second
    
    # Task-based example
    task = lib.GetTaskReturningInt(500)
    result = await task  # Returns 500 after 0.5 seconds
    
    # Async enumerable example
    cts = lib.CreateCancellationTokenSource()
    async_enum = lib.GetAsyncEnumerableInt(5, 200, cts.Token)
    async for value in async_enum:
        print(value)  # Prints 0, 1, 2, 3, 4 with 200ms delays

if __name__ == "__main__":
    asyncio.run(main())
```

## Requirements

- .NET 9.0 SDK
- Python.NET for Python integration
- Visual Studio 2022 or JetBrains Rider (recommended)

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

## License

MIT License
