# DiscordRPC-NativeAOT

[English](#english) | [中文](#中文)

## English

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

---

## 中文

一个高性能、**Native AOT 兼容** 的 Discord Rich Presence 客户端，适用于 .NET 9.0。

[![NuGet](https://img.shields.io/nuget/v/DiscordRPC-NativeAOT.svg)](https://www.nuget.org/packages/DiscordRPC-NativeAOT)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

## ✨ 特性

- ✅ **Native AOT 兼容** - 零反射，完全可裁剪
- ✅ **System.Text.Json 源生成** - 优化的 JSON 序列化
- ✅ **零外部依赖** - 无需第三方库
- ✅ **跨平台** - 支持 Windows、Linux、macOS
- ✅ **修复 Discord API 兼容性** - 自定义转换器处理 Discord 的字符串转数字字段

## 📦 安装

```bash
dotnet add package DiscordRPC-NativeAOT
```

## 🚀 快速开始

```csharp
using DiscordRPC;
using DiscordRPC.Logging;

var client = new DiscordRpcClient("你的应用ID")
{
    Logger = new ConsoleLogger() { Level = LogLevel.Info }
};

client.OnReady += (sender, e) =>
{
    Console.WriteLine($"已连接到 Discord：{e.User.Username}");
};

client.Initialize();

client.SetPresence(new RichPresence()
{
    Details = "正在玩游戏",
    State = "对战中",
    Timestamps = new Timestamps()
    {
        Start = DateTime.UtcNow
    },
    Assets = new Assets()
    {
        LargeImageKey = "large_image",
        LargeImageText = "我的游戏",
        SmallImageKey = "small_image",
        SmallImageText = "10 级"
    }
});

// 保持程序运行
Console.ReadLine();
client.Dispose();
```

## 🔧 与源仓库的改动

此 Fork 基于 [Lachee/discord-rpc-csharp](https://github.com/Lachee/discord-rpc-csharp)，修复了以下 Native AOT 兼容性问题：

### v1.0.1 - Native AOT JSON 反序列化修复

**问题**：Discord API 返回某些字段为字符串（如 `"id":"1164487009784569939"`、`"discriminator":"0"`、`"flags":0`），但库期望数值类型，导致在 Native AOT 模式下 `System.Text.Json` 反序列化失败。

**解决方案**：添加自定义 JSON 转换器：

1. **`UlongStringConverter`** - 处理 Discord 雪花 ID
   ```csharp
   // Discord 返回："id":"1164487009784569939"
   // C# 期望：ulong ID
   ```

2. **`IntStringConverter`** - 处理 discriminator 字段
   ```csharp
   // Discord 返回："discriminator":"0"
   // C# 期望：int Discriminator
   ```

3. **`EnumStringConverter<T>`** - 处理枚举字段（flags、premium_type）
   ```csharp
   // Discord 返回："flags":0 或 "flags":"0"
   // C# 期望：Flag Flags (枚举)
   ```

4. **命名管道超时修复** - 添加 5 秒连接超时防止无限阻塞：
   ```csharp
   _stream.Connect(5000); // 原为：Connect(0)
   ```

5. **枚举序列化** - 为 `Command` 和 `ServerEvent` 枚举添加 `JsonStringEnumConverter<T>` 以支持 Discord 协议。

### 与原版的主要差异

| 特性 | 原版 | 此 Fork |
|---------|----------|--------|
| Native AOT | ❌ 不支持 | ✅ 完全兼容 |
| JSON 库 | Newtonsoft.Json | System.Text.Json (源生成) |
| User 字段类型 | 运行时转换 | 编译时转换器 |
| 管道连接 | 无超时 | 5 秒超时 |
| 裁剪 | 不可裁剪 | 完全可裁剪 |

## 📋 要求

- .NET 9.0 或更高版本
- Discord 桌面客户端运行中

## 🛠️ Native AOT 发布

```bash
dotnet publish -c Release -r win-x64 --self-contained
```

## 📄 许可证

MIT 许可证 - 与原始 [discord-rpc-csharp](https://github.com/Lachee/discord-rpc-csharp) 项目相同。

## 🙏 致谢

- 原始库：[Lachee/discord-rpc-csharp](https://github.com/Lachee/discord-rpc-csharp)
- Native AOT 适配：[samse33](https://github.com/samse33)

## 🐛 问题反馈

如果遇到 Native AOT 兼容性问题，请在 [GitHub Issues](https://github.com/samse33/DiscordRPC-NativeAOT/issues) 上报告。
