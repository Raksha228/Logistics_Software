using Backend.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Interfaces
{
    public interface ICrudCategory
    {
        DataTable Select();

        bool Insert(Category insertedCategory);

        bool Update(Category updatedCategory);

        bool Delete(Category deletedCategory);

        DataTable Search(string keywords);
    }
}
