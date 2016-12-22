using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }


        private void Form1_Load(object sender, EventArgs e)
        // using a Monte Carlo simulation to estimate the value of Pi
        {
            const int width = 500;
            const int height = 500;
            Bitmap bmp = new Bitmap(width, height);

            const int K = 1000;
            const long num_trial = 500 * K;
            const double radius = 1.0;
            int in_count = 0;  // in circle

            Random rand = new Random();
            double x = 0.0;
            double y = 0.0;

            for (long i = 0; i < num_trial; i++)
            {
                int xsign = rand.Next(2);
                int ysign = rand.Next(2);
                xsign = xsign == 0 ? -1 : xsign;
                ysign = ysign == 0 ? -1 : ysign;

                x = rand.NextDouble() * xsign;
                y = rand.NextDouble() * ysign;

                int x_pos = width / 2 + (int)(x * width / 2);
                int y_pos = height / 2 + (int)(y * height / 2);


                if (inCircle(x, y, radius))
                {
                    in_count++;
                    bmp.SetPixel(x_pos, y_pos, Color.Orange);
                }
                else
                    bmp.SetPixel(x_pos, y_pos, Color.CadetBlue);

                if (i % 100000 == 0)
                    Console.Write('.');
            }

            double pi_est = (4.0 * in_count) / num_trial;
            pictureBox1.Image = bmp;
            string msg = string.Format("   Estimated value of Pi is {0:f6}" +
                                       "\nbased on {1:n0} number of trials." +
                                       "\n            Difference {2:f6}", 
                                       pi_est, num_trial, Math.PI - pi_est);

            // set title
            string new_title = string.Format("Estimated value of Pi is {0:f8}" +
                                             "  (diff {1:f8})", pi_est, Math.PI - pi_est);
            this.Text = new_title;       

            //MessageBox.Show(msg);

            Font myFont = new Font("Arial", 12, FontStyle.Regular);

            Graphics g = Graphics.FromImage(bmp);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            TextRenderer.DrawText(g, msg, myFont, new Point(120, height / 2), Color.Black);
        }


        static bool inCircle(double x, double y, double r)
        // is the point x, y inside the circle with radius r
        {
            return (x * x) + (y * y) <= (r * r);
        }

    }
}
