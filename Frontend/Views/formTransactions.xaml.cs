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
        private readonly TransactionData _transactionData = new TransactionData();

        /// <summary>
        /// Инициализирует новый экземпляр окна управления транзакциями.
        /// </summary>
        public formTransactions()
        {
            InitializeComponent();
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
                DataTable transactions = _transactionData.DisplayAllTransactions();
                dgvTransactions.ItemsSource = transactions.DefaultView;
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Ошибка загрузки транзакций: {ex.Message}");
            }
        }

        /// <summary>
        /// Обработчик изменения типа транзакции в комбобоксе.
        /// Фильтрует транзакции по выбранному типу.
        /// </summary>
        private void cmbTransactionType_SelectedIndexChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbTransactionType.SelectedIndex == -1)
                return;

            try
            {
                string selectedType = ((ComboBoxItem)cmbTransactionType.SelectedItem).Content.ToString();
                DataTable filteredTransactions = _transactionData.DisplayTransactionByType(selectedType);
                dgvTransactions.ItemsSource = filteredTransactions.DefaultView;
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Ошибка фильтрации: {ex.Message}");
            }
        }

        /// <summary>
        /// Обработчик кнопки "Все". Сбрасывает фильтры и показывает все транзакции.
        /// </summary>
        private void btnAll_Click(object sender, RoutedEventArgs e)
        {
            cmbTransactionType.SelectedIndex = -1;
            LoadAllTransactions();
        }

        /// <summary>
        /// Обработчик кнопки "Очистить". Удаляет все транзакции из системы.
        /// </summary>
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            if (ConfirmClearTransactions())
            {
                try
                {
                    _transactionData.DeleteAllTransactions();
                    LoadAllTransactions();
                    ShowSuccessMessage("Все транзакции очищены");
                }
                catch (Exception ex)
                {
                    ShowErrorMessage($"Ошибка очистки: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// Запрашивает подтверждение на очистку всех транзакций.
        /// </summary>
        private bool ConfirmClearTransactions()
        {
            return MessageBox.Show("Вы уверены, что хотите удалить все транзакции?",
                "Подтверждение удаления",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning) == MessageBoxResult.Yes;
        }

        /// <summary>
        /// Показывает сообщение об ошибке.
        /// </summary>
        private void ShowErrorMessage(string message)
        {
            MessageBox.Show(message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        /// <summary>
        /// Показывает сообщение об успешном выполнении операции.
        /// </summary>
        private void ShowSuccessMessage(string message)
        {
            MessageBox.Show(message, "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}