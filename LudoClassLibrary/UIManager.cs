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
        public Button[] btnBlueRoute;
        public Button[] btnGreenRoute;
        public Button[] btnRedRoute;
        public Button[] btnYellowRoute;

        public void DefineBtnRoutes(object blueRoute, object greenRoute, object redRoute, object yellowRoute)
        {
            btnBlueRoute = blueRoute as Button[];
            btnGreenRoute = greenRoute as Button[];
            btnRedRoute = redRoute as Button[];
            btnYellowRoute = yellowRoute as Button[];
        }

        public void InitializeUILabelsAndActionBtns(object die, object endTurn, object information, object playerTurn)
        {
            btnDie = die as Button;
            btnEndTurn = endTurn as Button;
            lblPlayerTurn = playerTurn as Label;
            lblInformation = information as Label;
        }

        public void ShowCurrentPlayer(string currentPlayerColor)
        {
            lblPlayerTurn.Content = currentPlayerColor + "'s turn";
        }
    }
}
