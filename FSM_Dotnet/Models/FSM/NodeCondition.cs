using FSM_Dotnet.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSM_Dotnet.Models.FSM
{
    public class NodeCondition
    {
        public IInvokable<IEntityWithState, bool>? Invokable { get; set; }
    }
}
