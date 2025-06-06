using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Backend.BusinesLogic;
using Backend.BusinessLogic;
using Backend.DataAccess;
using Backend.Interfaces;

namespace Frontend.Views
{
    /// <summary>
    /// Окно управления товарами в системе.
    /// Позволяет добавлять, редактировать, удалять и просматривать товары.
    /// </summary>
    public partial class formProducts : Window
    {
        private readonly ICrudProduct _productService = new ProductData();
        private readonly ICrudCategory _categoryService = new CategoryData();
        private readonly UserData _userData = new UserData();
        private readonly string _loggedInUser;

        /// <summary>
        /// Инициализирует новый экземпляр окна управления товарами.
        /// </summary>
        /// <param name="loggedInUser">Логин авторизованного пользователя</param>
        public formProducts(string loggedInUser)
        {
            InitializeComponent();
            _loggedInUser = loggedInUser;
            InitializeWindow();
        }

        /// <summary>
        /// Инициализирует окно при загрузке.
        /// </summary>
        private void InitializeWindow()
        {
            Window_Loaded(null, null);
        }

        /// <summary>
        /// Загружает список категорий в комбобокс.
        /// </summary>
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
                ShowErrorMessage($"Ошибка загрузки категорий: {ex.Message}");
            }
        }

        /// <summary>
        /// Загружает список товаров в таблицу.
        /// </summary>
        private void LoadProducts()
        {
            try
            {
                DataTable products = _productService.Select();
                dgvProducts.ItemsSource = products.DefaultView;
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Ошибка загрузки товаров: {ex.Message}");
            }
        }

        /// <summary>
        /// Очищает поля ввода.
        /// </summary>
        private void ClearFields()
        {
            txtID.Clear();
            txtName.Clear();
            cmbCategory.SelectedIndex = -1;
            txtSpecialNumber.Clear();
            txtDescription.Clear();
            txtRate.Clear();
        }

        /// <summary>
        /// Проверяет корректность введенных данных.
        /// </summary>
        /// <returns>True если данные валидны, иначе False</returns>
        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                ShowWarningMessage("Введите название товара");
                return false;
            }

            if (cmbCategory.SelectedValue == null)
            {
                ShowWarningMessage("Выберите категорию");
                return false;
            }

            if (!decimal.TryParse(txtRate.Text, out _))
            {
                ShowErrorMessage("Некорректный формат цены");
                return false;
            }

            return true;
        }

        /// <summary>
        /// Обработчик кнопки добавления товара.
        /// </summary>
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateInputs()) return;

            try
            {
                var product = CreateProductFromInput();
                if (_productService.Insert(product))
                {
                    ShowSuccessMessage("Товар успешно добавлен");
                    ResetView();
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Ошибка добавления: {ex.Message}");
            }
        }

        /// <summary>
        /// Создает объект Product на основе данных формы.
        /// </summary>
        private Product CreateProductFromInput()
        {
            User currentUser = _userData.GetIDFromUsername(_loggedInUser);

            return new Product
            {
                Name = txtName.Text,
                Category = cmbCategory.SelectedValue.ToString(),
                SpecialNumber = txtSpecialNumber.Text,
                Description = txtDescription.Text,
                Rate = decimal.Parse(txtRate.Text),
                AddedDate = DateTime.Now,
                AddedBy = currentUser.Id,
                AddedByName = _loggedInUser
            };
        }

        /// <summary>
        /// Обработчик кнопки обновления товара.
        /// </summary>
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateInputs() || string.IsNullOrEmpty(txtID.Text)) return;

            try
            {
                var product = CreateProductFromInput();
                product.Id = int.Parse(txtID.Text);

                if (_productService.Update(product))
                {
                    ShowSuccessMessage("Товар успешно обновлён");
                    ResetView();
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Ошибка обновления: {ex.Message}");
            }
        }

        /// <summary>
        /// Обработчик кнопки удаления товара.
        /// </summary>
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtID.Text)) return;

            try
            {
                if (ConfirmDelete())
                {
                    var product = new Product { Id = int.Parse(txtID.Text) };

                    if (_productService.Delete(product))
                    {
                        ShowSuccessMessage("Товар успешно удалён");
                        ResetView();
                    }
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Ошибка удаления: {ex.Message}");
            }
        }

        /// <summary>
        /// Запрашивает подтверждение удаления товара.
        /// </summary>
        private bool ConfirmDelete()
        {
            return MessageBox.Show("Вы уверены, что хотите удалить этот товар?",
                "Подтверждение удаления",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question) == MessageBoxResult.Yes;
        }

        /// <summary>
        /// Обработчик кнопки очистки формы.
        /// </summary>
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            ClearFields();
        }

        /// <summary>
        /// Обработчик двойного клика по списку товаров.
        /// </summary>
        private void dgvProducts_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (dgvProducts.SelectedItem is DataRowView row)
                {
                    txtID.Text = row["id"].ToString();
                    txtName.Text = row["name"].ToString();
                    cmbCategory.SelectedValue = row["category"];
                    txtSpecialNumber.Text = row["special_number"].ToString();
                    txtDescription.Text = row["description"].ToString();
                    txtRate.Text = row["rate"].ToString();
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Ошибка загрузки данных: {ex.Message}");
            }
        }

        /// <summary>
        /// Обработчик поиска товаров.
        /// </summary>
        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                DataTable result = _productService.Search(txtSearch.Text);
                dgvProducts.ItemsSource = result.DefaultView;
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Ошибка поиска: {ex.Message}");
            }
        }

        /// <summary>
        /// Обработчик загрузки окна.
        /// </summary>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadProducts();
            LoadCategories();
        }

        /// <summary>
        /// Обновляет данные в окне.
        /// </summary>
        private void ResetView()
        {
            LoadProducts();
            ClearFields();
        }

        /// <summary>
        /// Показывает сообщение об ошибке.
        /// </summary>
        private void ShowErrorMessage(string message)
        {
            MessageBox.Show(message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        /// <summary>
        /// Показывает предупреждающее сообщение.
        /// </summary>
        private void ShowWarningMessage(string message)
        {
            MessageBox.Show(message, "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
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