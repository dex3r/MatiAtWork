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
    public partial class MainForm : Form
    {
        public ClientController Controller;

        public MainForm()
        {
            InitializeComponent();
            Controller = new ClientController();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Controller.StartService();
        }
    }
}
