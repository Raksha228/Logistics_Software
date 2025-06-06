using System;
using System.Collections.Generic;
using System.Data;
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
using Backend.BusinesLogic;
using Backend.BusinessLogic;
using Backend.DataAccess;

namespace Frontend.Views
{
    /// <summary>
    /// Окно управления категориями товаров.
    /// Позволяет добавлять, редактировать, удалять и просматривать категории.
    /// </summary>
    public partial class formCategories : Window
    {
        private readonly CategoryData _categoryData;
        private DataTable _categoriesTable;
        private readonly UserData _userData = new UserData();
        private readonly string _loggedInUser;

        /// <summary>
        /// Инициализирует новый экземпляр окна управления категориями.
        /// </summary>
        /// <param name="loggedInUser">Логин авторизованного пользователя</param>
        public formCategories(string loggedInUser)
        {
            InitializeComponent();
            _categoryData = new CategoryData();
            _loggedInUser = loggedInUser;
            LoadCategories();
        }

        /// <summary>
        /// Загружает список категорий из базы данных.
        /// </summary>
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

        /// <summary>
        /// Создает объект Category на основе данных формы.
        /// </summary>
        /// <returns>Объект Category с данными из формы</returns>
        private Category GetCategoryFromForm()
        {
            var category = new Category()
            {
                Id = string.IsNullOrEmpty(txtCategoryID.Text) ? 0 : int.Parse(txtCategoryID.Text),
                Title = txtTitle.Text.Trim(),
                Description = txtDescription.Text.Trim(),
                AddedDate = DateTime.Now
            };

            User currentUser = _userData.GetIDFromUsername(_loggedInUser);
            category.AddedBy = currentUser.Id;
            category.AddedByName = _loggedInUser;

            return category;
        }

        /// <summary>
        /// Очищает поля ввода на форме.
        /// </summary>
        private void ClearForm()
        {
            txtCategoryID.Clear();
            txtTitle.Clear();
            txtDescription.Clear();
        }

        /// <summary>
        /// Обработчик нажатия кнопки "Добавить".
        /// Добавляет новую категорию в базу данных.
        /// </summary>
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

        /// <summary>
        /// Обработчик нажатия кнопки "Обновить".
        /// Обновляет выбранную категорию в базе данных.
        /// </summary>
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

        /// <summary>
        /// Обработчик нажатия кнопки "Удалить".
        /// Удаляет выбранную категорию из базы данных.
        /// </summary>
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

        /// <summary>
        /// Обработчик нажатия кнопки "Очистить".
        /// Сбрасывает форму ввода.
        /// </summary>
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            ClearForm();
        }

        /// <summary>
        /// Обработчик двойного клика по списку категорий.
        /// Заполняет форму данными выбранной категории.
        /// </summary>
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

        /// <summary>
        /// Обработчик изменения текста в поле поиска.
        /// Выполняет поиск категорий по введенному ключевому слову.
        /// </summary>
        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                string keyword = txtSearch.Text;
                DataTable dt = _userData.Search(keyword);
                dgvCategories.ItemsSource = dt.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка поиска: {ex.Message}", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Обработчик загрузки окна.
        /// Загружает список категорий при открытии окна.
        /// </summary>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadCategories();
        }
    }
}