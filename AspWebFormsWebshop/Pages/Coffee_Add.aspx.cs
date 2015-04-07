using AspWebFormsWebshop.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AspWebFormsWebshop.Pages {
    public partial class Coffee_Add : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            ShowImages();
        }

        private void ShowImages() {
            //Получаем все файлы
            string[] images = Directory.GetFiles(Server.MapPath(@"\Images\Coffee\"));

            for (int i = 0; i < images.Length; i++) {
                ddlImage.Items.Insert(i, Path.GetFileName(images[i]));
            }
        }

        private void ClearTextFields() {
            txtCountry.Text = "";
            txtName.Text = "";
            txtPrice.Text = "";
            txtReview.Text = "";
            txtRoast.Text = "";
            txtType.Text = "";
        }

        protected void btnUploadImage_Click(object sender, EventArgs e) {
            try {
                string fileName = Path.GetFileName(FileUpload1.FileName);
                FileUpload1.SaveAs(Server.MapPath(@"\Images\Coffee\") + fileName);
                lblResult.Text = string.Format("Картинка {0} загрузилась", fileName);
                Page_Load(sender, e);
            } catch (Exception ex) {
                lblResult.Text = "Загрузка оборвалась";
            }
        }

        protected void btnSave_Click(object sender, EventArgs e) {
            try {
                string name = txtName.Text;
                string type = txtType.Text;
                double price = Convert.ToDouble(txtPrice.Text);
                price = price / 100;
                string roast = txtRoast.Text;
                string country = txtCountry.Text;
                string image = "../Images/Coffee/" + ddlImage.SelectedValue;
                string review = txtReview.Text;

                var coffee = new AspWebFormsWebshop.Repository.Entites.Coffee(-1, name, type, price, roast, country, image, review);
                ConnectionClass.AddCoffee(coffee);
                lblResult.Text = "Upload succesful!";
                ClearTextFields();
            } catch (Exception) {
                lblResult.Text = "Upload failed!";
            }
        }
    }
}