using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Backend.BusinessLogic;
using Backend.DataAccess;

namespace Frontend.Views
{
    /// <summary>
    /// Окно управления доставками товаров.
    /// Позволяет просматривать, фильтровать и отмечать доставки как выполненные.
    /// </summary>
    public partial class formDelivery : Window
    {
        private readonly LogisticData logisticData;
        private readonly PersonalLogisticData personalLogisticData;

        /// <summary>
        /// Инициализирует новый экземпляр окна управления доставками.
        /// </summary>
        public formDelivery()
        {
            InitializeComponent();
            logisticData = new LogisticData();
            personalLogisticData = new PersonalLogisticData();
        }

        /// <summary>
        /// Обработчик загрузки окна. Загружает список доставок и инициализирует элементы управления.
        /// </summary>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadLogistics();
            cmbDate.SelectedIndex = 0;
        }

        /// <summary>
        /// Загружает список всех доставок из базы данных.
        /// </summary>
        private void LoadLogistics()
        {
            try
            {
                DataTable dt = logisticData.Select();
                dgvLogistic.ItemsSource = dt.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Обработчик изменения выбора в комбобоксе даты.
        /// Фильтрует доставки по выбранному типу (Закупка/Продажа).
        /// </summary>
        private void cmbDate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                string type = ((ComboBoxItem)cmbDate.SelectedItem).Content.ToString() == "Закупка" ? "Purchase" : "Sales";
                DataTable dt = personalLogisticData.DisplayLogisticnByDate(type, "");
                dgvLogistic.ItemsSource = dt.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Обработчик нажатия кнопки "Все".
        /// Сбрасывает фильтры и показывает все доставки.
        /// </summary>
        private void btnAll_Click(object sender, RoutedEventArgs e)
        {
            LoadLogistics();
        }

        /// <summary>
        /// Обработчик нажатия кнопки "Доставлено".
        /// Помечает выбранную доставку как выполненную.
        /// </summary>
        private void btnDelivered_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgvLogistic.SelectedItem != null)
                {
                    DataRowView row = (DataRowView)dgvLogistic.SelectedItem;
                    Logistic logistic = new Logistic
                    {
                        Id = Convert.ToInt32(row["id"]),
                        Empleyee = row["employee"].ToString(),
                        FirstNameEmployee = row["first_name_employee"].ToString(),
                        LastNameEmployee = row["last_name_employee"].ToString(),
                        Address = row["address"].ToString(),
                        Contact = row["contact"].ToString(),
                        Date = row["date"].ToString(),
                        Description = row["description"].ToString(),
                        Price = Convert.ToDecimal(row["price"])
                    };

                    if (logisticData.Delete(logistic))
                    {
                        MessageBox.Show("Доставка отмечена как выполненная", "Успех",
                            MessageBoxButton.OK, MessageBoxImage.Information);
                        LoadLogistics();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Обработчик двойного клика по списку доставок.
        /// Показывает информацию о выбранной доставке.
        /// </summary>
        private void dgvLogistic_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (dgvLogistic.SelectedItem != null)
            {
                DataRowView row = (DataRowView)dgvLogistic.SelectedItem;
                // Логика обработки двойного клика (например, открытие деталей)
                MessageBox.Show($"Выбрана доставка ID: {row["id"]}", "Информация",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}