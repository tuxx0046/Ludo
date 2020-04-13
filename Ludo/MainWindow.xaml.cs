using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LudoClassLibrary;

namespace Ludo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public delegate void StartGameHandler();
        public delegate void AIHandler(List<LudoColor> AIPlayersEnabled);
        public delegate void ThrowDieHandler();
        public delegate void EndTurnHandler();
        public delegate void ColoredRoutesHandler(object blueRoute, object greenRoute, object redRoute, object yellowRoute);
        public delegate void ColoredBasesHandler(object blueBase, object greenBase, object redBase, object yellowBase);
        public delegate void UIButtonsAndLabelsHandler(object die, object endTurn, object infolabel, object playerTurnInfo);
        public event StartGameHandler StartGameEvent;
        public event AIHandler AIEnableEvent;
        public event ThrowDieHandler ThrowDieEvent;
        public event EndTurnHandler EndTurnEvent;
        public event ColoredRoutesHandler PrepareColoredRoutes;
        public event UIButtonsAndLabelsHandler PrepareBtnsAndLabels;
        public event ColoredBasesHandler PrepareColoredBases;
        public event EventHandler MovePiece;
        

        public MainWindow()
        {
            InitializeComponent();
            GameManager gm = new GameManager();
            //gm.ludoBoard.InitWholeBoardAndCreateRoutes();
            StartGameEvent += gm.RunGame;
            AIEnableEvent += gm.RegisterEnabledAIPlayers;
            ThrowDieEvent += gm.ThrowDie;
            EndTurnEvent += gm.StartNextTurn;
            PrepareColoredRoutes += gm.ui.DefineBtnRoutes;
            PrepareColoredBases += gm.ui.DefineBtnBases;
            PrepareBtnsAndLabels += gm.ui.InitializeUILabelsAndActionBtns;
            MovePiece += gm.ChoosePieceToMove;
        }

        private void MovePieceFromAndTo(object sender, RoutedEventArgs e)
        {
            MovePiece?.Invoke(sender, e);
            
        }

        private void ThrowDie(object sender, RoutedEventArgs e)
        {
            ThrowDieEvent?.Invoke();
        }

        private void EndTurn(object sender, RoutedEventArgs e)
        {
            EndTurnEvent?.Invoke();
        }

        /// <summary>
        /// Starts game. Will send AI info to the game manager and lock(disable checkbox) AI settings.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartGame(object sender, RoutedEventArgs e)
        {
            btnStart.IsEnabled = false;
            CheckEnabledAI();
            CreateRoutesAndBases();
            SendButtonsAndLabelsToUIManager();
            StartGameEvent?.Invoke();
        }

        /// <summary>
        /// Checks for enabled AIs
        /// </summary>
        private void CheckEnabledAI()
        {
            // Lock AI settings
            chkBxBlueAI.IsEnabled = false;
            chkBxGreenAI.IsEnabled = false;
            chkBxRedAI.IsEnabled = false;
            chkBxYellowAI.IsEnabled = false;

            // Register enabled AIs
            List<LudoColor> EnabledAIs = new List<LudoColor>();

            if (chkBxBlueAI.IsChecked == true)
            {
                EnabledAIs.Add(LudoColor.Blue);
            }

            if (chkBxGreenAI.IsChecked == true)
            {
                EnabledAIs.Add(LudoColor.Green);
            }

            if (chkBxRedAI.IsChecked == true)
            {
                EnabledAIs.Add(LudoColor.Red);
            }

            if (chkBxYellowAI.IsChecked == true)
            {
                EnabledAIs.Add(LudoColor.Yellow);
            }

            // Raise registration event
            AIEnableEvent?.Invoke(EnabledAIs);
        }

        private void ExitApplication(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void RestartApplication(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
            Application.Current.Shutdown();
        }

        /// <summary>
        /// Initializes button routes and sends them back to UIManager.<br/>
        /// Raises event.
        /// </summary>
        private void CreateRoutesAndBases()
        {
            List<Button> blueRoute = new List<Button>
            {
                Field1,
                Field2,
                Field3,
                Field4,
                Field5,
                Field6,
                Field7,
                Field8,
                Field9,
                Field10,
                Field11,
                Field12,
                Field13,
                Field14,
                Field15,
                Field16,
                Field17,
                Field18,
                Field19,
                Field20,
                Field21,
                Field22,
                Field23,
                Field24,
                Field25,
                Field26,
                Field27,
                Field28,
                Field29,
                Field30,
                Field31,
                Field32,
                Field33,
                Field34,
                Field35,
                Field36,
                Field37,
                Field38,
                Field39,
                Field40,
                Field41,
                Field42,
                Field43,
                Field44,
                Field45,
                Field46,
                Field47,
                Field48,
                Field49,
                Field50,
                Field51,
                BlueField0,
                BlueField1,
                BlueField2,
                BlueField3,
                BlueField4,
                BlueField5
            };
            List<Button> greenRoute = new List<Button>
            {
                Field14,
                Field15,
                Field16,
                Field17,
                Field18,
                Field19,
                Field20,
                Field21,
                Field22,
                Field23,
                Field24,
                Field25,
                Field26,
                Field27,
                Field28,
                Field29,
                Field30,
                Field31,
                Field32,
                Field33,
                Field34,
                Field35,
                Field36,
                Field37,
                Field38,
                Field39,
                Field40,
                Field41,
                Field42,
                Field43,
                Field44,
                Field45,
                Field46,
                Field47,
                Field48,
                Field49,
                Field50,
                Field51,
                Field0,
                Field1,
                Field2,
                Field3,
                Field4,
                Field5,
                Field6,
                Field7,
                Field8,
                Field9,
                Field10,
                Field11,
                Field12,
                GreenField0,
                GreenField1,
                GreenField2,
                GreenField3,
                GreenField4,
                GreenField5
            };
            List<Button> redRoute = new List<Button>
            {
                Field40,
                Field41,
                Field42,
                Field43,
                Field44,
                Field45,
                Field46,
                Field47,
                Field48,
                Field49,
                Field50,
                Field51,
                Field0,
                Field1,
                Field2,
                Field3,
                Field4,
                Field5,
                Field6,
                Field7,
                Field8,
                Field9,
                Field10,
                Field11,
                Field12,
                Field13,
                Field14,
                Field15,
                Field16,
                Field17,
                Field18,
                Field19,
                Field20,
                Field21,
                Field22,
                Field23,
                Field24,
                Field25,
                Field26,
                Field27,
                Field28,
                Field29,
                Field30,
                Field31,
                Field32,
                Field33,
                Field34,
                Field35,
                Field36,
                Field37,
                Field38,
                RedField0,
                RedField1,
                RedField2,
                RedField3,
                RedField4,
                RedField5
            };
            List<Button> yellowRoute = new List<Button>
            {
                Field27,
                Field28,
                Field29,
                Field30,
                Field31,
                Field32,
                Field33,
                Field34,
                Field35,
                Field36,
                Field37,
                Field38,
                Field39,
                Field40,
                Field41,
                Field42,
                Field43,
                Field44,
                Field45,
                Field46,
                Field47,
                Field48,
                Field49,
                Field50,
                Field51,
                Field0,
                Field1,
                Field2,
                Field3,
                Field4,
                Field5,
                Field6,
                Field7,
                Field8,
                Field9,
                Field10,
                Field11,
                Field12,
                Field13,
                Field14,
                Field15,
                Field16,
                Field17,
                Field18,
                Field19,
                Field20,
                Field21,
                Field22,
                Field23,
                Field24,
                Field25,
                YellowField0,
                YellowField1,
                YellowField2,
                YellowField3,
                YellowField4,
                YellowField5
            };
            List<Button> blueBase = new List<Button>
            {
                BlueBase0,
                BlueBase1,
                BlueBase2,
                BlueBase3
            };
            List<Button> greenBase = new List<Button>
            {
                GreenBase0,
                GreenBase1,
                GreenBase2,
                GreenBase3
            };
            List<Button> redBase = new List<Button>
            {
                RedBase0,
                RedBase1,
                RedBase2,
                RedBase3
            };
            List<Button> yellowBase = new List<Button>
            {
                YellowBase0,
                YellowBase1,
                YellowBase2,
                YellowBase3
            };
            PrepareColoredRoutes?.Invoke(blueRoute, greenRoute, redRoute, yellowRoute);
            PrepareColoredBases?.Invoke(blueBase, greenBase, redBase, yellowBase);
        }
        
        /// <summary>
        /// Send back UI objects (except the fields that the pieces use) to UIManager.<br/>
        /// Raises event.
        /// </summary>
        private void SendButtonsAndLabelsToUIManager()
        {
            PrepareBtnsAndLabels?.Invoke(btnThrowDie, btnEndTurn, lblInfo, lblPlayerTurn);
        }
    }
}
