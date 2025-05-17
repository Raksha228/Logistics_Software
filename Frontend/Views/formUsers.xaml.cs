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
using Backend.BusinesLogic;

namespace Frontend.Views
{
    public partial class formUsers : Window
    {
        private UserData userData = new UserData();
        private bool isPasswordVisible = false;

        public formUsers()
        {
            InitializeComponent();
            Loaded += Window_Loaded;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadUsers();
            ClearFields();
        }

        private void LoadUsers()
        {
            try
            {
                DataTable dt = userData.Select();
                dgvUsers.ItemsSource = dt.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки пользователей: {ex.Message}", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ClearFields()
        {
            txtUserID.Clear();
            txtFirstName.Clear();
            txtLastName.Clear();
            txtEmail.Clear();
            txtUsername.Clear();
            txtPassword.Clear();
            txtContact.Clear();
            txtAddress.Clear();
            cmbGender.SelectedIndex = -1;
            cmbUserType.SelectedIndex = -1;
        }

        private User GetUserFromFields()
        {
            return new User
            {
                Id = string.IsNullOrEmpty(txtUserID.Text) ? 0 : int.Parse(txtUserID.Text),
                FirstName = txtFirstName.Text,
                LastName = txtLastName.Text,
                Email = txtEmail.Text,
                Username = txtUsername.Text,
                Password = txtPassword.Password,
                Contact = txtContact.Text,
                Address = txtAddress.Text,
                Gender = ((ComboBoxItem)cmbGender.SelectedItem)?.Content.ToString(),
                UserType = ((ComboBoxItem)cmbUserType.SelectedItem)?.Content.ToString()
            };
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                User newUser = GetUserFromFields();
                if (userData.Insert(newUser))
                {
                    MessageBox.Show("Пользователь успешно добавлен", "Успех",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    LoadUsers();
                    ClearFields();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка добавления: {ex.Message}", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtUserID.Text)) return;

            try
            {
                User user = GetUserFromFields();
                if (userData.Update(user))
                {
                    MessageBox.Show("Данные обновлены", "Успех",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    LoadUsers();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка обновления: {ex.Message}", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtUserID.Text)) return;

            try
            {
                User user = GetUserFromFields();
                if (userData.Delete(user))
                {
                    MessageBox.Show("Пользователь удален", "Успех",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    LoadUsers();
                    ClearFields();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка удаления: {ex.Message}", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            ClearFields();
        }

        private void DgvUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgvUsers.SelectedItem == null) return;

            DataRowView row = (DataRowView)dgvUsers.SelectedItem;
            txtUserID.Text = row["id"].ToString();
            txtFirstName.Text = row["first_name"].ToString();
            txtLastName.Text = row["last_name"].ToString();
            txtEmail.Text = row["email"].ToString();
            txtUsername.Text = row["username"].ToString();
            txtContact.Text = row["contact"].ToString();
            txtAddress.Text = row["address"].ToString();

            // Для пароля требуется отдельная логика получения
        }

        private void CheckPass_Checked(object sender, RoutedEventArgs e)
        {
            txtPassword.Visibility = Visibility.Collapsed;
            var visiblePasswordBox = new TextBox
            {
                Text = txtPassword.Password,
                FontWeight = FontWeights.SemiBold,
                Margin = txtPassword.Margin
            };
            Grid.SetRow(visiblePasswordBox, Grid.GetRow(txtPassword));
            ((Panel)txtPassword.Parent).Children.Add(visiblePasswordBox);
        }

        private void CheckPass_Unchecked(object sender, RoutedEventArgs e)
        {
            foreach (UIElement element in ((Panel)txtPassword.Parent).Children)
            {
                if (element is TextBox textBox && element != txtPassword)
                {
                    txtPassword.Password = textBox.Text;
                    ((Panel)txtPassword.Parent).Children.Remove(element);
                    break;
                }
            }
            txtPassword.Visibility = Visibility.Visible;
        }

        private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                DataTable dt = userData.Search(txtSearch.Text);
                dgvUsers.ItemsSource = dt.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка поиска: {ex.Message}", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            // Логика при закрытии окна
        }
    }
}