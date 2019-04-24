using ClassProject2.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nile.Windows
{
    public partial class ProductForm : Form
    {
        DataAccess da = new DataAccess();
        public bool editMode = false;
        public bool deleteMode = false;
        public string id = "";
        public string Name_ = "";
        public string Price = "";
        private Action updateProducts;
        public ProductForm(Action updateProducts)
        {
            InitializeComponent();
            this.updateProducts = updateProducts; //Used to refresh ProductsList
        }

        public Database Database { get; set; }
        public Product Product { get; set; }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (Product != null)
            {
                txtName.Text = Product.Name;
                txtPrice.Text = Product.UnitPrice.ToString();
                chkDiscontinued.Checked = Product.IsDiscontinued;
            }

            ValidateChildren();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;

            Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateChildren())
                return;

            try
            {
                if (editMode == false)
                {
                    da.CommandText = "INSERT INTO Products(Name,UnitPrice,Discontinued) VALUES (@Name, @Price,@Disc)";
                    da.OpenDBConnection();
                    da.CreateCommandObject();
                    da.Parameters("@Name", txtName.Text);
                    da.Parameters("@Price", Convert.ToDecimal(txtPrice.Text));
                    da.Parameters("@Disc", chkDiscontinued.Checked);
                    da.ExecuteNonQuery();
                    da.CloseConnection();

                    DialogResult result = MessageBox.Show("Product has been added!", "Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (result == DialogResult.OK)
                    {
                        Close();
                    }
                }
                else
                {
                    da.CommandText = "UPDATE Products SET Name = @Name, UnitPrice = @Price, Discontinued = @Disc WHERE Id = @Id";
                    da.OpenDBConnection();
                    da.CreateCommandObject();
                    da.Parameters("@Id", id);
                    da.Parameters("@Name", txtName.Text);
                    da.Parameters("@Price", Convert.ToDecimal(txtPrice.Text));
                    da.Parameters("@Disc", chkDiscontinued.Checked);
                    da.ExecuteNonQuery();
                    da.CloseConnection();

                    DialogResult result = MessageBox.Show("Information has been updated!", "Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (result == DialogResult.OK)
                    {
                        Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Save Failed",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                updateProducts();
            }
        }

        private void deleteBtn_Click(object sender, EventArgs e)
        {
            if (!ValidateChildren())
                return;

            try
            {
                if (deleteMode == true)
                {
                    DialogResult result = MessageBox.Show("Delete canceled", "Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (result == DialogResult.OK)
                    {
                        Close();
                    }
                }
                else if (deleteMode == false)
                {
                    da.CommandText = "DELETE FROM Products WHERE Id = @Id";
                    da.OpenDBConnection();
                    da.CreateCommandObject();
                    da.Parameters("@Id", id);
                    da.Parameters("@Name", txtName.Text);
                    da.Parameters("@Price", Convert.ToDecimal(txtPrice.Text));
                    da.Parameters("@Disc", chkDiscontinued.Checked);
                    da.ExecuteNonQuery();
                    da.CloseConnection();

                    DialogResult result = MessageBox.Show("Delete has been updated!", "Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (result == DialogResult.OK)
                    {
                        Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Save Failed",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                updateProducts();
            }
        }

        private int GetId()
        {
            throw new NotImplementedException();
        }

        private void txtName_Validating(object sender, CancelEventArgs e)
        {
            if (String.IsNullOrEmpty(txtName.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtName, "Name is required");
            }
            else
            {
                errorProvider1.SetError(txtName, "");
            };
        }

        private void txtPrice_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (txtPrice.Text == "" || (Convert.ToInt32(txtPrice.Text) <= 0))
                {
                    e.Cancel = true;
                    errorProvider1.SetError(txtPrice, "Price must be > 0");
                }
                else
                {
                    errorProvider1.SetError(txtPrice, "");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Save Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private int GetPrice()
        {
            //throw new NotImplementedException();
            return 1;
        }

        private void ProductForm_Load(object sender, EventArgs e)
        {
            if (editMode == false) //Determine if in editMode
            {
                GetMaxId();
            }            
            else
            {
                txtId.Text = id;
                txtName.Text = Name_;
                txtPrice.Text = Price;
            }
        }
        private void GetMaxId()
        {
            da.CommandText = "SELECT MAX(id)+1 FROM Products";
            da.OpenDBConnection();
            da.CreateCommandObject();
            txtId.Text = da.FillDataTable().Rows[0][0].ToString();
            if (txtId.Text == "")
            {
                txtId.Text = "1";
            }
            da.CloseConnection();
        }

        
    }
}


