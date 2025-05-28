using Backend.BusinesLogic;
using Backend.BusinessLogic;
using Backend.DataAccess;
using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace Frontend.Views
{
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

        public formLogistic(string loggedInUser)
        {
            InitializeComponent();
            _loggedInUser = loggedInUser;
            InitializeDataTable();
            LoadInitialData();
        }

        // Инициализация таблицы товаров
        private void InitializeDataTable()
        {
            transactionTable.Columns.Add("Артикул");
            transactionTable.Columns.Add("Наименование");
            transactionTable.Columns.Add("Цена");
            transactionTable.Columns.Add("Количество");
            transactionTable.Columns.Add("Сумма");
        }

        // Загрузка начальных данных
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

        // Обновление данных о доставках
        private void RefreshLogisticsData()
        {
            DataTable logistics = logisticData.Select();
            dgvLogistic.ItemsSource = logistics.DefaultView;
        }

        // Обработчик кнопки "Добавить"
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

        // Очистка формы
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

        // Выбор доставки из таблицы
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

        // Обработчик кнопки "Изменить"
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

        // Обработчик кнопки "Удалить"
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

        // Поиск доставок
        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = txtSearch.Text.Trim();

            DataTable result = string.IsNullOrEmpty(searchText)
                ? logisticData.Select()
                : logisticData.Search(searchText);

            dgvLogistic.ItemsSource = result.DefaultView;
        }

        // Выбор сотрудника
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

        // Поиск товара
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

        // Очистка полей товара
        private void ClearProductFields()
        {
            txtProductName.Clear();
            txtInventory.Clear();
            txtRate.Clear();
            txtQty.Clear();
            productId = 0;
            specialNumber = null;
        }

        // Добавление товара в доставку
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

        // Очистка формы
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
    }
}









//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows;
//using System.Windows.Controls;
//using System.Windows.Data;
//using System.Windows.Documents;
//using System.Windows.Input;
//using System.Windows.Media;
//using System.Windows.Media.Imaging;
//using System.Windows.Shapes;
//using Backend.BusinessLogic;
//using Backend.DataAccess;
//using Backend.Interfaces;
//using System;
//using System.Collections.Generic;
//using System.Data;
//using Backend.BusinesLogic;

//namespace Frontend.Views
//{
//    public partial class formLogistic : Window
//    {
//        private readonly LogisticData logisticData;
//        private readonly ProductData productData;
//        private readonly UserData userData;
//        private DataTable addedProducts;
//        private decimal totalAmount = 0;

//        // Временная заглушка для CurrentUser (должна быть заменена вашей реализацией)
//        private class CurrentUser
//        {
//            public static int Id => 1; // Заменить реальным значением
//            public static string Name => "Admin"; // Заменить реальным значением
//        }

//        public formLogistic()
//        {
//            InitializeComponent();
//            logisticData = new LogisticData();
//            productData = new ProductData();
//            userData = new UserData();
//            addedProducts = new DataTable();
//            InitializeAddedProductsTable();
//        }

//        private void InitializeAddedProductsTable()
//        {
//            addedProducts.Columns.Add("ProductID", typeof(int));
//            addedProducts.Columns.Add("Name", typeof(string));
//            addedProducts.Columns.Add("Rate", typeof(decimal));
//            addedProducts.Columns.Add("Quantity", typeof(decimal));
//            addedProducts.Columns.Add("Total", typeof(decimal));
//        }

//        private void Window_Loaded(object sender, RoutedEventArgs e)
//        {
//            try
//            {
//                LoadEmployees();
//                LoadLogistics();
//                txtDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
//            }
//        }

//        private void LoadEmployees()
//        {
//            DataTable users = userData.Select();
//            cmbEmployee.ItemsSource = users.DefaultView;
//            cmbEmployee.DisplayMemberPath = "username";
//            cmbEmployee.SelectedValuePath = "id";
//        }

//        private void LoadLogistics()
//        {
//            try
//            {
//                DataTable dt = logisticData.Select();
//                dgvLogistic.ItemsSource = dt.DefaultView;
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
//            }
//        }

//        private void cmbEmployee_SelectionChanged(object sender, SelectionChangedEventArgs e)
//        {
//            try
//            {
//                if (cmbEmployee.SelectedValue != null)
//                {
//                    User user = userData.SearchUserForLogistic(cmbEmployee.Text);
//                    txtFirstName.Text = user.FirstName;
//                    txtLastName.Text = user.LastName;
//                }
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
//            }
//        }

//        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
//        {
//            try
//            {
//                DataTable dt = logisticData.Search(txtSearch.Text);
//                dgvLogistic.ItemsSource = dt.DefaultView;
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
//            }
//        }

//        private void btnAdd_Click(object sender, RoutedEventArgs e)
//        {
//            try
//            {
//                if (ValidateLogisticFields() && addedProducts.Rows.Count > 0)
//                {
//                    Logistic logistic = new Logistic
//                    {
//                        Empleyee = cmbEmployee.Text,
//                        FirstNameEmployee = txtFirstName.Text,
//                        LastNameEmployee = txtLastName.Text,
//                        Address = txtAddress.Text,
//                        Contact = txtContact.Text,
//                        Date = txtDate.Text,
//                        Description = txtDescription.Text,
//                        Price = totalAmount,
//                        AddedDate = DateTime.Now,
//                        AddedBy = CurrentUser.Id,
//                        AddedByName = CurrentUser.Name
//                    };

