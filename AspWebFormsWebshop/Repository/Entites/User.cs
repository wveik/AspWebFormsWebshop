using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AspWebFormsWebshop.Repository.Entites {
    public class User {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Type { get; set; }

        public User(int id, string name, string password, string email, string type) {
            Id = id;
            Name = name;
            Password = password;
            Email = email;
            Type = type;
        }

        public User(string name, string password, string email, string type) {
            Name = name;
            Password = password;
            Email = email;
            Type = type;
        }

        public static User GetUser(SqlDataReader reader) {
            User result = new User(
                    int.Parse(reader["id"].ToString()),
                    reader["name"].ToString(),
                    reader["password"].ToString(),
                    reader["email"].ToString(),
                    reader["user_type"].ToString()
            );

            return result;
        }
    }
}