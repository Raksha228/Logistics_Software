using System;
using System.Windows;
using System.Windows.Controls;
using Backend.BusinessLogic;
using Backend.DataAccess;


namespace Frontend.Views
{
    public partial class formUserDashboard : Window
    {
        private Login _loggedUser;
        public formUserDashboard(Login loggedInUser)
        {
            InitializeComponent();
            _loggedUser = loggedInUser;
        }

        public static string transactionType;

        private void Window_Closed(object sender, EventArgs e)
        {
            formLogin login = new formLogin();
            login.Show();
            Application.Current.Shutdown();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtLoggedUser.Text = formLogin.loggedIn;
        }

        private void DealerAndCustomerMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Login _UloggedUser = _loggedUser;
            formDealerCustomer dealerAndCustomer = new formDealerCustomer(_UloggedUser.Username);
            dealerAndCustomer.Show();
        }

        private void PurchaseMenuItem_Click(object sender, RoutedEventArgs e)
        {
            transactionType = "Purchase";
            formPurchaseSales purchase = new formPurchaseSales();
            purchase.Show();
        }

        private void SalesMenuItem_Click(object sender, RoutedEventArgs e)
        {
            transactionType = "Sales";
            formPurchaseSales sales = new formPurchaseSales();
            sales.Show();
        }

        private void InventoryMenuItem_Click(object sender, RoutedEventArgs e)
        {
            formInventory inventory = new formInventory();
            inventory.Show();
        }

        private void DeliveryMenuItem_Click(object sender, RoutedEventArgs e)
        {
            formDelivery logistic = new formDelivery();
            logistic.Show();
        }

        private void ArchiveDeliveriesMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var achivedeliveriesWindow = new formArchiveDeliveries();
            achivedeliveriesWindow.Show();
        }
        private void LogOutMenuItem_Click(object sender, RoutedEventArgs e)
        {
            formLogin login = new formLogin();
            login.Show();
            this.Close();
        }
    }
}