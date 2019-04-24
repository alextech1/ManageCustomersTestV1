using System;
using System.Collections.Generic;
using System.Windows.Forms;

using Microsoft.Win32;
using ClassProject2.Data;
using System.Configuration;

namespace ClassProject2.Winforms
{
    public partial class MainForm : Form
    {
        #region Constructor

        public MainForm()
        {
            InitializeComponent();
        }
        #endregion

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            var connString = ConfigurationManager.ConnectionStrings["Database"];

            _database = new Database(connString.ConnectionString);

            BindGrid(false);            
        }

        private void miFileExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void miHelpAbout_Click(object sender, EventArgs e)
        {
            var dlg = new AboutDialog();

            dlg.ShowDialog(this);
        }

        private void miProductAdd_Click(object sender, EventArgs e)
        {
            var dlg = new ProductForm();
            dlg.Database = _database;

            dlg.ShowDialog(this);
            BindGrid(true);
        }

        private void miProductsCategories_Click(object sender, EventArgs e)
        {
            var form = new OrderForm();
            form.Products = _database.Products;
            form.ShowDialog();
        }

        #region Private Members

        private Database _database;
        #endregion

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var grid = sender as DataGridView;
            var product = GetSelectedProduct(grid, e.RowIndex);

            var form = new ProductForm();
            form.Database = _database;
            form.Product = product;

            form.ShowDialog(this);
            BindGrid(true);
        }

        private void BindGrid ( bool refresh )
        {
            productBindingSource.DataSource = _database.Products.GetAll();

            if (refresh)
            {
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = productBindingSource;
            };
        }

        private Product GetSelectedProduct ( DataGridView grid, int rowIndex )
        {
            if (rowIndex < 0 || rowIndex >= grid.Rows.Count)
                return null;

            var row = grid.Rows[rowIndex];
            return row.DataBoundItem as Product;
        }
    }
}
