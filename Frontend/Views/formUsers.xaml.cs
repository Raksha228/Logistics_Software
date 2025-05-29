using Backend.BusinesLogic;
using Backend.BusinessLogic;
using Backend.DataAccess;
using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace Frontend.Views
{
    public partial class formUsers : Window
    {
        private User _user = new User();
        private UserData _userData = new UserData();
        private string _loggedInUser;

        public formUsers(string loggedInUser)
        {
            InitializeComponent();
            _loggedInUser = loggedInUser;
            LoadData();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                DataTable dt = _userData.Select();
                dgvUsers.ItemsSource = dt.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Валидация данных
                try { _user.FirstName = txtFirstName.Text; }
                catch { MessageBox.Show("Имя должно содержать минимум 3 символа!"); return; }

                try { _user.LastName = txtLastName.Text; }
                catch { MessageBox.Show("Фамилия должна содержать минимум 3 символа!"); return; }

                try { _user.Email = txtEmail.Text; }
                catch { MessageBox.Show("Неверный формат email!"); return; }

                try { _user.Username = txtUsername.Text; }
                catch { MessageBox.Show("Логин должен содержать минимум 3 символа!"); return; }

                try { _user.Password = txtPassword.Password; }
                catch { MessageBox.Show("Пароль должен содержать минимум 5 символов!"); return; }

                _user.Contact = txtContact.Text;
                _user.Address = txtAddress.Text;
                _user.Gender = (cmbGender.SelectedItem as ComboBoxItem)?.Content.ToString();
                _user.UserType = (cmbUserType.SelectedItem as ComboBoxItem)?.Content.ToString();
                _user.AddedDate = DateTime.Now;

                // Получаем ID текущего пользователя
                User currentUser = _userData.GetIDFromUsername(_loggedInUser);
                _user.AddedBy = currentUser.Id;
                _user.AddedByName = _loggedInUser;

                bool success = _userData.Insert(_user);
                if (success)
                {
                    MessageBox.Show("Пользователь успешно добавлен!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    Clear();
                    LoadData();
                }
                else
                {
                    MessageBox.Show("Ошибка при добавлении пользователя!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtUserID.Text))
                {
                    MessageBox.Show("Выберите пользователя для редактирования!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                _user.Id = int.Parse(txtUserID.Text);

                // Валидация данных
                try { _user.FirstName = txtFirstName.Text; }
                catch { MessageBox.Show("Имя должно содержать минимум 3 символа!"); return; }

                try { _user.LastName = txtLastName.Text; }
                catch { MessageBox.Show("Фамилия должна содержать минимум 3 символа!"); return; }

                try { _user.Email = txtEmail.Text; }
                catch { MessageBox.Show("Неверный формат email!"); return; }

                try { _user.Username = txtUsername.Text; }
                catch { MessageBox.Show("Логин должен содержать минимум 3 символа!"); return; }

                try { _user.Password = txtPassword.Password; }
                catch { MessageBox.Show("Пароль должен содержать минимум 5 символов!"); return; }

                _user.Contact = txtContact.Text;
                _user.Address = txtAddress.Text;
                _user.Gender = (cmbGender.SelectedItem as ComboBoxItem)?.Content.ToString();
                _user.UserType = (cmbUserType.SelectedItem as ComboBoxItem)?.Content.ToString();
                _user.AddedDate = DateTime.Now;

                // Обновляем информацию о том, кто изменил запись
                User currentUser = _userData.GetIDFromUsername(_loggedInUser);
                _user.AddedBy = currentUser.Id;
                _user.AddedByName = _loggedInUser;

                bool success = _userData.Update(_user);
                if (success)
                {
                    MessageBox.Show("Данные пользователя обновлены!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    Clear();
                    LoadData();
                }
                else
                {
                    MessageBox.Show("Ошибка при обновлении данных!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtUserID.Text))
                {
                    MessageBox.Show("Выберите пользователя для удаления!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                MessageBoxResult result = MessageBox.Show(
                    "Вы уверены, что хотите удалить этого пользователя?",
                    "Подтверждение",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    _user.Id = int.Parse(txtUserID.Text);
                    bool success = _userData.Delete(_user);
                    if (success)
                    {
                        MessageBox.Show("Пользователь удалён!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                        Clear();
                        LoadData();
                    }
                    else
                    {
                        MessageBox.Show("Ошибка при удалении!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            Clear();
        }

        private void Clear()
        {
            txtUserID.Text = "";
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtEmail.Text = "";
            txtUsername.Text = "";
            txtPassword.Password = "";
            txtContact.Text = "";
            txtAddress.Text = "";
            cmbGender.SelectedIndex = -1;
            cmbUserType.SelectedIndex = -1;
        }

        private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                string keyword = txtSearch.Text;
                DataTable dt = _userData.Search(keyword);
                dgvUsers.ItemsSource = dt.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка поиска: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DgvUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgvUsers.SelectedItem != null)
            {
                DataRowView row = (DataRowView)dgvUsers.SelectedItem;
                txtUserID.Text = row["id"].ToString();
                txtFirstName.Text = row["first_name"].ToString();
                txtLastName.Text = row["last_name"].ToString();
                txtEmail.Text = row["email"].ToString();
                txtUsername.Text = row["username"].ToString();
                txtPassword.Password = row["password"].ToString();
                txtContact.Text = row["contact"].ToString();
                txtAddress.Text = row["address"].ToString();

                // Установка пола
                foreach (ComboBoxItem item in cmbGender.Items)
                {
                    if (item.Content.ToString() == row["gender"].ToString())
                    {
                        cmbGender.SelectedItem = item;
                        break;
                    }
                }

                // Установка типа пользователя
                foreach (ComboBoxItem item in cmbUserType.Items)
                {
                    if (item.Content.ToString() == row["user_type"].ToString())
                    {
                        cmbUserType.SelectedItem = item;
                        break;
                    }
                }
            }
        }
        private void Window_Closed(object sender, EventArgs e)
        {
            // Логика при закрытии окна
        }
    }
}