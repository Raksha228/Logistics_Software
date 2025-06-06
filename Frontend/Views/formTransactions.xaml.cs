using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using Backend.BusinessLogic;
using Backend.DataAccess;

namespace Frontend.Views
{
    /// <summary>
    /// Окно для просмотра и управления транзакциями системы.
    /// Позволяет просматривать, фильтровать и очищать историю транзакций.
    /// </summary>
    public partial class formTransactions : Window
    {
        private TransactionData transactionData;

        /// <summary>
        /// Инициализирует новый экземпляр окна управления транзакциями.
        /// </summary>
        public formTransactions()
        {
            InitializeComponent();
            transactionData = new TransactionData();
            Loaded += FormTransactions_Loaded;
        }

        /// <summary>
        /// Обработчик загрузки окна. Загружает список всех транзакций.
        /// </summary>
        private void FormTransactions_Loaded(object sender, RoutedEventArgs e)
        {
            LoadAllTransactions();
        }

        /// <summary>
        /// Загружает все транзакции из базы данных.
        /// </summary>
        private void LoadAllTransactions()
        {
            try
            {
                DataTable dt = transactionData.DisplayAllTransactions();
                dgvTransactions.ItemsSource = dt.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки транзакций: {ex.Message}", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Обработчик изменения типа транзакции в комбобоксе.
        /// Фильтрует транзакции по выбранному типу.
        /// </summary>
        private void cmbTransactionType_SelectedIndexChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbTransactionType.SelectedIndex == -1 || transactionData == null)
                return;

            try
            {
                string type = ((ComboBoxItem)cmbTransactionType.SelectedItem).Content.ToString();
                DataTable dt = transactionData.DisplayTransactionByType(type);
                dgvTransactions.ItemsSource = dt.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка фильтрации: {ex.Message}", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Обработчик кнопки "Все". Сбрасывает фильтры и показывает все транзакции.
        /// </summary>
        private void btnAll_Click(object sender, RoutedEventArgs e)
        {
            LoadAllTransactions();
        }

        /// <summary>
        /// Обработчик кнопки "Очистить". Удаляет все транзакции из системы.
        /// </summary>
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                transactionData.DeleteAllTransactions();
                LoadAllTransactions();
                MessageBox.Show("Все транзакции очищены", "Успех",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка очистки: {ex.Message}", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Запрашивает подтверждение на очистку всех транзакций.
        /// </summary>


        /// <summary>
        /// Показывает сообщение об ошибке.
        /// </summary>


        /// <summary>
        /// Показывает сообщение об успешном выполнении операции.
        /// </summary>

    }
}