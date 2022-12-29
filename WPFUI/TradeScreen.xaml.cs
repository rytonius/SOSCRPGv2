using Engine.Models;
using Engine.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WPFUI
{
    /// <summary>
    /// Interaction logic for TradeScreen.xaml
    /// </summary>
    public partial class TradeScreen : Window
    {
        public GameSession Session => DataContext as GameSession;
        double _gold;
        public TradeScreen()
        {
            InitializeComponent();
        }

        private void OnClick_Sell(object sender, RoutedEventArgs e)
        {
            GameItem item = ((FrameworkElement)sender).DataContext as GameItem;

            if (item != null)
            {
                Session.CurrencyChange(item.Price);

                Session.CurrentTrader.AddItemToInventory(item);
                Session.CurrentPlayer.RemoveItemFromInventory(item);
            }
        }

        private void OnClick_Buy(object sender, RoutedEventArgs e)
        {
            GameItem item = ((FrameworkElement)sender).DataContext as GameItem;

            if (Session.CurrentPlayer.gold >= item.Price)
            {
                _gold = Session.CurrentPlayer.gold;
                _gold -= item.Price;
                Session.CurrentPlayer.GoldString = Convert.ToString(_gold);

                Session.CurrentTrader.RemoveItemFromInventory(item);
                Session.CurrentPlayer.AddItemToInventory(item);
            }
            else
            {
                MessageBox.Show("You do not have enough gold");
            }
        }

        private void OnClick_Close(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
