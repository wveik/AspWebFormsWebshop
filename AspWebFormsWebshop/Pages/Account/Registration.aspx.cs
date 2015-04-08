using AspWebFormsWebshop.Repository;
using AspWebFormsWebshop.Repository.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AspWebFormsWebshop.Pages.Account {
    public partial class Registration : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {

        }

        protected void btnRegister_Click(object sender, EventArgs e) {
            //Create a new user
            User user = new User(txtName.Text, txtPassword.Text, txtEmail.Text, "user");

            //Register the user and return a result message
            if (ConnectionClass.RegisterUser(user))
                lblResult.Text = "Пользователь зарегистрирован";
            else
                lblResult.Text = "Такой пользователь уже зарегестрирован";
        }
    }
}