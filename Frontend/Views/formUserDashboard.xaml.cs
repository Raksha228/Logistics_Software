using System;
using System.Windows;
using System.Windows.Controls;
using Backend.BusinessLogic;
using Backend.DataAccess;

namespace Frontend.Views
{
    /// <summary>
    /// Главное окно пользовательской панели управления.
    /// Предоставляет доступ к основным функциям системы для обычных пользователей.
    /// </summary>
    public partial class formUserDashboard : Window
    {
        private Login _loggedUser;

        /// <summary>
        /// Инициализирует новый экземпляр пользовательской панели управления.
        /// </summary>
        /// <param name="loggedInUser">Данные авторизованного пользователя</param>
        public formUserDashboard(Login loggedInUser)
        {
            InitializeComponent();
            _loggedUser = loggedInUser;
        }

        /// <summary>
        /// Статическое поле для хранения типа текущей транзакции (Purchase/Sales)
        /// </summary>
        public static string transactionType;

        /// <summary>
        /// Инициализирует элементы пользовательского интерфейса.
        /// </summary>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtLoggedUser.Text = $"{_loggedUser.Username}";

        }


        /// <summary>
        /// Обработчик закрытия окна. Возвращает на окно авторизации.
        /// </summary>
        private void Window_Closed(object sender, EventArgs e)
        {
            formLogin login = new formLogin();
            login.Show();
            Application.Current.Shutdown();
        }

        /// <summary>
        /// Возвращает на окно авторизации и закрывает приложение.
        /// </summary>
        private void DealerAndCustomerMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Login _UloggedUser = _loggedUser;
            formDealerCustomer dealerAndCustomer = new formDealerCustomer(_UloggedUser.Username);
            dealerAndCustomer.Show();
        }

        /// <summary>
        /// Обработчик пункта меню "Дилеры/Клиенты".
        /// Открывает окно управления дилерами и клиентами.
        /// </summary>
        private void PurchaseMenuItem_Click(object sender, RoutedEventArgs e)
        {
            transactionType = "Purchase";
            formPurchaseSales purchase = new formPurchaseSales();
            purchase.Show();
        }

        /// <summary>
        /// Обработчик пункта меню "Закупки".
        /// Устанавливает тип транзакции и открывает окно покупок/продаж.
        /// </summary>
        private void SalesMenuItem_Click(object sender, RoutedEventArgs e)
        {
            transactionType = "Sales";
            formPurchaseSales sales = new formPurchaseSales();
            sales.Show();
        }

        /// <summary>
        /// Обработчик пункта меню "Продажи".
        /// Устанавливает тип транзакции и открывает окно покупок/продаж.
        /// </summary>
        private void InventoryMenuItem_Click(object sender, RoutedEventArgs e)
        {
            formInventory inventory = new formInventory();
            inventory.Show();
        }

        /// <summary>
        /// Обработчик пункта меню "Инвентаризация".
        /// Открывает окно управления инвентаризацией.
        /// </summary>
        private void DeliveryMenuItem_Click(object sender, RoutedEventArgs e)
        {
            formDelivery logistic = new formDelivery();
            logistic.Show();
        }

        /// <summary>
        /// Обработчик пункта меню "Доставки".
        /// Открывает окно управления доставками.
        /// </summary>
        private void ArchiveDeliveriesMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var achivedeliveriesWindow = new formArchiveDeliveries();
            achivedeliveriesWindow.Show();
        }

        /// <summary>
        /// Обработчик пункта меню "Архив доставок".
        /// Открывает окно просмотра архивных доставок.
        /// </summary>
        private void LogOutMenuItem_Click(object sender, RoutedEventArgs e)
        {
            formLogin login = new formLogin();
            login.Show();
            this.Close();
        }

        /// <summary>
        /// Обработчик пункта меню "Выход".
        /// Завершает текущую сессию и возвращает на окно авторизации.
        /// </summary>


        /// <summary>
        /// Открывает указанное окно.
        /// </summary>
        /// <param name="window">Окно для отображения</param>

    }
}