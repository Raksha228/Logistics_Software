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
using Backend.Interfaces;
using System;
using System.Data;

namespace Frontend.Views
{
    public partial class formDelivery : Window
    {
        private readonly LogisticData logisticData;
        private readonly PersonalLogisticData personalLogisticData;

        public formDelivery()
        {
            InitializeComponent();
            logisticData = new LogisticData();
            personalLogisticData = new PersonalLogisticData();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadLogistics();
            cmbDate.SelectedIndex = 0;
        }

        private void LoadLogistics()
        {
            try
            {
                DataTable dt = logisticData.Select();
                dgvLogistic.ItemsSource = dt.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void cmbDate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                string type = ((ComboBoxItem)cmbDate.SelectedItem).Content.ToString() == "Закупка" ? "Purchase" : "Sales";
                DataTable dt = personalLogisticData.DisplayLogisticnByDate(type, "");
                dgvLogistic.ItemsSource = dt.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnAll_Click(object sender, RoutedEventArgs e)
        {
            LoadLogistics();
        }

        private void btnDelivered_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgvLogistic.SelectedItem != null)
                {
                    DataRowView row = (DataRowView)dgvLogistic.SelectedItem;
                    Logistic logistic = new Logistic
                    {
                        Id = Convert.ToInt32(row["id"]),
                        Empleyee = row["employee"].ToString(),
                        FirstNameEmployee = row["first_name_employee"].ToString(),
                        LastNameEmployee = row["last_name_employee"].ToString(),
                        Address = row["address"].ToString(),
                        Contact = row["contact"].ToString(),
                        Date = row["date"].ToString(),
                        Description = row["description"].ToString(),
                        Price = Convert.ToDecimal(row["price"])
                    };

                    if (logisticData.Delete(logistic))
                    {
                        MessageBox.Show("Доставка отмечена как выполненная", "Успех",
                            MessageBoxButton.OK, MessageBoxImage.Information);
                        LoadLogistics();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void dgvLogistic_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (dgvLogistic.SelectedItem != null)
            {
                DataRowView row = (DataRowView)dgvLogistic.SelectedItem;
                // Логика обработки двойного клика (например, открытие деталей)
                MessageBox.Show($"Выбрана доставка ID: {row["id"]}", "Информация",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}