using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromotIt.Entitey
{
    public interface ICommand
    {
        void ExecuteCommand();
    }

    public interface ICommandOneObj
    {
        void ExecuteCommand(object one);
    }

    public interface ICommandTwoObj
    {
        void ExecuteCommand(object one, object two);
    }

    public interface ICommandThreeObj
    {
        void ExecuteCommand(object one, object two, object three);
    }

    public interface ICommandFourObj
    {
        void ExecuteCommand(object one, object two, object three, object four);
    }
}
