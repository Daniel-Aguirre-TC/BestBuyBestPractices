using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace BestBuyBestPractices
{
    class DapperDepartmentRepository : IDepartmentRepository
    {

        private readonly IDbConnection _connection;

        public DapperDepartmentRepository(IDbConnection connection)
        {
            _connection = connection;
        }


        public IEnumerable<Department> GetAllDepartments()
        {
            // send query to select all from Departments
            return _connection.Query<Department>("SELECT * FROM Departments;").ToList();
        }


        public void InsertDepartment(string newDepartmentName)
        {
            // Execute will execute a parameterizied query. the @departmentName is the parameter, which is assigned using the  new {departmentName = newDeparmentName}. 
            _connection.Execute("INSERT INTO DEPARTMENTS (Name) VALUES (@departmentName);",
                new { departmentName = newDepartmentName });
        }

    }
}
