using MessagePack.CodeGenerator.Generator;
using Mono.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace MessagePack.CodeGenerator
{
    class CommandlineArguments
    {
        public string InputPath { get; private set; }
        public string OutputPath { get; private set; }
        public string ConfigurationPath { get; private set; }
        public List<string> ConditionalSymbols { get; private set; }
        public string ResolverName { get; private set; }
        public string NamespaceRoot { get; private set; }
        public bool IsUseMap { get; private set; }
        public List<string> AssemblyPaths { get; set; }

        public bool IsParsed { get; set; }

        public CommandlineArguments(string[] args)
        {
            ConditionalSymbols = new List<string>();
            NamespaceRoot = "MessagePack";
            ResolverName = "GeneratedResolver";
            IsUseMap = false;
            AssemblyPaths = new List<string>();

            var option = new OptionSet()
            {
                { "i|input=", "[optional, default=empty]Input path of analyze csproj", x => { InputPath = x; } },
                { "o|output=", "[required]Output file path", x => { OutputPath = x; } },
                { "c|config=", "[required]Model configuration file", x => { ConfigurationPath = x; } },
                { "s|conditionalsymbol=", "[optional, default=empty]conditional compiler symbol", x => { ConditionalSymbols.AddRange(x.Split(',')); } },
                { "r|resolvername=", "[optional, default=GeneratedResolver]Set resolver name", x => { ResolverName = x; } },
                { "n|namespace=", "[optional, default=MessagePack]Set namespace root name", x => { NamespaceRoot = x; } },
                { "m|usemapmode", "[optional, default=false]Force use map mode serialization", x => { IsUseMap = true; } },
                { "a|assemblies=", "[required]Input assemblies", x => { AssemblyPaths.AddRange(x.Split(',')); } },
            };
            if (args.Length == 0)
            {
                goto SHOW_HELP;
            }
            else
            {
                option.Parse(args);

                if (AssemblyPaths.Count == 0 || OutputPath == null)
                {
                    Console.WriteLine("Invalid Argument:" + string.Join(" ", args));
                    Console.WriteLine();
                    goto SHOW_HELP;
                }

                IsParsed = true;
                return;
            }

            SHOW_HELP:
            Console.WriteLine("mpc arguments help:");
            option.WriteOptionDescriptions(Console.Out);
            IsParsed = false;
        }

        public string GetNamespaceDot()
        {
            return string.IsNullOrWhiteSpace(NamespaceRoot) ? "" : NamespaceRoot + ".";
        }
    }


    class Program
    {
        private static Stopwatch stopwatch;

        static void Main(string[] args)
        {
            var cmdArgs = new CommandlineArguments(args);
            if (!cmdArgs.IsParsed)
            {
                return;
            }

            // Generator Start...

            stopwatch = Stopwatch.StartNew();

            //Debugger.Launch();
            var collectedInfo = CollectUsingReflection(cmdArgs);

            Console.WriteLine("Output Generation Start");
            stopwatch.Restart();

            var objectFormatterTemplates = collectedInfo.ObjectInfo
                .GroupBy(x => x.Namespace)
                .Select(x => new FormatterTemplate()
                {
                    Namespace = cmdArgs.GetNamespaceDot() + "Formatters",
                    objectSerializationInfos = x.ToArray(),
                })
                .ToArray();

            var enumFormatterTemplates = collectedInfo.EnumInfo
                .GroupBy(x => x.Namespace)
                .Select(x => new EnumTemplate()
                {
                    Namespace = cmdArgs.GetNamespaceDot() + "Formatters",
                    enumSerializationInfos = x.ToArray()
                })
                .ToArray();

            var unionFormatterTemplates = collectedInfo.UnionInfo
                .GroupBy(x => x.Namespace)
                .Select(x => new UnionTemplate()
                {
                    Namespace = cmdArgs.GetNamespaceDot() + "Formatters",
                    unionSerializationInfos = x.ToArray()
                })
                .ToArray();

            var resolverTemplate = new ResolverTemplate()
            {
                Namespace = cmdArgs.GetNamespaceDot() + "Resolvers",
                FormatterNamespace = cmdArgs.GetNamespaceDot() + "Formatters",
                ResolverName = cmdArgs.ResolverName,
                registerInfos = collectedInfo.GenericInfo.Cast<IResolverRegisterInfo>()
                    .Concat(collectedInfo.EnumInfo)
                    .Concat(collectedInfo.UnionInfo)
                    .Concat(collectedInfo.ObjectInfo)
                    .ToArray()
            };

            var sb = new StringBuilder();
            sb.AppendLine(resolverTemplate.TransformText());
            sb.AppendLine();
            foreach (var item in enumFormatterTemplates)
            {
                var text = item.TransformText();
                sb.AppendLine(text);
            }
            sb.AppendLine();
            foreach (var item in unionFormatterTemplates)
            {
                var text = item.TransformText();
                sb.AppendLine(text);
            }
            sb.AppendLine();
            foreach (var item in objectFormatterTemplates)
            {
                var text = item.TransformText();
                sb.AppendLine(text);
            }

            Output(cmdArgs.OutputPath, sb.ToString());

            Console.WriteLine("String Generation Complete:" + stopwatch.Elapsed.ToString());
        }

        private static CollectedInfo CollectUsingReflection(CommandlineArguments cmdArgs)
        {
            Console.WriteLine("Loading assembly: {0}", cmdArgs.InputPath);

            var collector = new ReflectionTypeCollector(cmdArgs.AssemblyPaths, cmdArgs.ConfigurationPath);

            var infos = collector.Collect();

            Console.WriteLine("Serialization info collected {0}", stopwatch.Elapsed);

            return infos;
        }

        private static void Output(string path, string text)
        {
            path = path.Replace("global::", "");

            const string prefix = "[Out]";
            Console.WriteLine(prefix + path);

            var fi = new FileInfo(path);
            if (!fi.Directory.Exists)
            {
                fi.Directory.Create();
            }

            System.IO.File.WriteAllText(path, text, Encoding.UTF8);
        }
    }
}
