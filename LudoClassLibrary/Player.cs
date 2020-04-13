using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;


namespace LudoClassLibrary
{
    internal abstract class Player
    {
        #region Properties
        internal LudoColor color { get; }
        internal bool HasFinished { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Valid colors are chosen from the Colors enumeration
        /// </summary>
        /// <param name="color"></param>
        internal Player(LudoColor color)
        {
            this.color = color;
        }
        #endregion

        
    }
}