using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monsterkampf_Simulator
{
    public class InfoBoard
    {
        private static readonly object lockObject = new object();
        private const int maxEntries = 5;
        private static List<InfoBoardAction> infoBoardContent = new List<InfoBoardAction>();

        public static InfoBoardAction[] GetInfoBoardContent()
        {
            lock (lockObject)
            {
                return infoBoardContent.ToArray();
            }
        }

        public static void Clear()
        {
            lock (lockObject)
            {
                infoBoardContent.Clear();
            }
        }

        private static void RemoveEntry(int index)
        {
            lock (lockObject)
            {
                if (infoBoardContent.Count() < index || infoBoardContent.Count() == 0)
                {
                    return;
                }
                infoBoardContent.RemoveAt(index);
            }
        }

        public static void AddEntry(InfoBoardAction entry)
        {
            lock (lockObject)
            {
                if (infoBoardContent.Count() >= maxEntries)
                {
                    RemoveEntry(0);
                }

                infoBoardContent.Add(
                    new InfoBoardAction
                    {
                        content = entry.content,
                        fgColor = entry.fgColor,
                        bgColor = entry.bgColor
                    }
                );

            }
        }
    }
}
