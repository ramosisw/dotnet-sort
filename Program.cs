using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using System.Xml.XPath;
using System.Xml.Xsl;
using System.Xml;
using System;

namespace dotnet_sort
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Contains("-h") || args.Contains("--help"))
            {
                WriteHelp();
                return;
            }

            try
            {
                var options = ParseArgs(args);
                Console.WriteLine(options);
                var codeFiles = new string[] { };
                var projectFiles = new string[] { };

                if (options.To == ApplyEnum.IMPORTS || options.To == ApplyEnum.REFERENCES_IMPORTS)
                    codeFiles = options.PathIsFile ? new[] { options.Path } : Directory.GetFiles(options.Path, "*.cs", SearchOption.AllDirectories);

                if (options.To == ApplyEnum.REFERENCES || options.To == ApplyEnum.REFERENCES_IMPORTS)
                    projectFiles = options.PathIsFile ? new[] { options.Path } : Directory.GetFiles(options.Path, "*.csproj", SearchOption.AllDirectories);

                foreach (var codeFile in codeFiles)
                {
                    SortImports(codeFile, options.Sort);
                }

                var xslt = GetCompiledTransform(options.Sort);
                foreach (var projectFile in projectFiles)
                {
                    SortReferences(projectFile, xslt);
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message + "\n");
                WriteHelp();
            }
        }

        private static XslCompiledTransform GetCompiledTransform(SortEnum sort)
        {
            string xsltFile = "";
            switch (sort)
            {
                case SortEnum.ALPHABETICALLY_ASCENDING: xsltFile = "Sort-a.xsl"; break;
                case SortEnum.ALPHABETICALLY_DESCENDENTLY: xsltFile = "Sort-ad.xsl"; break;
                case SortEnum.LENGTH_ASCENDING: xsltFile = "Sort-l.xsl"; break;
                case SortEnum.LENGTH_DESCENDENTLY: xsltFile = "Sort-ld.xsl"; break;
            }
            var xslt = new XslCompiledTransform();
            var assembly = Assembly.GetExecutingAssembly();
            using (var stream = assembly.GetManifestResourceStream("dotnet_sort." + xsltFile))
            {
                using (var reader = XmlReader.Create(stream))
                {

                    xslt.Load(reader);
                    return xslt;
                }
            }
        }

        private static void SortReferences(string filePath, XslCompiledTransform xslt)
        {
            Console.Write($"Sorting.. {filePath}");
            using (StringWriter sw = new StringWriter())
            {
                var doc = XDocument.Parse(System.IO.File.ReadAllText(filePath));
                xslt.Transform(doc.CreateNavigator(), null, sw);
                File.WriteAllText(filePath, sw.ToString());
            }
            Console.WriteLine("\t done");
        }

        private static void SortImports(string filePath, SortEnum sort)
        {
            Console.Write($"Sorting.. {filePath}");
            var file = new StreamReader(filePath);
            var linesToSort = new List<string>();
            var restOfFile = "";

            string line;
            string pattern = @"^using.*;";
            while ((line = file.ReadLine()) != null && Regex.Match(line, pattern).Success)
            {
                linesToSort.Add(line);
            }

            restOfFile = line + "\r\n" + file.ReadToEnd();
            file.Close();

            switch (sort)
            {
                case SortEnum.ALPHABETICALLY_ASCENDING:
                    linesToSort = linesToSort.OrderBy(x => x).ToList(); break;
                case SortEnum.ALPHABETICALLY_DESCENDENTLY:
                    linesToSort = linesToSort.OrderByDescending(x => x).ToList(); break;
                case SortEnum.LENGTH_ASCENDING:
                    linesToSort = linesToSort.OrderBy(x => x.Length).ToList(); break;
                case SortEnum.LENGTH_DESCENDENTLY:
                    linesToSort = linesToSort.OrderByDescending(x => x.Length).ToList(); break;
            }
            //Write lines

            using (StreamWriter writer = new System.IO.StreamWriter(filePath))
            {
                foreach (var strLine in linesToSort)
                    writer.WriteLine(strLine);
                writer.Write(restOfFile);
                writer.Close();
            }
            Console.WriteLine("\t done");
        }

        private static Apply ParseArgs(string[] args)
        {
            var validOptions = new[] { "-p", "--path", "-s", "-a" };
            var options = new Apply();
            foreach (var argItem in args)
            {
                var kv = argItem.Split("=");
                if (!validOptions.Contains(kv[0])) throw new ArgumentException($"Unknown option {argItem}");
                if (kv.Count() != 2) continue;
                switch (kv[0])
                {
                    case "-p":
                    case "--path":
                        options.Path = kv[1];
                        break;
                    case "-a":
                        switch (kv[1])
                        {
                            case "r": options.To = ApplyEnum.REFERENCES; break;
                            case "i": options.To = ApplyEnum.IMPORTS; break;
                            case "ri":
                            case "ir": options.To = ApplyEnum.REFERENCES_IMPORTS; break;
                            default: throw new ArgumentException($"Unknown value to option -a={kv[1]}");
                        }
                        break;
                    case "-s":
                        switch (kv[1])
                        {
                            case "a": options.Sort = SortEnum.ALPHABETICALLY_ASCENDING; break;
                            case "ad": options.Sort = SortEnum.ALPHABETICALLY_DESCENDENTLY; break;
                            case "l": options.Sort = SortEnum.LENGTH_ASCENDING; break;
                            case "ld": options.Sort = SortEnum.LENGTH_DESCENDENTLY; break;
                            default: throw new ArgumentException($"Unknown value to option -l={kv[1]}");
                        }
                        break;
                }

            }

            options.Path = Path.GetFullPath(options.Path);

            var isFile = File.Exists(options.Path);
            var isDirectory = Directory.Exists(options.Path);

            if (isFile && options.To == ApplyEnum.REFERENCES && !Path.GetExtension(options.Path).Equals(".csproj", StringComparison.OrdinalIgnoreCase))
                throw new ArgumentException($"Only accept *.csproj files, actual {options.Path}.");
            if (!isFile && !isDirectory)
                throw new ArgumentException($"The path {options.Path} not found.");
            if (isFile && options.To == ApplyEnum.REFERENCES_IMPORTS)
                throw new ArgumentException($"The path {options.Path} is a file, expected directory.");

            options.PathIsFile = isFile;
            return options;
        }

        private static void WriteHelp()
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
            Console.WriteLine("                    Sorting only references on *.csproj: -a=r");
            Console.WriteLine("                    Sorting only imports on *.cs: -a=i");
            Console.WriteLine("                    Sorting references and imports on *.cs: -a=ri");
            Console.WriteLine("");
            Console.WriteLine("  -s <MODE>         Sort by length/alphabetically");
            Console.WriteLine("                    Examples:");
            Console.WriteLine("                    alphabetically ascending (default): -s=a");
            Console.WriteLine("                    alphabetically descendently: -s=ad");
            Console.WriteLine("                    length ascending: -s=l");
            Console.WriteLine("                    length descendently: -s=ld");
        }
    }
}
