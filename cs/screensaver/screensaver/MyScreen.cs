using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace screensaver
{
    public partial class MyScreen : Form
    {
        private readonly int id;

        public MyScreen(int _id)
        {
            InitializeComponent();
            id = _id;
            KeyPress += new KeyPressEventHandler(MyScreen_OnKeyPress);
        }

        private void MyScreen_Load(object sender, EventArgs e)
        {
            Bounds = Screen.AllScreens[id].Bounds;
            Cursor.Hide();
            TopMost = true;
        }

        private void MyScreen_OnKeyPress(object sender, KeyPressEventArgs e)
        {
            Close();
        }
    }
}
