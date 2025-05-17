using Backend.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Interfaces
{
    public interface ITransaction
    {
        DataTable DisplayAllTransactions();

        bool InsertTransaction(Transaction insertedTransaction, out int transactionID);

    }
}