//                    if (logisticData.Insert(logistic))
//                    {
//                        MessageBox.Show("Доставка успешно добавлена", "Успех",
//                            MessageBoxButton.OK, MessageBoxImage.Information);
//                        ClearFields();
//                        LoadLogistics();
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
//            }
//        }

//        private bool ValidateLogisticFields()
//        {
//            if (string.IsNullOrEmpty(cmbEmployee.Text))
//                throw new ArgumentException("Выберите сотрудника");
//            if (string.IsNullOrEmpty(txtContact.Text))
//                throw new ArgumentException("Введите контакт");
//            if (string.IsNullOrEmpty(txtAddress.Text))
//                throw new ArgumentException("Введите адрес");

//            return true;
//        }

//        private void btnUpdate_Click(object sender, RoutedEventArgs e)
//        {
//            try
//            {
//                if (dgvLogistic.SelectedItem != null && ValidateLogisticFields())
//                {
//                    DataRowView row = (DataRowView)dgvLogistic.SelectedItem;
//                    Logistic logistic = new Logistic
//                    {
//                        Id = Convert.ToInt32(row["id"]),
//                        Empleyee = cmbEmployee.Text,
//                        FirstNameEmployee = txtFirstName.Text,
//                        LastNameEmployee = txtLastName.Text,
//                        Address = txtAddress.Text,
//                        Contact = txtContact.Text,
//                        Date = txtDate.Text,
//                        Description = txtDescription.Text,
//                        Price = totalAmount
//                    };

//                    if (logisticData.Update(logistic))
//                    {
//                        MessageBox.Show("Доставка успешно обновлена", "Успех",
//                            MessageBoxButton.OK, MessageBoxImage.Information);
//                        LoadLogistics();
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
//            }
//        }

//        private void btnDelete_Click(object sender, RoutedEventArgs e)
//        {
//            try
//            {
//                if (dgvLogistic.SelectedItem != null)
//                {
//                    DataRowView row = (DataRowView)dgvLogistic.SelectedItem;
//                    Logistic logistic = new Logistic { Id = Convert.ToInt32(row["id"]) };

//                    if (logisticData.Delete(logistic))
//                    {
//                        MessageBox.Show("Доставка успешно удалена", "Успех",
//                            MessageBoxButton.OK, MessageBoxImage.Information);
//                        LoadLogistics();
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
//            }
//        }

//        private void txtSearchProduct_TextChanged(object sender, TextChangedEventArgs e)
//        {
//            try
//            {
//                Product product = productData.GetProductsForTransaction(txtSearchProduct.Text);

//                if (product != null)
//                {
//                    txtProductName.Text = product.Name;
//                    txtInventory.Text = product.Quantity.ToString();
//                    txtRate.Text = product.Rate.ToString();
//                }
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
//            }
//        }

//        private void btnAddProduct_Click(object sender, RoutedEventArgs e)
//        {
//            try
//            {
//                if (ValidateProductFields())
//                {
//                    Product product = productData.GetProductIDFromName(txtProductName.Text);

//                    decimal rate = decimal.Parse(txtRate.Text);
//                    decimal qty = decimal.Parse(txtQty.Text);
//                    decimal total = rate * qty;

//                    addedProducts.Rows.Add(
//                        productData.GetProductIDFromName(txtProductName.Text).Id,
//                        txtProductName.Text,
//                        rate,
//                        qty,
//                        total
//                    );

//                    totalAmount += total;
//                    txtTotal.Text = totalAmount.ToString("N2");
//                    dgvAddedProducts.ItemsSource = addedProducts.DefaultView;
//                }
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
//            }
//        }

//        private bool ValidateProductFields()
//        {
//            if (string.IsNullOrEmpty(txtProductName.Text))
//                throw new ArgumentException("Выберите продукт");
//            if (!decimal.TryParse(txtQty.Text, out decimal qty) || qty <= 0)
//                throw new ArgumentException("Некорректное количество");

//            return true;
//        }

//        private void dgvLogistic_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
//        {
//            try
//            {
//                if (dgvLogistic.SelectedItem != null)
//                {
//                    DataRowView row = (DataRowView)dgvLogistic.SelectedItem;
//                    LoadLogisticDetails(row);
//                }
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
//            }
//        }

//        private void LoadLogisticDetails(DataRowView row)
//        {
//            txtID.Text = row["id"].ToString();
//            cmbEmployee.Text = row["employee"].ToString();
//            txtFirstName.Text = row["first_name_employee"].ToString();
//            txtLastName.Text = row["last_name_employee"].ToString();
//            txtAddress.Text = row["address"].ToString();
//            txtContact.Text = row["contact"].ToString();
//            txtDate.Text = row["date"].ToString();
//            txtDescription.Text = row["description"].ToString();
//            txtTotal.Text = row["price"].ToString();
//        }

//        private void btnClear_Click(object sender, RoutedEventArgs e)
//        {
//            ClearFields();
//        }

//        private void ClearFields()
//        {
//            txtID.Clear();
//            cmbEmployee.SelectedIndex = -1;
//            txtFirstName.Clear();
//            txtLastName.Clear();
//            txtAddress.Clear();
//            txtContact.Clear();
//            txtDescription.Clear();
//            txtTotal.Text = "0";
//            addedProducts.Rows.Clear();
//            dgvAddedProducts.ItemsSource = null;
//            totalAmount = 0;
//        }
//    }
//}