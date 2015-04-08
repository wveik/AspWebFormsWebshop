using AspWebFormsWebshop.Repository.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AspWebFormsWebshop.Pages.Account {
    public partial class Admin : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            var user = (User)Session["user"];

            if (user == null || user.Type != "admin")
                Response.Redirect("/Pages/Account/Login.aspx");
        }
    }
}