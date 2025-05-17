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
    public partial class formCategories : Window
    {
        private CategoryData _categoryData;
        private DataTable _categoriesTable;

        public formCategories()
        {
            InitializeComponent();
            _categoryData = new CategoryData();
            LoadCategories();
        }

        private void LoadCategories()
        {
            try
            {
                _categoriesTable = _categoryData.Select();
                dgvCategories.ItemsSource = _categoriesTable.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки категорий: {ex.Message}", "Ошибка",
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private Category GetCategoryFromForm()
        {
            return new Category()
            {
                Id = string.IsNullOrEmpty(txtCategoryID.Text) ? 0 : int.Parse(txtCategoryID.Text),
                Title = txtTitle.Text.Trim(),
                Description = txtDescription.Text.Trim()
            };
        }

        private void ClearForm()
        {
            txtCategoryID.Clear();
            txtTitle.Clear();
            txtDescription.Clear();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var category = GetCategoryFromForm();
                if (string.IsNullOrEmpty(category.Title))
                {
                    MessageBox.Show("Введите название категории", "Ошибка",
                                  MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                bool success = _categoryData.Insert(category);
                if (success)
                {
                    LoadCategories();
                    ClearForm();
                    MessageBox.Show("Категория успешно добавлена", "Успех",
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
                if (string.IsNullOrEmpty(txtCategoryID.Text))
                {
                    MessageBox.Show("Выберите категорию для редактирования", "Ошибка",
                                  MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var category = GetCategoryFromForm();
                bool success = _categoryData.Update(category);
                if (success)
                {
                    LoadCategories();
                    MessageBox.Show("Категория успешно обновлена", "Успех",
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
                if (string.IsNullOrEmpty(txtCategoryID.Text))
                {
                    MessageBox.Show("Выберите категорию для удаления", "Ошибка",
                                  MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var category = GetCategoryFromForm();
                bool success = _categoryData.Delete(category);
                if (success)
                {
                    LoadCategories();
                    ClearForm();
                    MessageBox.Show("Категория успешно удалена", "Успех",
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

        private void dgvCategories_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (dgvCategories.SelectedItem != null)
                {
                    DataRowView row = (DataRowView)dgvCategories.SelectedItem;
                    txtCategoryID.Text = row["id"].ToString();
                    txtTitle.Text = row["title"].ToString();
                    txtDescription.Text = row["description"].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка выбора категории: {ex.Message}", "Ошибка",
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                DataView dv = _categoriesTable.DefaultView;
                dv.RowFilter = $"id LIKE '%{txtSearch.Text}%' OR title LIKE '%{txtSearch.Text}%' OR description LIKE '%{txtSearch.Text}%'";
                dgvCategories.ItemsSource = dv;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка поиска: {ex.Message}", "Ошибка",
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadCategories();
        }
    }
}