using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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
            ui.btnEndTurn.IsEnabled = true;
            StartNextTurn();
            await Task.Delay(1000);
        }

        /// <summary>
        /// Sets next currentPlayer, activaces die and updates information label
        /// </summary>
        public void StartNextTurn()
        {
            GoToNextPlayer();
            ui.ShowCurrentPlayer(currentPlayer.color.ToString());
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
                ui.UpdateInformationLabel("Rolled " + dieValue.ToString());

            }
            // When all in base and thrown 1-2 times
            else if (Die.TimesThrown < 2)
            {
                ui.UpdateInformationLabel("Rolled " + dieValue.ToString());
                Die.TimesThrown++;
            }
            // All in base, and thrown three times
            else
            {
                ui.UpdateInformationLabel("Rolled " + dieValue.ToString());
                Die.TimesThrown = 0;
                ui.btnDie.IsEnabled = false;
            }
        }

        public void EndTurn()
        {
            
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

        

        

        

        /*
         * Let player throw die
         * update labels
         * logic to figure out which pieces can be moved
         * Let player choose piece to move
         * Logic to let player know if piece can be moved
         * Event end turn - logic 
         * Event startnextturn
         */

            

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

            if (isValidField == true)
            {
                // Is the chosen field to move Piece from part of base?
                bool isBaseField = ui.btnColorBases[currentPlayer.color].Contains(chosenField) ? true : false;
                // Find Position/index of field. Base(0-3) or Route(0-57)
                int positionOfPiece = isBaseField ? ui.btnColorBases[currentPlayer.color].IndexOf(chosenField)
                    : ui.btnColorRoutes[currentPlayer.color].IndexOf(chosenField);
                
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
                else
                {
                    // Make sure not to pass ally
                    // Remove from old field
                    // Check for opponents
                    // Update field



                    // end turn
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
                ludoBoard.AddPieceToFieldOnRoute(pieceToMove, pieceToMove.RoutePosition);
                // Check to see if the target position is free for the taking
                bool canMovePiece = CanTakeField(pieceToMove.RoutePosition, pieceToMove);
                if (canMovePiece == true)
                {
                    // Change color on button base field to show that current Piece has left it
                    ui.ChangeColor(currentPlayer.color, chosenField);

                    // Update occupantcount on target field (UI part)
                    ui.ChangeNumberOfOccuppants(ludoBoard.routes[currentPlayer.color][pieceToMove.RoutePosition].CountOccupants(), chosenField);
                }
                else if (canMovePiece == false)
                {
                    MessageYouGotHitBack();
                }
                
                // End turn
            }
            else
            {
                MessageNotAValidMove();
            }
        }

        public void MovePieceFromFieldToField()
        {
            // dievalue + current position
        }

        /// <summary>
        /// Determines whether or not current players piece will hit opponent home or the opposite.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="pieceToMove"></param>
        /// <returns></returns>
        private bool CanTakeField(int index, Piece pieceToMove)
        {
            Field targetField = ludoBoard.routes[currentPlayer.color][index];
            
            // Hit opponent back to base if only one
            if (targetField.GetOccupantColor() != currentPlayer.color && targetField.CountOccupants() == 1)
            {
                // Set opponent status to in base
                targetField.occupants[0].inBase = true;
                // Put opponent color back in opponent base (UI part)
                int opponentPieceId = targetField.occupants[0].BasePositionId;
                LudoColor opponentColor = targetField.occupants[0].color;
                ui.ChangeColor(opponentColor, ui.btnColorBases[opponentColor][opponentPieceId]);
                // Remove opponent from ludoboard field
                targetField.RemoveOccupant();
                // Put currentPlayers piece in ludoboard field
                targetField.AddOccupant(pieceToMove);

                return true;
            }
            // Get hit back by opponent
            else if (targetField.GetOccupantColor() != currentPlayer.color && targetField.CountOccupants() > 1)
            {
                pieceToMove.inBase = true;

                return false;
            }
            else
            {
                return true;
            }
        }

        private bool ValidateIfChosenBtnFieldIsPartOfRoute(Button chosenField)
        {
            // Check if clicked field is part of the base or route of the current player
            if (ui.btnColorBases[currentPlayer.color].Contains(chosenField) || ui.btnColorRoutes[currentPlayer.color].Contains(chosenField))
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

        private void ValidateMoveInBackEnd(int routeIndex)
        {
            Field[] route = GetCorrespondingColoredRoute(currentPlayer.color);
            // check to see if there is a piece on field
        }

        private Field[] GetCorrespondingColoredRoute(LudoColor color)
        {
            if (color == LudoColor.Blue)
            {
                return ludoBoard.blueRoute;
            }
            else if (color == LudoColor.Green)
            {
                return ludoBoard.greenRoute;
            }
            else if (color == LudoColor.Red)
            {
                return ludoBoard.redRoute;
            }
            else
            {
                return ludoBoard.yellowRoute;
            }
        }

        public void RegisterMove(object source, object lblinfo, EventArgs e)
        {
            if (APieceHasBeenChosen == true)
            {
                MovePieceToField(source, e);
            }
            else
            {
                MovePieceFromField(source, e);
            }
        }

        public void MovePieceFromField(object source, EventArgs e)
        {
            Button fieldWithPiecePicked = source as Button;
            if (fieldWithPiecePicked.Background  == Brushes.Blue)// && currentPlayer.color == LudoColor.Blue)
            {
                APieceHasBeenChosen = true;
                ui.btnFieldToMoveFrom = fieldWithPiecePicked;
            }
        }

        public void MovePieceToField(object source, EventArgs e)
        {
            Button targetFieldButton = source as Button;
            targetFieldButton.Background = Brushes.Blue;
            ui.btnFieldToMoveFrom.Background = Brushes.White;
            APieceHasBeenChosen = false;
        }
        #endregion

    }
}


