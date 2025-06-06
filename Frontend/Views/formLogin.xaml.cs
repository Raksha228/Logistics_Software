using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Backend.BusinessLogic;
using Backend.DataAccess;

namespace Frontend.Views
{
    /// <summary>
    /// Окно авторизации пользователей в системе.
    /// Проверяет учетные данные и предоставляет доступ в зависимости от типа пользователя.
    /// </summary>
    public partial class formLogin : Window
    {
        Login login = new Login();
        LoginData loginData = new LoginData();


        /// <summary>
        /// Статическое поле для хранения логина авторизованного пользователя
        /// </summary>
        public static string loggedIn;

        /// <summary>
        /// Инициализирует новый экземпляр окна авторизации.
        /// </summary>
        public formLogin()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Обработчик нажатия кнопки закрытия окна.
        /// </summary>
        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Обработчик перемещения окна при зажатии левой кнопки мыши.
        /// </summary>
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        /// <summary>
        /// Обработчик нажатия кнопки входа в систему.
        /// Проверяет учетные данные и авторизует пользователя.
        /// </summary>
        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                login.Username = txtUsername.Text.Trim();
                login.Password = txtPassword.Password.Trim();
                login.UserType = cmbUserType.Text.Trim();

                //Проверка, есть ли такой пользователь с такими правами
                bool sucess = loginData.loginCheck(login);



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


                if (sucess == true)
                {
                    // Определяем какое окно открывать в зависимости от типа пользователя
                    if (login.UserType == "Admin")
                    {
                        formAdminDashboard adminDashboard = new formAdminDashboard(login);
                        adminDashboard.Show();
                    }
                    else
                    {
                        formUserDashboard userDashboard = new formUserDashboard(login);
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

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Закрываем приложение при закрытии окна авторизации
            //Application.Current.Shutdown();
        }

        /// <summary>
        /// Проверяет корректность введенных данных.
        /// </summary>
        /// <returns>True если данные валидны, иначе False</returns>


        /// <summary>
        /// Открывает соответствующее окно в зависимости от типа пользователя.
        /// </summary>


        /// <summary>
        /// Показывает сообщение об ошибке авторизации.
        /// </summary>


        /// <summary>
        /// Показывает стандартное сообщение об ошибке.
        /// </summary>
        /// <param name="message">Текст сообщения</param>


        /// <summary>
        /// Обработчик изменения выбранного типа пользователя.
        /// </summary>
        private void cmbUserType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}