using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Pong
{
    public partial class Form1 : Form
    {
        double x;
        double y;
        double speed = 1;
        bool Paused;
        Timer tick;
        public List<Cell> cells = new List<Cell>();
        public Form1()
        {
            InitializeComponent();
            Paused = true;
            panel1.Paint += Form1_Paint;
            button1.Click += button1_Click;

            //MouseClick += Form1_MouseClick;
            panel1.Click += panel1_Click;

            //Toad
            /*cells.Add(new Cell(40,80));
            cells.Add(new Cell(40,40));
            cells.Add(new Cell(40, 120));

            cells.Add(new Cell(80, 80));
            cells.Add(new Cell(80, 120));
            cells.Add(new Cell(80, 160));*/

            //Glider
            cells.Add(new Cell(160, 160));
            cells.Add(new Cell(200, 160));
            cells.Add(new Cell(240, 160));
            cells.Add(new Cell(240, 120));
            cells.Add(new Cell(200, 80));

            tick = new Timer();
            tick.Tick += new EventHandler(logic);
            tick.Interval = 250;
            //tick.Enabled = true;            
            tick.Stop();
        }

        void panel1_Click(object sender, EventArgs e)
        {
            AddCells();
        }

        void button1_Click(object sender, EventArgs e)
        {
            switch (Paused)
            {
                case false:
                    tick.Stop();
                    button1.Text = "Start";
                    Paused = true;
                    break;

                case true:
                    tick.Start();
                    button1.Text = "Pause";
                    Paused = false;
                    break;
            }
        }

        public void AddCells()
        {
            if (Paused)
            {
                Cell newCell = new Cell(MousePosition.X - (MousePosition.X - panel1.Left - this.Left - 10) % 40 - panel1.Left - this.Left - 10, MousePosition.Y - (MousePosition.Y - panel1.Top - this.Top - 30) % 40 - panel1.Top - this.Top - 30);

                if (cells.Contains(newCell))
                {
                    cells.Remove(newCell);
                }
                else
                {
                    cells.Add(newCell);
                }
                panel1.Refresh();
            }
        }

        public void logic(object sender, EventArgs e)
        {
            cells = Board.Iterate(cells);
            panel1.Refresh();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            x += speed;
            //var graphicsObj = this.CreateGraphics();
            var g = e.Graphics;
            Pen myPen = new Pen(System.Drawing.Color.Red, 4);
            foreach (var cell in cells)
            {
                if (true)
                {
                    g.DrawRectangle(myPen, new Rectangle(cell.X, cell.Y, cell.Size, cell.Size));
                    cell.Drew = true;
                }
            }
        }
    }
}
