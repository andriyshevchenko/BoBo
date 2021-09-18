# BoBo
Readable stack trace embedded into ASP.NET app
## Features
- JSON output
- XML output

## Where it may be useful
- quick-checking internal server error cause during full-stack development
- displaying internal server errors on client application
- storing exception data in some 3rd party storage/API

## Code examples

### Catch an exception and build XML response with `500` status code:
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
#### Generated XML
```xml
<Exception>
  <Message>wow</Message>
  <Dump>
    <Frame>
      <File>C:\projects\BoBo\BoBo.ASPNETCore.TestEndpoints\Controllers\SampleController.cs</File>
      <Method>Get</Method>
      <LineNumber>14</LineNumber>
    </Frame>
    <Frame>
      <File>C:\projects\BoBo\BoBo.ASPNETCore\Middleware\Catch.cs</File>
      <Method>MoveNext</Method>
      <LineNumber>31</LineNumber>
    </Frame>
  </Dump>
</Exception>
```

### Catch an exception and build detailed JSON response with `500` status code

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
#### Generated JSON
```json
{
  "Footprint": [
    {
      "File": "C:\\projects\\BoBo\\BoBo.ASPNETCore.TestEndpoints\\Controllers\\SampleController.cs",
      "Method": "Get",
      "LineNumber": 14
    },
    {
      "File": "C:\\projects\\BoBo\\BoBo.ASPNETCore\\Middleware\\Catch.cs",
      "Method": "MoveNext",
      "LineNumber": 31
    }
  ],
  "Message": "wow"
}
```
### Catch an exception and build simple JSON response with `500` status code:

```csharp
app.UseMiddleware(typeof(Catch),
    System.Net.HttpStatusCode.InternalServerError,
    new WithContentType("application/json"),
    new JsonDigest(
        new BoBo.Formatting.JSON.RecursiveDump(
            new BoBo.Formatting.JSON.BasicDump()
        )
    )
);
```
#### Generated JSON
```json
{
  "Footprint": "File: C:\\projects\\BoBo\\BoBo.ASPNETCore.TestEndpoints\\Controllers\\SampleController.cs
                Method: Get
                LineNumber: 14
                ----------> 
                File: C:\\projects\\BoBo\\BoBo.ASPNETCore\\Middleware\\Catch.cs
                Method: MoveNext
                LineNumber: 31",
  "Message": "wow"
}
```
