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
using System.Collections.Generic;
using System.Data;
using Backend.BusinesLogic;

namespace Frontend.Views
{
    public partial class formLogistic : Window
    {
        private readonly LogisticData logisticData;
        private readonly ProductData productData;
        private readonly UserData userData;
        private DataTable addedProducts;
        private decimal totalAmount = 0;

        // Временная заглушка для CurrentUser (должна быть заменена вашей реализацией)
        private class CurrentUser
        {
            public static int Id => 1; // Заменить реальным значением
            public static string Name => "Admin"; // Заменить реальным значением
        }

        public formLogistic()
        {
            InitializeComponent();
            logisticData = new LogisticData();
            productData = new ProductData();
            userData = new UserData();
            addedProducts = new DataTable();
            InitializeAddedProductsTable();
        }

        private void InitializeAddedProductsTable()
        {
            addedProducts.Columns.Add("ProductID", typeof(int));
            addedProducts.Columns.Add("Name", typeof(string));
            addedProducts.Columns.Add("Rate", typeof(decimal));
            addedProducts.Columns.Add("Quantity", typeof(decimal));
            addedProducts.Columns.Add("Total", typeof(decimal));
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                LoadEmployees();
                LoadLogistics();
                txtDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoadEmployees()
        {
            DataTable users = userData.Select();
            cmbEmployee.ItemsSource = users.DefaultView;
            cmbEmployee.DisplayMemberPath = "username";
            cmbEmployee.SelectedValuePath = "id";
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

        private void cmbEmployee_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (cmbEmployee.SelectedValue != null)
                {
                    User user = userData.SearchUserForLogistic(cmbEmployee.Text);
                    txtFirstName.Text = user.FirstName;
                    txtLastName.Text = user.LastName;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                DataTable dt = logisticData.Search(txtSearch.Text);
                dgvLogistic.ItemsSource = dt.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ValidateLogisticFields() && addedProducts.Rows.Count > 0)
                {
                    Logistic logistic = new Logistic
                    {
                        Empleyee = cmbEmployee.Text,
                        FirstNameEmployee = txtFirstName.Text,
                        LastNameEmployee = txtLastName.Text,
                        Address = txtAddress.Text,
                        Contact = txtContact.Text,
                        Date = txtDate.Text,
                        Description = txtDescription.Text,
                        Price = totalAmount,
                        AddedDate = DateTime.Now,
                        AddedBy = CurrentUser.Id,
                        AddedByName = CurrentUser.Name
                    };

                    if (logisticData.Insert(logistic))
                    {
                        MessageBox.Show("Доставка успешно добавлена", "Успех",
                            MessageBoxButton.OK, MessageBoxImage.Information);
                        ClearFields();
                        LoadLogistics();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool ValidateLogisticFields()
        {
            if (string.IsNullOrEmpty(cmbEmployee.Text))
                throw new ArgumentException("Выберите сотрудника");
            if (string.IsNullOrEmpty(txtContact.Text))
                throw new ArgumentException("Введите контакт");
            if (string.IsNullOrEmpty(txtAddress.Text))
                throw new ArgumentException("Введите адрес");

            return true;
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgvLogistic.SelectedItem != null && ValidateLogisticFields())
                {
                    DataRowView row = (DataRowView)dgvLogistic.SelectedItem;
                    Logistic logistic = new Logistic
                    {
                        Id = Convert.ToInt32(row["id"]),
                        Empleyee = cmbEmployee.Text,
                        FirstNameEmployee = txtFirstName.Text,
                        LastNameEmployee = txtLastName.Text,
                        Address = txtAddress.Text,
                        Contact = txtContact.Text,
                        Date = txtDate.Text,
                        Description = txtDescription.Text,
                        Price = totalAmount
                    };

                    if (logisticData.Update(logistic))
                    {
                        MessageBox.Show("Доставка успешно обновлена", "Успех",
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

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgvLogistic.SelectedItem != null)
                {
                    DataRowView row = (DataRowView)dgvLogistic.SelectedItem;
                    Logistic logistic = new Logistic { Id = Convert.ToInt32(row["id"]) };

                    if (logisticData.Delete(logistic))
                    {
                        MessageBox.Show("Доставка успешно удалена", "Успех",
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

        private void txtSearchProduct_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                Product product = productData.GetProductsForTransaction(txtSearchProduct.Text);

                if (product != null)
                {
                    txtProductName.Text = product.Name;
                    txtInventory.Text = product.Quantity.ToString();
                    txtRate.Text = product.Rate.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnAddProduct_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ValidateProductFields())
                {
                    Product product = productData.GetProductIDFromName(txtProductName.Text);

                    decimal rate = decimal.Parse(txtRate.Text);
                    decimal qty = decimal.Parse(txtQty.Text);
                    decimal total = rate * qty;

                    addedProducts.Rows.Add(
                        productData.GetProductIDFromName(txtProductName.Text).Id,
                        txtProductName.Text,
                        rate,
                        qty,
                        total
                    );

                    totalAmount += total;
                    txtTotal.Text = totalAmount.ToString("N2");
                    dgvAddedProducts.ItemsSource = addedProducts.DefaultView;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool ValidateProductFields()
        {
            if (string.IsNullOrEmpty(txtProductName.Text))
                throw new ArgumentException("Выберите продукт");
            if (!decimal.TryParse(txtQty.Text, out decimal qty) || qty <= 0)
                throw new ArgumentException("Некорректное количество");

            return true;
        }

        private void dgvLogistic_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            try
            {
                if (dgvLogistic.SelectedItem != null)
                {
                    DataRowView row = (DataRowView)dgvLogistic.SelectedItem;
                    LoadLogisticDetails(row);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoadLogisticDetails(DataRowView row)
        {
            txtID.Text = row["id"].ToString();
            cmbEmployee.Text = row["employee"].ToString();
            txtFirstName.Text = row["first_name_employee"].ToString();
            txtLastName.Text = row["last_name_employee"].ToString();
            txtAddress.Text = row["address"].ToString();
            txtContact.Text = row["contact"].ToString();
            txtDate.Text = row["date"].ToString();
            txtDescription.Text = row["description"].ToString();
            txtTotal.Text = row["price"].ToString();
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            ClearFields();
        }

        private void ClearFields()
        {
            txtID.Clear();
            cmbEmployee.SelectedIndex = -1;
            txtFirstName.Clear();
            txtLastName.Clear();
            txtAddress.Clear();
            txtContact.Clear();
            txtDescription.Clear();
            txtTotal.Text = "0";
            addedProducts.Rows.Clear();
            dgvAddedProducts.ItemsSource = null;
            totalAmount = 0;
        }
    }
}