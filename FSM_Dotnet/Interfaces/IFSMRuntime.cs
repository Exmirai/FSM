using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSM_Dotnet.Interfaces
{
    public interface IFSMRuntime
    {
        public IInvokable<T1> Compile<T1>(string sourceCode);

        public IInvokable<T1, R> Compile<T1, R>(string sourceCode);
    }
}
