using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
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
using System.Xml.Linq;
using Engine.ViewModels;
using static WPFUI.MainWindow;

namespace WPFUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private GameSession _gameSession;

        public MainWindow()
        {
            InitializeComponent();
            _gameSession = new GameSession();
            // Data context is a built in property for the xaml window
            DataContext = _gameSession;
            //TestBorder();
            //Startup(); 
        }

 


        private void XPButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            _gameSession.CurrentPlayer.XPtillNextLvl = _gameSession.CurrentPlayer.XPtillNextLvl - 10;
        }

        private void OnClick_North(object sender, RoutedEventArgs e)
        {
            _gameSession.MoveNorth();
        }
        private void OnClick_West(object sender, RoutedEventArgs e)
        {
            _gameSession.MoveWest();
        }
        private void OnClick_East(object sender, RoutedEventArgs e)
        {
            _gameSession.MoveEast();
        }
        private void OnClick_South(object sender, RoutedEventArgs e)
        {
            _gameSession.MoveSouth();
        }
        private void OnClick_Up(object sender, RoutedEventArgs e)
        {
            _gameSession.MoveUp();
        }
        private void OnClick_Down(object sender, RoutedEventArgs e)
        {
            _gameSession.MoveDown();
        }

        private void FontSizeMethod(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            if (btn.Name == "FontHigher")
            {
                if (_gameSession.FontSizeLabel < 40)
                    _gameSession.FontSizeLabel += 2;
                //_gameSession.LeftSideGridSize += 2;
            }
            else
            {
                if (_gameSession.FontSizeLabel > 8)
                _gameSession.FontSizeLabel -= 1;
                //_gameSession.LeftSideGridSize -= 2;
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.W: _gameSession.MoveNorth();
                    break;
                case Key.S: _gameSession.MoveSouth();
                    break;
                case Key.A: _gameSession.MoveWest();
                    break;
                case Key.D: _gameSession.MoveEast();
                    break;
                case Key.Q: _gameSession.MoveUp();
                    break;
                case Key.Z: _gameSession.MoveDown();
                    break;

            }
        }

        //public void TestBorder()
        //{
        //    int _childrencount = 0;
        //    Border border= new Border();
        //    border.BorderThickness = new Thickness(2);
        //    border.BorderBrush = new SolidColorBrush(Color.FromArgb(0xFF,0x30,0x11,0xD1));
        //    border.CornerRadius = new CornerRadius(10);
            
        //    DirectionGrid.ShowGridLines= true;

        //    foreach (Button x in DirectionGrid.Children.OfType<Button>()) {
        //        x.BorderBrush = new SolidColorBrush(Color.FromArgb(0xFF, 0xB0, 0x31, 0xB7));
        //        x.BorderThickness = new Thickness(2);
        //    }
        //}

        //private void Startup()
        //{
        //    TextBlock _LocationName = LocationName;
        //    _LocationName.Text = _gameSession.CurrentLocation.Name;
        //}

    }

}
