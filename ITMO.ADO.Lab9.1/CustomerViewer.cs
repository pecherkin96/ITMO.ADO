﻿using System;
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
        SampleContext context = new SampleContext();
        
        
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
                    FirstName = this.textBoxname.Text,
                    LastName = this.textBoxlastname.Text,
                    Email = this.textBoxmail.Text,
                    Age = Int32.Parse(this.textBoxage.Text),
                    Photo = Ph
                };
                context.Customers.Add(customer);
                context.SaveChanges();
                Output();
                textBoxname.Text = String.Empty;
                textBoxlastname.Text = String.Empty;
                textBoxmail.Text = String.Empty;
                textBoxage.Text = String.Empty;
                customer.Orders = orderlistBox.SelectedItems.OfType<Order>().ToList();
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
        private void Output()
        {
            if (this.CustomerradioButton.Checked == true)
                GridView.DataSource = context.Customers.ToList();
            else if (this.OrderradioButton.Checked == true)
                GridView.DataSource = context.Orders.ToList();
        }

        private void buttonOut_Click(object sender, EventArgs e)
        {
            Output();
            var query = from b in context.Customers
                        orderby b.FirstName
                        select b;

        }

        private void customerList_MouseClick(object sender, MouseEventArgs e)
        {
            var query = from b in context.Customers
                        orderby b.FirstName
                        select b;
            customerList.DataSource = query.ToList();
        }

        private void CustomerViewer_Load(object sender, EventArgs e)
        {
            context.Orders.Add(new Order { ProductName = "Аудио", Quantity = 12, PurchaseDate = DateTime.Parse("12.01.2016") });
            context.Orders.Add(new Order { ProductName = "Видео", Quantity = 22, PurchaseDate = DateTime.Parse("10.01.2016") });
            context.SaveChanges();
            orderlistBox.DataSource = context.Orders.ToList();
        }

        private void GridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (GridView.CurrentRow == null) return;
            var customer = GridView.CurrentRow.DataBoundItem as Customer;
            if (customer == null) return;
            labelid.Text = Convert.ToString(customer.CustomerId);
            textBoxCustomer.Text = customer.ToString();

            textBoxname.Text = customer.FirstName;
            textBoxlastname.Text = customer.LastName;
            textBoxmail.Text = customer.Email;
            textBoxage.Text = Convert.ToString(customer.Age);

        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (labelid.Text == String.Empty) return;

            var id = Convert.ToInt32(labelid.Text);
            var customer = context.Customers.Find(id);
            if (customer == null) return;

            customer.FirstName = this.textBoxname.Text;
            customer.LastName = this.textBoxlastname.Text;
            customer.Email = this.textBoxmail.Text;
            customer.Age = Int32.Parse(this.textBoxage.Text);

            context.Entry(customer).State = EntityState.Modified;

            context.SaveChanges();
            Output();

        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            if (labelid.Text == String.Empty) return;

            var id = Convert.ToInt32(labelid.Text);
            var customer = context.Customers.Find(id);

            context.Entry(customer).State = EntityState.Deleted;
            context.SaveChanges();
            Output();

        }
    }
}
