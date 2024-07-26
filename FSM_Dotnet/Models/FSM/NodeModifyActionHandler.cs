using FSM_Dotnet.Enums;
using FSM_Dotnet.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSM_Dotnet.Models.FSM
{
    public class NodeModifyActionHandler
    {
        public ModifyActionPreBehaviorFlags PreBehaviorFlags { get; set; }

        public IInvokable<IEntityWithState, ModifyActionInvokableBehaviorEnum>? CustomCode { get; set; }
    }
}
