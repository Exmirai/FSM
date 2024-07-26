using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSM_Dotnet.Interfaces
{
    public interface IInvokable<T>
    {
        public void Invoke(T param);
    }
    public interface IInvokable<T, R>
    {
        public R Invoke(T param);
    }

    public interface IInvokable<T1, T2, R> 
    {
        public R Invoke(T1 param1, T2 param2);
    }

    public interface IInvokable<T1, T2, T3, R>
    {
        public R Invoke(T1 param1, T2 param2, T3 param3);
    }
}
