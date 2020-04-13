using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace LudoClassLibrary
{
    /// <summary>
    /// Manages the user interface, how the buttons/fields look and the information labels.
    /// </summary>
    public class UIManager
    {

        internal Button btnDie;
        internal Button btnEndTurn;
        private Label lblPlayerTurn;
        private Label lblInformation;
        internal Dictionary<LudoColor, List<Button>> btnColorRoutes = new Dictionary<LudoColor, List<Button>>();
        internal Dictionary<LudoColor, List<Button>> btnColorBases = new Dictionary<LudoColor, List<Button>>();

        /// <summary>
        /// Gets XAML button Lists from Ludo project and adds the to the ui object Color Routes dictionary
        /// </summary>
        /// <param name="blueRoute"></param>
        /// <param name="greenRoute"></param>
        /// <param name="redRoute"></param>
        /// <param name="yellowRoute"></param>
        public void DefineBtnRoutes(object blueRoute, object greenRoute, object redRoute, object yellowRoute)
        {
            btnColorRoutes.Add(LudoColor.Blue, blueRoute as List<Button>);
            btnColorRoutes.Add(LudoColor.Green, greenRoute as List<Button>);
            btnColorRoutes.Add(LudoColor.Red, redRoute as List<Button>);
            btnColorRoutes.Add(LudoColor.Yellow, yellowRoute as List<Button>);
        }

        /// <summary>
        /// Gets XAML button Lists from Ludo project and adds the to the ui object Color Bases dictionary
        /// </summary>
        /// <param name="blueBase"></param>
        /// <param name="greenBase"></param>
        /// <param name="redBase"></param>
        /// <param name="yellowBase"></param>
        public void DefineBtnBases(object blueBase, object greenBase, object redBase, object yellowBase)
        {
            btnColorBases.Add(LudoColor.Blue, blueBase as List<Button>);
            btnColorBases.Add(LudoColor.Green, greenBase as List<Button>);
            btnColorBases.Add(LudoColor.Red, redBase as List<Button>);
            btnColorBases.Add(LudoColor.Yellow, yellowBase as List<Button>);
        }

        /// <summary>
        /// Gets XAML buttons and labels from Ludo project and adds the to the ui fields for manipulation in GameManager
        /// </summary>
        /// <param name="die"></param>
        /// <param name="endTurn"></param>
        /// <param name="information"></param>
        /// <param name="playerTurn"></param>
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
        internal void ShowCurrentPlayer(string currentPlayerColor)
        {
            lblPlayerTurn.Content = currentPlayerColor + "'s turn";
        }

        /// <summary>
        /// Shows information like die value in label
        /// </summary>
        /// <param name="information"></param>
        internal async void UpdateInformationLabel(string information)
        {
            lblInformation.Content = "";
            await Task.Delay(500);
            lblInformation.Content = information;
        }

        /// <summary>
        /// Changes the color of the button field.
        /// If base, changes to white if ludocolored, or ludocolored if white
        /// </summary>
        /// <param name="color"></param>
        /// <param name="field"></param>
        internal void ChangeColor(LudoColor color, Button field)
        {
            if (btnColorBases[color].Contains(field))
            {
                if (field.Background == Brushes.White)
                {
                    ChangeColorToPlayerColor(color, field);
                }
                else
                {
                    field.Background = Brushes.White;
                }
            }
            else if (btnColorRoutes[color].Contains(field))
            {
                switch (field)
                {
                    case Button index0 when field == btnColorRoutes[color][0]:
                        ChangeColorToPlayerColor(color, field);
                        break;
                    case Button index56 when field == btnColorRoutes[color][56]:
                    case Button index55 when field == btnColorRoutes[color][55]:
                    case Button index54 when field == btnColorRoutes[color][54]:
                    case Button index53 when field == btnColorRoutes[color][53]:
                    case Button index52 when field == btnColorRoutes[color][52]:
                        break;
                    default:
                        ChangeColorToPlayerColor(color, field);
                        break;
                }
            }
            
        }

        /// <summary>
        /// Updates the number of occuppants on the button field.
        /// </summary>
        /// <param name="numberOfOccuppants"></param>
        /// <param name="field"></param>
        internal void ChangeNumberOfOccuppants(int numberOfOccuppants, Button field)
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

        private void ChangeColorToPlayerColor(LudoColor color, Button field)
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

        /// <summary>
        /// Changes field back to its starting color. Use on route fields.
        /// </summary>
        /// <param name="field"></param>
        internal void ChangeColorToOriginalColor(Button field)
        {
            if (field == btnColorRoutes[LudoColor.Blue][0] || 
                field == btnColorRoutes[LudoColor.Blue][51] || 
                field == btnColorRoutes[LudoColor.Blue][52] || 
                field == btnColorRoutes[LudoColor.Blue][53] ||
                field == btnColorRoutes[LudoColor.Blue][54] ||
                field == btnColorRoutes[LudoColor.Blue][55])
            {
                field.Background = Brushes.Blue;
            }
            else if (field == btnColorRoutes[LudoColor.Green][0] ||
                     field == btnColorRoutes[LudoColor.Green][51] ||
                     field == btnColorRoutes[LudoColor.Green][52] ||
                     field == btnColorRoutes[LudoColor.Green][53] ||
                     field == btnColorRoutes[LudoColor.Green][54] ||
                     field == btnColorRoutes[LudoColor.Green][55])
            {
                field.Background = Brushes.Green;
            }
            else if (field == btnColorRoutes[LudoColor.Red][0] ||
                     field == btnColorRoutes[LudoColor.Red][51] ||
                     field == btnColorRoutes[LudoColor.Red][52] ||
                     field == btnColorRoutes[LudoColor.Red][53] ||
                     field == btnColorRoutes[LudoColor.Red][54] ||
                     field == btnColorRoutes[LudoColor.Red][55])
            {
                field.Background = Brushes.Red;
            }
            else if (field == btnColorRoutes[LudoColor.Yellow][0] ||
                     field == btnColorRoutes[LudoColor.Yellow][51] ||
                     field == btnColorRoutes[LudoColor.Yellow][52] ||
                     field == btnColorRoutes[LudoColor.Yellow][53] ||
                     field == btnColorRoutes[LudoColor.Yellow][54] ||
                     field == btnColorRoutes[LudoColor.Yellow][55])
            {
                field.Background = Brushes.Yellow;
            }
            else
            {
                field.Background = Brushes.White;
            }
        }

        internal int GetIndexOfField(LudoColor color, Button chosenField)
        {
            int index;

            if (btnColorBases[color].Contains(chosenField))
            {
                index = btnColorBases[color].IndexOf(chosenField);
            }
            else
            {
                index = btnColorRoutes[color].IndexOf(chosenField);
            }
            
            return index;
        }
    }
}
