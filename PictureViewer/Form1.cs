using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PictureViewer
{
    public partial class Form1 : Form
    {
        int deg;
        Image gmbrAsli;

        public Form1()
        {
            InitializeComponent();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            
        }

        private void openToolStripButton_Click(object sender, EventArgs e)
        {
            if(openFileDialog1168.ShowDialog() == DialogResult.OK)
            {
                pictureBox1168.Image = Image.FromFile(openFileDialog1168.FileName);
                gmbrAsli = pictureBox1168.Image;
            }
        }

        

       

        private void timerRotateStart(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void timerRotateStop(object sender, EventArgs e)
        {
            timer1.Stop();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            onRotateRight(this,null);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.KeyPreview = true;
            pictureBox1168.MouseWheel += new MouseEventHandler(scrollHandler);
            panel1.MouseWheel -= new MouseEventHandler(scrollHandler);
        }

        private void scrollHandler(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
            {
                onZoomInButtonClicked(this, null);
            }
            else if (e.Delta < 0)
            {
                onZoomOutButtonClicked(this, null);
            }

        }
        private void onRotateRight(object sender, EventArgs e)
        {
            Image flipImage = pictureBox1168.Image;
            flipImage.RotateFlip(RotateFlipType.Rotate90FlipNone);
            if (deg > 270)
            {
                pictureBox1168.Image = gmbrAsli;
                deg = 0;
                return;
            }
            pictureBox1168.Image = flipImage;
            deg += 90;
        }

        private void onRotateLeft(object sender, EventArgs e)
        {
            Image flipImage = pictureBox1168.Image;
            flipImage.RotateFlip(RotateFlipType.Rotate270FlipNone);
            if (deg < 180)
            {
                pictureBox1168.Image = gmbrAsli;
                deg = 0;
                return;
            }
            pictureBox1168.Image = flipImage;
            deg -= 90;
        }

        private void Form1168_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Oemplus)
            {
                onZoomInButtonClicked(this, null);
            }
            if(e.KeyCode == Keys.OemMinus)
            {
                onZoomOutButtonClicked(this, null);
            }
            if(e.KeyCode == Keys.Right)
            {
                onRotateRight(this, null);
            }
            if (e.KeyCode == Keys.Left)
            {
                onRotateLeft(this, null);
            }
        }
        private void onZoomInButtonClicked(object sender, EventArgs e)
        {

            pictureBox1168.Width = pictureBox1168.Width + 50;
            pictureBox1168.Height = pictureBox1168.Height + 50;
        }

        private void onZoomOutButtonClicked(object sender, EventArgs e)
        {
            pictureBox1168.Width = pictureBox1168.Width - 50;
            pictureBox1168.Height = pictureBox1168.Height - 50;
        }

        private void onSaveImage(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "JPG(*JPG)|*.jpg";

            if(save.ShowDialog() == DialogResult.OK)
            {
                pictureBox1168.Image.Save(save.FileName);
            }
        }

        private void onPrintImage(object sender, EventArgs e)
        {
            PrintDialog p = new PrintDialog();
            PrintDocument doc = new PrintDocument();
            doc.PrintPage += printPage;
            p.Document = doc;
            if(p.ShowDialog() == DialogResult.OK)
            {
                doc.Print();
            }
        }
        private void printPage(object sender, PrintPageEventArgs e)
        {
            Bitmap bm = new Bitmap(pictureBox1168.Width, pictureBox1168.Height);
            pictureBox1168.DrawToBitmap(bm, new Rectangle(0, 0, pictureBox1168.Width, pictureBox1168.Height));
            e.Graphics.DrawImage(bm, 0, 0);
            bm.Dispose();
        }

       

        
        
    }
}
