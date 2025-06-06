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
        private readonly DealerCustomerData _dealerCustomerData = new DealerCustomerData();
        private readonly ProductData _productData = new ProductData();
        private readonly TransactionData _transactionData = new TransactionData();
        private readonly TransactionDetailsData _transactionDetailsData = new TransactionDetailsData();

        private readonly DataTable _addedProducts = new DataTable();
        private decimal _subTotal = 0;
        private readonly int _loggedUserId = 1; // Временное значение, должно заменяться реальным ID
        private readonly string _loggedUserName = "admin"; // Временное значение, должно заменяться реальным именем

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
            _addedProducts.Columns.Add("ProductID", typeof(int));
            _addedProducts.Columns.Add("Name", typeof(string));
            _addedProducts.Columns.Add("Rate", typeof(decimal));
            _addedProducts.Columns.Add("Quantity", typeof(decimal));
            _addedProducts.Columns.Add("Total", typeof(decimal));
            dgvAddedProducts.ItemsSource = _addedProducts.DefaultView;
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
        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                var dealerCustomer = _dealerCustomerData.SearchDealerCustomerForTransaction(txtSearch.Text);
                txtName.Text = dealerCustomer.Name;
                txtEmail.Text = dealerCustomer.Email;
                txtContact.Text = dealerCustomer.Contact;
                txtAddress.Text = dealerCustomer.Address;
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
        }

        /// <summary>
        /// Обработчик поиска товаров.
        /// </summary>
        private void txtSearchProduct_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                var product = _productData.GetProductsForTransaction(txtSearchProduct.Text);
                txtProductName.Text = product.Name;
                txtInventory.Text = product.Quantity.ToString();
                txtRate.Text = product.Rate.ToString();
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
        }

        /// <summary>
        /// Обработчик добавления товара в список.
        /// </summary>
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!ValidateProductAddition())
                    return;

                AddProductToTransaction();
                CalculateSubTotal();
                ClearProductFields();
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
        }

        /// <summary>
        /// Проверяет возможность добавления товара в транзакцию.
        /// </summary>
        private bool ValidateProductAddition()
        {
            if (decimal.Parse(txtQty.Text) > decimal.Parse(txtInventory.Text))
            {
                ShowErrorMessage("Недостаточно товара на складе!");
                return false;
            }
            return true;
        }

        /// <summary>
        /// Добавляет товар в текущую транзакцию.
        /// </summary>
        private void AddProductToTransaction()
        {
            var newRow = _addedProducts.NewRow();
            newRow["ProductID"] = _productData.GetProductIDFromName(txtProductName.Text).Id;
            newRow["Name"] = txtProductName.Text;
            newRow["Rate"] = decimal.Parse(txtRate.Text);
            newRow["Quantity"] = decimal.Parse(txtQty.Text);
            newRow["Total"] = decimal.Parse(txtRate.Text) * decimal.Parse(txtQty.Text);
            _addedProducts.Rows.Add(newRow);
        }

        /// <summary>
        /// Рассчитывает промежуточную сумму транзакции.
        /// </summary>
        private void CalculateSubTotal()
        {
            _subTotal = _addedProducts.AsEnumerable()
                .Sum(row => row.Field<decimal>("Total"));

            txtSubTotal.Text = _subTotal.ToString("N2");
            CalculateGrandTotal();
        }

        /// <summary>
        /// Рассчитывает итоговую сумму с учетом скидки и налогов.
        /// </summary>
        private void CalculateGrandTotal()
        {
            decimal discount = GetDecimalValue(txtDiscount.Text);
            decimal tax = GetDecimalValue(txtVat.Text);

            decimal grandTotal = _subTotal;
            grandTotal -= (grandTotal * discount / 100);
            grandTotal += (grandTotal * tax / 100);

            txtGrandTotal.Text = grandTotal.ToString("N2");
        }

        /// <summary>
        /// Преобразует текст в decimal значение.
        /// </summary>
        private decimal GetDecimalValue(string text)
        {
            return string.IsNullOrEmpty(text) ? 0 : decimal.Parse(text);
        }

        /// <summary>
        /// Обработчик изменения скидки.
        /// </summary>
        private void txtDiscount_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (IsNumericInput(txtDiscount.Text))
                CalculateGrandTotal();
        }

        /// <summary>
        /// Обработчик изменения налога.
        /// </summary>
        private void txtVat_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (IsNumericInput(txtVat.Text))
                CalculateGrandTotal();
        }

        /// <summary>
        /// Обработчик изменения суммы оплаты.
        /// </summary>
        private void txtPaidAmount_TextChanged(object sender, TextChangedEventArgs e)
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
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!ValidateTransaction())
                    return;

                var transaction = CreateTransaction();
                bool success = _transactionData.InsertTransaction(transaction, out int transactionId);

                if (success)
                {
                    SaveTransactionDetails(transaction);
                    ShowSuccessMessage("Транзакция успешно сохранена!");
                    ResetForm();
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Ошибка сохранения: {ex.Message}");
            }
        }

        /// <summary>
        /// Проверяет валидность данных транзакции.
        /// </summary>
        private bool ValidateTransaction()
        {
            if (_addedProducts.Rows.Count == 0)
            {
                ShowErrorMessage("Добавьте хотя бы один товар!");
                return false;
            }
            return true;
        }

        /// <summary>
        /// Создает объект транзакции на основе введенных данных.
        /// </summary>
        private Transaction CreateTransaction()
        {
            return new Transaction
            {
                Type = txtType.Text,
                DealerCustomerId = _dealerCustomerData.GetDeaCustIDFromName(txtName.Text).Id,
                Description = "Транзакция продажи/покупки",
                GrandTotal = decimal.Parse(txtGrandTotal.Text),
                TransactionDate = dtpBillDate.SelectedDate.Value,
                Tax = decimal.Parse(txtVat.Text),
                Discount = decimal.Parse(txtDiscount.Text),
                PaidAmount = decimal.Parse(txtPaidAmount.Text),
                ReturnAmount = decimal.Parse(txtReturnAmount.Text),
                AddedBy = _loggedUserId,
                AddedByName = _loggedUserName,
                TransactionDetails = _addedProducts
            };
        }

        /// <summary>
        /// Сохраняет детали транзакции и обновляет количество товаров.
        /// </summary>
        private void SaveTransactionDetails(Transaction transaction)
        {
            foreach (DataRow row in _addedProducts.Rows)
            {
                var detail = new TransactionDetails
                {
                    ProductId = (int)row["ProductID"],
                    Rate = (decimal)row["Rate"],
                    Quantity = (decimal)row["Quantity"],
                    Total = (decimal)row["Total"],
                    DealerCustomerId = transaction.DealerCustomerId,
                    AddedDate = DateTime.Now,
                    AddedBy = _loggedUserId,
                    AddedByName = _loggedUserName
                };

                _transactionDetailsData.InsertTransactionDetail(detail);
                _productData.DecreaseProduct(detail.ProductId, detail.Quantity);
            }
        }

        /// <summary>
        /// Сбрасывает форму к начальному состоянию.
        /// </summary>
        private void ResetForm()
        {
            _addedProducts.Rows.Clear();
            _subTotal = 0;
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
        private void ShowErrorMessage(string message)
        {
            MessageBox.Show(message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        /// <summary>
        /// Показывает сообщение об успешном выполнении.
        /// </summary>
        private void ShowSuccessMessage(string message)
        {
            MessageBox.Show(message, "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}