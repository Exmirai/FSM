using FSM_Dotnet.Interfaces;
using FSM_Dotnet.Models.FSM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSM_Dotnet.FSM_ExtenalActions
{
    public class StateMachineSignalAction<T> : IStateMachineAction<T> where T : IEntityWithState
    {
        private readonly Signal _signal;

        public StateMachineSignalAction(Signal signal)
        {
            _signal = signal;
        }

        public void Execute(StateMachine<T> stateMachine)
        {
            stateMachine.HandleSignal(_signal);
        }
    }
}
