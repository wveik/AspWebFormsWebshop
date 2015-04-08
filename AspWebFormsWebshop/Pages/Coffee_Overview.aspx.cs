using AspWebFormsWebshop.Repository.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AspWebFormsWebshop.Pages {
    public partial class Coffee_Overview : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            var user = (User)Session["user"];

            if (user == null)
                Response.Redirect("/Pages/Account/Login.aspx");
        }
    }
}