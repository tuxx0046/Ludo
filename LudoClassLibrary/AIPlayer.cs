using System;
using System.Collections.Generic;
using System.Text;

namespace LudoClassLibrary
{
    internal class AIPlayer: Player
    {
        #region Constructor
        internal AIPlayer(LudoColor color): base(color) { }
        #endregion

        #region Methods
        /// <summary>
        /// Takes boolean that tells if all pieces are in base or not to decide whether or not to roll more times.<br/>
        /// Returns die value.
        /// </summary>
        /// <param name="pieces"></param>
        /// <param name="areAllPiecesInBase"></param>
        /// <returns></returns>
        internal int RollDie(bool areAllPiecesInBase)
        {
            // 1st time rolling die 
            int dieValue = Die.RandomDieValue;

            //If all pieces are in base and dieValue is not 6, allow rolls up to 2 more throws or until die value is 6
            for (int i = 0; i < 2; i++)
            {
                if (areAllPiecesInBase == true && dieValue != 6)
                {
                    dieValue = Die.RandomDieValue;
                }
            }
            return dieValue;
        }

        /// <summary>
        /// Check to see if there are pieces still in base.<br/>
        /// Returns the last checked piece in base or null.
        /// </summary>
        internal Piece CheckIfAPieceIsInBase(List<Piece> pieces)
        {
            Piece pieceInBase = null;
            for (int i = 0; i < pieces.Count; i++)
            {
                // Make sure that the pieces have not reachedGoal or they will also be included
                if(pieces[i].InBase == true && pieces[i].ReachedGoal != true)
                {
                    // Get the last piece in base to return
                    pieceInBase = pieces[i];
                }
            }
            return pieceInBase;
        }

        /// <summary>
        /// Check if all pieces are in base. <br/>
        /// Returns true if all playable pieces are in base.<br/>
        /// Use to determine number of die rolls
        /// </summary>
        /// <param name="pieces"></param>
        /// <returns></returns>
        internal bool CheckIfAllPiecesAreInBase(List<Piece> pieces)
        {
            bool allInBase = true;
            for (int i = 0; i < pieces.Count; i++)
            {
                if (pieces[i].InBase == false && pieces[i].ReachedGoal != true)
                {
                    allInBase = false;
                }
            }
            return allInBase;
        }

        /// <summary>
        /// Select a random VALID piece that can be moved, and returns its' position.
        /// </summary>
        internal int SelectRandomPiece(int dieValue, List<Piece> pieces)
        {
            List<Piece> piecesOnRoute = new List<Piece>();
            // Separate pieces not in base and no in goal
            foreach (Piece piece in pieces)
            {
                if (piece.InBase == false && piece.ReachedGoal != true)
                {
                    piecesOnRoute.Add(piece);
                }
            }

            // If only 1 in list, just return it
            if (piecesOnRoute.Count == 1)
            {
                return piecesOnRoute[0].routePosition;
            }

            // If more than one, randomly pick one from list.
            Random rnd = new Random();
            int randomPieceBaseId = rnd.Next(0, piecesOnRoute.Count);
            int routeposition = piecesOnRoute[randomPieceBaseId].routePosition;
            bool pieceWillPassAllyWithMove = CheckIfPieceWillPassAlly(dieValue, routeposition, piecesOnRoute);
            
            if (pieceWillPassAllyWithMove == false)
            {
                // Return the position of the random piece
                return piecesOnRoute[randomPieceBaseId].routePosition;
            }
            else
            {
                // Just choose the position of piece furthest ahead
                int routePositionOfPiece = PickPieceFurthestAhead(piecesOnRoute);
                return routePositionOfPiece;
            }

        }

        /// <summary>
        /// Checks if a piece's route position + value of die will surpass allied piece's position (illegal move in ludo)
        /// </summary>
        /// <returns></returns>
        private bool CheckIfPieceWillPassAlly(int dieValue, int currentPosition, List<Piece> pieces)
        {
            int intendedPosition = currentPosition + dieValue;
            for (int i = 0; i < pieces.Count; i++)
            {
                if (currentPosition < pieces[i].routePosition && intendedPosition > pieces[i].routePosition)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Returns the piece position of the piece closest to goal
        /// </summary>
        /// <param name="pieces"></param>
        /// <returns></returns>
        private int PickPieceFurthestAhead(List<Piece> pieces)
        {
            // Put the route positions of each valid piece in list
            List<int> sortedAfterRoutePosition = new List<int>();
            foreach (Piece piece in pieces)
            {
                sortedAfterRoutePosition.Add(piece.routePosition);
            }
            sortedAfterRoutePosition.Sort();
            int routePositionOfPieceFurthest = sortedAfterRoutePosition.Count - 1;
            return routePositionOfPieceFurthest;
        }

        #endregion
    }
}
