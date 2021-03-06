﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace screensaver
{
    public partial class MyScreen : Form
    {
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, IntPtr dwNewLong);
        [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool GetClientRect(IntPtr hWnd, out Rectangle lpRect);
        private bool isPreviewMode = false;
        private readonly int NUM = 300;
        private readonly int id;
        private Gravity grv = new Gravity();
        private Wind wind;
        private List<Snowball> snowballs = new List<Snowball>();
        private List<Snowflake> snowflakes = new List<Snowflake>();

        public MyScreen(int _id)
        {
            InitializeComponent();
            id = _id;
            wind = new Wind(Screen.AllScreens[_id].Bounds.Width, Screen.AllScreens[_id].Bounds.Height);
            Paint += new PaintEventHandler(MyScreen_Render);
            MouseMove += new MouseEventHandler(MyScreen_OnMouseMove);
            KeyPress += new KeyPressEventHandler(MyScreen_OnKeyPress);
        }

        public MyScreen(IntPtr previewHandle)
        {
            InitializeComponent();
            SetParent(this.Handle, previewHandle);
            SetWindowLong(this.Handle, -16, new IntPtr(GetWindowLong(this.Handle, -16) | 0x40000000));
            Rectangle ParentRect;
            GetClientRect(previewHandle, out ParentRect);
            this.Size = ParentRect.Size;
            this.Location = new Point(0, 0);
            this.isPreviewMode = true;
            wind = new Wind(ParentRect.Width, ParentRect.Height);
            Paint += new PaintEventHandler(MyScreen_Render);
            MouseMove += new MouseEventHandler(MyScreen_OnMouseMove);
            KeyPress += new KeyPressEventHandler(MyScreen_OnKeyPress);
        }

        private void MyScreen_Load(object sender, EventArgs e)
        {
            Bounds = Screen.AllScreens[id].Bounds;
            Cursor.Hide();
            TopMost = true;
        }

        private void MyScreen_Render(object sender, PaintEventArgs pe)
        {
            if (snowballs.Count() <= NUM)
                snowballs.Add(new Snowball(id));
            Thread.Sleep(1);
            if (snowflakes.Count() <= NUM)
                snowflakes.Add(new Snowflake(id));
            Graphics g = pe.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            foreach (Snowball sb in snowballs)
                sb.Draw(g);
            foreach (Snowflake sf in snowflakes)
                sf.Draw(g);
        }

        private void MyScreen_OnMouseMove(object sender, MouseEventArgs me)
        {
            if (!this.isPreviewMode)
            {
                wind.Blow(new PointF(me.X, me.Y));
            }
        }

        private void MyScreen_OnKeyPress(object sender, KeyPressEventArgs ke)
        {
            if (!this.isPreviewMode)
            {
                Close();
            }
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            foreach (Snowball sb in snowballs)
            {
                sb.Apply_Force(grv.Get_Vector());
                sb.Apply_Force(wind.Get_Vector());
                sb.Update_Position();
            }
            foreach (Snowflake sf in snowflakes)
            {
                sf.Apply_Force(grv.Get_Vector());
                sf.Apply_Force(wind.Get_Vector());
                sf.Update_Position();
            }
            Invalidate();
        }
    }
}
