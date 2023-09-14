# dotnet-sort
[![License](https://img.shields.io/badge/License-MIT-blue.svg?style=flat-square&logo=read-the-docs)](https://github.com/ramosisw/dotnet-sort/blob/master/LICENSE)
[![NuGet Version](https://img.shields.io/nuget/v/dotnet-sort.svg?style=flat-square&logo=nuget)](https://www.nuget.org/packages/dotnet-sort/)
[![NuGet Download](https://img.shields.io/nuget/dt/dotnet-sort.svg?style=flat-square&logo=nuget)](https://www.nuget.org/packages/dotnet-sort/)

A global .NET Core tool for ordering alphabetically, length, the references of packages or imports in your .NET Core and .NET Standard projects.

# Installation
```sh
dotnet tool install --global dotnet-sort
```


# Usage

```
Usage:
  dotnet-sort [options]

Options:
  -h, --help        Show command line help.
  -p, --path        Path where found the *.csproj or *.cs files (defaults to the current directory)
  -a <APPLY>        Apply to references or imports.
                    Examples:
                    Sorting references and imports on *.cs (default): -a=ri
                    Sorting only references on *.csproj: -a=r
                    Sorting only imports on *.cs: -a=i

  -s <MODE>         Sort by length/alphabetically
                    Examples:
                    alphabetically ascending (default): -s=a
                    alphabetically descendently: -s=ad
                    length ascending: -s=l
                    length descendently: -s=ld
```

# Examples

## Command to sort references `-a=r` alphabetically ascending `-s=a`
```sh
dotnet-sort --path=Project.csproj -a=r -s=a
```
`Project.csproj`
```xml
<Project Sdk="Microsoft.NET.Sdk">
  <ItemGroup>
    <PackageReference Include="Grpc" Version="1.19.0" />
    <PackageReference Include="Google.Protobuf" Version="3.7.0" />
    <PackageReference Include="Google.Protobuf.Tools" Version="3.7.0" />
  </ItemGroup>
</Project>
```
```xml
<Project Sdk="Microsoft.NET.Sdk">
  <ItemGroup>
    <PackageReference Include="Google.Protobuf" Version="3.7.0" />
    <PackageReference Include="Google.Protobuf.Tools" Version="3.7.0" />
    <PackageReference Include="Grpc" Version="1.19.0" />
  </ItemGroup>
</Project>
```

## Command to sort imports `-a=i` by length descendently `-s=ld`
```sh
dotnet-sort --path=Code.cs -a=i -s=ld
```

`Code.cs`
```cs
using System.Text.RegularExpressions;     
using System.Linq;                        
using System;                             
using System.IO;                          
using System.Text;                        
using System.Xml.XPath;                   
```
```cs
using System.Text.RegularExpressions;
using System.Xml.XPath;
using System.Text;
using System.Linq;
using System.IO;
using System;
```

## Command to sort imports `-a=i` by length ascending `-s=la`
```sh
dotnet-sort --path=Code.cs -a=i -s=ld
```

`Code.cs`
```cs
using System.Text.RegularExpressions;     
using System.Linq;                        
using System;                             
using System.IO;                          
using System.Text;                        
using System.Xml.XPath;                   
```
```cs
using System;
using System.IO;
using System.Text;
using System.Linq;
using System.Xml.XPath;
using System.Text.RegularExpressions;
```



















