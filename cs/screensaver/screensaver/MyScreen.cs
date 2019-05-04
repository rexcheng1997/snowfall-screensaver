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
        private readonly int NUM = 50;
        private readonly int id;
        private Gravity grv = new Gravity();
        private List<Snowball> snowballs = new List<Snowball>();

        public MyScreen(int _id)
        {
            InitializeComponent();
            id = _id;
            Paint += new PaintEventHandler(MyScreen_Render);
            KeyPress += new KeyPressEventHandler(MyScreen_OnKeyPress);

            for (int i = 0; i < NUM; i++)
            {
                snowballs.Add(new Snowball(id));
            }
        }

        private void MyScreen_Load(object sender, EventArgs e)
        {
            Bounds = Screen.AllScreens[id].Bounds;
            Cursor.Hide();
            TopMost = true;
        }

        private void MyScreen_Render(object sender, PaintEventArgs pe)
        {
            Graphics g = pe.Graphics;
            foreach (Snowball sb in snowballs)
            {
                sb.Draw(g);
            }
        }

        private void MyScreen_OnKeyPress(object sender, KeyPressEventArgs ke)
        {
            Close();
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            foreach (Snowball sb in snowballs)
            {
                sb.Update_Position(grv.Get_Vector());
            }
            Invalidate();
        }
    }
}
