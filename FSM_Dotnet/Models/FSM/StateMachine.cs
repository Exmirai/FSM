using FSM_Dotnet.Interfaces;
using FSM_Dotnet.Validation;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace FSM_Dotnet.Models.FSM
{
    public class StateMachine<Ent> : IStateMachine where Ent : IEntityWithState
    {
        private readonly ActionBlock<IStateMachineAction<Ent>> _actionBlock;
        private readonly StateMachineDescription<Ent> _description;
        private readonly EntityValidatorsProvider<Ent> _validatorsProvider;
        private readonly IFSMRuntime _runtime;

        private Ent _entity;

        public StateMachineDescription<Ent> Description { get { return _description; } }
        public EntityValidatorsProvider<Ent> ValidatorsProvider { get { return _validatorsProvider; } }

        public StateMachine(StateMachineDescription<Ent> description, EntityValidatorsProvider<Ent> validatorsProvider, IFSMRuntime runtime, Ent entity)
        {
            _entity = entity;
            _description = description;
            _validatorsProvider = validatorsProvider;
            _runtime = runtime;

            _actionBlock = new ActionBlock<IStateMachineAction<Ent>>(act =>
            {
                act.Execute(this);
            });
        }

        public Ent Entity { get
            {
                return _entity;
            }
            set
            {
                _entity = value;
            }
        }


        public void QueueAction(IStateMachineAction<Ent> action)
        {
             _actionBlock.Post(action);
        }

        public void HandleSignal(Signal signal)
        {
            if (!_description.SignalHandlers.TryGetValue(signal.Name, out var signalHandler))
            {
                throw new Exception();
            }

            signalHandler(this, signal.Arguments);
        }
    }
}
