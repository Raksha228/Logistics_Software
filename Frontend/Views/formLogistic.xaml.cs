using Backend.BusinesLogic;
using Backend.BusinessLogic;
using Backend.DataAccess;
using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Frontend.Views
{
    /// <summary>
    /// Окно управления логистикой и доставками товаров.
    /// Позволяет создавать, редактировать и отслеживать доставки,
    /// а также управлять товарами в доставках.
    /// </summary>
    public partial class formLogistic : Window
    {
        private UserData userData = new UserData();
        private Logistic logistic = new Logistic();
        private LogisticData logisticData = new LogisticData();
        private ProductData productData = new ProductData();

        private string _loggedInUser;



        private DataTable transactionTable = new DataTable(); // Таблица для хранения товаров
        private int productId; // ID текущего продукта
        private string description = null; // Описание доставки
        private string specialNumber = null; // Артикул товара

        /// <summary>
        /// Инициализирует новый экземпляр окна управления логистикой.
        /// </summary>
        /// <param name="loggedInUser">Логин авторизованного пользователя</param>
        public formLogistic(string loggedInUser)
        {
            InitializeComponent();
            _loggedInUser = loggedInUser;
            InitializeDataTable();
            LoadInitialData();
        }

        /// <summary>
        /// Инициализирует таблицу для хранения товаров в доставке.
        /// </summary>
        private void InitializeDataTable()
        {
            transactionTable.Columns.Add("Артикул");
            transactionTable.Columns.Add("Наименование");
            transactionTable.Columns.Add("Цена");
            transactionTable.Columns.Add("Количество");
            transactionTable.Columns.Add("Сумма");
        }

        /// <summary>
        /// Загружает начальные данные при запуске окна.
        /// </summary>
        private void LoadInitialData()
        {
            try
            {
                // Загрузка списка сотрудников
                DataTable employees = userData.Select();
                cmbEmployee.ItemsSource = employees.DefaultView;
                cmbEmployee.DisplayMemberPath = "username";
                cmbEmployee.SelectedValuePath = "username";

                // Загрузка данных о доставках
                RefreshLogisticsData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}");
            }
        }


        /// <summary>
        /// Обновляет данные о доставках в таблице.
        /// </summary>
        private void RefreshLogisticsData()
        {
            DataTable logistics = logisticData.Select();
            dgvLogistic.ItemsSource = logistics.DefaultView;
        }

        /// <summary>
        /// Обработчик кнопки добавления новой доставки.
        /// </summary>
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            // Валидация контактных данных
            if (string.IsNullOrWhiteSpace(txtContact.Text))
            {
                MessageBox.Show("Введите корректные контактные данные!");
                return;
            }

            try
            {
                // Заполнение данных о доставке
                logistic.Empleyee = cmbEmployee.Text;
                logistic.FirstNameEmployee = txtFirstName.Text;
                logistic.LastNameEmployee = txtLastName.Text;
                logistic.Address = txtAddress.Text;
                logistic.Contact = txtContact.Text;
                logistic.Date = txtDate.Text;
                logistic.Description = description;
                logistic.Price = decimal.Parse(txtTotal.Text);
                logistic.AddedDate = DateTime.Now;

                // Получение данных текущего пользователя
                User currentUser = userData.GetIDFromUsername(formLogin.loggedIn);
                logistic.AddedBy = currentUser.Id;
                logistic.AddedByName = _loggedInUser;

                // Сохранение в БД
                bool success = logisticData.Insert(logistic);

                if (success)
                {
                    MessageBox.Show("Доставка успешно добавлена!");
                    ClearForm();
                    RefreshLogisticsData();
                    transactionTable.Clear();
                    description = null;
                    dgvAddedProducts.ItemsSource = null;
                }
                else
                {
                    MessageBox.Show("Ошибка при добавлении доставки!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }



        /// <summary>
        /// Очищает основные поля формы.
        /// </summary>
        private void ClearForm()
        {
            txtID.Clear();
            cmbEmployee.SelectedIndex = -1;
            txtFirstName.Clear();
            txtLastName.Clear();
            txtAddress.Clear();
            txtContact.Clear();
            txtDate.Clear();
            txtTotal.Text = "0";
            txtDescription.Clear();
        }

        /// <summary>
        /// Обработчик двойного клика по списку доставок.
        /// Заполняет форму данными выбранной доставки.
        /// </summary>
        private void dgvLogistic_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (dgvLogistic.SelectedItem != null)
            {
                DataRowView row = (DataRowView)dgvLogistic.SelectedItem;
                txtID.Text = row["Logistic ID"].ToString();
                cmbEmployee.Text = row["Employee"].ToString();
                txtFirstName.Text = row["Employee Name"].ToString();
                txtLastName.Text = row["Employee Last Name"].ToString();
                txtAddress.Text = row["Address"].ToString();
                txtContact.Text = row["Contact"].ToString();
                txtDate.Text = row["Date"].ToString();
                txtDescription.Text = row["Description"].ToString();
                txtTotal.Text = row["Total Price"].ToString();
            }
        }

        /// <summary>
        /// Обработчик кнопки обновления данных доставки.
        /// </summary>
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtID.Text))
            {
                MessageBox.Show("Выберите запись для изменения!");
                return;
            }

            try
            {
                logistic.Id = int.Parse(txtID.Text);
                logistic.Empleyee = cmbEmployee.Text;
                logistic.FirstNameEmployee = txtFirstName.Text;
                logistic.LastNameEmployee = txtLastName.Text;
                logistic.Address = txtAddress.Text;
                logistic.Contact = txtContact.Text;
                logistic.Date = txtDate.Text;
                logistic.Description = txtDescription.Text;
                logistic.Price = decimal.Parse(txtTotal.Text);
                logistic.AddedDate = DateTime.Now;

                // Получение данных текущего пользователя
                User currentUser = userData.GetIDFromUsername(formLogin.loggedIn);
                logistic.AddedBy = currentUser.Id;
                logistic.AddedByName = currentUser.Username;

                bool success = logisticData.Update(logistic);

                if (success)
                {
                    MessageBox.Show("Данные доставки обновлены!");
                    ClearForm();
                    RefreshLogisticsData();
                }
                else
                {
                    MessageBox.Show("Ошибка при обновлении данных!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }


        /// <summary>
        /// Обработчик кнопки удаления доставки.
        /// </summary>
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtID.Text))
            {
                MessageBox.Show("Выберите запись для удаления!");
                return;
            }

            try
            {
                logistic.Id = int.Parse(txtID.Text);

                // Возврат товаров на склад
                if (!string.IsNullOrEmpty(txtDescription.Text))
                {
                    string[] items = txtDescription.Text.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string item in items)
                    {
                        string[] parts = item.Split('=');
                        if (parts.Length == 2)
                        {
                            string productName = parts[0];
                            int qty = int.Parse(parts[1]);
                            Product product = productData.GetProductIDFromName(productName);
                            productData.IncreaseProduct(product.Id, qty);
                        }
                    }
                }

                bool success = logisticData.Delete(logistic);

                if (success)
                {
                    MessageBox.Show("Доставка удалена!");
                    ClearForm();
                    RefreshLogisticsData();
                }
                else
                {
                    MessageBox.Show("Ошибка при удалении!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        /// <summary>
        /// Обработчик поиска доставок.
        /// </summary>
        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = txtSearch.Text.Trim();

            DataTable result = string.IsNullOrEmpty(searchText)
                ? logisticData.Select()
                : logisticData.Search(searchText);

            dgvLogistic.ItemsSource = result.DefaultView;
        }

        /// <summary>
        /// Обработчик выбора сотрудника.
        /// </summary>
        private void cmbEmployee_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbEmployee.SelectedItem != null)
            {
                DataRowView selected = (DataRowView)cmbEmployee.SelectedItem;
                string username = selected["username"].ToString();
                User user = userData.SearchUserForLogistic(username);
                txtFirstName.Text = user.FirstName;
                txtLastName.Text = user.LastName;
            }
        }

        /// <summary>
        /// Обработчик поиска товара.
        /// </summary>
        private void txtSearchProduct_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = txtSearchProduct.Text.Trim();

            if (string.IsNullOrEmpty(searchText))
            {
                ClearProductFields();
                return;
            }

            Product product = logisticData.GetProductsForLogistic(searchText);
            if (product != null)
            {
                productId = product.Id;
                specialNumber = product.SpecialNumber;
                txtProductName.Text = product.Name;
                txtInventory.Text = product.Quantity.ToString();
                txtRate.Text = product.Rate.ToString();
            }
            else
            {
                ClearProductFields();
                MessageBox.Show("Товар не найден!");
            }
        }

        /// <summary>
        /// Очищает поля информации о товаре.
        /// </summary>
        private void ClearProductFields()
        {
            txtProductName.Clear();
            txtInventory.Clear();
            txtRate.Clear();
            txtQty.Clear();
            productId = 0;
            specialNumber = null;
        }

        /// <summary>
        /// Обработчик добавления товара в доставку.
        /// </summary>
        private void btnAddProduct_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtProductName.Text))
            {
                MessageBox.Show("Сначала выберите товар!");
                return;
            }

            if (!decimal.TryParse(txtQty.Text, out decimal quantity) || quantity <= 0)
            {
                MessageBox.Show("Введите корректное количество!");
                return;
            }

            try
            {
                decimal price = decimal.Parse(txtRate.Text);
                decimal total = price * quantity;
                decimal currentTotal = decimal.Parse(txtTotal.Text);
                txtTotal.Text = (currentTotal + total + 8).ToString(); // +8 за доставку

                // Добавление в описание
                description += $"{txtProductName.Text}={quantity} ";

                // Добавление в таблицу
                transactionTable.Rows.Add(
                    specialNumber,
                    txtProductName.Text,
                    price,
                    quantity,
                    total
                );

                dgvAddedProducts.ItemsSource = transactionTable.DefaultView;
                txtDescription.Text = description;

                // Уменьшение количества на складе
                bool success = productData.DecreaseProduct(productId, quantity);
                if (!success)
                {
                    MessageBox.Show("Недостаточно товара на складе!");
                    return;
                }

                ClearProductFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }



        /// <summary>
        /// Обработчик кнопки очистки формы.
        /// </summary>
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            ClearForm();
            txtSearch.Clear();
            ClearProductFields();
            txtTotal.Text = "0";
            txtDescription.Clear();
            dgvAddedProducts.ItemsSource = null;
            transactionTable.Clear();
            description = null;
        }

        /// <summary>
        /// Полностью очищает форму и сбрасывает состояние.
        /// </summary>


    }
}