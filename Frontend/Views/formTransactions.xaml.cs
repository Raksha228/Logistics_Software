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
using System;
using System.Data;

namespace Frontend.Views
{
    public partial class formTransactions : Window
    {
        private TransactionData transactionData;

        public formTransactions()
        {
            InitializeComponent();
            transactionData = new TransactionData();
            Loaded += FormTransactions_Loaded;
        }

        private void FormTransactions_Loaded(object sender, RoutedEventArgs e)
        {
            LoadAllTransactions();
        }

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

        private void btnAll_Click(object sender, RoutedEventArgs e)
        {
            LoadAllTransactions();
        }

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
    }
}