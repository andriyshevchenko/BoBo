# BoBo

[![CI](https://github.com/OWNER/BoBo/actions/workflows/dotnet.yml/badge.svg)](https://github.com/OWNER/BoBo/actions/workflows/dotnet.yml)

Readable stack trace embedded into ASP.NET apps.

## Features

- JSON output
- XML output
- NuGet package
- Documentation

## Installation

```bash
dotnet add package BoBo --version 1.0.0
```

## Where it may be useful

- quick-checking internal server error cause during full-stack development
- displaying internal server errors on client application
- storing exception data in some 3rd party storage/API

## Build

Run tests to verify the library:

```bash
dotnet test
```

## Usage

### XML

```csharp
app.UseMiddleware(typeof(Catch),
    System.Net.HttpStatusCode.InternalServerError,
    new WithContentType("text/xml"),
    new XmlDigest(
        new BoBo.Formatting.XML.RecursiveDump(
            new XmlDump()
        )
    )
);
```

### JSON

```csharp
app.UseMiddleware(typeof(Catch),
    System.Net.HttpStatusCode.InternalServerError,
    new WithContentType("application/json"),
    new JsonDigest(
        new BoBo.Formatting.JSON.RecursiveDump(
            new JsonDump()
        )
    )
);
```

## Contributing

- pull requests are welcome!
- issues are welcome!
- feature requests are especially welcome!
- contact me via [Telegram](https://t.me/e86356bc3414991aabee873f5) or [Email](mailto:shewchenkoandriy@gmail.com)

