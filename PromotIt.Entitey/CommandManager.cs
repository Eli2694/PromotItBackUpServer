using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromotIt.Entitey
{
    public interface ICommand
    {
        
    }
    public class CommandManager
    {
        public Dictionary<string, ICommand> _CommandList;

        public Dictionary<string, ICommand> CommandList 
        { 
            get 
            {
                if (_CommandList == null) Init();
                return _CommandList;
            } 
        }

        private void Init()
        {
            _CommandList = new Dictionary<string, ICommand>();
        }
    }
}
