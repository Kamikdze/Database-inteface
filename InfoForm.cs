using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DB_Interface_lab_7
{
    public partial class InfoForm : Form
    {
        public InfoForm()
        {
            InitializeComponent();
            label1.Parent = pictureBox2;
            label1.BackColor = Color.Transparent;
            label2.Parent = pictureBox2;
            label2.BackColor = Color.Transparent;
            pictureBox1.Parent = pictureBox2;
            pictureBox1.BackColor = Color.Transparent;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
