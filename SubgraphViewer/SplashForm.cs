using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SubgraphViewer
{
    public partial class SplashForm : Form
    {
        public SplashForm()
        {
            InitializeComponent();
        }

        public SplashForm(string msg)
        {
            InitializeComponent();
            this.ClientSize = new Size(ViewerConfig.SplashFormWidth, ViewerConfig.SplashFormHeight);
            this.FormBorderStyle = FormBorderStyle.None;
            this.ShowInTaskbar = false;
            this.TopMost = true;
            this.StartPosition = FormStartPosition.CenterScreen;

            label1.Text = msg;

            progressBar1.Name = msg;
            progressBar1.Location = new Point(0, 20);
            progressBar1.Style = ProgressBarStyle.Marquee;
            progressBar1.Width = ViewerConfig.SplashFormWidth;
            progressBar1.Height = ViewerConfig.SplashFormHeight;

        }
    }
}
