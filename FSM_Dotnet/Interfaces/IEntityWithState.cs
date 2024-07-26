using FSM_Dotnet.Models.FSM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSM_Dotnet.Interfaces
{
    public interface IEntityWithState : ICloneable
    {
        public State State { get; }

        public Guid Id { get; }
    }
}
