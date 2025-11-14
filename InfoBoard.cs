using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monsterkampf_Simulator
{
    /// <summary>
    /// Provides a message board that stores a limited number of recent InfoBoardAction entries.
    /// Used to display the latest events or status messages in the fight simulation.
    /// </summary>
    public class InfoBoard
    {
        private static readonly object lockObject = new object();
        private const int maxEntries = 5;
        private static List<InfoBoardAction> infoBoardContent = new List<InfoBoardAction>();

        /// <summary>
        /// Returns the current contents of the InfoBoard as an array.
        /// </summary>
        public static InfoBoardAction[] GetInfoBoardContent()
        {
            lock (lockObject)
            {
                return infoBoardContent.ToArray();
            }
        }

        /// <summary>
        /// Clears all entries from the InfoBoard.
        /// </summary>
        public static void Clear()
        {
            lock (lockObject)
            {
                infoBoardContent.Clear();
            }
        }

        /// <summary>
        /// Removes an entry at the specified index, if it exists.
        /// Does nothing if index is out of range.
        /// </summary>
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

        /// <summary>
        /// Adds a new entry to the InfoBoard. If the board is full,
        /// the oldest entry is removed first.
        /// </summary>
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
