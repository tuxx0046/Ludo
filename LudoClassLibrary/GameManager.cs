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
        public Player currentPlayer;
        public bool HasChosenPieceToMove = false;
        public int activePlayersIndex = 0;
        public int dieValue;

        public bool APieceHasBeenChosen { get; set; } = false;
                

        /// <summary>
        /// List of enabled AI players
        /// </summary>
        public List<LudoColor> enabledAIPlayers;

        /// <summary>
        /// List of active players. Can be human or AI. <br/>
        /// Use this list when processing in game players.
        /// </summary>
        public List<Player> activePlayers;

        // Prepare all the ludo objects for the game
        public Player bluePlayer;
        public Player greenPlayer;
        public Player redPlayer;
        public Player yellowPlayer;

        public List<Piece> bluePieces = new List<Piece>
        {
            new Piece(LudoColor.Blue, 0),
            new Piece(LudoColor.Blue, 1),
            new Piece(LudoColor.Blue, 2),
            new Piece(LudoColor.Blue, 3)
        };

        public List<Piece> greenPieces = new List<Piece>
        {
            new Piece(LudoColor.Green, 0),
            new Piece(LudoColor.Green, 1),
            new Piece(LudoColor.Green, 2),
            new Piece(LudoColor.Green, 3)
        };

        public List<Piece> redPieces = new List<Piece>
        {
            new Piece(LudoColor.Red, 0),
            new Piece(LudoColor.Red, 1),
            new Piece(LudoColor.Red, 2),
            new Piece(LudoColor.Red, 3)
        };

        public List<Piece> yellowPieces = new List<Piece>
        {
            new Piece(LudoColor.Yellow, 0),
            new Piece(LudoColor.Yellow, 1),
            new Piece(LudoColor.Yellow, 2),
            new Piece(LudoColor.Yellow, 3)
        };

        public Dictionary<Player, List<Piece>> playerPiecePairs = new Dictionary<Player, List<Piece>>();
            

        public Board ludoBoard = new Board();
        public UIManager ui = new UIManager();

        #region Methods
        // Subscribed to event
        /// <summary>
        /// Adds players to game and starts turn
        /// </summary>
        public async void RunGame()
        {
            AddPlayersToGame();
            playerPiecePairs.Add(bluePlayer, bluePieces);
            playerPiecePairs.Add(greenPlayer, greenPieces);
            playerPiecePairs.Add(redPlayer, redPieces);
            playerPiecePairs.Add(yellowPlayer, yellowPieces);
            ui.btnEndTurn.IsEnabled = true;
            await Task.Delay(1000);
            StartNextTurn();

        }

        /// <summary>
        /// Sets next currentPlayer, activaces die and updates information label
        /// </summary>
        public void StartNextTurn()
        {
            GoToNextPlayer();
            ui.ShowCurrentPlayer(currentPlayer.color.ToString());
            dieValue = 0;
            ui.btnDie.IsEnabled = true;

            ui.UpdateInformationLabel("Please throw die");

        }

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
                if (playerPiecePairs[currentPlayer][i].inBase == false)
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
        public void GoToNextPlayer()
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
        /// Registers into a list the AI players to be instantiated
        /// </summary>
        /// <param name="enabledAIPlayers"></param>
        public void RegisterEnabledAIPlayers(List<LudoColor> enabledAIPlayers)
        {
            this.enabledAIPlayers = enabledAIPlayers;
        }

        /// <summary>
        /// Adds players to activePlayers list. If AI is enabled AI will be added instead of HumanPlayer
        /// </summary>
        public void AddPlayersToGame()
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

        
        public void ChoosePieceToMove(object fieldPiece, EventArgs e)
        {
            Button chosenField = fieldPiece as Button;
            // Check if choice is valid
            bool isValidField = ValidateIfChosenBtnFieldIsPartOfRoute(chosenField);
            // Get index of field/piece
            int fieldIndexPosition = ui.GetIndexOfField(currentPlayer.color, chosenField);

            // Is the field part of current players route? Is there a piece on the field?
            if (isValidField == true)
            {

                bool fieldHasValidPiece = FieldHasAPieceOnIt(currentPlayer.color, fieldIndexPosition);

                // Is the chosen field to move Piece from part of base?
                bool isBaseField = ui.btnColorBases[currentPlayer.color].Contains(chosenField) ? true : false;
                // Find Position/index of field. Base(0-3) or Route(0-57)
                int positionOfPiece = isBaseField ? ui.btnColorBases[currentPlayer.color].IndexOf(chosenField)
                    : ui.btnColorRoutes[currentPlayer.color].IndexOf(chosenField);
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
                    bool alliesOnPath = WillIPassAllyWithThisMove(currentPlayer.color, positionOfPiece);
                    if (alliesOnPath == true)
                    {
                        MessageNotAValidMove();
                    }
                    else
                    {
                        MovePieceFromFieldToField(positionOfPiece, chosenField);
                        // goal? or reverse

                    }
                }
            }
            else
            {
                MessageNotAValidMove();
            }

        }

        /// <summary>
        /// Moves piece from base position to route start position
        /// </summary>
        /// <param name="positionOfPiece"></param>
        /// <param name="chosenField"></param>
        public void MovePieceFromBaseToRoute(int positionOfPiece, Button chosenField)
        {
            // Base position
            Piece pieceToMove = playerPiecePairs[currentPlayer][positionOfPiece];
            // Check if there is actually a piece standing on the basefield
            if (pieceToMove.inBase == true)
            {
                // Allow move to first field on route (index 0)
                // First field on route does not need colorchange
                pieceToMove.inBase = false;
                pieceToMove.RoutePosition = 0;
                
                // Check to see if the target position is free for the taking
                bool canMovePiece = CanTakeField(pieceToMove.RoutePosition, pieceToMove);
                if (canMovePiece == true)
                {
                    // Change color on button base field to show that current Piece has left it
                    ui.ChangeColor(currentPlayer.color, chosenField);

                    // Update occupantcount on target field (UI part). Index 0 because we're moving piece from base to first field on route
                    ui.ChangeNumberOfOccuppants(ludoBoard.routes[currentPlayer.color][0].CountOccupants(), ui.btnColorRoutes[currentPlayer.color][0]);
                   

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

        public void MovePieceFromFieldToField(int pieceIndexPosition, Button chosenField)
        {
            Field fieldToMoveFrom = ludoBoard.routes[currentPlayer.color][pieceIndexPosition];
            Piece pieceToMove = ludoBoard.routes[currentPlayer.color][pieceIndexPosition].occupants[0];

            // Old position before move attempt
            int currentPosition = pieceToMove.RoutePosition;

            // Update piece position to new value for new field position
            int newPosition = pieceToMove.RoutePosition + dieValue;
            if (newPosition > 56)
            {
                int positionsToMoveBack = newPosition - 56;
                newPosition = 56 - positionsToMoveBack;
            }
            pieceToMove.RoutePosition = newPosition;

            // Set goal if reached goal
            if (pieceToMove.RoutePosition == 56)
            {
                pieceToMove.reachedGoal = true;
            }

            Field fieldToMoveTo = ludoBoard.routes[currentPlayer.color][pieceToMove.RoutePosition];

            bool canMovePiece = CanTakeField(pieceToMove.RoutePosition, pieceToMove);
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
                ui.ChangeColor(currentPlayer.color, ui.btnColorRoutes[currentPlayer.color][pieceToMove.RoutePosition]);
                ui.ChangeNumberOfOccuppants(fieldToMoveTo.CountOccupants(), ui.btnColorRoutes[currentPlayer.color][pieceToMove.RoutePosition]);

            }
            else
            {
                // Remove piece from current field
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

                MessageYouGotHitBack();
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

        public bool FieldHasAPieceOnIt(LudoColor color, int fieldIndexPosition)
        {
            if (ludoBoard.routes[color][fieldIndexPosition].CountOccupants() > 0 && ludoBoard.routes[color][fieldIndexPosition].GetOccupantColor() == currentPlayer.color)
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
            Field targetField = ludoBoard.routes[currentPlayer.color][index];
            
            // Hit opponent back to base if only one opponent
            if (targetField.CountOccupants() == 1 && targetField.GetOccupantColor() != currentPlayer.color)
            {
                // Set opponent status to "in base"
                targetField.occupants[0].inBase = true;
                // Reset opponent position
                targetField.occupants[0].RoutePosition = 0;
                // Put opponent color back in opponent base (UI part)
                int opponentPieceId = targetField.occupants[0].BasePositionId;
                LudoColor opponentColor = targetField.occupants[0].color;
                ui.ChangeColor(opponentColor, ui.btnColorBases[opponentColor][opponentPieceId]);
                // Remove opponent from ludoboard field
                targetField.RemoveOccupant();
                // Put currentPlayer's piece in ludoboard field
                targetField.AddOccupant(pieceToMove);

                return true;
            }
            // Get hit back by opponent
            else if (targetField.CountOccupants() > 1 && targetField.GetOccupantColor() != currentPlayer.color)
            {
                
                pieceToMove.inBase = true;
                pieceToMove.RoutePosition = 0;
                // Update ui
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
            if (ui.btnColorBases[currentPlayer.color].Contains(chosenField) || ui.btnColorRoutes[currentPlayer.color].Contains(chosenField) && chosenField != ui.btnColorRoutes[currentPlayer.color][56])
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
        private async void MessageNotAValidMove()
        {
            ui.UpdateInformationLabel("");
            await Task.Delay(500);
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
            int numberOfPieces = 4;
            bool allPiecesReachedGoal = true;
            for (int i = 1; i <= numberOfPieces; i++)
            {
                if(playerPiecePairs[currentPlayer][i].reachedGoal == false)
                {
                    allPiecesReachedGoal = false;
                }
            }

            if (allPiecesReachedGoal == true)
            {
                ui.UpdateInformationLabel("Won the game!");
                MessageBox.Show(currentPlayer.color.ToString() + " has won the game!");
            }
            
        }
        #endregion

    }
}

