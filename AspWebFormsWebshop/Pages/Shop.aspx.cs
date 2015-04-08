using AspWebFormsWebshop.Repository;
using Entite = AspWebFormsWebshop.Repository.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AspWebFormsWebshop.Repository.Entites;
using System.Text;

namespace AspWebFormsWebshop.Pages {
    public partial class Shop : System.Web.UI.Page {

        private User user = null;
        
        protected void Page_Load(object sender, EventArgs e) {
            user = (User)Session["user"];

            if (user == null)
                Response.Redirect("/Pages/Account/Login.aspx");

            GenerateControls();
        }

        protected void btnOk_Click(object sender, EventArgs e) {
            SendOrder();

            lblResult.Text = "Ваш заказ был размещен, спасибо за покупки в нашем магазине";
            btnOk.Visible = false;
            btnCancel.Visible = false;
        }

        protected void btnCancel_Click(object sender, EventArgs e) {
            Session["orders"] = null;
            btnOk.Visible = false;
            btnCancel.Visible = false;
            lblResult.Visible = false;
        }

        protected void btnOrder_Click(object sender, EventArgs e) {
            GenerateReview();
        }

        private void GenerateReview() {
            double totalAmount = 0;
            var orderList = GetOrders();
            Session["orders"] = orderList;

            StringBuilder sb = new StringBuilder();
            sb.Append("<table>");
            sb.Append("<h3>Please review your order</h3>");

            //Generate a row for each Order
            foreach (Order order in orderList) {
                double totalRow = order.Price * order.Amount;
                sb.Append(String.Format(@"<tr>
                                            <td width = '50px'>{0} X </td>
                                            <td width = '200px'>{1} ({2})</td>
                                            <td>{3}</td><td>$</td>
                                        </tr>", 
                                              order.Amount, 
                                              order.Product, 
                                              order.Price, 
                                              String.Format("{0:0.00}", 
                                              totalRow)));

                totalAmount = totalAmount + totalRow;
            }

            //Generate row for Total Amount
            sb.Append(String.Format(@"<tr>
                                        <td><b>Total: </b></td>
                                        <td><b>{0} $ </b></td>
                                      </tr>", totalAmount));
            sb.Append("</table>");

            //Export data and make Controls visible
            lblResult.Text = sb.ToString();
            lblResult.Visible = true;
            btnOk.Visible = true;
            btnCancel.Visible = true;
        }

        private void SendOrder() {
            var orderList = (IList<Order>)Session["orders"];
            ConnectionClass.AddOrders(orderList);
            Session["orders"] = null;
        }

        //Fill page with dynamic controls showing products in database
        private void GenerateControls() {
            //Get all coffeeObjects from database
            IList<Entite.Coffee> coffeeList = ConnectionClass.GetCoffeeByType("%");

            foreach (var coffee in coffeeList) {
                //Create Controls
                Panel coffeePanel = new Panel();
                Image image = new Image { ImageUrl = coffee.Image, CssClass = "ProductsImage" };
                Literal literal = new Literal { Text = "<br />" };
                Literal literal2 = new Literal { Text = "<br />" };
                Label lblName = new Label { Text = coffee.Name, CssClass = "ProductsName" };
                Label lblPrice = new Label {
                    Text = String.Format("{0:0.00}", coffee.Price + "<br />"),
                    CssClass = "ProductsPrice"
                };
                TextBox textBox = new TextBox {
                    ID = coffee.Id.ToString(),
                    CssClass = "ProductsTextBox",
                    Width = 60,
                    Text = "0"
                };

                //Add validation so only numbers can be entered into the textfields
                RegularExpressionValidator regex = new RegularExpressionValidator {
                    ValidationExpression = "^[0-9]*",
                    ControlToValidate = textBox.ID,
                    ErrorMessage = "Please enter a number."
                };

                //Add controls to Panels
                coffeePanel.Controls.Add(image);
                coffeePanel.Controls.Add(literal);
                coffeePanel.Controls.Add(lblName);
                coffeePanel.Controls.Add(literal2);
                coffeePanel.Controls.Add(lblPrice);
                coffeePanel.Controls.Add(textBox);
                coffeePanel.Controls.Add(regex);

                pnlProducts.Controls.Add(coffeePanel);
            }
        }

        private IList<Order> GetOrders() {
            //Get list of Textbox objects in ContentPlaceHolder
            ContentPlaceHolder cph = (ContentPlaceHolder)Master.FindControl("ContentPlaceHolder1");
            ControlFinder<TextBox> cf = new ControlFinder<TextBox>();
            cf.FindChildControlsRecursive(cph);
            var textBoxList = cf.FoundControls;

            //Create orders using data from textfields
            List<Order> orderList = new List<Order>();

            foreach (TextBox textBox in textBoxList) {
                //Make sure textbox.Text is not null
                if (textBox.Text != "") {
                    int amountOfOrders = Convert.ToInt32(textBox.Text);

                    //Generate Order for each textbox which has an order greater than 0
                    if (amountOfOrders > 0) {
                        Entite.Coffee coffee = ConnectionClass.GetCoffeeById(Convert.ToInt32(textBox.ID));
                        Order order = new Order(
                            user.Name, 
                            coffee.Name, 
                            amountOfOrders, 
                            coffee.Price, 
                            DateTime.Now, 
                            false);

                        //Add order to ArrayList
                        orderList.Add(order);
                    }
                }
            }
            return orderList;
        }
    }
}