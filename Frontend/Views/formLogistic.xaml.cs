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
        private readonly UserData _userData = new UserData();
        private readonly Logistic _logistic = new Logistic();
        private readonly LogisticData _logisticData = new LogisticData();
        private readonly ProductData _productData = new ProductData();

        private readonly string _loggedInUser;
        private readonly DataTable _transactionTable = new DataTable();
        private int _productId;
        private string _description;
        private string _specialNumber;

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
            _transactionTable.Columns.Add("Артикул");
            _transactionTable.Columns.Add("Наименование");
            _transactionTable.Columns.Add("Цена");
            _transactionTable.Columns.Add("Количество");
            _transactionTable.Columns.Add("Сумма");
        }

        /// <summary>
        /// Загружает начальные данные при запуске окна.
        /// </summary>
        private void LoadInitialData()
        {
            try
            {
                LoadEmployees();
                RefreshLogisticsData();
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Ошибка загрузки данных: {ex.Message}");
            }
        }

        /// <summary>
        /// Загружает список сотрудников в комбобокс.
        /// </summary>
        private void LoadEmployees()
        {
            DataTable employees = _userData.Select();
            cmbEmployee.ItemsSource = employees.DefaultView;
            cmbEmployee.DisplayMemberPath = "username";
            cmbEmployee.SelectedValuePath = "username";
        }

        /// <summary>
        /// Обновляет данные о доставках в таблице.
        /// </summary>
        private void RefreshLogisticsData()
        {
            DataTable logistics = _logisticData.Select();
            dgvLogistic.ItemsSource = logistics.DefaultView;
        }

        /// <summary>
        /// Обработчик кнопки добавления новой доставки.
        /// </summary>
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateLogisticInput())
                return;

            try
            {
                FillLogisticData();

                if (_logisticData.Insert(_logistic))
                {
                    ShowSuccessMessage("Доставка успешно добавлена!");
                    ResetForm();
                }
                else
                {
                    ShowErrorMessage("Ошибка при добавлении доставки!");
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Ошибка: {ex.Message}");
            }
        }

        /// <summary>
        /// Проверяет корректность введенных данных о доставке.
        /// </summary>
        private bool ValidateLogisticInput()
        {
            if (string.IsNullOrWhiteSpace(txtContact.Text))
            {
                ShowErrorMessage("Введите корректные контактные данные!");
                return false;
            }
            return true;
        }

        /// <summary>
        /// Заполняет объект Logistic данными из формы.
        /// </summary>
        private void FillLogisticData()
        {
            _logistic.Empleyee = cmbEmployee.Text;
            _logistic.FirstNameEmployee = txtFirstName.Text;
            _logistic.LastNameEmployee = txtLastName.Text;
            _logistic.Address = txtAddress.Text;
            _logistic.Contact = txtContact.Text;
            _logistic.Date = txtDate.Text;
            _logistic.Description = _description;
            _logistic.Price = decimal.Parse(txtTotal.Text);
            _logistic.AddedDate = DateTime.Now;

            User currentUser = _userData.GetIDFromUsername(formLogin.LoggedInUser);
            _logistic.AddedBy = currentUser.Id;
            _logistic.AddedByName = _loggedInUser;
        }

        /// <summary>
        /// Очищает форму и сбрасывает состояние.
        /// </summary>
        private void ResetForm()
        {
            ClearForm();
            RefreshLogisticsData();
            _transactionTable.Clear();
            _description = null;
            dgvAddedProducts.ItemsSource = null;
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
        private void dgvLogistic_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (dgvLogistic.SelectedItem is DataRowView row)
            {
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
                ShowErrorMessage("Выберите запись для изменения!");
                return;
            }

            try
            {
                UpdateLogisticData();

                if (_logisticData.Update(_logistic))
                {
                    ShowSuccessMessage("Данные доставки обновлены!");
                    ResetForm();
                }
                else
                {
                    ShowErrorMessage("Ошибка при обновлении данных!");
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Ошибка: {ex.Message}");
            }
        }

        /// <summary>
        /// Обновляет данные объекта Logistic для редактирования.
        /// </summary>
        private void UpdateLogisticData()
        {
            _logistic.Id = int.Parse(txtID.Text);
            FillLogisticData();

            User currentUser = _userData.GetIDFromUsername(formLogin.LoggedInUser);
            _logistic.AddedByName = currentUser.Username;
        }

        /// <summary>
        /// Обработчик кнопки удаления доставки.
        /// </summary>
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtID.Text))
            {
                ShowErrorMessage("Выберите запись для удаления!");
                return;
            }

            try
            {
                _logistic.Id = int.Parse(txtID.Text);
                ReturnProductsToStock();

                if (_logisticData.Delete(_logistic))
                {
                    ShowSuccessMessage("Доставка удалена!");
                    ResetForm();
                }
                else
                {
                    ShowErrorMessage("Ошибка при удалении!");
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Ошибка: {ex.Message}");
            }
        }

        /// <summary>
        /// Возвращает товары из доставки на склад.
        /// </summary>
        private void ReturnProductsToStock()
        {
            if (!string.IsNullOrEmpty(txtDescription.Text))
            {
                string[] items = txtDescription.Text.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string item in items)
                {
                    ProcessReturnedItem(item);
                }
            }
        }

        /// <summary>
        /// Обрабатывает возврат одного товара на склад.
        /// </summary>
        private void ProcessReturnedItem(string item)
        {
            string[] parts = item.Split('=');
            if (parts.Length == 2)
            {
                string productName = parts[0];
                int qty = int.Parse(parts[1]);
                Product product = _productData.GetProductIDFromName(productName);
                _productData.IncreaseProduct(product.Id, qty);
            }
        }

        /// <summary>
        /// Обработчик поиска доставок.
        /// </summary>
        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = txtSearch.Text.Trim();
            DataTable result = string.IsNullOrEmpty(searchText)
                ? _logisticData.Select()
                : _logisticData.Search(searchText);
            dgvLogistic.ItemsSource = result.DefaultView;
        }

        /// <summary>
        /// Обработчик выбора сотрудника.
        /// </summary>
        private void cmbEmployee_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbEmployee.SelectedItem is DataRowView selected)
            {
                string username = selected["username"].ToString();
                User user = _userData.SearchUserForLogistic(username);
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

            Product product = _logisticData.GetProductsForLogistic(searchText);
            if (product != null)
            {
                DisplayProductInfo(product);
            }
            else
            {
                ClearProductFields();
                ShowErrorMessage("Товар не найден!");
            }
        }

        /// <summary>
        /// Отображает информацию о найденном товаре.
        /// </summary>
        private void DisplayProductInfo(Product product)
        {
            _productId = product.Id;
            _specialNumber = product.SpecialNumber;
            txtProductName.Text = product.Name;
            txtInventory.Text = product.Quantity.ToString();
            txtRate.Text = product.Rate.ToString();
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
            _productId = 0;
            _specialNumber = null;
        }

        /// <summary>
        /// Обработчик добавления товара в доставку.
        /// </summary>
        private void btnAddProduct_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateProductInput())
                return;

            try
            {
                AddProductToDelivery();
                ClearProductFields();
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Ошибка: {ex.Message}");
            }
        }

        /// <summary>
        /// Проверяет корректность введенных данных о товаре.
        /// </summary>
        private bool ValidateProductInput()
        {
            if (string.IsNullOrEmpty(txtProductName.Text))
            {
                ShowErrorMessage("Сначала выберите товар!");
                return false;
            }

            if (!decimal.TryParse(txtQty.Text, out decimal quantity) || quantity <= 0)
            {
                ShowErrorMessage("Введите корректное количество!");
                return false;
            }

            return true;
        }

        /// <summary>
        /// Добавляет товар в текущую доставку.
        /// </summary>
        private void AddProductToDelivery()
        {
            decimal price = decimal.Parse(txtRate.Text);
            decimal quantity = decimal.Parse(txtQty.Text);
            decimal total = price * quantity;
            decimal currentTotal = decimal.Parse(txtTotal.Text);

            UpdateDeliveryTotal(currentTotal + total + 8); // +8 за доставку
            AddToDescription(txtProductName.Text, quantity);
            AddToTransactionTable(price, quantity, total);

            if (!_productData.DecreaseProduct(_productId, quantity))
            {
                ShowErrorMessage("Недостаточно товара на складе!");
            }
        }

        /// <summary>
        /// Обновляет общую стоимость доставки.
        /// </summary>
        private void UpdateDeliveryTotal(decimal newTotal)
        {
            txtTotal.Text = newTotal.ToString();
        }

        /// <summary>
        /// Добавляет товар в описание доставки.
        /// </summary>
        private void AddToDescription(string productName, decimal quantity)
        {
            _description += $"{productName}={quantity} ";
            txtDescription.Text = _description;
        }

        /// <summary>
        /// Добавляет товар в таблицу товаров доставки.
        /// </summary>
        private void AddToTransactionTable(decimal price, decimal quantity, decimal total)
        {
            _transactionTable.Rows.Add(
                _specialNumber,
                txtProductName.Text,
                price,
                quantity,
                total
            );
            dgvAddedProducts.ItemsSource = _transactionTable.DefaultView;
        }

        /// <summary>
        /// Обработчик кнопки очистки формы.
        /// </summary>
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            ClearAll();
        }

        /// <summary>
        /// Полностью очищает форму и сбрасывает состояние.
        /// </summary>
        private void ClearAll()
        {
            ClearForm();
            txtSearch.Clear();
            ClearProductFields();
            txtTotal.Text = "0";
            txtDescription.Clear();
            dgvAddedProducts.ItemsSource = null;
            _transactionTable.Clear();
            _description = null;
        }

        /// <summary>
        /// Показывает сообщение об ошибке.
        /// </summary>
        private void ShowErrorMessage(string message)
        {
            MessageBox.Show(message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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