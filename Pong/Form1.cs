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
            panel1.Click += panel1_Click;
            button1.Click += button1_Click;
            button2.Click += button2_Click;
            button3.Click += button3_Click;

            populate();

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

        void button3_Click(object sender, EventArgs e)
        {
            cells.Clear();
            panel1.Refresh();
        }

        void button2_Click(object sender, EventArgs e)
        {
            KeyValuePair<Automata, String> auto = (KeyValuePair<Automata, String>)listBox1.SelectedItem;

            int x;
            int y;
            Int32.TryParse(textBox1.Text, out x);
            Int32.TryParse(textBox2.Text, out y);

            foreach (var cell in auto.Key)
            {
                Cell newCell = new Cell(cell.X + x - x % Cell.Size,
                    cell.Y + y - y % Cell.Size);
                cells.Add(newCell);
            }
            panel1.Refresh();

        }

        public void populate()
        {
            Automata blinker = new Automata();
            blinker.Name = "Blinker";
            blinker.AddRange(new List<Cell>()
            {
                new Cell(20, 40),
                new Cell(20, 20),
                new Cell(20, 60)
            });

            Automata glider = new Automata();
            glider.Name = "Crawler";
            glider.AddRange(new List<Cell>()
            {
                new Cell(80, 80),
                new Cell(100, 80),
                new Cell(120, 80),
                new Cell(120, 60),
                new Cell(100, 40)
            });

            Automata beacon = new Automata();
            beacon.Name = "Beacon";
            beacon.AddRange(new List<Cell>()
            {
                new Cell(20,20),
                new Cell(20,40),
                new Cell(40,20),
                new Cell(80,60),
                new Cell(60,80),
                new Cell(80,80)
            });

            Automata block = new Automata();
            block.Name = "Block";
            block.AddRange(new List<Cell>()
            {
                new Cell(20,20),
                new Cell(20,40),
                new Cell(40,20),
                new Cell(40,40)
            });

            listBox1.Items.Add(new KeyValuePair<Automata, String>(block, block.Name));
            listBox1.Items.Add(new KeyValuePair<Automata,String>(blinker,blinker.Name));
            listBox1.Items.Add(new KeyValuePair<Automata, String>(glider, glider.Name));
            listBox1.Items.Add(new KeyValuePair<Automata, String>(beacon, beacon.Name));
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
