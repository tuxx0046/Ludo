using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace LudoClassLibrary
{
    public class GameManager
    {
        #region Fields & Properties
        private Player currentPlayer;
        // Use this value to rotate the players to take turns - GoToNextPlayer()
        private int activePlayersIndex = 0;
        private int dieValue;

        /// <summary>
        /// List of enabled AI players, used by RegisterEnabledAIPlayers() and AddPlayersToGame()
        /// </summary>
        private List<LudoColor> enabledAIPlayers;

        /// <summary>
        /// List of active players. Can be human or AI. <br/>
        /// Use this list when processing in game players.
        /// </summary>
        private List<Player> activePlayers;

        // Prepare all the ludo objects for the game
        private Player bluePlayer;
        private Player greenPlayer;
        private Player redPlayer;
        private Player yellowPlayer;

        private List<Piece> bluePieces = new List<Piece>
        {
            new Piece(LudoColor.Blue, 0),
            new Piece(LudoColor.Blue, 1),
            new Piece(LudoColor.Blue, 2),
            new Piece(LudoColor.Blue, 3)
        };

        private List<Piece> greenPieces = new List<Piece>
        {
            new Piece(LudoColor.Green, 0),
            new Piece(LudoColor.Green, 1),
            new Piece(LudoColor.Green, 2),
            new Piece(LudoColor.Green, 3)
        };

        private List<Piece> redPieces = new List<Piece>
        {
            new Piece(LudoColor.Red, 0),
            new Piece(LudoColor.Red, 1),
            new Piece(LudoColor.Red, 2),
            new Piece(LudoColor.Red, 3)
        };

        private List<Piece> yellowPieces = new List<Piece>
        {
            new Piece(LudoColor.Yellow, 0),
            new Piece(LudoColor.Yellow, 1),
            new Piece(LudoColor.Yellow, 2),
            new Piece(LudoColor.Yellow, 3)
        };

        private Dictionary<Player, List<Piece>> playerPiecePairs = new Dictionary<Player, List<Piece>>();
            

        private Board ludoBoard = new Board();
        public UIManager ui = new UIManager();

        #endregion

        public GameManager()
        {
            ludoBoard.InitWholeBoardAndCreateRoutes();
        }


        #region Methods
        // Subscribed to event
        /// <summary>
        /// Adds players to game and starts turn
        /// </summary>
        public void RunGame()
        {
            AddPlayersToGame();
            playerPiecePairs.Add(bluePlayer, bluePieces);
            playerPiecePairs.Add(greenPlayer, greenPieces);
            playerPiecePairs.Add(redPlayer, redPieces);
            playerPiecePairs.Add(yellowPlayer, yellowPieces);
            ui.btnEndTurn.IsEnabled = true;
            StartNextTurn();
        }

        // Subscribed to end turn click
        /// <summary>
        /// Sets next currentPlayer, activaces die and updates information label
        /// </summary>
        public async void StartNextTurn()
        {
            await Task.Delay(1000);
            GoToNextPlayer();
            ui.ShowCurrentPlayer(currentPlayer.Color.ToString());
            dieValue = 0;
            ui.btnDie.IsEnabled = true;

            ui.UpdateInformationLabel("Please throw die");

            if (currentPlayer.GetType() == typeof(AIPlayer))
            {
                RunAI();
            }
        }

        private void RunAI()
        {
            AIPlayer ai = currentPlayer as AIPlayer;

            // Check to see if ALL pieces are in base to decide times to allow die rolls
            bool allPiecesInBase = ai.CheckIfAllPiecesAreInBase(playerPiecePairs[ai]);

            // Throw the die and save the value
            dieValue = ai.RollDie(allPiecesInBase);

            // Show what the AI rolled
            ui.UpdateInformationLabel("Rolled " + dieValue.ToString());

            // Find the piece to move out of base. Gives null if none, or the last checked piece that is in base
            Piece pieceInBase = ai.CheckIfAPieceIsInBase(playerPiecePairs[ai]);

            // If all pieces or at least one piece is in base and rolled 6
            if ((allPiecesInBase == true && dieValue == 6) || (pieceInBase != null && dieValue == 6))
            {
                // Move piece out of BASE - 1st priority
                int indexOfPieceInbase = pieceInBase.BasePositionId;
                ChoosePieceToMove(ui.btnColorBases[ai.Color][indexOfPieceInbase], EventArgs.Empty);
                StartNextTurn();
            }
            // All pieces in base, but can't move them out of base - end turn
            else if (allPiecesInBase == true && dieValue != 6)
            {
                StartNextTurn();
            }
            // At least 1 piece is not in base - Pick a piece not in base and move it
            else if (allPiecesInBase == false)
            {
                // Move piece FROM FIELD TO FIELD
                int routeIndexOfRandomPiece = ai.SelectRandomPiece(dieValue, playerPiecePairs[ai]);
                ChoosePieceToMove(ui.btnColorRoutes[ai.Color][routeIndexOfRandomPiece], EventArgs.Empty);
                StartNextTurn();
            }

        }

        // Subscribed to click event
        /// <summary>
        /// Gets called when button click event occurs.<br/>
        /// Saves current Die value to variable, and checks if all pieces are in base to allow at most 3 throws.
        /// </summary>
        public void ThrowDie()
        {
            // Save value of Die
            dieValue = Die.RandomDieValue;
            bool allPiecesAreInBase = true;
            // Check if all pieces are in base, to determine if player gets to roll die 3 times or gets of 6 whichever comes first            
            for (int i = 0; i < playerPiecePairs[currentPlayer].Count - 1; i++)
            {
                if (playerPiecePairs[currentPlayer][i].InBase == false)
                {
                    allPiecesAreInBase = false;
                    break;
                }
            }
            // When at least one piece is out of base or rolls 6
            if (allPiecesAreInBase == false || dieValue == 6)
            {
                // Reset times thrown
                Die.TimesThrown = 0;
                // Disable button because at least one piece is moveable
                ui.btnDie.IsEnabled = false;
                // Display value of die

            }
            // When all in base and thrown 1-2 times
            else if (Die.TimesThrown < 2)
            {
                Die.TimesThrown++;
            }
            // All in base, and thrown three times
            else
            {
                Die.TimesThrown = 0;
                ui.btnDie.IsEnabled = false;
            }
            ui.UpdateInformationLabel("Rolled " + dieValue.ToString());

        }


        /// <summary>
        /// Sets current player to be next color from LudoColors. <br/>
        /// Starts from blue, green, red, yellow and then loops.
        /// </summary>
        private void GoToNextPlayer()
        {
            int playersLeft = activePlayers.Count;
            // If current player is last player in player list
            if (currentPlayer == activePlayers[playersLeft - 1])
            {
                // Set next player to first player in player list
                activePlayersIndex = 0;
                currentPlayer = activePlayers[activePlayersIndex];
                
            }
            else
            {
                // Next player is next player in player list
                currentPlayer = activePlayers[activePlayersIndex];
            }
            activePlayersIndex++;
        }
           

        // Subscribed to event
        /// <summary>
        /// Registers into a List the AI players to be instantiated. Eventdriven.
        /// </summary>
        /// <param name="enabledAIPlayers"></param>
        public void RegisterEnabledAIPlayers(List<LudoColor> enabledAIPlayers)
        {
            this.enabledAIPlayers = enabledAIPlayers;
        }

        /// <summary>
        /// Adds players to activePlayers list. If AI is enabled AI will be added instead of HumanPlayer
        /// </summary>
        private void AddPlayersToGame()
        {
            // Determine if players are human or AI
            bluePlayer = enabledAIPlayers.Contains(LudoColor.Blue) == true ? bluePlayer = new AIPlayer(LudoColor.Blue) 
                : bluePlayer = new HumanPlayer(LudoColor.Blue);

            greenPlayer = enabledAIPlayers.Contains(LudoColor.Green) == true ? greenPlayer = new AIPlayer(LudoColor.Green)
                : greenPlayer = new HumanPlayer(LudoColor.Green);

            redPlayer = enabledAIPlayers.Contains(LudoColor.Red) == true ? redPlayer = new AIPlayer(LudoColor.Red)
                : redPlayer = new HumanPlayer(LudoColor.Red);

            yellowPlayer = enabledAIPlayers.Contains(LudoColor.Yellow) == true ? yellowPlayer = new AIPlayer(LudoColor.Yellow)
                : yellowPlayer = new HumanPlayer(LudoColor.Yellow);

            // Add the players to active players list
            activePlayers = new List<Player> { bluePlayer, greenPlayer, redPlayer, yellowPlayer };
        }

        // Subscribed to click event
        /// <summary>
        /// Gets called when button click happens/field is chosen.<br/>
        /// Corresponds to choosing a piece to move after rolling die.
        /// </summary>
        /// <param name="fieldPiece"></param>
        /// <param name="e"></param>
        public void ChoosePieceToMove(object fieldPiece, EventArgs e)
        {
            Button chosenField = fieldPiece as Button;
            // Check if choice is valid
            bool isValidField = ValidateIfChosenBtnFieldIsPartOfRoute(chosenField);
            // Get index of field/piece
            int fieldIndexPosition = ui.GetIndexOfField(currentPlayer.Color, chosenField);

            // Is the field part of current players route? Is there a piece on the field?
            if (isValidField == true)
            {

                bool fieldHasValidPiece = FieldHasAPieceOnIt(currentPlayer.Color, fieldIndexPosition);

                // Is the chosen field to move Piece from part of base?
                bool isBaseField = ui.btnColorBases[currentPlayer.Color].Contains(chosenField) ? true : false;

                // Find Position/index of field. Base(0-3) or Route(0-57)
                int positionOfPiece = isBaseField ? ui.btnColorBases[currentPlayer.Color].IndexOf(chosenField)
                    : ui.btnColorRoutes[currentPlayer.Color].IndexOf(chosenField);

                // Move from base to field
                if (isBaseField == true && dieValue == 6)
                {
                    MovePieceFromBaseToRoute(positionOfPiece, chosenField);
                }
                else if (isBaseField == true && dieValue != 6)
                {
                    // Trying to move piece from base but did not roll 6
                    MessageNotAValidMove();
                }
                // Move from field to field
                else if (fieldHasValidPiece == true)
                {
                    // Make sure not to pass ally
                    bool alliesOnPath = WillIPassAllyWithThisMove(currentPlayer.Color, positionOfPiece);
                    if (alliesOnPath == true)
                    {
                        MessageNotAValidMove();
                    }
                    else
                    {
                        MovePieceFromFieldToField(positionOfPiece, chosenField);
                    }
                }
            }
            else
            {
                MessageNotAValidMove();
            }
            CheckForWinner();

        }

        /// <summary>
        /// Moves piece from base position to route start position
        /// </summary>
        /// <param name="positionOfPiece"></param>
        /// <param name="chosenField"></param>
        private void MovePieceFromBaseToRoute(int positionOfPiece, Button chosenField)
        {
            // Base position
            Piece pieceToMove = playerPiecePairs[currentPlayer][positionOfPiece];
            // Check if there is actually a piece standing on the basefield
            if (pieceToMove.InBase == true)
            {
                // Allow move to first field on route (index 0)
                // First field on route does not need colorchange
                pieceToMove.InBase = false;
                pieceToMove.routePosition = 0;
                
                // Check to see if the target position is free for the taking
                bool canMovePiece = CanTakeField(pieceToMove.routePosition, pieceToMove);
                if (canMovePiece == true)
                {
                    // Change color on button base field to show that current Piece has left it
                    ui.ChangeColor(currentPlayer.Color, chosenField);

                    // Update occupantcount on target field (UI part). Index 0 because we're moving piece from base to first field on route
                    ui.ChangeNumberOfOccuppants(ludoBoard.routes[currentPlayer.Color][0].CountOccupants(), ui.btnColorRoutes[currentPlayer.Color][0]);
                }
                else if (canMovePiece == false)
                {
                    MessageYouGotHitBack();
                }
                
                // End turn
            }
            // No piece on base
            else
            {
                MessageNotAValidMove();
            }
        }

        private void MovePieceFromFieldToField(int pieceIndexPosition, Button chosenField)
        {
            Field fieldToMoveFrom = ludoBoard.routes[currentPlayer.Color][pieceIndexPosition];
            Piece pieceToMove = ludoBoard.routes[currentPlayer.Color][pieceIndexPosition].occupants[0];

            // Old position before move attempt
            int currentPosition = pieceToMove.routePosition;

            // Update piece position to new value for new field position
            int newPosition = pieceToMove.routePosition + dieValue;
            if (newPosition > 56)
            {
                int positionsToMoveBack = newPosition - 56;
                newPosition = 56 - positionsToMoveBack;
            }
            pieceToMove.routePosition = newPosition;

            Field fieldToMoveTo = ludoBoard.routes[currentPlayer.Color][pieceToMove.routePosition];

            bool canMovePiece = CanTakeField(pieceToMove.routePosition, pieceToMove);
            if (canMovePiece == true)
            {
                // Remove piece from old field
                fieldToMoveFrom.RemoveOccupant();
                // Change color on field moving from
                if (fieldToMoveFrom.CountOccupants() == 0)
                {
                    ui.ChangeColorToOriginalColor(chosenField);
                    ui.ChangeNumberOfOccuppants(fieldToMoveFrom.CountOccupants(), chosenField);
                }
                else
                {
                    // If there are occupants leave color but change number
                    ui.ChangeNumberOfOccuppants(fieldToMoveFrom.CountOccupants(), chosenField);
                }
                
                // Change color on field moving to
                ui.ChangeColor(currentPlayer.Color, ui.btnColorRoutes[currentPlayer.Color][pieceToMove.routePosition]);
                ui.ChangeNumberOfOccuppants(fieldToMoveTo.CountOccupants(), ui.btnColorRoutes[currentPlayer.Color][pieceToMove.routePosition]);

            }
            else
            {
                // Remove moving piece from move-from field
                fieldToMoveFrom.RemoveOccupant();
                // Change color on field moving from
                if (fieldToMoveFrom.CountOccupants() == 0)
                {
                    ui.ChangeColorToOriginalColor(chosenField);
                    ui.ChangeNumberOfOccuppants(fieldToMoveFrom.CountOccupants(), chosenField);
                }
                else
                {
                    // If there are occupants leave color but change number
                    ui.ChangeNumberOfOccuppants(fieldToMoveFrom.CountOccupants(), chosenField);
                }

                MessageYouGotHitBack();
            }
            // Set goal if reached goal
            if (pieceToMove.routePosition == 56)
            {
                pieceToMove.ReachedGoal = true;
                // Remove piece from player's list of pieces
                //playerPiecePairs[currentPlayer].Remove(pieceToMove);
            }

        }

        private bool WillIPassAllyWithThisMove(LudoColor color, int pieceIndexPosition)
        {
            if (pieceIndexPosition + dieValue <= 55)
            {
                for (int i = pieceIndexPosition + 1; i < pieceIndexPosition + dieValue; i++)
                {
                    if (ludoBoard.routes[color][i].CountOccupants() > 0 && ludoBoard.routes[color][i].GetOccupantColor() == color)
                    {
                        return true;
                    }
                    
                }
                return false;
            }
            else
            {
                // If no ally is on path
                return false;
            }
        }

        /// <summary>
        /// Check to see if there is a piece on field to actually move
        /// </summary>
        /// <param name="color"></param>
        /// <param name="fieldIndexPosition"></param>
        /// <returns></returns>
        private bool FieldHasAPieceOnIt(LudoColor color, int fieldIndexPosition)
        {
            if (ludoBoard.routes[color][fieldIndexPosition].CountOccupants() > 0 && ludoBoard.routes[color][fieldIndexPosition].GetOccupantColor() == currentPlayer.Color)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Determines whether or not current players piece will hit opponent home or the opposite.<br/>
        /// Performs the move if possible.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="pieceToMove"></param>
        /// <returns></returns>
        private bool CanTakeField(int index, Piece pieceToMove)
        {
            Field targetField = ludoBoard.routes[currentPlayer.Color][index];
            
            // Hit opponent back to base if only one opponent
            if (targetField.CountOccupants() == 1 && targetField.GetOccupantColor() != currentPlayer.Color)
            {
                // Set opponent status to "in base"
                targetField.occupants[0].InBase = true;
                // Reset opponent position
                targetField.occupants[0].routePosition = 0;
                // Put opponent color back in opponent base (UI part)
                int opponentPieceId = targetField.occupants[0].BasePositionId;
                LudoColor opponentColor = targetField.occupants[0].Color;
                ui.ChangeColor(opponentColor, ui.btnColorBases[opponentColor][opponentPieceId]);
                // Remove opponent from ludoboard field
                targetField.RemoveOccupant();
                // Put currentPlayer's piece in ludoboard field
                targetField.AddOccupant(pieceToMove);

                return true;
            }
            // Get hit back by opponent
            else if (targetField.CountOccupants() > 1 && targetField.GetOccupantColor() != currentPlayer.Color)
            {
                
                pieceToMove.InBase = true;
                pieceToMove.routePosition = 0;
                // Update ui on base field
                ui.ChangeColor(pieceToMove.Color, ui.btnColorBases[pieceToMove.Color][pieceToMove.BasePositionId]);
                return false;
            }
            else
            {
                targetField.AddOccupant(pieceToMove);
                return true;
            }
        }

        private bool ValidateIfChosenBtnFieldIsPartOfRoute(Button chosenField)
        {
            // Check if clicked field is part of the base or route of the current player, and is not the goal field
            if (ui.btnColorBases[currentPlayer.Color].Contains(chosenField) || ui.btnColorRoutes[currentPlayer.Color].Contains(chosenField) && chosenField != ui.btnColorRoutes[currentPlayer.Color][56])
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Gives a 'not valid move' message in information label
        /// </summary>
        private void MessageNotAValidMove()
        {
            ui.UpdateInformationLabel("");
            ui.UpdateInformationLabel("Not a valid move!");
        }

        private void MessageYouGotHitBack()
        {
            ui.UpdateInformationLabel("You got hit back to base!");
        }

        /// <summary>
        /// Checks if current player has won the game
        /// </summary>
        private void CheckForWinner()
        {
            int index = playerPiecePairs[currentPlayer].Count;
            bool allPiecesReachedGoal = false;
            for (int i = 0; i < index; i++)
            {
                if (playerPiecePairs[currentPlayer][i].ReachedGoal == true)
                {
                    allPiecesReachedGoal = true;
                }
            }


            //bool allPiecesReachedGoal = false;
            //if (playerPiecePairs[currentPlayer].Count == 0)
            //{
            //    allPiecesReachedGoal = true;
            //}

            if (allPiecesReachedGoal == true)
            {
                ui.UpdateInformationLabel("Won the game!");
                MessageBox.Show(currentPlayer.Color.ToString() + " has won the game!");
                // Restart application
                System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
                Application.Current.Shutdown();
            }
            
        }
        #endregion

    }
}

