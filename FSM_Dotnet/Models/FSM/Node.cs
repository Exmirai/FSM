using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FSM_Dotnet.Models.FSM
{
    public class Node
    {
        public string NodeId { get; set; } = string.Empty;

        public List<NodeCondition> EntryConditions { get; set; } = new List<NodeCondition>();

        public List<NodeAction> EntryActions { get; set; } = new List<NodeAction>();

        public List<NodeCondition> ExitConditions { get; set; } = new List<NodeCondition>();

        public List<NodeAction> ExitActions { get; set; } = new List<NodeAction>();

        [JsonIgnore]
        public NodeModifyActionHandler ModifyActionHandler { get; set; } = new NodeModifyActionHandler();
    }
}
