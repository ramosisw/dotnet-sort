using System.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace dotnet_sort
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Args [{0}]", String.Join(", ", args));
            if (args.Length == 0 || args.Contains("-h") || args.Contains("--help"))
            {
                WriteHelp();
                return;
            }
            ValidateArgs(args);
        }

        static void ValidateArgs(string[] args)
        {
            var programArgs = new List<ProgramArgs>{
                new ProgramArgs(new []{"-h", "--help"}),
                new ProgramArgs(new []{"-p", "--path"}),
                new ProgramArgs(new []{"-s"}),
                new ProgramArgs(new []{"-l", "+l"}),
                new ProgramArgs(new []{"-a", "+a"})
            };

            //count args
            foreach (var programArg in programArgs)
            {
                foreach (var arg in args)
                {
                    var keyValueArg = arg.Split('=');
                    if (programArg.Keys.Contains(keyValueArg[0]))
                    {
                        programArg.Count++;
                        if (programArg.Count == 1 && keyValueArg.Count() == 2)
                        {
                            programArg.Value = keyValueArg[1];
                        }
                    }
                }
            }

            Console.WriteLine("Found\n");
            foreach (var programArg in programArgs)
            {
                Console.WriteLine("{0} {1} [{2}]", programArg.Count, programArg.Keys[0], programArg.Value);
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
            Console.WriteLine("  -p, --path        Path where found the *.csproj or *.cs files (defaults to the current directory).");
            Console.WriteLine("  -a <APPLY>        Apply to references or imports.");
            Console.WriteLine("                    Examples:");
            Console.WriteLine("                    Sorting only references on *.csproj: -s=r");
            Console.WriteLine("                    Sorting only imports on *.cs: -s=i");
            Console.WriteLine("                    Sorting references and imports on *.cs: -s=ri");
            Console.WriteLine("");
            Console.WriteLine("  -l <TYPE>         Sort by length");
            Console.WriteLine("                    Examples:");
            Console.WriteLine("                    ascending (default): -l=a");
            Console.WriteLine("                    descendently: -l=d");
            Console.WriteLine("");
            Console.WriteLine("  -a <TYPE>         Sort alphabetically");
            Console.WriteLine("                    Examples: ");
            Console.WriteLine("                    ascending (default): -a=a");
            Console.WriteLine("                    descendently: -a=d");
        }
    }
}
