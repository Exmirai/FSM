using FSM_Dotnet.Interfaces;
using FSM_Dotnet.Models.FSM;
using System.Diagnostics.CodeAnalysis;

namespace TestApp.Providers
{
    public class StateMachineProvider
    {
        private readonly ILogger<StateMachineProvider> _logger;
        private readonly Dictionary<Guid, IStateMachine> _cachedFsm;
        private readonly IServiceProvider _serviceProvider;

        public StateMachineProvider(ILogger<StateMachineProvider> logger)
        {
            _logger = logger;
            _cachedFsm = new Dictionary<Guid, IStateMachine>();
        }
        
        public bool TryGetFsm<T>(Guid id, [NotNullWhen(true)] out StateMachine<T>? stateMachine) where T : IEntityWithState
        {
            if (_cachedFsm.ContainsKey(id))
            {
                stateMachine = (StateMachine<T>)_cachedFsm[id];
                return true;
            }
            stateMachine = null;
            return false;
        }

        public bool TryGetFsm(Guid id, [NotNullWhen(true)] out IStateMachine? stateMachine)
        {
            if (_cachedFsm.ContainsKey(id))
            {
                stateMachine = _cachedFsm[id];
                return true;
            }
            stateMachine = null;
            return false;
        }

        public bool TryAllocNewFsm<T>(T entity, [NotNullWhen(true)] out StateMachine<T> stateMachine) where T : IEntityWithState
        {
            if (_cachedFsm.ContainsKey(entity.Id))
            {
                _logger.LogWarning($"Попытка выделить уже существующую машину для сущности {entity.Id}");

                stateMachine = (StateMachine<T>)_cachedFsm[entity.Id];
                return true;
            }
            var factory = _serviceProvider.GetRequiredService<StateMachineFactory<T>>();
            stateMachine = factory.AllocNewFsm(entity);
            return true;
        }
    }
}
