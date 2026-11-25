using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Main
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        Manage_People frm = new Manage_People();
        private void peopleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(frm.IsDisposed)
            {
                frm = new Manage_People();
            }
            frm.MdiParent = this;
            frm.Show();
            frm.Activate();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }
    }
}
