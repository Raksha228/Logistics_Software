using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using Backend.BusinessLogic;
using Backend.DataAccess;

namespace Frontend.Views
{
    /// <summary>
    /// Окно управления инвентаризацией товаров.
    /// Позволяет просматривать товары по категориям и выполнять инвентаризацию.
    /// </summary>
    public partial class formInventory : Window
    {
        private readonly ProductData _productData;
        private readonly CategoryData _categoryData;

        /// <summary>
        /// Инициализирует новый экземпляр окна инвентаризации.
        /// </summary>
        public formInventory()
        {
            InitializeComponent();
            _productData = new ProductData();
            _categoryData = new CategoryData();
        }

        /// <summary>
        /// Обработчик загрузки окна. Загружает список категорий и товаров.
        /// </summary>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                // Инициализация комбобокса категорий
                InitializeCategoryComboBox();
                // Загрузка всех товаров
                LoadAllProducts();
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
        }

        /// <summary>
        /// Загружает и инициализирует комбобокс категорий товаров.
        /// </summary>
        private void InitializeCategoryComboBox()
        {
            DataTable categories = _categoryData.Select();
            cmbCategories.ItemsSource = categories.DefaultView;
            cmbCategories.DisplayMemberPath = "title";
            cmbCategories.SelectedValuePath = "id";
        }

        /// <summary>
        /// Загружает все товары из базы данных.
        /// </summary>
        private void LoadAllProducts()
        {
            try
            {
                DataTable products = _productData.Select();
                dgvProducts.ItemsSource = products.DefaultView;
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
        }

        /// <summary>
        /// Обработчик изменения выбранной категории.
        /// Фильтрует товары по выбранной категории.
        /// </summary>
        private void cmbCategories_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (cmbCategories.SelectedValue != null &&
                    int.TryParse(cmbCategories.SelectedValue.ToString(), out int categoryId))
                {
                    DataTable filteredProducts = _productData.DisplayProductsByCategory(cmbCategories.Text);
                    dgvProducts.ItemsSource = filteredProducts.DefaultView;
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
        }

        /// <summary>
        /// Обработчик нажатия кнопки "Все".
        /// Сбрасывает фильтр категорий и показывает все товары.
        /// </summary>
        private void btnAll_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                LoadAllProducts();
                cmbCategories.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
        }

        /// <summary>
        /// Показывает сообщение об ошибке.
        /// </summary>
        /// <param name="message">Текст сообщения об ошибке</param>
        private void ShowErrorMessage(string message)
        {
            MessageBox.Show(message, "Ошибка",
                MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}