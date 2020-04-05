using System;
using System.Collections.Generic;
using System.Text;

namespace LudoClassLibrary
{
    /// <summary>
    /// A die that will return random values from 1 to 6.
    /// </summary>
    public static class Die
    {
        #region Fields
        private static Random random = new Random();
        #endregion

        #region Properties
        /// <summary>
        /// Returns a random value between 1 and 6
        /// </summary>
        public static int RandomDieValue
        {
            get
            {
                int dieValue = random.Next(1, 7);
                return dieValue;
            }
        }
        #endregion
    }
}