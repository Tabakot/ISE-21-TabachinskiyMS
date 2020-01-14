using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TechProgWin
{
    public partial class FormDock : Form
    {
        Dock<ITransport, IEngine> dock;

        public FormDock()
        {
            InitializeComponent();
            dock = new Dock<ITransport, IEngine>(20, pictureBoxDock.Width,
           pictureBoxDock.Height);
            Draw();
        }

        private void Draw()
        {
            Bitmap bmp = new Bitmap(pictureBoxDock.Width, pictureBoxDock.Height);
            Graphics gr = Graphics.FromImage(bmp);
            dock.Draw(gr);
            pictureBoxDock.Image = bmp;
        }
        /// <summary>
        /// Обработка нажатия кнопки "Припарковать автомобиль"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSetPlane_Click(object sender, EventArgs e)
        {
            ColorDialog dialog = new ColorDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                var plane = new Plane(100, 1000, dialog.Color);
                int place = dock + plane;
                Draw();
            }
        }

        private void buttonSetSeaplane_Click(object sender, EventArgs e)
        {
            ColorDialog dialog = new ColorDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                ColorDialog dialogDop = new ColorDialog();
                if (dialogDop.ShowDialog() == DialogResult.OK)
                {
                    var plane = new Seaplane(100, 1000, 10, dialog.Color, dialogDop.Color, 
                        false, true, false, CountEngine.Four);
                    int place = dock + plane;
                    Draw();
                }
            }
        }
        /// <summary>
        /// Обработка нажатия кнопки "Забрать"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonTakePlane_Click(object sender, EventArgs e)
        {
            if (maskedTextBox.Text != "")
            {
                var car = dock - Convert.ToInt32(maskedTextBox.Text);
                if (car != null)
                {
                    Bitmap bmp = new Bitmap(pictureBoxTakePlane.Width,
                   pictureBoxTakePlane.Height);
                    Graphics gr = Graphics.FromImage(bmp);
                    car.SetPosition(5, 5, pictureBoxTakePlane.Width,
                   pictureBoxTakePlane.Height);
                    car.DrawPlane(gr);
                    pictureBoxTakePlane.Image = bmp;
                }
                else
                {
                    Bitmap bmp = new Bitmap(pictureBoxTakePlane.Width,
                   pictureBoxTakePlane.Height);
                    pictureBoxTakePlane.Image = bmp;
                }
                Draw();
            }
        }

        private void buttonFindMatches_Click(object sender, EventArgs e)
        {
            if (maskedTextBox.Text != "")
            {
                
                if (dock == Convert.ToInt32(maskedTextBox.Text))
                {
                    MessageBox.Show("Совпадения найдены", "Результат",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Совпадений нет", "Результат",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            Draw();
        }
    }
}