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
    /// <summary>
    /// Окно административной панели управления системой.
    /// Предоставляет доступ ко всем основным функциям управления через меню.
    /// </summary>
    public partial class formAdminDashboard : Window
    {
        private Login _loggedUser;

        /// <summary>
        /// Инициализирует новый экземпляр административной панели.
        /// </summary>
        /// <param name="loggedInUser">Объект с данными авторизованного пользователя.</param>
        public formAdminDashboard(Login loggedInUser)
        {
            InitializeComponent();
            _loggedUser = loggedInUser;
            Loaded += FormAdminDashboard_Loaded;
        }

        /// <summary>
        /// Обработчик загрузки окна. Инициализирует основные элементы интерфейса.
        /// </summary>
        private void FormAdminDashboard_Loaded(object sender, RoutedEventArgs e)
        {
            // Инициализация данных при загрузке окна
            LoggedUserLabel.Text = $"{_loggedUser.Username}";
            AppFNameLabel.Text = "Магазин";
            AppLNameLabel.Text = "Система";
            DescriptionLabel.Text = "Управление продажами и инвентаризацией";
        }

        #region Обработчики меню

        /// <summary>
        /// Открывает окно управления пользователями.
        /// </summary>
        private void UsersMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Login _UloggedUser = _loggedUser;
            var usersWindow = new formUsers(_UloggedUser.Username);
            usersWindow.Show();
        }

        /// <summary>
        /// Открывает окно управления категориями товаров.
        /// </summary>
        private void CategoryMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Login _UloggedUser = _loggedUser;
            var categoriesWindow = new formCategories(_UloggedUser.Username);
            categoriesWindow.Show();
        }

        /// <summary>
        /// Открывает окно управления товарами.
        /// </summary>
        private void ProductsMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Login _UloggedUser = _loggedUser;
            var productsWindow = new formProducts(_UloggedUser.Username);
            productsWindow.Show();
        }

        /// <summary>
        /// Открывает окно управления дилерами и клиентами.
        /// </summary>
        private void DealerAndCustomerMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Login _UloggedUser = _loggedUser;
            var dealerCustomerWindow = new formDealerCustomer(_UloggedUser.Username);
            dealerCustomerWindow.Show();
        }

        /// <summary>
        /// Открывает окно управления инвентаризацией.
        /// </summary>
        private void InventoryMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var inventoryWindow = new formInventory();
            inventoryWindow.Show();
        }

        /// <summary>
        /// Открывает окно управления транзакциями.
        /// </summary>
        private void TransactionsMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Login _UloggedUser = _loggedUser;
            var transactionsWindow = new formTransactions();
            transactionsWindow.Show();
        }

        /// <summary>
        /// Открывает окно управления логистикой.
        /// </summary>
        private void LogisticMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Login _UloggedUser = _loggedUser;
            var logisticWindow = new formLogistic(_UloggedUser.Username);
            logisticWindow.Show();
        }

        /// <summary>
        /// Открывает окно управления доставками.
        /// </summary>
        private void DeliveriesMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var deliveriesWindow = new formDelivery();
            deliveriesWindow.Show();
        }

        /// <summary>
        /// Открывает окно архива доставок.
        /// </summary>
        private void ArchiveDeliveriesMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var achivedeliveriesWindow = new formArchiveDeliveries();
            achivedeliveriesWindow.Show();
        }

        /// <summary>
        /// Выполняет выход из системы с возвратом на окно авторизации.
        /// </summary>
        private void LogOutMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var loginWindow = new formLogin();
            loginWindow.Show();
            this.Close();
        }

        #endregion

        /// <summary>
        /// Обработчик закрытия окна. Запрашивает подтверждение выхода.
        /// </summary>
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

        /// <summary>
        /// Обеспечивает возможность перетаскивания окна за любую область.
        /// </summary>
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            // Возможность перетаскивания окна
            this.DragMove();
        }
    }
}