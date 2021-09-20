using System;
using System.Collections.Generic;
using System.Text;

namespace BestBuyBestPractices
{
    interface IDepartmentRepository
    {

        IEnumerable<Department> GetAllDepartments();



    }
}
