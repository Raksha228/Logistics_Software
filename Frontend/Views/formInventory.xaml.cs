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
    public partial class formInventory : Window
    {
        private readonly ProductData productData;
        private readonly CategoryData categoryData;

        public formInventory()
        {
            InitializeComponent();
            productData = new ProductData();
            categoryData = new CategoryData();
        }

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