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
    public partial class formDealerCustomer : Window
    {
        private DealerCustomerData _dealerCustomerData;
        private DataTable _dealerCustomerTable;

        public formDealerCustomer()
        {
            InitializeComponent();
            _dealerCustomerData = new DealerCustomerData();
            LoadDealerCustomers();
        }

        private void LoadDealerCustomers()
        {
            try
            {
                _dealerCustomerTable = _dealerCustomerData.Select();
                dgvDeaCust.ItemsSource = _dealerCustomerTable.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}", "Ошибка",
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private DealerCustomer GetDealerCustomerFromForm()
        {
            return new DealerCustomer()
            {
                Id = string.IsNullOrEmpty(txtDeaCustID.Text) ? 0 : int.Parse(txtDeaCustID.Text),
                Type = (cmbDeaCust.SelectedItem as ComboBoxItem)?.Content.ToString(),
                Name = txtName.Text.Trim(),
                Email = txtEmail.Text.Trim(),
                Contact = txtContact.Text.Trim(),
                Address = txtAddress.Text.Trim()
            };
        }

        private void ClearForm()
        {
            txtDeaCustID.Clear();
            cmbDeaCust.SelectedIndex = -1;
            txtName.Clear();
            txtEmail.Clear();
            txtContact.Clear();
            txtAddress.Clear();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var dealerCustomer = GetDealerCustomerFromForm();

                if (string.IsNullOrEmpty(dealerCustomer.Type))
                {
                    MessageBox.Show("Выберите тип", "Ошибка",
                                  MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (string.IsNullOrEmpty(dealerCustomer.Name))
                {
                    MessageBox.Show("Введите название", "Ошибка",
                                  MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                bool success = _dealerCustomerData.Insert(dealerCustomer);
                if (success)
                {
                    LoadDealerCustomers();
                    ClearForm();
                    MessageBox.Show("Запись успешно добавлена", "Успех",
                                  MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка добавления: {ex.Message}", "Ошибка",
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtDeaCustID.Text))
                {
                    MessageBox.Show("Выберите запись для редактирования", "Ошибка",
                                  MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var dealerCustomer = GetDealerCustomerFromForm();
                bool success = _dealerCustomerData.Update(dealerCustomer);
                if (success)
                {
                    LoadDealerCustomers();
                    MessageBox.Show("Запись успешно обновлена", "Успех",
                                  MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка обновления: {ex.Message}", "Ошибка",
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtDeaCustID.Text))
                {
                    MessageBox.Show("Выберите запись для удаления", "Ошибка",
                                  MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var dealerCustomer = GetDealerCustomerFromForm();
                bool success = _dealerCustomerData.Delete(dealerCustomer);
                if (success)
                {
                    LoadDealerCustomers();
                    ClearForm();
                    MessageBox.Show("Запись успешно удалена", "Успех",
                                  MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка удаления: {ex.Message}", "Ошибка",
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            ClearForm();
        }

        private void dgvDeaCust_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (dgvDeaCust.SelectedItem != null)
                {
                    DataRowView row = (DataRowView)dgvDeaCust.SelectedItem;
                    txtDeaCustID.Text = row["id"].ToString();
                    cmbDeaCust.Text = row["type"].ToString();
                    txtName.Text = row["name"].ToString();
                    txtEmail.Text = row["email"].ToString();
                    txtContact.Text = row["contact"].ToString();
                    txtAddress.Text = row["address"].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка выбора записи: {ex.Message}", "Ошибка",
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                DataView dv = _dealerCustomerTable.DefaultView;
                dv.RowFilter = $"id LIKE '%{txtSearch.Text}%' OR type LIKE '%{txtSearch.Text}%' OR name LIKE '%{txtSearch.Text}%'";
                dgvDeaCust.ItemsSource = dv;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка поиска: {ex.Message}", "Ошибка",
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadDealerCustomers();
        }
    }
}