using System;
using System.Collections.Generic;
using System.Text;

namespace LudoClassLibrary
{
    /// <summary>
    /// The pieces to be moved by players. Each player has 4 pieces.
    /// </summary>
    internal class Piece
    {
        #region Fields
        internal int routePosition = 0;

        #endregion

        #region Properties
        internal int BasePositionId { get; }
        internal LudoColor Color { get; }

        /// <summary>
        /// The position tells where on the route (Field) a Piece is.<br/>
        /// Use as Route index.
        /// </summary>
        internal bool ReachedGoal { get; set; } = false;
        internal bool InBase { get; set; } = true;
        #endregion

        #region Constructor
        /// <summary>
        /// Each player has four Pieces, and each Piece must have a LudoColor matching the player that owns it.
        /// </summary>
        /// <param name="color">Must be a valid color from LudoColor</param>
        /// <param name="basePositionId">Position from 0 to 3</param>
        internal Piece(LudoColor color, int basePositionId) 
        {
            this.Color = color;
            this.BasePositionId = basePositionId;
        }
        #endregion
        
    }
}
