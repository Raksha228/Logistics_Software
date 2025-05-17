using Backend.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Interfaces
{
    public interface ICrudDealerCustomer
    {
        DataTable Select();

        bool Insert(DealerCustomer insertedDealerCustomer);

        bool Update(DealerCustomer updatedDealerCustomer);

        bool Delete(DealerCustomer deletedDealerCustomer);

        DataTable Search(string keyword);
    }
}
