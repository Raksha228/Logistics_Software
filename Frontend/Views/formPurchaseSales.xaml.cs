using System;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Backend.BusinessLogic;
using Backend.DataAccess;
using Backend.Interfaces;

namespace Frontend.Views
{
    /// <summary>
    /// Окно для обработки операций покупки и продажи товаров.
    /// Позволяет создавать транзакции, управлять списком товаров и рассчитывать итоговые суммы.
    /// </summary>
    public partial class formPurchaseSales : Window
    {
        // Изменяем получение ID и имени пользователя
        private int _loggedUserId = 1; //((MainWindow)Application.Current.MainWindow).LoggedUserId;
        private string _loggedUserName = "admin"; //((MainWindow)Application.Current.MainWindow).LoggedUserName;



        private DealerCustomerData dealerCustomerData = new DealerCustomerData();
        private ProductData productData = new ProductData();
        private TransactionData transactionData = new TransactionData();
        private TransactionDetailsData transactionDetailsData = new TransactionDetailsData();

        private DataTable addedProducts = new DataTable();
        private decimal subTotal = 0;

        /// <summary>
        /// Инициализирует новый экземпляр окна покупки/продажи.
        /// </summary>
        public formPurchaseSales()
        {
            InitializeComponent();
            InitializeDataTables();
            LoadDefaultValues();
        }

        /// <summary>
        /// Инициализирует таблицы данных для хранения информации о товарах.
        /// </summary>
        private void InitializeDataTables()
        {
            addedProducts.Columns.Add("ProductID", typeof(int));
            addedProducts.Columns.Add("Name", typeof(string));
            addedProducts.Columns.Add("Rate", typeof(decimal));
            addedProducts.Columns.Add("Quantity", typeof(decimal));
            addedProducts.Columns.Add("Total", typeof(decimal));
            dgvAddedProducts.ItemsSource = addedProducts.DefaultView;
        }

        /// <summary>
        /// Загружает начальные значения в элементы управления.
        /// </summary>
        private void LoadDefaultValues()
        {
            dtpBillDate.SelectedDate = DateTime.Now;
            txtSubTotal.Text = "0";
            txtGrandTotal.Text = "0";
            txtReturnAmount.Text = "0";
        }

