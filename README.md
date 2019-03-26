# dotnet-sort
[![License](https://img.shields.io/badge/License-MIT-blue.svg?style=flat-square)](https://github.com/ramosisw/dotnet-sort/blob/master/LICENSE)
[![Source](https://img.shields.io/badge/Source-Github-purple.svg?style=flat-square)](https://github.com/ramosisw/dotnet-sort)

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
