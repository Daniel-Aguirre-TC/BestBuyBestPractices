using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace BestBuyBestPractices
{
    class DapperProductRepository : IProductRepository
    {
        private readonly IDbConnection _connection;

        public DapperProductRepository(IDbConnection connection)
        {
            _connection = connection;
        }


        public void CreateProduct(string name, double price, int categoryID)
        {
            _connection.Execute("INSERT INTO Products (Name, Price, CategoryID) VALUES (@Name, @Price, @CategoryID)",
                new {name, price, categoryID });
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _connection.Query<Product>("SELECT * FROM Products;");
        }


        public IEnumerable<Product> GetSpecificProduct(string name)
        {
            return _connection.Query<Product>("SELECT * FROM Products WHERE Name = @Name;", new { name } );
        }

        public void UpdateProduct(string name, double price)
        {
            _connection.Execute("UPDATE Products SET price = @Price WHERE name = @Name",
                new { name, price, });
        }

        public void DeleteProduct(int productID)
        {

            _connection.Execute("DELETE FROM reviews WHERE ProductID = @productID;",
                new { productID });

            _connection.Execute("DELETE FROM sales WHERE ProductID = @productID;",
                new { productID });

            _connection.Execute("DELETE FROM products WHERE ProductID = @productID;",
                new { productID });

        }

    }
}