        /// <summary>
        /// Обработчик поиска дилеров/клиентов.
        /// </summary>
        private void txtSearch_TextChanged(object sender, RoutedEventArgs e)
        {
            try
            {
                var dealerCustomer = dealerCustomerData.SearchDealerCustomerForTransaction(txtSearch.Text);
                txtName.Text = dealerCustomer.Name;
                txtEmail.Text = dealerCustomer.Email;
                txtContact.Text = dealerCustomer.Contact;
                txtAddress.Text = dealerCustomer.Address;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Обработчик поиска товаров.
        /// </summary>
        private void txtSearchProduct_TextChanged(object sender, RoutedEventArgs e)
        {
            try
            {
                var product = productData.GetProductsForTransaction(txtSearchProduct.Text);
                txtProductName.Text = product.Name;
                txtInventory.Text = product.Quantity.ToString();
                txtRate.Text = product.Rate.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Обработчик добавления товара в список.
        /// </summary>
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (decimal.Parse(txtQty.Text) > decimal.Parse(txtInventory.Text))
                {
                    MessageBox.Show("Недостаточно товара на складе!", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                var newRow = addedProducts.NewRow();
                newRow["ProductID"] = productData.GetProductIDFromName(txtProductName.Text).Id;
                newRow["Name"] = txtProductName.Text;
                newRow["Rate"] = decimal.Parse(txtRate.Text);
                newRow["Quantity"] = decimal.Parse(txtQty.Text);
                newRow["Total"] = decimal.Parse(txtRate.Text) * decimal.Parse(txtQty.Text);
                addedProducts.Rows.Add(newRow);

                CalculateSubTotal();
                ClearProductFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Проверяет возможность добавления товара в транзакцию.
        /// </summary>


        /// <summary>
        /// Добавляет товар в текущую транзакцию.
        /// </summary>


        /// <summary>
        /// Рассчитывает промежуточную сумму транзакции.
        /// </summary>
        private void CalculateSubTotal()
        {
            subTotal = addedProducts.AsEnumerable()
                .Sum(row => row.Field<decimal>("Total"));

            txtSubTotal.Text = subTotal.ToString("N2");
            CalculateGrandTotal();
        }

        /// <summary>
        /// Рассчитывает итоговую сумму с учетом скидки и налогов.
        /// </summary>
        private void CalculateGrandTotal()
        {
            decimal discount = string.IsNullOrEmpty(txtDiscount.Text) ? 0 : decimal.Parse(txtDiscount.Text);
            decimal tax = string.IsNullOrEmpty(txtVat.Text) ? 0 : decimal.Parse(txtVat.Text);

            decimal grandTotal = subTotal;
            grandTotal -= (grandTotal * discount / 100);
            grandTotal += (grandTotal * tax / 100);

            txtGrandTotal.Text = grandTotal.ToString("N2");
        }

        /// <summary>
        /// Преобразует текст в decimal значение.
        /// </summary>


        /// <summary>
        /// Обработчик изменения скидки.
        /// </summary>
        private void txtDiscount_TextChanged(object sender, RoutedEventArgs e)
        {
            if (IsNumericInput(txtDiscount.Text))
                CalculateGrandTotal();
        }

        /// <summary>
        /// Обработчик изменения налога.
        /// </summary>
        private void txtVat_TextChanged(object sender, RoutedEventArgs e)
        {
            if (IsNumericInput(txtVat.Text))
                CalculateGrandTotal();
        }

        /// <summary>
        /// Обработчик изменения суммы оплаты.
        /// </summary>
        private void txtPaidAmount_TextChanged(object sender, RoutedEventArgs e)
        {
            if (IsNumericInput(txtPaidAmount.Text) && decimal.TryParse(txtGrandTotal.Text, out decimal grandTotal))
            {
                decimal paidAmount = decimal.Parse(txtPaidAmount.Text);
                txtReturnAmount.Text = (paidAmount - grandTotal).ToString("N2");
            }
        }

        /// <summary>
        /// Проверяет, является ли ввод числовым значением.
        /// </summary>
        private bool IsNumericInput(string input)
        {
            return decimal.TryParse(input, out _);
        }

        /// <summary>
        /// Обработчик сохранения транзакции.
        /// </summary>
        private async void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var transaction = new Transaction
                {
                    Type = txtType.Text,
                    DealerCustomerId = dealerCustomerData.GetDeaCustIDFromName(txtName.Text).Id,
                    Description = "Транзакция продажи/покупки",
                    GrandTotal = decimal.Parse(txtGrandTotal.Text),
                    TransactionDate = dtpBillDate.SelectedDate.Value,
                    Tax = decimal.Parse(txtVat.Text),
                    Discount = decimal.Parse(txtDiscount.Text),
                    PaidAmount = decimal.Parse(txtPaidAmount.Text),
                    ReturnAmount = decimal.Parse(txtReturnAmount.Text),
                    AddedBy = _loggedUserId, //((App)Application.Current).LoggedUserId,
                    AddedByName = _loggedUserName, //((App)Application.Current).LoggedUserName,
                    TransactionDetails = addedProducts
                };

                bool success = transactionData.InsertTransaction(transaction, out int transactionId);

                if (success)
                {
                    foreach (DataRow row in addedProducts.Rows)
                    {
                        var detail = new TransactionDetails
                        {
                            ProductId = (int)row["ProductID"],
                            Rate = (decimal)row["Rate"],
                            Quantity = (decimal)row["Quantity"],
                            Total = (decimal)row["Total"],
                            DealerCustomerId = transaction.DealerCustomerId,
                            AddedDate = DateTime.Now,
                            AddedBy = _loggedUserId, //((App)Application.Current).LoggedUserId,
                            AddedByName = _loggedUserName //((App)Application.Current).LoggedUserName
                        };

                        transactionDetailsData.InsertTransactionDetail(detail);
                        productData.DecreaseProduct(detail.ProductId, detail.Quantity);
                    }

                    MessageBox.Show("Транзакция успешно сохранена!", "Успех",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    ResetForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка сохранения: {ex.Message}", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Проверяет валидность данных транзакции.
        /// </summary>


        /// <summary>
        /// Создает объект транзакции на основе введенных данных.
        /// </summary>


        /// <summary>
        /// Сохраняет детали транзакции и обновляет количество товаров.
        /// </summary>


        /// <summary>
        /// Сбрасывает форму к начальному состоянию.
        /// </summary>
        private void ResetForm()
        {
            addedProducts.Rows.Clear();
            subTotal = 0;
            txtSearch.Text = "";
            txtName.Text = "";
            txtEmail.Text = "";
            txtContact.Text = "";
            txtAddress.Text = "";
            txtSubTotal.Text = "0";
            txtGrandTotal.Text = "0";
            txtReturnAmount.Text = "0";
            txtDiscount.Text = "";
            txtVat.Text = "";
            txtPaidAmount.Text = "";
            ClearProductFields();
        }

        /// <summary>
        /// Очищает поля ввода информации о товаре.
        /// </summary>
        private void ClearProductFields()
        {
            txtSearchProduct.Text = "";
            txtProductName.Text = "";
            txtInventory.Text = "";
            txtRate.Text = "";
            txtQty.Text = "";
        }

        /// <summary>
        /// Показывает сообщение об ошибке.
        /// </summary>


        /// <summary>
        /// Показывает сообщение об успешном выполнении.
        /// </summary>

    }
}