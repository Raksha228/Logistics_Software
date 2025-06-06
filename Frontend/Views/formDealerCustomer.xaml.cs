using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Backend.BusinesLogic;
using Backend.BusinessLogic;
using Backend.DataAccess;

namespace Frontend.Views
{
    /// <summary>
    /// Окно для управления дилерами и клиентами системы.
    /// Позволяет добавлять, редактировать, удалять и просматривать записи.
    /// </summary>
    public partial class formDealerCustomer : Window
    {
        private DealerCustomerData _dealerCustomerData;
        private DataTable _dealerCustomerTable;

        private UserData _userData = new UserData();
        private string _loggedInUser;

        /// <summary>
        /// Инициализирует новый экземпляр окна управления дилерами/клиентами.
        /// </summary>
        /// <param name="loggedInUser">Логин авторизованного пользователя</param>
        public formDealerCustomer(string loggedInUser)
        {
            InitializeComponent();
            _dealerCustomerData = new DealerCustomerData();
            _loggedInUser = loggedInUser;
            LoadDealerCustomers();
        }

        /// <summary>
        /// Загружает список дилеров/клиентов из базы данных.
        /// </summary>
        private void LoadDealerCustomers()
        {
            try
            {
                _dealerCustomerTable = _dealerCustomerData.Select();
                dgvDeaCust.ItemsSource = _dealerCustomerTable.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}", "Ошибка",
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        /// <summary>
        /// Создает объект DealerCustomer на основе данных формы.
        /// </summary>
        /// <returns>Заполненный объект DealerCustomer</returns>
        private DealerCustomer GetDealerCustomerFromForm()
        {
            DealerCustomer dealerCustomer = new DealerCustomer()
            {
                Id = string.IsNullOrEmpty(txtDeaCustID.Text) ? 0 : int.Parse(txtDeaCustID.Text),
                Type = (cmbDeaCust.SelectedItem as ComboBoxItem)?.Content.ToString(),
                Name = txtName.Text.Trim(),
                Email = txtEmail.Text.Trim(),
                Contact = txtContact.Text.Trim(),
                Address = txtAddress.Text.Trim(),
                AddedDate = DateTime.Now
            };

            User currentUser = _userData.GetIDFromUsername(_loggedInUser);
            dealerCustomer.AddedBy = currentUser.Id;
            dealerCustomer.AddedByName = _loggedInUser;

            return dealerCustomer;
        }

        /// <summary>
        /// Очищает поля ввода на форме.
        /// </summary>
        private void ClearForm()
        {
            txtDeaCustID.Clear();
            cmbDeaCust.SelectedIndex = -1;
            txtName.Clear();
            txtEmail.Clear();
            txtContact.Clear();
            txtAddress.Clear();
        }





        /// <summary>
        /// Обработчик нажатия кнопки "Добавить".
        /// Добавляет нового дилера/клиента в базу данных.
        /// </summary>
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var dealerCustomer = GetDealerCustomerFromForm();

                if (string.IsNullOrEmpty(dealerCustomer.Type))
                {
                    MessageBox.Show("Выберите тип", "Ошибка",
                                  MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (string.IsNullOrEmpty(dealerCustomer.Name))
                {
                    MessageBox.Show("Введите название", "Ошибка",
                                  MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                bool success = _dealerCustomerData.Insert(dealerCustomer);
                if (success)
                {
                    LoadDealerCustomers();
                    ClearForm();
                    MessageBox.Show("Запись успешно добавлена", "Успех",
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
        /// Обновляет данные выбранного дилера/клиента.
        /// </summary>
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtDeaCustID.Text))
                {
                    MessageBox.Show("Выберите запись для редактирования", "Ошибка",
                                  MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var dealerCustomer = GetDealerCustomerFromForm();
                bool success = _dealerCustomerData.Update(dealerCustomer);
                if (success)
                {
                    LoadDealerCustomers();
                    MessageBox.Show("Запись успешно обновлена", "Успех",
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
        /// Удаляет выбранного дилера/клиента из базы данных.
        /// </summary>
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtDeaCustID.Text))
                {
                    MessageBox.Show("Выберите запись для удаления", "Ошибка",
                                  MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var dealerCustomer = GetDealerCustomerFromForm();
                bool success = _dealerCustomerData.Delete(dealerCustomer);
                if (success)
                {
                    LoadDealerCustomers();
                    ClearForm();
                    MessageBox.Show("Запись успешно удалена", "Успех",
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
        /// Обработчик двойного клика по списку записей.
        /// Заполняет форму данными выбранной записи.
        /// </summary>
        private void dgvDeaCust_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (dgvDeaCust.SelectedItem != null)
                {
                    DataRowView row = (DataRowView)dgvDeaCust.SelectedItem;
                    txtDeaCustID.Text = row["id"].ToString();
                    cmbDeaCust.Text = row["type"].ToString();
                    txtName.Text = row["name"].ToString();
                    txtEmail.Text = row["email"].ToString();
                    txtContact.Text = row["contact"].ToString();
                    txtAddress.Text = row["address"].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка выбора записи: {ex.Message}", "Ошибка",
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            ClearForm();
        }

        /// <summary>
        /// Обработчик изменения текста поиска.
        /// Фильтрует список по введенному ключевому слову.
        /// </summary>
        
        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                string keyword = txtSearch.Text;
                DataTable dt = _userData.Search(keyword);
                dgvDeaCust.ItemsSource = dt.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка поиска: {ex.Message}", "Ошибка",
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Обработчик загрузки окна.
        /// Инициализирует данные при открытии окна.
        /// </summary>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadDealerCustomers();
        }
    }
}