using AspWebFormsWebshop.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entity = AspWebFormsWebshop.Repository.Entites;


namespace AspWebFormsWebshop.Pages {
    public partial class Coffee : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            FillPage();
        }

        private void FillPage() {
            var type = "%";
            if (IsPostBack) {
                type = _DropDownListType.SelectedValue;
            }

            var list = ConnectionClass.GetCoffeeByType(type);
            StringBuilder sb = new StringBuilder();

            foreach (var coffee in list) {
                sb.Append(
                    string.Format(
                        @"<table class='coffeeTable'>
                            <tr>
                                <th rowspan='6' width='150px'><img runat='server' src='{6}' /></th>
                                <th width='50px'>Name: </td>
                                <td>{0}</td>
                            </tr>

                            <tr>
                                <th>Type: </th>
                                <td>{1}</td>
                            </tr>

                            <tr>
                                <th>Price: </th>
                                <td>{2} $</td>
                            </tr>

                            <tr>
                                <th>Roast: </th>
                                <td>{3}</td>
                            </tr>

                            <tr>
                                <th>Origin: </th>
                                <td>{4}</td>
                            </tr>

                            <tr>
                                <td colspan='2'>{5}</td>
                            </tr>           
            
                           </table>", 
                                coffee.Name, 
                                coffee.Type, 
                                coffee.Price, 
                                coffee.Roast, 
                                coffee.Country, 
                                coffee.Review, 
                                coffee.Image));
            }
            
            lblOutput.Text = sb.ToString();
        }

        protected void _DropDownListType_SelectedIndexChanged(object sender, EventArgs e) {
            FillPage();
        }
    }
}