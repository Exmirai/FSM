using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSM_Dotnet.Models.FSM
{
    public class Signal
    {
        public string Name { get; set; } = string.Empty;

        public Dictionary<string, object> Arguments { get; set; } = new Dictionary<string, object>();
    }
}
