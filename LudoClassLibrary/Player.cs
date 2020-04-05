using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;


namespace LudoClassLibrary
{
    public abstract class Player
    {
        #region Fields
        public LudoColor color { get; }
        /// <summary>
        /// Determine if it is the turn of the player
        /// </summary>
        public bool myTurn = false;
        #endregion

        #region Properties
        public bool HasFinished { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Valid colors are chosen from the Colors enumeration
        /// </summary>
        /// <param name="color"></param>
        public Player(LudoColor color)
        {
            this.color = color;
        }
        #endregion

        
    }
}
