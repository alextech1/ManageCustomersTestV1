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
    public partial class ManageProductsForm : Form
    {
        DataAccess da = new DataAccess();
        bool value;
        public Database Database { get; set; }

        public ManageProductsForm()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            //var items = Products.GetAll();

            
        }

        private void ManageProductsForm_Load(object sender, EventArgs e)
        {
            CustomersList();
        }
        public void CustomersList()
        {
            da.CommandText = "SELECT Id, Name, Discontinued, UnitPrice FROM Products ORDER BY Id DESC";
            da.OpenDBConnection();
            da.CreateCommandObject();
            gridProducts.DataSource = da.FillDataTable();
        }
        private Product GetSelectedProduct(DataGridView grid, int rowIndex)
        {
            if (rowIndex < 0 || rowIndex >= grid.Rows.Count)
                return null;

            var row = grid.Rows[rowIndex];
            return row.DataBoundItem as Product;
        }
        private void IsLinkClicked(MouseEventArgs e)
        {
            try
            {
                if (e.Button != MouseButtons.Left) return;
                DataGridView.HitTestInfo Hti = gridProducts.HitTest(e.X, e.Y);
                if (Hti.Type == DataGridViewHitTestType.Cell)
                {
                    if (gridProducts.Rows.Count > 0)
                    {
                        gridProducts.CurrentCell = gridProducts[Hti.ColumnIndex, Hti.RowIndex];
                    }
                }
                if (Hti.ColumnIndex < 0 || Hti.ColumnIndex > gridProducts.ColumnCount)
                {
                    value = false;
                }
                ProductForm pf = new ProductForm(CustomersList);
                pf.editMode = true;
                int rowIndex = gridProducts.CurrentCell.RowIndex;
                pf.id = gridProducts.Rows[rowIndex].Cells["colId"].Value.ToString();
                pf.Name_ = gridProducts.Rows[rowIndex].Cells["colName"].Value.ToString();
                pf.Price = gridProducts.Rows[rowIndex].Cells["colPrice"].Value.ToString();
                pf.chkDiscontinued.Checked = Convert.ToBoolean(gridProducts.Rows[rowIndex].Cells["colDiscontinued"].Value.ToString());
                pf.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void gridProducts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            GetSelectedProduct(gridProducts, 1);
        }

        private void gridProducts_MouseUp(object sender, MouseEventArgs e)
        {
            IsLinkClicked(e);
        }

        private void addProductToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dlg = new ProductForm(CustomersList);
            dlg.ShowDialog(this);
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dlg = new AboutForm();
            dlg.ShowDialog(this);
        }
    }
}
