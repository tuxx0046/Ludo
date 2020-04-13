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
        internal int TakeTurn(int dieValue, List<Piece> pieces)
        {
            

            // If rolled 6
            if (dieValue == 6)
            {
                Piece pieceInBase = CheckIfPiecesAreInBase(pieces);
                // If a piece is in the base, prioritize to get pieces out of base
                if (pieceInBase != null )
                {
                    // Return the pieces position
                    return pieceInBase.BasePositionId;
                    
                }
                // If no pieces are in base (null)
                else
                {
                    // Select a random piece
                    int routePositionOfRandomPiece = SelectRandomPiece(dieValue, pieces);
                    return routePositionOfRandomPiece;
                }
            }
            else
            {
                int routePositionOfRandomPiece = SelectRandomPiece(dieValue, pieces);
                return routePositionOfRandomPiece;
            }
        }

        /// <summary>
        /// Check to see if there are pieces still in base.<br/>
        /// Returns the last checked piece in base or null.
        /// </summary>
        /// <param name="dieValue"></param>
        internal Piece CheckIfPiecesAreInBase(List<Piece> pieces)
        {
            Piece pieceInBase = null;
            for (int i = 0; i < pieces.Count - 1; i++)
            {
                if(pieces[i].inBase == true)
                {
                    // Get the last piece in base to return
                    pieceInBase = pieces[i];
                }
            }
            return pieceInBase;
        }

        /// <summary>
        /// Check if all inBase fields are true for all piece items in list. <br/>
        /// Returns true if all are in base.
        /// </summary>
        /// <param name="pieces"></param>
        /// <returns></returns>
        internal bool CheckIfAllPiecesAreInBase(List<Piece> pieces)
        {
            bool allInBase = true;
            for (int i = 0; i < pieces.Count - 1; i++)
            {
                if (pieces[i].inBase == false)
                {
                    allInBase = false;
                }
            }
            return allInBase;
        }

        /// <summary>
        /// Select a random VALID piece that can be moved
        /// </summary>
        internal int SelectRandomPiece(int dieValue, List<Piece> pieces)
        {
            Random rnd = new Random();
            int randomPieceId = rnd.Next(0, pieces.Count - 1);
            bool pieceWillPassAllyWithMove = CheckIfPieceWillPassAlly(dieValue, pieces[randomPieceId].RoutePosition, pieces);
            
            if (pieceWillPassAllyWithMove == true)
            {
                // Return the position of the random piece
                return pieces[randomPieceId].RoutePosition;
            }
            else
            {
                // Just choose the position of piece furthest ahead
                int routePositionOfPiece = PickPieceFurthestAhead(pieces);
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
            for (int i = 0; i < pieces.Count - 1; i++)
            {
                if (currentPosition < pieces[i].RoutePosition && intendedPosition > pieces[i].RoutePosition)
                {
                    return true;
                }

            }
            return false;
        }

        /// <summary>
        /// Returns the piece position of the piece closests to goal
        /// </summary>
        /// <param name="pieces"></param>
        /// <returns></returns>
        private int PickPieceFurthestAhead(List<Piece> pieces)
        {
            List<int> sortedAfterRoutePosition = new List<int>();
            foreach (Piece piece in pieces)
            {
                sortedAfterRoutePosition.Add(piece.RoutePosition);
            }
            sortedAfterRoutePosition.Sort();
            int indexOfPieceFurthestAhead = sortedAfterRoutePosition.Count - 1;
            return indexOfPieceFurthestAhead;
        }

        #endregion
    }
}
