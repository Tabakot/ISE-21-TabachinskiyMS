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
        MultiLevelDock dock;

        private const int countLevel = 5;


        public FormDock()
        {
            InitializeComponent();
            dock = new MultiLevelDock(countLevel, pictureBoxDock.Width,
                pictureBoxDock.Height);
            Draw();
            for (int i = 0; i < countLevel; i++)
            {
                listBoxLevels.Items.Add("Уровень " + (i + 1));
            }
            listBoxLevels.SelectedIndex = 0;
        }

        private void Draw()
        {
            if (listBoxLevels.SelectedIndex > -1)
            {
                Bitmap bmp = new Bitmap(pictureBoxDock.Width,
                    pictureBoxDock.Height);
                Graphics gr = Graphics.FromImage(bmp);
                dock[listBoxLevels.SelectedIndex].Draw(gr);
                pictureBoxDock.Image = bmp;

            }
        }
           

        private void buttonSetPlane_Click(object sender, EventArgs e)
        {
            if (listBoxLevels.SelectedIndex > -1)
            {
                ColorDialog dialog = new ColorDialog();
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    var plane = new Plane(100, 1000, dialog.Color);
                    int place = dock[listBoxLevels.SelectedIndex] + plane;
                    if (place == -1)
                    {
                        MessageBox.Show("No free places", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    Draw();
                }
            }
        }

        private void buttonSetSeaplane_Click(object sender, EventArgs e)
        {
            if (listBoxLevels.SelectedIndex > -1)
            {
                ColorDialog dialog = new ColorDialog();
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    ColorDialog dialogDop = new ColorDialog();
                    if (dialogDop.ShowDialog() == DialogResult.OK)
                    {
                        var plane = new Seaplane(100, 1000, 10, dialog.Color,
                       dialogDop.Color, false, true, false, CountEngine.Four);
                        int place = dock[listBoxLevels.SelectedIndex] + plane;
                        if (place == -1)
                        {
                            MessageBox.Show("No free places", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        Draw();
                    }
                }
            }
        }
       
        private void buttonTakePlane_Click(object sender, EventArgs e)
        {
            if (listBoxLevels.SelectedIndex > -1)
            {
                if (maskedTextBox.Text != "")
                {
                    var plane = dock[listBoxLevels.SelectedIndex] -
                   Convert.ToInt32(maskedTextBox.Text);
                    if (plane != null)
                    {
                        Bitmap bmp = new Bitmap(pictureBoxTakePlane.Width,
                       pictureBoxTakePlane.Height);
                        Graphics gr = Graphics.FromImage(bmp);
                        plane.SetPosition(5, 5, pictureBoxTakePlane.Width,
                       pictureBoxTakePlane.Height);
                        plane.DrawPlane(gr);
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

        }

        private void listBoxLevels_SelectedIndexChanged(object sender, EventArgs e)
        {
            Draw();
        }
    }
}