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
        int oldWidth;
        int oldHeight;
        public Form1()
        {
            oldHeight = this.Height;
            oldWidth = this.Width;

            ResizeEnd += Form1_ResizeEnd;

            InitializeComponent();
            Paused = true;
            panel1.Paint += Form1_Paint;
            button1.Click += button1_Click;

            panel1.Click += panel1_Click;

            //Toad
            /*cells.Add(new Cell(40,80));
            cells.Add(new Cell(40,40));
            cells.Add(new Cell(40, 120));

            cells.Add(new Cell(80, 80));
            cells.Add(new Cell(80, 120));
            cells.Add(new Cell(80, 160));*/

            //Glider
            cells.Add(new Cell(80, 80));
            cells.Add(new Cell(100, 80));
            cells.Add(new Cell(120, 80));
            cells.Add(new Cell(120, 60));
            cells.Add(new Cell(100, 40));

            foreach (var cell in cells)
            {
                cell.X += 20;
                cell.Y += 20;
            }

            tick = new Timer();
            tick.Tick += new EventHandler(logic);
            tick.Interval = 250;    
            tick.Stop();
        }

        void Form1_ResizeEnd(object sender, EventArgs e)
        {
            
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
                Cell newCell = new Cell(MousePosition.X - (MousePosition.X - panel1.Left - this.Left - 10) % Cell.Size - panel1.Left - this.Left - 10, MousePosition.Y - (MousePosition.Y - panel1.Top - this.Top - 30) % Cell.Size - panel1.Top - this.Top - 30);

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
            var g = e.Graphics;
            Pen myPen = new Pen(System.Drawing.Color.Red, 4);
            foreach (var cell in cells)
            {
                if (true)
                {
                    g.FillRectangle(new SolidBrush(Color.PaleVioletRed), new Rectangle(cell.X, cell.Y, Cell.Size, Cell.Size));
                    cell.Drew = true;
                }
            }
        }
    }
}
