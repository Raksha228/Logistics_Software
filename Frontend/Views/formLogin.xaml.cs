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
using System;
using Backend.Interfaces;
using Backend.BusinessLogic;
using Backend.DataAccess;
using Backend.Utils;
using Microsoft.Extensions.Configuration;

namespace Frontend.Views
{
    public partial class formLogin : Window
    {
        public formLogin()
        {
            InitializeComponent();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Создаем объект Login с данными из формы
                Login login = new Login()
                {
                    Username = txtUsername.Text.Trim(),
                    Password = txtPassword.Password,
                    UserType = (cmbUserType.SelectedItem as ComboBoxItem)?.Content.ToString()
                };

                // Проверяем введенные данные
                if (string.IsNullOrEmpty(login.Username))
                {
                    MessageBox.Show("Введите логин", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (string.IsNullOrEmpty(login.Password))
                {
                    MessageBox.Show("Введите пароль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (string.IsNullOrEmpty(login.UserType))
                {
                    MessageBox.Show("Выберите тип пользователя", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Проверяем учетные данные через DataAccess
                LoginData loginData = new LoginData();
                bool isSuccess = loginData.loginCheck(login);

                if (isSuccess)
                {
                    // Определяем какое окно открывать в зависимости от типа пользователя
                    if (login.UserType == "Администратор")
                    {
                        formAdminDashboard adminDashboard = new formAdminDashboard(login);
                        adminDashboard.Show();
                    }
                    else
                    {
                        formUserDashboard userDashboard = new formUserDashboard();
                        userDashboard.Show();
                    }

                    this.Close();
                }
                else
                {
                    MessageBox.Show("Неверный логин, пароль или тип пользователя", "Ошибка авторизации",
                                  MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка",
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CheckPass_Checked(object sender, RoutedEventArgs e)
        {
            // Показываем пароль
            txtPassword.Visibility = Visibility.Collapsed;
            var visiblePasswordBox = new TextBox
            {
                Text = txtPassword.Password,
                FontSize = 14,
                FontWeight = FontWeights.SemiBold,
                Margin = new Thickness(20, 0, 20, 5),
                Height = 30
            };
            Grid.SetRow(visiblePasswordBox, 4);
            ((Grid)this.Content).Children.Add(visiblePasswordBox);
        }

        private void CheckPass_Unchecked(object sender, RoutedEventArgs e)
        {
            // Скрываем пароль
            foreach (UIElement element in ((Grid)this.Content).Children)
            {
                if (element is TextBox && Grid.GetRow(element) == 4)
                {
                    txtPassword.Password = ((TextBox)element).Text;
                    ((Grid)this.Content).Children.Remove(element);
                    break;
                }
            }
            txtPassword.Visibility = Visibility.Visible;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Закрываем приложение при закрытии окна авторизации
            Application.Current.Shutdown();
        }
    }
}