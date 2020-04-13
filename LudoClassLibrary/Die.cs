using System;
using System.Collections.Generic;
using System.Text;

namespace LudoClassLibrary
{
    /// <summary>
    /// A die that will return random values from 1 to 6.
    /// </summary>
    internal static class Die
    {
        #region Fields
        private static Random random = new Random();
        #endregion

        #region Properties
        /// <summary>
        /// Returns a random value between 1 and 6. <br/>
        /// Registers that is has been thrown when calling value.
        /// </summary>
        internal static int RandomDieValue
        {
            get
            {
                int dieValue = random.Next(1, 7);
                HasBeenThrown = true;
                return dieValue;
            }
        }

        /// <summary>
        /// True if it has been thrown
        /// </summary>
        internal static bool HasBeenThrown { get; set; } = false;
        /// <summary>
        /// Counts number of times it has been thrown.
        /// </summary>
        internal static int TimesThrown { get; set; } = 0;
        #endregion
    }
}