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
        /// If base, changes to white if ludocolored, or ludocolored if white
        /// </summary>
        /// <param name="color"></param>
        /// <param name="field"></param>
        public void ChangeColor(LudoColor color, Button field)
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
        public void ChangeColorToOriginalColor(Button field)
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

        public int GetIndexOfField(LudoColor color, Button chosenField)
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
        
        //public void DisableAllRoutesAndBases()
        //{
        //    foreach (Button button in btnColorBases[LudoColor.Blue])
        //    {
        //        button.IsEnabled = false;
        //    }
        //    foreach (Button button in btnColorBases[LudoColor.Green])
        //    {
        //        button.IsEnabled = false;
        //    }
        //    foreach (Button button in btnColorBases[LudoColor.Red])
        //    {
        //        button.IsEnabled = false;
        //    }
        //    foreach (Button button in btnColorBases[LudoColor.Yellow])
        //    {
        //        button.IsEnabled = false;
        //    }

        //    foreach (Button button in btnColorRoutes[LudoColor.Blue])
        //    {
        //        button.IsEnabled = false;
        //    }
        //    foreach (Button button in btnColorRoutes[LudoColor.Green])
        //    {
        //        button.IsEnabled = false;
        //    }
        //    foreach (Button button in btnColorRoutes[LudoColor.Red])
        //    {
        //        button.IsEnabled = false;
        //    }
        //    foreach (Button button in btnColorRoutes[LudoColor.Yellow])
        //    {
        //        button.IsEnabled = false;
        //    }
        //}

        //public void EnableAllRoutesAndBases()
        //{
        //    foreach (Button button in btnColorBases[LudoColor.Blue])
        //    {
        //        button.IsEnabled = true;
        //    }
        //    foreach (Button button in btnColorBases[LudoColor.Green])
        //    {
        //        button.IsEnabled = true;
        //    }
        //    foreach (Button button in btnColorBases[LudoColor.Red])
        //    {
        //        button.IsEnabled = true;
        //    }
        //    foreach (Button button in btnColorBases[LudoColor.Yellow])
        //    {
        //        button.IsEnabled = true;
        //    }

        //    foreach (Button button in btnColorRoutes[LudoColor.Blue])
        //    {
        //        button.IsEnabled = true;
        //    }
        //    foreach (Button button in btnColorRoutes[LudoColor.Green])
        //    {
        //        button.IsEnabled = true;
        //    }
        //    foreach (Button button in btnColorRoutes[LudoColor.Red])
        //    {
        //        button.IsEnabled = true;
        //    }
        //    foreach (Button button in btnColorRoutes[LudoColor.Yellow])
        //    {
        //        button.IsEnabled = true;
        //    }
        //}
        
    }
}
