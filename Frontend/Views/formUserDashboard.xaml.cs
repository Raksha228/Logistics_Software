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

        private void LogOutMenuItem_Click(object sender, RoutedEventArgs e)
        {
            formLogin login = new formLogin();
            login.Show();
            this.Close();
        }
    }
}








////using System;
////using System.Collections.Generic;
////using System.Linq;
////using System.Text;
////using System.Threading.Tasks;
////using System.Windows;
////using System.Windows.Controls;
////using System.Windows.Data;
////using System.Windows.Documents;
////using System.Windows.Input;
////using System.Windows.Media;
////using System.Windows.Media.Imaging;
////using System.Windows.Shapes;
////using Backend.BusinessLogic;
////using Backend.DataAccess;
////using Backend.BusinesLogic;

////namespace Frontend.Views
////{
////    public partial class formUserDashboard : Window
////    {
////        private User currentUser;
////        private UserData userData = new UserData();

////        private Login _loggedUser;
////        public formUserDashboard(Login loggedInUser)
////        {
////            InitializeComponent();
////            _loggedUser = loggedInUser;
////            Loaded += Window_Loaded;
////            Closed += Window_Closed;
////        }

////        private void Window_Loaded(object sender, RoutedEventArgs e)
////        {
////            // Загрузка данных пользователя (пример)
////            //currentUser = userData.GetUserById(1); // Здесь должна быть реальная логика авторизации
////            UpdateUserInterface();
////        }

////        private void UpdateUserInterface()
////        {
////            txtLoggedUser.Text = $"{currentUser.FirstName} {currentUser.LastName}";
////        }

////        private void Window_Closed(object sender, System.EventArgs e)
////        {
////            Application.Current.Shutdown();
////        }

////        // Обработчики меню
////        private void PurchaseMenuItem_Click(object sender, RoutedEventArgs e)
////        {
////            var form = new formPurchaseSales();
////            form.Show();
////            this.Hide();
////        }

////        private void SalesMenuItem_Click(object sender, RoutedEventArgs e)
////        {
////            var form = new formPurchaseSales();
////            form.Show();
////            this.Hide();
////        }

////        private void InventoryMenuItem_Click(object sender, RoutedEventArgs e)
////        {
////            var form = new formInventory();
////            form.Show();
////            this.Hide();
////        }

////        private void DealerAndCustomerMenuItem_Click(object sender, RoutedEventArgs e)
////        {
////            Login _UloggedUser = _loggedUser;
////            var form = new formDealerCustomer(_UloggedUser.Username);
////            form.Show();
////            this.Hide();
////        }

////        private void DeliveryMenuItem_Click(object sender, RoutedEventArgs e)
////        {
////            var form = new formDelivery();
////            form.Show();
////            this.Hide();
////        }

////        private void LogOutMenuItem_Click(object sender, RoutedEventArgs e)
////        {
////            var loginForm = new formLogin();
////            loginForm.Show();
////            this.Close();
////        }
////    }
////}