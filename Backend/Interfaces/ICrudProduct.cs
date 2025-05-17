using Backend.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Interfaces
{
    public interface ICrudProduct
    {
        DataTable Select();

        bool Insert(Product insertedProduct);

        bool Update(Product updatedProduct);

        bool Delete(Product deletedProduct);

        DataTable Search(string keywords);
    }
}
