using FSM_Dotnet.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSM_Dotnet.Validation.Models
{
    public class EntityPropertyValidator<Entity> where Entity : IEntityWithState
    {
        public IInvokable<Entity, Task<bool>>? Invokable { get; set; }
    }
}
