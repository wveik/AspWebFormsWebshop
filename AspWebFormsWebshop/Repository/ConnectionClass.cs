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

        internal static void AddCoffee(Coffee coffee) {
            string query = string.Format(
            @"INSERT INTO coffee VALUES ('{0}', '{1}', @prices, '{2}', '{3}','{4}', '{5}')",
            coffee.Name, 
            coffee.Type, 
            coffee.Roast, 
            coffee.Country, 
            coffee.Image, 
            coffee.Review);

            try {
                using (var conn = new SqlConnection(_connectionString)) {
                    using (var command = new SqlCommand(query, conn)) {
                        command.Parameters.Add(new SqlParameter("@prices", coffee.Price));
                        conn.Open();

                        command.ExecuteNonQuery();
                    }
                }
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
        }

        #region Users
        public static User LoginUser(string name, string password) {
            //Check if user exists
            string query =
                string.Format("SELECT * FROM dbo.users WHERE name = '{0}' and password = '{1}' "
                , name
                , password);

            User result = null;

            try {
                using (var conn = new SqlConnection(_connectionString)) {
                    using (var command = new SqlCommand(query, conn)) {
                        conn.Open();

                        using (SqlDataReader reader = command.ExecuteReader()) {
                            if (reader.HasRows) {
                                while (reader.Read()) {
                                    result = User.GetUser(reader);
                                    break;
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

        public static User LoginUser(string name) {
            //Check if user exists
            string query =
                string.Format("SELECT * FROM dbo.users WHERE name = '{0}' "
                , name
            );

            User result = null;

            try {
                using (var conn = new SqlConnection(_connectionString)) {
                    using (var command = new SqlCommand(query, conn)) {
                        conn.Open();

                        using (SqlDataReader reader = command.ExecuteReader()) {
                            if (reader.HasRows) {
                                while (reader.Read()) {
                                    result = User.GetUser(reader);
                                    break;
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
        
        public static bool RegisterUser(User user) {
            //Check if user exists

            User result = LoginUser(user.Name);
            if (result != null)
                return false;

            string query = string.Format(
                "INSERT INTO users VALUES ('{0}', '{1}', '{2}', '{3}')", 
                    user.Name, 
                    user.Password,
                    user.Email, 
                    user.Type);

            try {
                using (var conn = new SqlConnection(_connectionString)) {
                    using (var command = new SqlCommand(query, conn)) {
                        conn.Open();

                        command.ExecuteNonQuery();
                    }
                }
            } catch (Exception ex) {
                return false;
            }

            return true;
        }
        #endregion
    }
}