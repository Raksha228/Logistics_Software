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
        private readonly ProductData productData;
        private readonly CategoryData categoryData;

        /// <summary>
        /// Инициализирует новый экземпляр окна инвентаризации.
        /// </summary>
        public formInventory()
        {
            InitializeComponent();
            productData = new ProductData();
            categoryData = new CategoryData();
        }

        /// <summary>
        /// Обработчик загрузки окна. Загружает список категорий и товаров.
        /// </summary>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                // Загрузка категорий в комбобокс
                DataTable categories = categoryData.Select();
                cmbCategories.ItemsSource = categories.DefaultView;
                cmbCategories.DisplayMemberPath = "title";
                cmbCategories.SelectedValuePath = "id";

                // Первоначальная загрузка всех продуктов
                LoadAllProducts();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }



        /// <summary>
        /// Загружает все товары из базы данных.
        /// </summary>
        private void LoadAllProducts()
        {
            try
            {
                DataTable products = productData.Select();
                dgvProducts.ItemsSource = products.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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
                if (cmbCategories.SelectedValue != null && int.TryParse(cmbCategories.SelectedValue.ToString(), out int categoryId))
                {
                    DataTable filteredProducts = productData.DisplayProductsByCategory(cmbCategories.Text);
                    dgvProducts.ItemsSource = filteredProducts.DefaultView;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}