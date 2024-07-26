using FSM_Dotnet.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FSM_Dotnet.Models.FSM
{
    public class StateMachineDescription<T> where T : IEntityWithState
    {
        public Dictionary<string, Node> Nodes { get; init; } = new Dictionary<string, Node>();

        [JsonIgnore]
        public Dictionary<string, Action<StateMachine<T>, Dictionary<string, object>>> SignalHandlers { get; set; } = new Dictionary<string, Action<StateMachine<T>, Dictionary<string, object>>>();

        public Dictionary<string, List<string>> AllowedTransitions { get; set; } = new Dictionary<string, List<string>>();
    }
}
