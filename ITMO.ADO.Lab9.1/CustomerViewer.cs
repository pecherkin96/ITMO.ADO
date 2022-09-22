using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using CodeFirst;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ITMO.ADO.Lab9._1
{
    public partial class CustomerViewer : Form
    {
        
        byte[] Ph;
        SampleContext context;
        public CustomerViewer()
        {
            InitializeComponent();
        }
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            try
            {
                Customer customer = new Customer
                {
                    Name = this.textBoxname.Text,
                    Email = this.textBoxmail.Text,
                    Age = Int32.Parse(this.textBoxage.Text),
                    Photo = Ph
                };
                context.Customers.Add(customer);
                context.SaveChanges();
                textBoxname.Text = String.Empty;
                textBoxmail.Text = String.Empty;
                textBoxage.Text = String.Empty;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.ToString());
            }
        }

        private void buttonFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog diag = new OpenFileDialog();
            if (diag.ShowDialog() == DialogResult.OK)
            {
                Image bm = new Bitmap(diag.OpenFile());

                ImageConverter converter = new ImageConverter();
                Ph = (byte[])converter.ConvertTo(bm, typeof(byte[]));
            }

        }
    }

}

