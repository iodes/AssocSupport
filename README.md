# AssocSupport
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
