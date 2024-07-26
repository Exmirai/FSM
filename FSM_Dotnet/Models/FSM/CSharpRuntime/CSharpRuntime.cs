using FSM_Dotnet.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSM_Dotnet.Models.FSM.CSharpRuntime
{
    public class CSharpRuntime : IFSMRuntime
    {
        public IInvokable<T1> Compile<T1>(string sourceCode)
        {
            return new InvokableBlock<T1>(sourceCode);
        }

        public IInvokable<T1, R> Compile<T1, R>(string sourceCode)
        {
            return new InvokableBlock<T1, R>(sourceCode);
        }
    }
}
