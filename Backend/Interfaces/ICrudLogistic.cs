using Backend.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Interfaces
{
    public interface ICrudLogistic
    {

        DataTable Select();

        bool Insert(Logistic logistic);

        bool Update(Logistic logistic);

        bool Delete(Logistic logistic);

        DataTable Search(string keywords);

        Product GetProductsForLogistic(string keyword);
    }
}
