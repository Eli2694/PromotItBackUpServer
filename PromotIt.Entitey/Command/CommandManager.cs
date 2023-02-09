
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromotIt.Entitey
{
    
    public class CommandManager 
    {
        private Dictionary<string, ICommand> _CommandListNoObj;
        private Dictionary<string, ICommandOneObj> _CommandListOneObj;
        private Dictionary<string, ICommandTwoObj> _CommandListTwoObj;
        private Dictionary<string, ICommandThreeObj> _CommandListThreeObj;
        private Dictionary<string, ICommandFourObj> _CommandListFourObj;

        private void Init()
        {
            _CommandListNoObj = new Dictionary<string, ICommand>();
            _CommandListOneObj = new Dictionary<string, ICommandOneObj>();
            _CommandListTwoObj = new Dictionary<string, ICommandTwoObj>();
            _CommandListThreeObj = new Dictionary<string, ICommandThreeObj>();
            _CommandListFourObj = new Dictionary<string, ICommandFourObj>();

        }
        public Dictionary<string, ICommand> CommandListNoObj
        { 
            get 
            {
                if (_CommandListNoObj == null) Init();
                return _CommandListNoObj;
            } 
        }

        public Dictionary<string, ICommandOneObj> CommandListOneObj
        {
            get
            {
                if (_CommandListOneObj == null) Init();
                return _CommandListOneObj;
            }
        }

        public Dictionary<string, ICommandTwoObj> CommandListTwoObj
        {
            get
            {
                if (_CommandListTwoObj == null) Init();
                return _CommandListTwoObj;
            }
        }

        public Dictionary<string, ICommandThreeObj> CommandListThreeObj
        {
            get
            {
                if (_CommandListThreeObj == null) Init();
                return _CommandListThreeObj;
            }
        }

        public Dictionary<string, ICommandFourObj> CommandListFourObj
        {
            get
            {
                if (_CommandListFourObj == null) Init();
                return _CommandListFourObj;
            }
        }

    }

    // Classes For Activist
    public class GetTwitterUserId : ICommandOneObj
    {
        public void ExecuteCommand(object one)
        {
            var json = MainManager.Instance.ActivistControl.SearchTwitterId((string)one);

        }
    }

    public class InitiateCampaigns : ICommandThreeObj
    {
        public void ExecuteCommand(object one, object two, object three)
        {

        }
    }


}
