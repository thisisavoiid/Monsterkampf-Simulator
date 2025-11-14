using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monsterkampf_Simulator
{
    /// <summary>
    /// Holds information such as formatting and content of an action inside the InfoBoard.
    /// </summary>
    public struct InfoBoardAction
    {
        public string content;
        public ConsoleColor fgColor;
        public ConsoleColor bgColor;
    }
}
