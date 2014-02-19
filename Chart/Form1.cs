using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Chart
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            label3.Text = "Ready";
        }
        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void CreateImage(string date1, string date2)
        {
            ReadData rd = new ReadData(date1, date2);

            if (rd.isempty == false)
            {
                List<Rate> rates = new List<Rate>();
                foreach (Rate rate in rd.rates)
                {
                    rates.Add(rate);
                }

                int count = rates.Count;


                int height = 395, width = 600;
                Pen linepen = new Pen(Color.Black, 1);
                Pen gridpen = new Pen(Color.DarkBlue);

                System.Drawing.Bitmap image = new System.Drawing.Bitmap(width, height);
                Graphics g = Graphics.FromImage(image);
                //background color
                g.Clear(Color.White);

                Font font1 = new System.Drawing.Font("Arial", 12, FontStyle.Regular);
                Font font2 = new System.Drawing.Font("Arial", 9, FontStyle.Regular);

                System.Drawing.Drawing2D.LinearGradientBrush brush = new System.Drawing.Drawing2D.LinearGradientBrush(new Rectangle(0, 0, image.Width, image.Height), Color.Blue, Color.Blue, 1.2f, true);
                g.FillRectangle(Brushes.LightGray, 0, 0, width, height);
                //Border
                g.DrawRectangle(new Pen(Color.DarkGray), 0, 0, width - 1, height - 1);


                //Title
                StringFormat sf = new StringFormat();
                sf.Alignment = StringAlignment.Center;
                sf.LineAlignment = StringAlignment.Near;
                g.DrawString("EURUSD Daily From " + rates[0].date.ToShortDateString() + " to " + rates[rates.Count - 1].date.ToShortDateString(), font1, Brushes.SaddleBrown, panel1.ClientRectangle, sf);

                //Grid
                //Vertical
                int x = 40;
                for (int i = 0; i < 9; i++)
                {
                    g.DrawLine(gridpen, x, 40, x, 360);
                    x = x + 64;
                }

                //Horizental
                int y = 40;
                for (int i = 0; i < 6; i++)
                {
                    g.DrawLine(gridpen, 40, y, 552, y);
                    y = y + 64;
                }

                int[] Count1 = new int[12];
                int[] Count2 = new int[12];
                string[] NumChr = new string[12];

                //x axis
                x = 38;
                for (int i = 0; i < 8; i++)
                {
                    g.DrawString(rates[i * (count - 1) / 8].date.ToShortDateString(), font2, Brushes.Red, x, 360);
                    x = x + 64;
                }

                //y axis
                y = 350;
                for (int i = 0; i < 6; i++)
                {
                    g.DrawString((rd.min + (rd.max - rd.min) * i / 5).ToString("F4"),
                                    font2, Brushes.Red, 0, y);
                    y = y - 64;
                }

                //line
                Point[] points = new Point[count];
                for (int i = 0; i < count; i++)
                {
                    points[i].X = i * 512 / count + 40;
                    points[i].Y = 360 - (int)((rates[i].popen - rd.min) / (rd.max - rd.min) * 320) ;
                }

                g.DrawLines(linepen, points);

                this.panel1.BackgroundImage = image;

                label3.Text = "Ready";
            }
            else
            {
                label3.Text = "No available data";
                this.panel1.Controls.Clear();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string date1 = dateTimePicker1.Value.ToString("yyyy-MM-dd");
            string date2 = dateTimePicker2.Value.ToString("yyyy-MM-dd");

            this.CreateImage(date1, date2);
        }



    }

}
