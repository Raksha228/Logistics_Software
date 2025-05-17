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
using Backend.DataAccess;
using System;
using System.Data;

namespace Frontend.Views
{
    public partial class formArchiveDeliveries : Window
    {
        private readonly ArchiveLogisticData _archiveLogisticData;

        public formArchiveDeliveries()
        {
            InitializeComponent();
            _archiveLogisticData = new ArchiveLogisticData();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                // Загрузка всех доставок при открытии окна
                DataTable dt = _archiveLogisticData.DisplayAllLogistics();
                dgvDeliveries.ItemsSource = dt.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}", "Ошибка",
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void cmbDate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (cmbDate.SelectedItem is ComboBoxItem selectedItem)
                {
                    string dateType = selectedItem.Content.ToString();
                    DataTable dt = _archiveLogisticData.DisplayLogisticnByDate(dateType);
                    dgvDeliveries.ItemsSource = dt.DefaultView;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка фильтрации данных: {ex.Message}", "Ошибка",
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnAll_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Сброс фильтров и загрузка всех данных
                cmbDate.SelectedIndex = -1;
                DataTable dt = _archiveLogisticData.DisplayAllLogistics();
                dgvDeliveries.ItemsSource = dt.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}", "Ошибка",
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}