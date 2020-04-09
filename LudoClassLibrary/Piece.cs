using System;
using System.Collections.Generic;
using System.Text;

namespace LudoClassLibrary
{
    /// <summary>
    /// The pieces to be moved by players. Each player has 4 pieces.
    /// </summary>
    public class Piece
    {
        #region Fields
        public LudoColor color;
        #endregion

        #region Properties
        public int BasePositionId { get; }

        /// <summary>
        /// The position tells where on the route (Field) a Piece is.<br/>
        /// Use as Route index.
        /// </summary>
        public int RoutePosition;
        public bool reachedGoal { get; set; } = false;
        public bool inBase { get; set; } = true;
        #endregion

        #region Constructor
        /// <summary>
        /// Each player has four Pieces, and each Piece must have a LudoColor matching the player that owns it.
        /// </summary>
        /// <param name="color">Must be a valid color from LudoColor</param>
        /// <param name="basePositionId">Position from 0 to 3</param>
        public Piece(LudoColor color, int basePositionId) 
        {
            this.color = color;
            this.BasePositionId = basePositionId;
        }
        #endregion
        
    }
}
