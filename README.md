# dotnet-sort
[![License](https://img.shields.io/badge/license-MIT-blue.svg?style=flat-square)](LICENSE)
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
  -s <APPLY>        Apply to references or imports.
                    Examples:
                    Sorting only references on *.csproj: -s=r
                    Sorting only imports on *.cs: -s=i
                    Sorting references and imports on *.cs: -s=ri

  -l <TYPE>         Sort by length
                    Examples:
                    ascending (default): -l=a
                    descendently: -l=d

  -a <TYPE>         Sort alphabetically
                    Examples: 
                    ascending (default): -a=a
                    descendently: -a=d
```
