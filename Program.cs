using System.Reflection;
using System;

namespace dotnet_sort
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(String.Join(' ', args));
            if (args.Length == 0)
            {
                WriteHelp();
                return;
            }
        }

        static void WriteHelp()
        {
            var versionString = Assembly.GetEntryAssembly()
                                        .GetCustomAttribute<AssemblyInformationalVersionAttribute>()
                                        .InformationalVersion
                                        .ToString();

            Console.WriteLine($"dotnet-sort v{versionString}");
            Console.WriteLine("A global .NET Core tool for ordering alphabetically, length, the references of packages or imports in your .NET Core and .NET Standard projects.");
            Console.WriteLine("");
            Console.WriteLine("Usage:");
            Console.WriteLine("  dotnet-sort [options]");
            Console.WriteLine("");
            Console.WriteLine("Options:");
            Console.WriteLine("  -h, --help        Show command line help.");
            Console.WriteLine("  -p, --path        Path where found the *.csproj or *.cs files (defaults to the current directory)");
            Console.WriteLine("  -s <MODE>         Sorting modes");
            Console.WriteLine("                    Examples:");
            Console.WriteLine("                    Sorting only references on *.csproj: -s=r");
            Console.WriteLine("                    Sorting only imports on *.cs: -s=i");
            Console.WriteLine("                    Sorting references and imports on *.cs: -s=ri");
            Console.WriteLine("");
            Console.WriteLine("  -l                Sort by length ascending");
            Console.WriteLine("  +l                Sort by length descendently");
            Console.WriteLine("  -a                Sort alphabetically ascending");
            Console.WriteLine("  +a                Sort alphabetically descendently");
        }
    }
}
