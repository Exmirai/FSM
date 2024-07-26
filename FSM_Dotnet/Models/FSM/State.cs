using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSM_Dotnet.Models.FSM
{
    public class State : Dictionary<string, string>, ICloneable
    {
        private List<string> _activeNodesBackingArray;
        public State(IEnumerable<KeyValuePair<string, string>> collection) : base(collection)
        {
            _activeNodesBackingArray = new List<string>();
            _activeNodesBackingArray = base["activeNodes"].Split(";").ToList();

            base["activeNodes"] = string.Empty;
        }
        public State(IDictionary<string, string> collection) : base(collection)
        {
            _activeNodesBackingArray = new List<string>();
            _activeNodesBackingArray = base["activeNodes"].Split(";").ToList();

            base["activeNodes"] = string.Empty;
        }
        public State(IEnumerable<string> states)
        {
            _activeNodesBackingArray = new List<string>(states);
            base["activeNodes"] = string.Join(";", _activeNodesBackingArray);
        }
        public List<string> ActiveNodes
        {
            get
            {
                return _activeNodesBackingArray;
            }
            set
            {
                _activeNodesBackingArray = value;
            }
        }

        public string this[int i]
        {
            get {
                return _activeNodesBackingArray[i]; 
            }
            set {
                _activeNodesBackingArray[i] = value; 
            }
        }

        public void SaveChanges()
        {
            base["activeNodes"] = string.Join(";", _activeNodesBackingArray);
        }

        public object Clone()
        {
            return new State(this);
        }
    }
}
