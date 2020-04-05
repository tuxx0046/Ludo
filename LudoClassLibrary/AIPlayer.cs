using System;
using System.Collections.Generic;
using System.Text;

namespace LudoClassLibrary
{
    public class AIPlayer: Player
    {
        #region Constructor
        public AIPlayer(LudoColor color): base(color) { }
        #endregion

        #region Methods
        public void TakeTurn()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Figures out what action to take
        /// </summary>
        /// <param name="dieValue"></param>
        public void EvaluateOptions(int dieValue)
        {

        }

        /// <summary>
        /// Checks to see if current AI can bump opponent Piece back to base
        /// </summary>
        public bool CanIThrowOpponentBackToBase(int dieValue)
        {
            // throw back if opponent is more than 50% over route
            return true;
        }

        /// <summary>
        /// Checks to see if current AI can get own Piece from base out to playing field
        /// </summary>
        public bool CanIGetOwnPieceOutOfBase(int dieValue)
        {
            return true;
        }

        /// <summary>
        /// Checks to see if one of the Pieces can get to goal
        /// </summary>
        public bool CanMyPieceGetToGoal(int dieValue)
        {
            return true;
        }

        public void MakeDecision()
        {

        }

        /// <summary>
        /// Decide what Piece to move that will give the "best" outcome.
        /// </summary>
        /// <param name="myOwnPieces"></param>
        public void ChoosePieceAndMove(List<Piece> myActivePieces)
        {
            // cannot jump over allied pieces, so maybe prioritize where nothing is "wasted"
        }
        #endregion
    }
}
