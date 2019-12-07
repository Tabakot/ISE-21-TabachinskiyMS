using NLog;
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

        /// <summary>
        /// Логгер
        /// </summary>
        private Logger logger;

        public FormDock()
        {
            InitializeComponent();
            logger = LogManager.GetCurrentClassLogger();
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
           
    /*
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
                       dialogDop.Color, false, true, false);
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
        }*/
       
        private void buttonTakePlane_Click(object sender, EventArgs e)
        {
            if (listBoxLevels.SelectedIndex > -1)
            {
                if (maskedTextBox.Text != "")
                {
                    try
                    {
                        var plane = dock[listBoxLevels.SelectedIndex] -
                   Convert.ToInt32(maskedTextBox.Text);
                        Bitmap bmp = new Bitmap(pictureBoxTakePlane.Width,
                       pictureBoxTakePlane.Height);
                        Graphics gr = Graphics.FromImage(bmp);
                        plane.SetPosition(5, 5, pictureBoxTakePlane.Width,
                       pictureBoxTakePlane.Height);
                        plane.DrawPlane(gr);
                        pictureBoxTakePlane.Image = bmp;
                        logger.Info("Pick up a plane " + plane.ToString() + " place: " 
                            + maskedTextBox.Text);
                        Draw();
                    }
                    catch (DockNotFoundException ex)
                    {
                        logger.Error(ex.Message);
                        MessageBox.Show(ex.Message, "Not found", MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        Bitmap bmp = new Bitmap(pictureBoxTakePlane.Width,
                       pictureBoxTakePlane.Height);
                        pictureBoxTakePlane.Image = bmp;
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex.Message);
                        MessageBox.Show(ex.Message, "Unknown error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

        }

        private void FormDock_Load(object sender, EventArgs e)
        {

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
                try
                {
                    int place = dock[listBoxLevels.SelectedIndex] + plane;
                    logger.Info("Add plane " + plane.ToString() + " place: " + place);
                    Draw();
                }
                catch (DockOverflowException ex)
                {
                    logger.Error(ex.Message);
                    MessageBox.Show(ex.Message, "Overflow", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    logger.Error(ex.Message);
                    MessageBox.Show(ex.Message, "Unknown error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try {
                    dock.SaveData(saveFileDialog.FileName);
                    MessageBox.Show("Saved successfully", "Dock",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    logger.Info("Save file " + saveFileDialog.FileName);
                }
                catch (Exception ex)
                {
                    logger.Error(ex.Message);
                    MessageBox.Show(ex.Message, "Save failed",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                dock.LoadData(openFileDialog.FileName);
                    MessageBox.Show("Loaded successfully", "Dock", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    logger.Info("Loaded from file " + openFileDialog.FileName);
                }
                catch (DockOccupiedPlaceException ex)
                {
                    logger.Error(ex.Message);
                    MessageBox.Show(ex.Message, "Load failed", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    logger.Error(ex.Message);
                    MessageBox.Show(ex.Message, "Unknown error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                Draw();
            }
        }
    }
}