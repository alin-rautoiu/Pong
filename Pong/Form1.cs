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
        public List<Cell> cells = new List<Cell>();
        int iter = 1;
        public Form1()
        {
            
            InitializeComponent();

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

            Timer tick = new Timer();
            tick.Tick += new EventHandler(logic);
            tick.Interval = 500;
            tick.Start();
            tick.Enabled = true;            
        }

        public void logic(object sender, EventArgs e)
        {
            cells = Board.Iterate(cells);
            this.Refresh();
        }
        /*public void logic(object sender, EventArgs e)
        {
            x += speed;
            this.Refresh();
        }*/

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            x += speed;
            System.Drawing.Graphics graphics;
            var graphicsObj = this.CreateGraphics();
            Pen myPen = new Pen(System.Drawing.Color.Red, 4);
            foreach (var cell in cells)
            {
                if (true)
                {
                    graphicsObj.DrawRectangle(myPen, new Rectangle(cell.X, cell.Y, cell.Size, cell.Size));
                    cell.Drew = true;
                }
            }
        }
    }
}
