using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace HomeWorkGallery
{
   

    public partial class Form1 : Form
    {
        public class ImageItem
        {
            public string Path { get; set; }
            public string Title { get; set; }

            public override string ToString()
            {
                return Title; 
            }
        }
        public Form1()
        {
            InitializeComponent();
        }
        private List<ImageItem> imageItems = new List<ImageItem>();
        private void btnLoad_Click(object input, EventArgs a)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Text files (*.txt)|*.txt";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string[] lines = File.ReadAllLines(openFileDialog.FileName);
                    imageItems.Clear();
                    comboBoxImages.Items.Clear();

                    foreach (var line in lines)
                    {
                        var parts = line.Split(',');
                        if (parts.Length == 2)
                        {
                            string path = parts[0].Trim();
                            string title = parts[1].Trim();
                            var imageItem = new ImageItem { Path = path, Title = title };
                            imageItems.Add(imageItem);
                            comboBoxImages.Items.Add(imageItem);
                        }
                    }
                    MessageBox.Show("Данные загружены");
                }
            }
        }

        private void btnSave_Click(object input, EventArgs a)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Text files (*.txt)|*.txt";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    using (StreamWriter writer = new StreamWriter(saveFileDialog.FileName))
                    {
                        foreach (var item in imageItems)
                        {
                            writer.WriteLine($"{item.Path},{item.Title}");
                        }
                    }
                    MessageBox.Show("Данные сохранены");
                }
            }
        }

        private void comboBoxImages_SelectedIndexChanged(object input, EventArgs a)
        {
            if (comboBoxImages.SelectedItem is ImageItem selectedItem)
            {
                textBoxTitle.Text = selectedItem.Title;

                if (File.Exists(selectedItem.Path))
                {
                    try
                    {
                        pictureBox.Image?.Dispose(); 
                        pictureBox.Image = Image.FromFile(selectedItem.Path);
                        label2.Visible = false;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Изображение не загрузилось: {ex.Message}");
                    }
                }
                else
                {
                    MessageBox.Show("Файл не существует: " + selectedItem.Path);
                }
            }
        }

        private void textBoxTitle_TextChanged(object input, EventArgs a)
        {

        }

        private void label1_Click(object input, EventArgs a)
        {

        }

        private void pictureBox_Click(object input, EventArgs a)
        {

        }
    }
}
