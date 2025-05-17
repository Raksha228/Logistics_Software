using Backend.BusinesLogic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Interfaces
{
    public interface ICrudUser
    {
        DataTable Select();

        bool Insert(User insertedUser);

        bool Update(User updatedUser);

        bool Delete(User deletedUser);

        DataTable Search(string keywords);
    }
}