//ui.btnEndTurn.RaiseEvent(new System.Windows.RoutedEventArgs(Button.ClickEvent));

/*
class Program
{
    
        // Prompt for number of players
        PromptUser.ChooseNumberOfPlayers();

        // Activate player chosen number of players
        for (int i = 0; i < numberOfPlayers; i++)
        {
            activePlayers.Add(allPlayers[i]);
        }
        PromptUser.TurnAIOnOff();

        // Run the game
        bool keepRunning = true;
        while (keepRunning)
        {
            Console.Clear();
            // check if the player's pieces have all reached the goal and remove from activePlayer list if yes
            RemovePlayersFromActivePlayListIfWon(activePlayers);
            // if no players in list, game is finished
            if (activePlayers.Count == 0)
            {
                keepRunning = false;
                continue;
            }
            // Logic to loop through each player's turn
            foreach (Player player in activePlayers)
            {
                Console.Clear();
                Console.WriteLine(player.Colour + " player's turn.");
                switch (player.Colour)
                {
                    case "Green":
                        // if all pieces are in base roll three times or until 6
                        if (board.CheckIfAllPiecesInBase(greenPieces) == true)
                        {
                            diceRollResult = Dice.RollDiceThreeTimes();
                            if (diceRollResult != 6)
                            {
                                break;
                            }
                        }
                        else
                        {
                            diceRollResult = Dice.RollDice();
                        }
                        board.DisplayBoard();
                        if (greenPlayer.ComputerAI == true)
                        {
                            ComputerAI.ChoosePieceToMove(greenPieces);
                        }
                        else
                        {
                            PromptUser.ChoosePieceToMove(greenPieces);

                        }
                        board.MovePiece(board.greenRoute, greenPieces[pieceToMove], diceRollResult);
                        board.DisplayBoard();
                        break;
                    case "Blue":
                        if (board.CheckIfAllPiecesInBase(bluePieces) == true)
                        {
                            diceRollResult = Dice.RollDiceThreeTimes();
                            if (diceRollResult != 6)
                            {
                                break;
                            }
                        }
                        else
                        {
                            diceRollResult = Dice.RollDice();
                        }
                        board.DisplayBoard();
                        if (bluePlayer.ComputerAI == true)
                        {
                            ComputerAI.ChoosePieceToMove(bluePieces);
                        }
                        else
                        {
                            PromptUser.ChoosePieceToMove(bluePieces);
                        }
                        board.MovePiece(board.blueRoute, bluePieces[pieceToMove], diceRollResult);
                        board.DisplayBoard();
                        break;
                    case "Yellow":
                        if (board.CheckIfAllPiecesInBase(yellowPieces) == true)
                        {
                            diceRollResult = Dice.RollDiceThreeTimes();
                            if (diceRollResult != 6)
                            {
                                break;
                            }
                        }
                        else
                        {
                            diceRollResult = Dice.RollDice();
                        }
                        board.DisplayBoard();
                        if (yellowPlayer.ComputerAI == true)
                        {
                            ComputerAI.ChoosePieceToMove(yellowPieces);
                        }
                        else
                        {
                            PromptUser.ChoosePieceToMove(yellowPieces);
                        }
                        board.MovePiece(board.yellowRoute, yellowPieces[pieceToMove], diceRollResult);
                        board.DisplayBoard();
                        break;
                    case "Red":
                        if (board.CheckIfAllPiecesInBase(redPieces) == true)
                        {
                            diceRollResult = Dice.RollDiceThreeTimes();
                            if (diceRollResult != 6)
                            {
                                break;
                            }
                        }
                        else
                        {
                            diceRollResult = Dice.RollDice();
                        }
                        board.DisplayBoard();
                        if (redPlayer.ComputerAI == true)
                        {
                            ComputerAI.ChoosePieceToMove(redPieces);
                        }
                        else
                        {
                            PromptUser.ChoosePieceToMove(redPieces);
                        }
                        board.MovePiece(board.redRoute, redPieces[pieceToMove], diceRollResult);
                        board.DisplayBoard();
                        break;
                }
            }
        }

        Console.WriteLine("Congratulations! You all won!");
        foreach (Player player in winners)
        {
            Console.WriteLine(player);
        }
        // optional: input code to show player-ranking. Use "List<Player> winners" for simple solution 

        //board.demoBoard() for testing board fields and piece movement. Deprecated. Need to make variables public 
        Console.ReadLine();

    }


    // Remove from active list at put into winners list
    static void RemovePlayersFromActivePlayListIfWon(List<Player> activePlayers)
    {
        if (activePlayers.Exists(Player => Player == greenPlayer))
        {
            if (Board.CheckIfAllPiecesReachedGoal(greenPieces) == true)
            {
                activePlayers.Remove(greenPlayer);
                winners.Add(greenPlayer);
            }
        }

        if (activePlayers.Exists(Player => Player == bluePlayer))
        {
            if (Board.CheckIfAllPiecesReachedGoal(bluePieces) == true)
            {
                activePlayers.Remove(bluePlayer);
                winners.Add(bluePlayer);
            }
        }

        if (activePlayers.Exists(Player => Player == yellowPlayer))
        {
            if (Board.CheckIfAllPiecesReachedGoal(yellowPieces) == true)
            {
                activePlayers.Remove(yellowPlayer);
                winners.Add(yellowPlayer);
            }
        }

        if (activePlayers.Exists(Player => Player == redPlayer))
        {
            if (Board.CheckIfAllPiecesReachedGoal(redPieces) == true)
            {
                activePlayers.Remove(redPlayer);
                winners.Add(redPlayer);
            }
        }
    }
}

class PromptUser
{
    public static void ChooseNumberOfPlayers()
    {
        Console.Write("Choose number of players: ");
        string userInput = Console.ReadLine();
        Console.Clear();
        if (string.IsNullOrWhiteSpace(userInput) ||
            Convert.ToInt32(userInput) > 4 ||
            Convert.ToInt32(userInput) < 2)
        {
            Console.WriteLine("Invalid number of players. Try again");
            ChooseNumberOfPlayers();
        }
        else
        {
            Program.numberOfPlayers = Convert.ToInt32(userInput);
        }
    }

    public static void ChoosePieceToMove(Piece[] pieces)
    {
        Console.Write("Choose which piece (number) you want to move: ");
        string userInput = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(userInput) ||
            Convert.ToInt32(userInput) > 4 ||
            Convert.ToInt32(userInput) < 1)
        {
            Console.WriteLine("Invalid piece number. Try again");
            ChoosePieceToMove(pieces);
        }
        else
        {
            // minus 1 so pieceToMove matches 0 index array
            Program.pieceToMove = Convert.ToInt32(userInput) - 1;
        }

        if (Piece.CheckIfPieceCanBeMoved(pieces[Program.pieceToMove]) == false)
        {
            ChoosePieceToMove(pieces);
        }
    }

    public static void TurnAIOnOff()
    {
        Console.WriteLine("Do you wish AI players? Write \"yes\" or \"no\" to choose. Answers are CASESENSITIVE!");
        string userinput = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(userinput) || userinput != "yes")
        {
            Console.WriteLine("Computer AI set to off");
        }
        else
        {
            ChooseAIPlayers(Program.activePlayers);
        }

    }

    public static void ChooseAIPlayers(List<Player> activeplayers)
    {
        Console.WriteLine("Which players should be controlled by the computer AI?");
        Console.WriteLine("You can choose from these players:");

        switch (activeplayers.Count)
        {
            case 2:
                // green blue
                foreach (Player player in activeplayers)
                {
                    Console.WriteLine("\t" + player.Colour);
                }
                break;
            case 3:
                // green blue yellow
                foreach (Player player in activeplayers)
                {
                    Console.WriteLine("\t" + player.Colour);
                }
                break;
            case 4:
                // green blue yellow red
                foreach (Player player in activeplayers)
                {
                    Console.WriteLine("\t" + player.Colour);
                }
                break;
        }
        Console.WriteLine("Choice is casesensitive. When done choosing write \"finished\" to start the game!");
        // boolean to check userchoice for turning AI on/ff
        bool makeAIChoices = true;
        // variable to avoid certain loops to happen
        bool colourMatch = false;

        string userInput;

        while (makeAIChoices)
        {
            userInput = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(userInput))
            {
                Console.WriteLine("Invalid input. Please write \"finished\" or a valid player colour");
                continue;
            }
            if (userInput == "finished")
            {
                makeAIChoices = false;
                continue;
            }
            // Loop to check if input matches colour of active players
            for (int i = 0; i < activeplayers.Count; i++)
            {
                if (userInput == activeplayers[i].Colour)
                {
                    // sest colourmatch to true so invalid colourloop does not run
                    colourMatch = true;
                    if (activeplayers[i].ComputerAI == false)
                    {
                        activeplayers[i].ComputerAI = true;
                        Console.WriteLine(activeplayers[i].Colour + " player AI turned ON");
                        break;
                    }
                    else
                    {
                        activeplayers[i].ComputerAI = false;
                        Console.WriteLine(activeplayers[i].Colour + " player AI turned OFF");
                        break;
                    }
                }
            }

            // if colour string doesn't match only then run this
            if (colourMatch == false)
            {
                for (int i = 0; i < activeplayers.Count; i++)
                {
                    if (userInput != activeplayers[i].Colour)
                    {
                        Console.WriteLine("Invalid player colour!");
                        break;
                    }
                }
            }
            else
            {
                continue;
            }
            // reset colourmatch
            colourMatch = false;

        }
    }
}
*/