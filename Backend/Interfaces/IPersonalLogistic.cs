using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Interfaces
{
    public interface IPersonalLogistic
    {
        DataTable DisplayLogisticByUsername(string username);


        DataTable DisplayLogisticnByDate(string date, string username);
    }
}
