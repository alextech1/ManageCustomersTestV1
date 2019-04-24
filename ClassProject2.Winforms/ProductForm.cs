using ClassProject2.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClassProject2.Winforms
{
    public partial class ProductForm : Form
    {
        public ProductForm()
        {
            InitializeComponent();
        }

        public Database Database { get; set; }
        public Product Product { get; set; }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            //Init the UI
            if (Product != null)
            {
                txtId.Text = Product.Id.ToString();
                txtName.Text = Product.Name;
                txtPrice.Text = Product.UnitPrice.ToString();
                cbDiscontinued.Checked = Product.IsDiscontinued;
            };

            ValidateChildren();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateChildren())
                return;

            var product = new Product(GetId())
            {
                Name = txtName.Text,
                UnitPrice = GetPrice(),
                IsDiscontinued = cbDiscontinued.Checked
            };

            try
            {
                if (product.Id == 0)
                    Database.Products.Add(product);
                else
                    Database.Products.Update(product);

                Product = product;
                DialogResult = DialogResult.OK;
                Close();
            } catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Save Failed", 
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            };
        }

        private void txtName_Validating(object sender, CancelEventArgs e)
        {
            if (String.IsNullOrEmpty(txtName.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtName, "Name is required");
            } else
            {
                errorProvider1.SetError(txtName, "");
            };
        }

        private void txtPrice_Validating(object sender, CancelEventArgs e)
        {
            if (GetPrice() <= 0)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtPrice, "Price must be > 0");
            }
            else
            {
                errorProvider1.SetError(txtPrice, "");
            };
        }

        private int GetId ()
        {
            return Product?.Id ?? 0;
        }

        private decimal GetPrice()
        {
            var text = txtPrice.Text;

            decimal value;
            if (Decimal.TryParse(text, out value))
                return value;

            return 0;
        }
    }
}
