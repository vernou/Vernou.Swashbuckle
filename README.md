# Vernou.Swashbuckle

Library to extend Swashbuckle.AspNetCore

## Getting started

Install the package [Vernou.Swashbuckle](https://www.nuget.org/packages/Vernou.Swashbuckle) :

```sh
dotnet add package Vernou.Swashbuckle
```

## Operation Filters

In Swashbuckle, [Operation Filters](https://github.com/domaindrivendev/Swashbuckle.AspNetCore?tab=readme-ov-file#operation-filters) can modify the operation in the Open Api Specification generated.

This library has many operation filters that you can use like :
```csharp
builder.Services
    .AddSwaggerGen(options =>
    {
        ...
        options.OperationFilter<AutomaticBadRequest>();
    });
```

### AutomaticBadRequest

The operation filter `AutomaticBadRequest` add a bad request response with the schema [`ValidationProblemDetails`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.validationproblemdetails) to operations generated from an controller action that had a parameter and can return a automatic bad request.

More information about automatic bad request in the Microsoft documentation :
- [Create web APIs with ASP.NET Core > Automatic HTTP 400 responses](https://learn.microsoft.com/en-us/aspnet/core/web-api#automatic-http-400-responses)
