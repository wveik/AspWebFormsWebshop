using AspWebFormsWebshop.Repository.Entites;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AspWebFormsWebshop.Repository {
    public static class ConnectionClass {
        private static string _connectionString;

        static ConnectionClass() {
            _connectionString = ConfigurationManager.ConnectionStrings["CoffeeConnection"].ToString();
        }

        public static IList<Coffee> GetCoffeeByType(string coffeeType) {
            List<Coffee> result = new List<Coffee>();
            string query = string.Format(@"
                            SELECT 
                                [id]
                              ,[name]
                              ,[type]
                              ,[price]
                              ,[roast]
                              ,[country]
                              ,[image]
                              ,[review]
                          FROM [dbo].[coffee]
                            WHERE type LIKE '{0}'
                        ", coffeeType);

            try {
                using (var conn = new SqlConnection(_connectionString)) {
                    using (var command = new SqlCommand(query, conn)) {
                        conn.Open();

                        using (SqlDataReader reader = command.ExecuteReader()) {
                            if (reader.HasRows) {
                                while (reader.Read()) {

                                    int id = int.Parse(reader["id"].ToString());
                                    string name = reader["name"].ToString();
                                    string type = reader["type"].ToString();
                                    double price = double.Parse(reader["price"].ToString());
                                    string roast = reader["roast"].ToString();
                                    string country = reader["country"].ToString();
                                    string image = reader["image"].ToString();
                                    string review = reader["review"].ToString();

                                    Coffee coffee = new Coffee(id, name, type, price, roast, country, image, review);
                                    result.Add(coffee);
                                }
                            }
                        }
                    }
                }
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }

            return result;
        }
    }
}