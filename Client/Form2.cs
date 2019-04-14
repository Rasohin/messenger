using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void buttonConn_Click(object sender, EventArgs e)
        {
            ConnectionServer.servername = textServerName.Text;
            ConnectionServer.newusername = textUserName.Text;
            Close();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            textServerName.Text = ConnectionServer.servername;
        }
    }
}
