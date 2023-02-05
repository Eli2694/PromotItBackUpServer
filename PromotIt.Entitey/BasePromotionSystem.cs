using PersonalUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromotIt.Entitey
{
    public class BasePromotionSystem
    {
        public BasePromotionSystem(LogManager Log) { MyLogManager = Log; }
        public LogManager MyLogManager { get; set; }
    }
}
