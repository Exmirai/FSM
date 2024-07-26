using FSM_Dotnet.Models.FSM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSM_Dotnet.Interfaces
{
    public interface IStateMachineAction<T> where T : IEntityWithState
    {
        public void Execute(StateMachine<T> stateMachine);
    }
}
