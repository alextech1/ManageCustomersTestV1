using ClassProject2.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nile.Windows
{
    public partial class CustomerForm : Form
    {
        DataAccess da = new DataAccess();
        public bool editMode = false;
        public string id = "";
        public string FName = "";
        public string LName = "";
        public string PhNumber = "";
        private Action updateCustomers;

        public Database Database { get; set; }
        public SelectedCustomer Customer { get; set; }

        
        public CustomerForm(Action updateCustomers)
        {
            InitializeComponent();
            this.updateCustomers = updateCustomers; //Used to refresh CustomersList
        }

        
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            //Init the UI
            if (Customer != null)
            {
                txtFirstName.Text = Customer.ToString(); //text box Design name
                txtLastName.Text = Customer.ToString();
                txtPhoneNumber.Text = Customer.ToString();
            };

            ValidateChildren();

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            

            if (!ValidateChildren())
                return;

            var customer = new Customer()
            {
                FirstName = txtFirstName.Text,
                LastName = txtLastName.Text,
                PhoneNumber = txtPhoneNumber.Text

            };

            try
            {
                if (editMode == false)
                {
                    da.CommandText = "INSERT INTO Customers(FIrstName,LastName,PhoneNumber) VALUES(@FName, @LName, @PhNumber)";
                    da.OpenDBConnection();
                    da.CreateCommandObject();
                    da.Parameters("@FName", txtFirstName.Text);
                    da.Parameters("@LName", txtLastName.Text);
                    da.Parameters("@PhNumber", txtPhoneNumber.Text);
                    da.ExecuteNonQuery();
                    da.CloseConnection();

                    DialogResult result = MessageBox.Show("Customer has been added!", "Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (result == DialogResult.OK)
                    {
                        Close();
                    }
                }
                else
                {
                    da.CommandText = "UPDATE Customers SET FIrstName = @FName, LastName = @LName, PhoneNumber = @PhNumber WHERE Id = @Id";
                    da.OpenDBConnection();
                    da.CreateCommandObject();
                    da.Parameters("@Id", id);
                    da.Parameters("@FName", txtFirstName.Text);
                    da.Parameters("@LName", txtLastName.Text);
                    da.Parameters("@PhNumber", txtPhoneNumber.Text);
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
                updateCustomers();
            }
        }
        private void txtFirstName_Validating(object sender, CancelEventArgs e)
        {
            if (String.IsNullOrEmpty(txtFirstName.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFirstName, "First Name is required");
            }
            else
            {
                errorProvider1.SetError(txtFirstName, "");
            };
        }


        


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void txtFirstName_Validating_1(object sender, CancelEventArgs e)
        {
            if (String.IsNullOrEmpty(txtFirstName.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFirstName, "First Name is required");
            }
            else
            {
                errorProvider1.SetError(txtFirstName, "");
            }
        }

        private void txtLastName_Validating(object sender, CancelEventArgs e)
        {
            if (String.IsNullOrEmpty(txtLastName.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtLastName, "Last Name is required");
            }
            else
            {
                errorProvider1.SetError(txtLastName, "");
            }
        }

        private void CustomerForm_Load(object sender, EventArgs e)
        {
            if (editMode == false)
            {
                GetId();
            }
            else
            {
                textId.Text = id;
                txtFirstName.Text = FName;
                txtLastName.Text = LName;
                txtPhoneNumber.Text = PhNumber;
            }
        }
        private void GetId()
        {
            da.CommandText = "SELECT MAX(id)+1 FROM Customers";
            da.OpenDBConnection();
            da.CreateCommandObject();
            textId.Text = da.FillDataTable().Rows[0][0].ToString();
            if (textId.Text == "")
            {
                textId.Text = "1";
            }
            da.CloseConnection();
        }
    }

}
