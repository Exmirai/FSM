using FSM_Dotnet.Interfaces;
using FSM_Dotnet.Models.FSM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSM_Dotnet.Models
{
    public class ExampleEntity : IEntityWithState
    {
        
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string SecName { get; set; } = string.Empty;

        public State State { get; set; } = new State(["", ""]);

        public object Clone()
        {
            return new ExampleEntity
            {
                Id = Id,
                Name = Name,
                State = (State)State.Clone(),
                SecName = SecName,
            };
        }
    }
}
