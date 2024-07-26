using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSM_Dotnet.Enums
{
    [Flags]
    public enum ModifyActionPreBehaviorFlags
    {
        EntityValidation = 1, // Валидировать сущность 
        ExecPostInvokable = 2, // Выполнять post-modify экшн
    }
}
