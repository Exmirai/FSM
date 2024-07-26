using FSM_Dotnet.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FSM_Dotnet.Models.FSM.CSharpRuntime
{
    public class InvokableBlock<T> : IInvokable<T>
    {
        private readonly Assembly _assembly;
        private readonly string _className;
        public InvokableBlock(string sourceCode)
        {
            _className = $"Compiled_{Guid.NewGuid().ToString().Split("-")[0]}";
            var processedSource = Compiler.ProcessProgramTemplate(sourceCode, Array.Empty<string>(), _className, null, [typeof(T)]);
            _assembly = Compiler.CompileCodeToAssembly(processedSource);
        }

        public void Invoke(T param)
        {
            var method = _assembly.DefinedTypes.FirstOrDefault(p => p.Name == _className)!.GetMethod("Invokable_EntryPoint");
            if (method == null)
            {
                throw new Exception(""); // TODO
            }
            method.Invoke(null, [param]);
        }
    }
    public class InvokableBlock<T, R> : IInvokable<T, R>
    {
        private readonly Assembly _assembly;
        private readonly string _className;
        public InvokableBlock(string sourceCode)
        {
            _className = $"Compiled_{Guid.NewGuid().ToString().Split("-")[0]}";
            var processedSource = Compiler.ProcessProgramTemplate(sourceCode, Array.Empty<string>(), _className, typeof(R), [typeof(T)]);
            _assembly = Compiler.CompileCodeToAssembly(processedSource);
        }

        public R Invoke(T param)
        {
            var method = _assembly.DefinedTypes.FirstOrDefault(p => p.Name == _className)!.GetMethod("Invokable_EntryPoint");
            if (method == null)
            {
                throw new Exception(""); // TODO
            }
            return (R)method.Invoke(null, [param])!;
        }
    }

    public class InvokableBlock<T1, T2, R> : IInvokable<T1,T2, R>
    {
        private readonly Assembly _assembly;
        private readonly string _className;
        public InvokableBlock(string sourceCode)
        {
            _className = $"Compiled_{Guid.NewGuid().ToString().Split("-")[0]}";
            var processedSource = Compiler.ProcessProgramTemplate(sourceCode, Array.Empty<string>(), _className, typeof(R), [typeof(T1), typeof(T2)]);
            _assembly = Compiler.CompileCodeToAssembly(processedSource);
        }

        public R Invoke(T1 param1, T2 param2)
        {
            var method = _assembly.DefinedTypes.FirstOrDefault(p => p.Name == _className)!.GetMethod("Invokable_EntryPoint");
            if (method == null)
            {
                throw new Exception(""); // TODO
            }
            return (R)method.Invoke(null, [param1, param2])!;
        }
    }

    public class InvokableBlock<T1, T2, T3, R> : IInvokable<T1, T2, T3, R>
    {
        private readonly Assembly _assembly;
        private readonly string _className;
        public InvokableBlock(string sourceCode)
        {
            _className = $"Compiled_{Guid.NewGuid().ToString().Split("-")[0]}";
            var processedSource = Compiler.ProcessProgramTemplate(sourceCode, Array.Empty<string>(), _className, typeof(R), [typeof(T1), typeof(T2), typeof(T3)]);
            _assembly = Compiler.CompileCodeToAssembly(processedSource);
        }

        public R Invoke(T1 param1, T2 param2, T3 param3)
        {
            var method = _assembly.DefinedTypes.FirstOrDefault(p => p.Name == _className)!.GetMethod("Invokable_EntryPoint");
            if (method == null)
            {
                throw new Exception(""); // TODO
            }
            return (R)method.Invoke(null, [param1, param2, param3])!;
        }
    }
}
