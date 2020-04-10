using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;

namespace LudoClassLibrary
{
    /// <summary>
    /// Manages the user interface, how the buttons/fields look and the information labels.
    /// </summary>
    public class UIManager
    {

        public Button btnDie;
        public Button btnFieldToMoveFrom;
        public Button btnEndTurn;
        public Label lblPlayerTurn;
        public Label lblInformation;
        public Dictionary<LudoColor, List<Button>> btnColorRoutes = new Dictionary<LudoColor, List<Button>>();
        public Dictionary<LudoColor, List<Button>> btnColorBases = new Dictionary<LudoColor, List<Button>>();

        public void DefineBtnRoutes(object blueRoute, object greenRoute, object redRoute, object yellowRoute)
        {
            btnColorRoutes.Add(LudoColor.Blue, blueRoute as List<Button>);
            btnColorRoutes.Add(LudoColor.Green, greenRoute as List<Button>);
            btnColorRoutes.Add(LudoColor.Red, redRoute as List<Button>);
            btnColorRoutes.Add(LudoColor.Yellow, yellowRoute as List<Button>);
        }

        public void DefineBtnBases(object blueBase, object greenBase, object redBase, object yellowBase)
        {
            btnColorBases.Add(LudoColor.Blue, blueBase as List<Button>);
            btnColorBases.Add(LudoColor.Green, greenBase as List<Button>);
            btnColorBases.Add(LudoColor.Red, redBase as List<Button>);
            btnColorBases.Add(LudoColor.Yellow, yellowBase as List<Button>);
        }

        public void InitializeUILabelsAndActionBtns(object die, object endTurn, object information, object playerTurn)
        {
            btnDie = die as Button;
            btnEndTurn = endTurn as Button;
            lblPlayerTurn = playerTurn as Label;
            lblInformation = information as Label;
        }

        /// <summary>
        /// Show who's turn it is in label
        /// </summary>
        /// <param name="currentPlayerColor"></param>
        public void ShowCurrentPlayer(string currentPlayerColor)
        {
            lblPlayerTurn.Content = currentPlayerColor + "'s turn";
        }

        /// <summary>
        /// Shows information like die value in label
        /// </summary>
        /// <param name="information"></param>
        public void UpdateInformationLabel(string information)
        {
            lblInformation.Content = information;
        }

        /// <summary>
        /// Changes the color of the button field.
        /// Changes to white if ludocolored, or ludocolored if white
        /// </summary>
        /// <param name="color"></param>
        /// <param name="field"></param>
        public void ChangeColor(LudoColor color, Button field)
        {
            if (field.Background == Brushes.White)
            {
                if (color == LudoColor.Blue)
                {
                    field.Background = Brushes.Blue;
                }
                else if (color == LudoColor.Green)
                {
                    field.Background = Brushes.Green;
                }
                else if (color == LudoColor.Red)
                {
                    field.Background = Brushes.Red;
                }
                else if (color == LudoColor.Yellow)
                {
                    field.Background = Brushes.Yellow;
                }
            }
            else
            {
                field.Background = Brushes.White;
            }
        }

        /// <summary>
        /// Updates the number of occuppants on the button field.
        /// </summary>
        /// <param name="numberOfOccuppants"></param>
        /// <param name="field"></param>
        public void ChangeNumberOfOccuppants(int numberOfOccuppants, Button field)
        {
            if (numberOfOccuppants == 0)
            {
                field.Content = "";
            }
            else
            {
                field.Content = numberOfOccuppants.ToString();
            }
        }
    }
}
/*
 * 
 * FIX PROBLEM WITH FIRST BUTTON ON ROUTE COLOR WHEN OPPONENTS ARE ON*/