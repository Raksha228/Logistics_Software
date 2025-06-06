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
        private readonly Login _loggedUser;

        /// <summary>
        /// Статическое поле для хранения типа текущей транзакции (Purchase/Sales)
        /// </summary>
        public static string TransactionType { get; private set; }

        /// <summary>
        /// Инициализирует новый экземпляр пользовательской панели управления.
        /// </summary>
        /// <param name="loggedInUser">Данные авторизованного пользователя</param>
        public formUserDashboard(Login loggedInUser)
        {
            InitializeComponent();
            _loggedUser = loggedInUser;
            InitializeUserInterface();
        }

        /// <summary>
        /// Инициализирует элементы пользовательского интерфейса.
        /// </summary>
        private void InitializeUserInterface()
        {
            txtLoggedUser.Text = _loggedUser.Username;
        }

        /// <summary>
        /// Обработчик закрытия окна. Возвращает на окно авторизации.
        /// </summary>
        private void Window_Closed(object sender, EventArgs e)
        {
            ReturnToLogin();
        }

        /// <summary>
        /// Возвращает на окно авторизации и закрывает приложение.
        /// </summary>
        private void ReturnToLogin()
        {
            new formLogin().Show();
            Application.Current.Shutdown();
        }

        /// <summary>
        /// Обработчик пункта меню "Дилеры/Клиенты".
        /// Открывает окно управления дилерами и клиентами.
        /// </summary>
        private void DealerAndCustomerMenuItem_Click(object sender, RoutedEventArgs e)
        {
            OpenWindow(new formDealerCustomer(_loggedUser.Username));
        }

        /// <summary>
        /// Обработчик пункта меню "Закупки".
        /// Устанавливает тип транзакции и открывает окно покупок/продаж.
        /// </summary>
        private void PurchaseMenuItem_Click(object sender, RoutedEventArgs e)
        {
            TransactionType = "Purchase";
            OpenWindow(new formPurchaseSales());
        }

        /// <summary>
        /// Обработчик пункта меню "Продажи".
        /// Устанавливает тип транзакции и открывает окно покупок/продаж.
        /// </summary>
        private void SalesMenuItem_Click(object sender, RoutedEventArgs e)
        {
            TransactionType = "Sales";
            OpenWindow(new formPurchaseSales());
        }

        /// <summary>
        /// Обработчик пункта меню "Инвентаризация".
        /// Открывает окно управления инвентаризацией.
        /// </summary>
        private void InventoryMenuItem_Click(object sender, RoutedEventArgs e)
        {
            OpenWindow(new formInventory());
        }

        /// <summary>
        /// Обработчик пункта меню "Доставки".
        /// Открывает окно управления доставками.
        /// </summary>
        private void DeliveryMenuItem_Click(object sender, RoutedEventArgs e)
        {
            OpenWindow(new formDelivery());
        }

        /// <summary>
        /// Обработчик пункта меню "Архив доставок".
        /// Открывает окно просмотра архивных доставок.
        /// </summary>
        private void ArchiveDeliveriesMenuItem_Click(object sender, RoutedEventArgs e)
        {
            OpenWindow(new formArchiveDeliveries());
        }

        /// <summary>
        /// Обработчик пункта меню "Выход".
        /// Завершает текущую сессию и возвращает на окно авторизации.
        /// </summary>
        private void LogOutMenuItem_Click(object sender, RoutedEventArgs e)
        {
            new formLogin().Show();
            this.Close();
        }

        /// <summary>
        /// Открывает указанное окно.
        /// </summary>
        /// <param name="window">Окно для отображения</param>
        private void OpenWindow(Window window)
        {
            window.Show();
        }
    }
}