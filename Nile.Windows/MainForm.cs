using ClassProject2.Data;
using System.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Data.OleDb;
using System.Collections;


namespace Nile.Windows
{
    public partial class MainForm : Form
    {
        DataAccess da = new DataAccess();
        bool value;
        public MainForm()
        {
            InitializeComponent();
        }

        public Database Database { get; set; }

        public event DataGridViewCellEventHandler CellContentClick;

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            //var connString = ConfigurationManager.ConnectionStrings["Database"]; 

            //_database = new Database(connString.ConnectionString);

            //BindGrid(false);
        }


        private void miFileExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void miHelpAbout_Click(object sender, EventArgs e)
        {
            var dlg = new AboutForm();
            dlg.ShowDialog(this);
        }

        private void miCustomerAdd_Click(object sender, EventArgs e)
        {
            var dlg = new CustomerForm(CustomersList);
            
            dlg.ShowDialog(this);
            

        }

        private void miProductAdd_Click(object sender, EventArgs e)
        {
            var dlg = new ProductForm(CustomersList);
            //dlg.Database = _database;

            dlg.ShowDialog(this);
        }

        private void BindGrid(bool refresh)
        {
            //productBindingSource.DataSource = _database.Products.GetAll();

            //if (refresh)
            //{
            //    dataGridView1.DataSource = null;
            //    dataGridView1.DataSource = productBindingSource;
            //};
        }

        private Customer GetSelectedCustomer ( DataGridView grid, int rowIndex )
        {
            if (rowIndex < 0 || rowIndex >= grid.Rows.Count)
                return null;

            var row = grid.Rows[rowIndex];
            return row.DataBoundItem as Customer;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            CustomersList();
        }             

        public void CustomersList()
        {
            
            da.CommandText = "SELECT Id, FIrstName, LastName, PhoneNumber FROM Customers ORDER BY Id DESC"; //DataPropertyName must match to SQL
            da.OpenDBConnection();
            da.CreateCommandObject();
            gridCustomers.DataSource = da.FillDataTable();           
        }

        private void gridCustomers_MouseUp(object sender, MouseEventArgs e)
        {
            IsLinkClicked(e);
        }
        private void IsLinkClicked(MouseEventArgs e)
        {
            try
            {
                if (e.Button != MouseButtons.Left) return;
                DataGridView.HitTestInfo Hti = gridCustomers.HitTest(e.X, e.Y);
                if (Hti.Type == DataGridViewHitTestType.Cell)
                {
                    if (gridCustomers.Rows.Count > 0)
                    {
                        gridCustomers.CurrentCell = gridCustomers[Hti.ColumnIndex, Hti.RowIndex];
                    }
                }
                if(Hti.ColumnIndex < 0 || Hti.ColumnIndex > gridCustomers.ColumnCount)
                {
                    value = false;
                }
                var cf = new CustomerForm(CustomersList);
                //CustomerForm cf = new CustomerForm();
                cf.editMode = true;                
                int rowIndex = gridCustomers.CurrentCell.RowIndex;
                cf.id = gridCustomers.Rows[rowIndex].Cells["colId"].Value.ToString();
                cf.FName = gridCustomers.Rows[rowIndex].Cells["colFirstName"].Value.ToString();
                cf.LName = gridCustomers.Rows[rowIndex].Cells["colLastName"].Value.ToString();
                cf.PhNumber = gridCustomers.Rows[rowIndex].Cells["colPhoneNumber"].Value.ToString();
                cf.Show();                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void gridCustomers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            GetSelectedCustomer( gridCustomers, 1);
        }

        private void miViewProducts_Click(object sender, EventArgs e)
        {
            var dlg = new ManageProductsForm();

            dlg.ShowDialog(this);
        }
    }    
}
