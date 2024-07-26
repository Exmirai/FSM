using FSM_Dotnet.Interfaces;
using FSM_Dotnet.Models.FSM;
using FSM_Dotnet.Validation;

namespace TestApp.Providers
{
    public class StateMachineFactory<T> where T : IEntityWithState
    {
        private readonly IFSMRuntime[] _availableRuntimes;
        private readonly StateMachineDescription<T> _machineDescription;
        private readonly EntityValidatorsProvider<T> _entityValidatorsProvider;

        public StateMachineFactory(IServiceProvider serviceProvider)
        {
            _availableRuntimes = serviceProvider.GetServices<IFSMRuntime>().ToArray();
            var descProvider = serviceProvider.GetRequiredService<StateMachineDescriptionProvider>();
            _machineDescription = descProvider.GetDescription<T>();
        }


        public StateMachine<T> AllocNewFsm(T entity)
        {
            return new StateMachine<T>(_machineDescription, _entityValidatorsProvider, _availableRuntimes.First(), entity);
        }
    }
}
