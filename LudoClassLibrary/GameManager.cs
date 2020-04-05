using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows.Controls;

namespace LudoClassLibrary
{
    public class GameManager
    {
        #region Fields

        #endregion

        #region Properties
        public bool PickedPieceToMove = false;
        public Player currentPlayer;
        #endregion

        public int numberOfPlayers = 4;
        
        public int finishedPlayerCount = 0;
        public event EventHandler dieRollResult;

        public Player bluePlayer = new HumanPlayer(LudoColor.Blue);
        public Player greenPlayer = new HumanPlayer(LudoColor.Green);
        public Player redPlayer = new HumanPlayer(LudoColor.Red);
        public Player yellowPlayer = new HumanPlayer(LudoColor.Yellow);

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

        public Board ludoBoard = new Board();
        



        public void ChangeButtonContent(object source, object lbl, EventArgs e)
        {
            Button field = source as Button;
            Label labl = lbl as Label;
            //field.Content = "TEST";
            //labl.Content = Die.RandomDieValue;
        }

        public void RunGame(object source, object lblPlayerTurn, object lblInfo, EventArgs e)
        {
            Label info = lblInfo as Label;
            info.Content = "Game started!";
            Button start = source as Button;
            start.IsEnabled = false;

            bool isRunning = true;

            while (isRunning)
            {
                Label test = lblPlayerTurn as Label;
                test.Content = "Blue's turn";
                isRunning = false;
            }

        }

        public void TurnAIOnOff()
        {

        }

        public void ShowCurrentPlayer(Label lblPlayerTurn)
        {
            lblPlayerTurn.Content = currentPlayer.color.ToString() + "'s turn";
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