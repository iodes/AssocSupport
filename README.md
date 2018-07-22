# AssocSupport
[![Release](https://img.shields.io/badge/Release-v1.0.0-brightgreen.svg)](https://github.com/iodes/AssocSupport/releases)
[![NuGet](https://img.shields.io/badge/NuGet-v1.0.0-blue.svg)](https://www.nuget.org/packages/AssocSupport/)  
Windows File Associations Support Library

# Getting Started
```csharp
var software = new Software
{
    Name = "My App",
    CompanyName = "My Company",
    Description = "Simple Description",
    Icon = "Logo.ico"
};

software.Identifiers.Add(new ProgrammaticID
{
    Type = new FileType
    {
        Extension = ".abc",
        ContentType = "application/abc",
        PerceivedType = PerceivedTypes.Application
    },
    Command = new ShellCommand
    {
        Path = "My App Path",
        Argument = "%1"
    },
    Description = "Simple File Description",
    Icon = "FileLogo.ico"
});

AssociationUtility.Register(software);
```

# Features
* Fully compatible with the latest versions of Windows
* Seamless integration with native system UI
* Support multiple file Identifiers
