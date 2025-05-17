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
using System.Windows.Shapes;

using Backend.BusinessLogic;
using Backend.DataAccess;
using Backend.BusinesLogic;

namespace Frontend.Views
{
    public partial class formAdminDashboard : Window
    {
        private Login _loggedUser;

        public formAdminDashboard(Login user)
        {
            InitializeComponent();
            _loggedUser = user;
            Loaded += FormAdminDashboard_Loaded;
        }

        private void FormAdminDashboard_Loaded(object sender, RoutedEventArgs e)
        {
            // Инициализация данных при загрузке окна
            LoggedUserLabel.Text = $"{_loggedUser.Username}";
            AppFNameLabel.Text = "Магазин";
            AppLNameLabel.Text = "Система";
            DescriptionLabel.Text = "Управление продажами и инвентаризацией";
        }

        #region Обработчики меню

        private void UsersMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var usersWindow = new formUsers();
            usersWindow.Show();
        }

        private void CategoryMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var categoriesWindow = new formCategories();
            categoriesWindow.Show();
        }

        private void ProductsMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var productsWindow = new formProducts(1,"admin");
            productsWindow.Show();
        }

        private void DealerAndCustomerMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var dealerCustomerWindow = new formDealerCustomer();
            dealerCustomerWindow.Show();
        }

        private void InventoryMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var inventoryWindow = new formInventory();
            inventoryWindow.Show();
        }

        private void TransactionsMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var transactionsWindow = new formTransactions();
            transactionsWindow.Show();
        }

        private void LogisticMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var logisticWindow = new formLogistic();
            logisticWindow.Show();
        }

        private void DeliveriesMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var deliveriesWindow = new formDelivery();
            deliveriesWindow.Show();
        }

        private void LogOutMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var loginWindow = new formLogin();
            loginWindow.Show();
            this.Close();
        }

        #endregion

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Подтверждение выхода
            if (MessageBox.Show("Вы уверены, что хотите выйти?", "Подтверждение",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
            {
                e.Cancel = true;
            }
            else
            {
                Application.Current.Shutdown();
            }
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            // Возможность перетаскивания окна
            this.DragMove();
        }
    }
}