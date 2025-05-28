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
using System;
using System.Data;
using Backend.BusinessLogic;
using Backend.DataAccess;
using Backend.Interfaces;
using Microsoft.Win32;
using System;
using Backend.BusinesLogic;

namespace Frontend.Views
{
    public partial class formProducts : Window
    {
        private readonly ICrudProduct _productService = new ProductData();
        private readonly ICrudCategory _categoryService = new CategoryData();
        private readonly int _currentUserId;
        private readonly string _currentUsername;

        private UserData _userData = new UserData();
        private string _loggedInUser;
        public formProducts(string loggedInUser)
        {
            InitializeComponent();
            _loggedInUser = loggedInUser;
            LoadCategories();
            LoadProducts();
        }

        private void LoadCategories()
        {
            try
            {
                DataTable categories = _categoryService.Select();
                cmbCategory.ItemsSource = categories.DefaultView;
                cmbCategory.DisplayMemberPath = "title";
                cmbCategory.SelectedValuePath = "id";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки категорий: {ex.Message}",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoadProducts()
        {
            try
            {
                DataTable products = _productService.Select();
                dgvProducts.ItemsSource = products.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки товаров: {ex.Message}",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ClearFields()
        {
            txtID.Clear();
            txtName.Clear();
            cmbCategory.SelectedIndex = -1;
            txtSpecialNumber.Clear();
            txtDescription.Clear();
            txtRate.Clear();
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(txtName.Text) ||
                cmbCategory.SelectedValue == null ||
                string.IsNullOrWhiteSpace(txtRate.Text))
            {
                MessageBox.Show("Заполните обязательные поля: Название, Категория, Цена",
                    "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (!decimal.TryParse(txtRate.Text, out _))
            {
                MessageBox.Show("Некорректный формат цены",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateInputs()) return;

            try
            {
                var product = new Product
                {
                    Name = txtName.Text,
                    Category = cmbCategory.SelectedValue.ToString(),
                    SpecialNumber = txtSpecialNumber.Text,
                    Description = txtDescription.Text,
                    Rate = decimal.Parse(txtRate.Text),
                    AddedDate = DateTime.Now,
                    AddedBy = _currentUserId,
                    AddedByName = _currentUsername
                };

                User currentUser = _userData.GetIDFromUsername(_loggedInUser);
                product.AddedBy = currentUser.Id;
                product.AddedByName = _loggedInUser;



                if (_productService.Insert(product))
                {
                    MessageBox.Show("Товар успешно добавлен",
                        "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    LoadProducts();
                    ClearFields();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка добавления: {ex.Message}",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateInputs() || string.IsNullOrEmpty(txtID.Text)) return;

            try
            {
                var product = new Product
                {
                    Id = int.Parse(txtID.Text),
                    Name = txtName.Text,
                    Category = cmbCategory.SelectedValue.ToString(),
                    SpecialNumber = txtSpecialNumber.Text,
                    Description = txtDescription.Text,
                    Rate = decimal.Parse(txtRate.Text)
                };

                if (_productService.Update(product))
                {
                    MessageBox.Show("Товар успешно обновлён",
                        "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    LoadProducts();
                    ClearFields();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка обновления: {ex.Message}",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtID.Text)) return;

            try
            {
                var product = new Product { Id = int.Parse(txtID.Text) };

                if (_productService.Delete(product))
                {
                    MessageBox.Show("Товар успешно удалён",
                        "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    LoadProducts();
                    ClearFields();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка удаления: {ex.Message}",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            ClearFields();
        }

        private void dgvProducts_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            try
            {
                if (dgvProducts.SelectedItem == null) return;

                DataRowView row = (DataRowView)dgvProducts.SelectedItem;
                txtID.Text = row["id"].ToString();
                txtName.Text = row["name"].ToString();
                cmbCategory.SelectedValue = row["category"];
                txtSpecialNumber.Text = row["special_number"].ToString();
                txtDescription.Text = row["description"].ToString();
                txtRate.Text = row["rate"].ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                DataTable result = _productService.Search(txtSearch.Text);
                dgvProducts.ItemsSource = result.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка поиска: {ex.Message}",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadProducts();
            LoadCategories();
        }
    }
}