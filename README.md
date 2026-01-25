# DiscordRPC-NativeAOT

A high-performance, **Native AOT compatible** Discord Rich Presence client for .NET 9.0.

[![NuGet](https://img.shields.io/nuget/v/DiscordRPC-NativeAOT.svg)](https://www.nuget.org/packages/DiscordRPC-NativeAOT)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

## ✨ Features

- ✅ **Native AOT Compatible** - Zero reflection, fully trimmable
- ✅ **System.Text.Json Source Generation** - Optimized JSON serialization
- ✅ **Zero External Dependencies** - No third-party libraries required
- ✅ **Cross-Platform** - Windows, Linux, macOS support
- ✅ **Fixed Discord API Compatibility** - Custom converters for Discord's string-to-number fields

## 📦 Installation

```bash
dotnet add package DiscordRPC-NativeAOT
```

## 🚀 Quick Start

```csharp
using DiscordRPC;
using DiscordRPC.Logging;

var client = new DiscordRpcClient("YOUR_APPLICATION_ID")
{
    Logger = new ConsoleLogger() { Level = LogLevel.Info }
};

client.OnReady += (sender, e) =>
{
    Console.WriteLine($"Connected to Discord as {e.User.Username}");
};

client.Initialize();

client.SetPresence(new RichPresence()
{
    Details = "Playing a game",
    State = "In a match",
    Timestamps = new Timestamps()
    {
        Start = DateTime.UtcNow
    },
    Assets = new Assets()
    {
        LargeImageKey = "large_image",
        LargeImageText = "My Game",
        SmallImageKey = "small_image",
        SmallImageText = "Level 10"
    }
});

// Keep application running
Console.ReadLine();
client.Dispose();
```

## 🔧 Changes from Original Repository

This fork is based on [Lachee/discord-rpc-csharp](https://github.com/Lachee/discord-rpc-csharp) with the following Native AOT compatibility fixes:

### v1.0.1 - Native AOT JSON Deserialization Fixes

**Problem**: Discord API returns certain fields as strings (e.g., `"id":"1164487009784569939"`, `"discriminator":"0"`, `"flags":0`) but the library expects numeric types, causing `System.Text.Json` deserialization failures in Native AOT mode.

**Solution**: Added custom JSON converters:

1. **`UlongStringConverter`** - Handles Discord snowflake IDs
   ```csharp
   // Discord returns: "id":"1164487009784569939"
   // C# expects: ulong ID
   ```

2. **`IntStringConverter`** - Handles discriminator field
   ```csharp
   // Discord returns: "discriminator":"0"
   // C# expects: int Discriminator
   ```

3. **`EnumStringConverter<T>`** - Handles enum fields (flags, premium_type)
   ```csharp
   // Discord returns: "flags":0 or "flags":"0"
   // C# expects: Flag Flags (enum)
   ```

4. **Named Pipe Timeout Fix** - Added 5-second connection timeout to prevent infinite blocking:
   ```csharp
   _stream.Connect(5000); // Was: Connect(0)
   ```

5. **Enum Serialization** - Added `JsonStringEnumConverter<T>` to `Command` and `ServerEvent` enums for proper Discord protocol compatibility.

### Key Differences from Original

| Feature | Original | This Fork |
|---------|----------|-----------|
| Native AOT | ❌ Not supported | ✅ Fully compatible |
| JSON Library | Newtonsoft.Json | System.Text.Json (Source Gen) |
| User Field Types | Runtime conversion | Compile-time converters |
| Pipe Connection | No timeout | 5-second timeout |
| Trimming | Not trimmable | Fully trimmable |

## 📋 Requirements

- .NET 9.0 or later
- Discord Desktop Client running

## 🛠️ Native AOT Publishing

```bash
dotnet publish -c Release -r win-x64 --self-contained
```

## 📄 License

MIT License - Same as the original [discord-rpc-csharp](https://github.com/Lachee/discord-rpc-csharp) project.

## 🙏 Credits

- Original Library: [Lachee/discord-rpc-csharp](https://github.com/Lachee/discord-rpc-csharp)
- Native AOT Adaptation: [samse33](https://github.com/samse33)

## 🐛 Issues

If you encounter any issues with Native AOT compatibility, please report them on [GitHub Issues](https://github.com/samse33/DiscordRPC-NativeAOT/issues).
