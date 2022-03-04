namespace Test1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            SetSize();
        }
        
        private bool isMouse = false;
        private ArrayPoints arrayPoints = new ArrayPoints(2);
        private bool drawPen = true;
        private bool drawLine = false;
        private bool drawRectangle = false;
        private bool drawEllipse = false;
        private bool fillFigure = false;

        Bitmap map = new Bitmap(100, 100);
        Graphics graphics;

        Pen pen = new Pen(Color.Black,3f);
        private Color c = Color.Black;
        private int trackBarValue = 3;
        private void SetSize()
        {
            Rectangle rectangle = Screen.PrimaryScreen.Bounds;
            map = new Bitmap(rectangle.Width, rectangle.Height);
            graphics = Graphics.FromImage(map);

            pen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
            pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
        }
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            isMouse = true;
            arrayPoints.SetPoint(e.X, e.Y);
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            isMouse = false;
            arrayPoints.SetPoint(e.X, e.Y);
            var width = Math.Abs(arrayPoints.GetPoints()[0].X - arrayPoints.GetPoints()[1].X);
            var height = Math.Abs(arrayPoints.GetPoints()[0].Y - arrayPoints.GetPoints()[1].Y);
            int x = Math.Min(arrayPoints.GetPoints()[0].X, arrayPoints.GetPoints()[1].X);
            int y = Math.Min(arrayPoints.GetPoints()[0].Y, arrayPoints.GetPoints()[1].Y);
            SolidBrush sb = new SolidBrush(pen.Color);
            if (drawLine)
            {
                graphics.DrawLine(pen, arrayPoints.GetPoints()[0], arrayPoints.GetPoints()[1]);
            }
            if (drawRectangle)
            {
                if (fillFigure)
                {
                    graphics.FillRectangle(sb, x, y, width, height);
                }
                else
                    graphics.DrawRectangle(pen, x, y, width, height);
            }
            if (drawEllipse)
            {
                if (fillFigure)
                {
                    graphics.FillEllipse(sb, x, y, width, height);
                }
                else
                    graphics.DrawEllipse(pen, x, y, width, height);
            }
            pictureBox1.Image = map;
            arrayPoints.ResetPoints();
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (!isMouse) {return;}
            if (drawPen)
            {
                arrayPoints.SetPoint(e.X, e.Y);
                if (arrayPoints.GetCountPoints() >= 2)
                {
                    graphics.DrawLines(pen, arrayPoints.GetPoints());
                    pictureBox1.Image = map;
                    arrayPoints.SetPoint(e.X, e.Y);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            pen.Color = ((Button)sender).BackColor;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if(colorDialog1.ShowDialog() == DialogResult.OK)
            {
                pen.Color = colorDialog1.Color;
                ((Button)sender).BackColor = colorDialog1.Color;
            }
        }

        private void стеретьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            graphics.Clear(pictureBox1.BackColor);
            pictureBox1.Image = map;
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            pen.Width = trackBar1.Value;
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Title = "Сохранить картинку как...";
            saveFileDialog1.Filter = "Изображение|*.jpg";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    pictureBox1.Image.Save(saveFileDialog1.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                }
                catch
                {
                    MessageBox.Show("Невозможно сохранить изображение", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                DialogResult res = MessageBox.Show("Сохранить изменения?",
            "Сообщение",
            MessageBoxButtons.YesNoCancel,
            MessageBoxIcon.Information,
            MessageBoxDefaultButton.Button1,
            MessageBoxOptions.DefaultDesktopOnly);
                if (res == DialogResult.Yes)
                    сохранитьToolStripMenuItem_Click(sender, e);
                if (res == DialogResult.Cancel)
                {
                    e.Cancel = true;
                    TopMost = true; 
                }
            
            }
        }
        private void даToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fillFigure = !fillFigure;
            if (fillFigure)
                даToolStripMenuItem.Text = "Отключить";
            else
                даToolStripMenuItem.Text = "Включить";
        }

        private void button11_Click(object sender, EventArgs e)
        {
            drawLine = false;
            drawPen = false;
            (drawRectangle,drawEllipse) = (true,false);
            trackBar1.Value = trackBarValue;
            pen.Color = c;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            drawLine = false;
            drawPen = false;
            (drawEllipse,drawRectangle)= (true,false);
            trackBar1.Value = trackBarValue;
            pen.Color = c;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            drawPen = true;
            drawLine = false;
            (drawEllipse, drawRectangle) = (false, false);
            trackBar1.Value = trackBarValue;
            pen.Color = c;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            drawLine = true;
            drawPen = false;
            (drawEllipse, drawRectangle) = (false, false);
            trackBar1.Value = trackBarValue;
            pen.Color = c;
        }

        private void button14_Click(object sender, EventArgs e)
        {
            c = pen.Color;
            pen.Color = pictureBox1.BackColor;
            drawPen = true;
            drawLine = false;
            (drawEllipse, drawRectangle) = (false, false);
            trackBarValue = trackBar1.Value;
            trackBar1.Value = 25;
        }
    }
}