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

        /// <summary>
        /// Форма для добавления
        /// </summary>
        FormPlaneConfig form;

        private const int countLevel = 5;


        public FormDock()
        {
            InitializeComponent();
            dock = new MultiLevelDock(countLevel, pictureBoxDock.Width,
                pictureBoxDock.Height);
            Draw();
            for (int i = 0; i < countLevel; i++)
            {
                listBoxLevels.Items.Add("Level " + (i + 1));
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

        /// <summary>
        /// Обработка нажатия кнопки "Create"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCreate_Click(object sender, EventArgs e)
        {
            form = new FormPlaneConfig();
            form.AddEvent(AddPlane);
            form.Show();
        }

        /// <summary>
        /// Метод добавления самолета
        /// </summary>
        /// <param name="car"></param>
        private void AddPlane(ITransport plane)
        {
            if (plane != null && listBoxLevels.SelectedIndex > -1)
            {
                int place = dock[listBoxLevels.SelectedIndex] + plane;
                if (place > -1)
                {
                    Draw();
                }
                else
                {
                    MessageBox.Show("failed to create");
                }
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (dock.SaveData(saveFileDialog.FileName))
                {
                    MessageBox.Show("Saved successfully", "Dock",
                   MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Save failed", "Dock",
                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (dock.LoadData(openFileDialog.FileName))
                {
                    MessageBox.Show("Loaded successfully", "Dock", MessageBoxButtons.OK,
 MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Load failed", "Dock", MessageBoxButtons.OK,
                   MessageBoxIcon.Error);
                }
                Draw();
            }
        }
    }
}