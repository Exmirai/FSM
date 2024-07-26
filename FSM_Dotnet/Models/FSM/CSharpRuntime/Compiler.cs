using FSM_Dotnet.Interfaces;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Basic.Reference.Assemblies;

namespace FSM_Dotnet.Models.FSM.CSharpRuntime
{
    public static class Compiler
    {
        private const string _programTemplate = """
            using System;
            using FSM_Dotnet.Interfaces;
            using FSM_Dotnet.Models;
            {0}

            namespace FSM_Dotnet.CSharpRuntime.Compiled {{

                public static class {1} {{

                    public static {2} Invokable_EntryPoint ({3}) {{

                        {4}
                        
                    }}
                }}

            }}
            """;

        public static Assembly CompileCodeToAssembly(string sourceCode)
        {
            var syntaxTree = CSharpSyntaxTree.ParseText(sourceCode);

            var assemblyName = Path.GetRandomFileName();
            var references = new List<MetadataReference>
            {
                MetadataReference.CreateFromFile(typeof(IEntityWithState).Assembly.Location),
            };

            references.AddRange(Basic.Reference.Assemblies.Net80.ReferenceInfos.All.Select(d => d.Reference));

            var compilation = CSharpCompilation.Create(
                assemblyName,
                syntaxTrees: new[] { syntaxTree },
                references: references,
                options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

            var memStream = new MemoryStream();
            var result = compilation.Emit(memStream);

            if (!result.Success)
            {
                var errors = result.Diagnostics.Where(diagnostic =>
                            diagnostic.IsWarningAsError ||
                            diagnostic.Severity == DiagnosticSeverity.Error);

                throw new Exception("Failed to compile");
            }
            memStream.Seek(0, SeekOrigin.Begin);

            var assembly = Assembly.Load(memStream.ToArray());

            return assembly;
        }


        public static string ProcessProgramTemplate(
            string rawSourceCode,
            string[] imports,
            string className,
            Type? returnType,
            Type[] parameterTypes
            )
        {
            var import = string.Join(";\n", imports);
            var retType = returnType == null ? "void" : returnType.ToString();
            var parameters = string.Join(", ", Enumerable.Range(0, parameterTypes.Length).Select(i => $"{parameterTypes[i].Name} p{i}"));
            return string.Format(
                _programTemplate,
                import,
                className,
                retType,
                parameters,
                rawSourceCode
            );
        }
    }
}
