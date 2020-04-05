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
        public delegate void InGameHandler(object source, object lbl, EventArgs e);
        public delegate void StartGameHandler(object source, object lblPlayerTurn, object lblInfo, EventArgs e);
        public event InGameHandler InGameEvent;
        public event StartGameHandler StartGameEvent;

        public MainWindow()
        {
            InitializeComponent();
            GameManager gm = new GameManager();
            gm.ludoBoard.InitWholeBoardAndCreateRoutes();
            this.StartGameEvent += gm.RunGame;
            this.InGameEvent += gm.ChangeButtonContent;
            
        }

        private void DieThrow_Click(object sender, RoutedEventArgs e)
        {
            InGameEvent?.Invoke(sender, lblInfo, e);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void StartGame(object sender, RoutedEventArgs e)
        {
            StartGameEvent?.Invoke(sender, lblPlayerTurn, lblInfo, e);
        }

        // Former event style
        //protected virtual void OnDieThrow_Click(EventArgs e)
        //{
        //    if (ThrowDieEvent != null)
        //    {
        //        ThrowDieEvent(this, e, e);
        //    }
        //}

    }
}
