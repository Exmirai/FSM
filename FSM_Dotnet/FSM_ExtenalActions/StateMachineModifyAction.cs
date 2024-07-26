using FSM_Dotnet.Interfaces;
using FSM_Dotnet.Models.FSM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSM_Dotnet.FSM_ExtenalActions
{
    public class StateMachineModifyAction<T> : IStateMachineAction<T> where T : IEntityWithState
    {
        private readonly Func<T, T> _modifier;
        public StateMachineModifyAction(Func<T, T> modifier)
        {
            _modifier = modifier;
        }

        public void Execute(StateMachine<T> stateMachine)
        {
            var pendingEntity = _modifier((T)stateMachine.Entity.Clone());

            foreach (var activeNode in stateMachine.Entity.State.ActiveNodes)
            {
                var currentNode = stateMachine.Description.Nodes[activeNode];

                var behFlags = currentNode.ModifyActionHandler.PreBehaviorFlags;

                if (behFlags.HasFlag(Enums.ModifyActionPreBehaviorFlags.EntityValidation))
                {
                    stateMachine.ValidatorsProvider.ValidateEntity(pendingEntity);
                }

                if (behFlags.HasFlag(Enums.ModifyActionPreBehaviorFlags.ExecPostInvokable))
                {
                    //node.ModifyActionHandler.CustomCode.Invoke(stateMachine.Entity);
                }

                var canBeTransitionedOut = currentNode.ExitConditions.All(cond => cond.Invokable?.Invoke(pendingEntity) ?? true);

                if (!canBeTransitionedOut)
                {
                    stateMachine.Entity = pendingEntity;
                    return;
                }

                var possibleTransitions = stateMachine.Description.AllowedTransitions[activeNode];


                var allowedTransitions = possibleTransitions
                    .Where(possibleNodeName => stateMachine.Description.Nodes[possibleNodeName].EntryConditions.All(possCond => possCond.Invokable?.Invoke(pendingEntity) ?? true) == true)
                    .Select(targetNode => stateMachine.Description.Nodes[targetNode]).ToList();

                if (!allowedTransitions.Any())
                {
                    stateMachine.Entity = pendingEntity;
                    return;
                }

                if (allowedTransitions.Count > 1)
                {
                    throw new Exception("Найдено более чем 1 возможных переходов. Откат изменений...");
                }

                var transitionNode = allowedTransitions.First();

                //Начинаем переход в другое состояние


                pendingEntity.State[0] = transitionNode.NodeId;
                pendingEntity.State.SaveChanges();

                currentNode.ExitActions.ForEach(act => act.Invokable?.Invoke(pendingEntity));
                transitionNode.EntryActions.ForEach(act => act.Invokable?.Invoke(pendingEntity));
            }

            stateMachine.Entity = pendingEntity;
        }
    }
}
