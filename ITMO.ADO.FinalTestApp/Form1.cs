using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ITMO.ADO.FinalTestApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = finalTestAppDataSet1.MainTable;
            sqlDataAdapter1.Fill(finalTestAppDataSet1.MainTable);
        }

        private FinalTestAppDataSet.MainTableRow GetSelectedRow()
        {
            string SelectedId = dataGridView1.CurrentRow.Cells["Id"].Value.ToString();
            int ValueId = Convert.ToInt32(SelectedId);
            FinalTestAppDataSet.MainTableRow SelectedRow =
            finalTestAppDataSet1.MainTable.FindById(ValueId);
            return SelectedRow;
        }


        private void UpdateButton_Click(object sender, EventArgs e)
        {
            sqlDataAdapter1.Update(finalTestAppDataSet1.MainTable);
        }

        private void DeleteRowButton_Click(object sender, EventArgs e)
        {
            GetSelectedRow().Delete();
        }
    }
}
