using FSM_Dotnet.Interfaces;
using FSM_Dotnet.Models.FSM;

namespace TestApp.Providers
{
    public class StateMachineDescriptionProvider
    {
        public StateMachineDescriptionProvider()
        {

        }

        public StateMachineDescription<T>? GetDescription<T>() where T : IEntityWithState
        {
            return new StateMachineDescription<T>
            {

            };
        }
    }
}
