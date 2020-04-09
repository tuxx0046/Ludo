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

        public Dictionary<Player, List<Piece>> playersAndPieces = new Dictionary<Player, List<Piece>>();
            
        

        public Board ludoBoard = new Board();
        public UIManager ui = new UIManager();
        
        
        public async void ThrowDie(object source, EventArgs e)
        {
            bool allPiecesAreOut = true;
            playersAndPieces.Add(bluePlayer, bluePieces);
            for (int i = 0; i < playersAndPieces[currentPlayer].Count - 1; i++)
            {
                if (playersAndPieces[currentPlayer][i].inBase == false)
                {
                    allPiecesAreOut = false;
                }
            }
            if (allPiecesAreOut == false)
            {
                // allow three throws
            }
            // allow only one throw
            else
            {

            }

            foreach (Piece piece in playersAndPieces[bluePlayer])
            {
                await Task.Delay(1000);
                ui.lblInformation.Content = piece.BasePositionId.ToString();
            }
            // if all pieces are still in base, allow three throws
            ui.lblInformation.Content = Die.RandomDieValue.ToString();
            ui.btnDie = source as Button;
            //btnDie.IsEnabled = false;
        }

        public void ChangeButtonContent(object source, object lbl, EventArgs e)
        {
            Button field = source as Button;
            Label labl = lbl as Label;
            //field.Content = "TEST";
            //labl.Content = Die.RandomDieValue;
        }

        /// <summary>
        /// Sets current player to be next color from LudoColors. <br/>
        /// Starts from blue, green, red, yellow and then loops.
        /// </summary>
        public void SetNextPlayer()
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
        public async void RunGame(object[] source, object lblPlayerTurn, object lblInfo, object die, object endTurn, EventArgs e)
        {
            // Enable btnDie and btnEndturn
            // labels need to show information
            AddPlayersToGame();
            StartNextTurn();
            await Task.Delay(1000);
        }

        

        public void StartNextTurn()
        {
            SetNextPlayer();
            ui.ShowCurrentPlayer(currentPlayer.color.ToString());
            ui.btnDie.IsEnabled = true;
            ui.lblInformation.Content = "Please throw die";
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

        /****** Change colors on buttons *************/
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

        
    }
}



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