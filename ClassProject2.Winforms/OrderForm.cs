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

namespace ClassProject2.Winforms
{
    public partial class OrderForm : Form
    {
        public OrderForm()
        {
            InitializeComponent();
        }

        public Products Products { get; set; }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            //Bind products to combo
            var items = Products.GetAll();
            
            _bs.DataSource = items;
            _bs.Sort = "Name";
            cbProducts.DataSource = _bs;
            //cbProducts.Items.Clear();
            //cbProducts.Items.AddRange(items.ToArray());

            UpdateUI();
        }

        private BindingSource _bs = new BindingSource();

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var form = new ProductForm();

            if (form.ShowDialog() == DialogResult.OK)
            {
                _bs.Add(form.Product);
            };
        }

        private void UpdateUI ()
        {
            var cb = cbProducts;
            //label1.Text = cb.SelectedText;
            if (cb.SelectedIndex >= 0)
                label1.Text = cb.Items[cb.SelectedIndex].ToString();
            else
                label1.Text = "";
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cbProducts_SelectedIndexChanged(object sender, EventArgs e)
        {
            var cb = sender as ComboBox;

            UpdateUI();
        }        
    }
}
