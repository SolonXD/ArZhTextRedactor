using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;

namespace CSharp
{
    public partial class Form1 : Form
    {
        static string open_path = "";
        static string filename = "";
        List<string> colorList = new List<string>();
        const int MIDDLE = 382;    // middle sum of RGB - max is 765
        int sumRGB;    // sum of the selected colors RGB
        public Form1()
        {
            InitializeComponent();
            saveFileDialog1.Filter = "Text File(*.txt)|*txt|Hehe File (*hf)|*.hf";
            richTextBox1.AllowDrop = true;
            richTextBox1.AcceptsTab = true;
            richTextBox1.ShortcutsEnabled = true;
            richTextBox1.DetectUrls = true;

            for (int i = 8; i < 80; i += 2)
            {
                fontSizeComboBox.Items.Add(i);
            }

            foreach (System.Reflection.PropertyInfo prop in typeof(Color).GetProperties())
            {
                if (prop.PropertyType.FullName == "System.Drawing.Color")
                {
                    colorList.Add(prop.Name);
                }
            }

            // fill the drop down items list
            foreach (string color in colorList)
            {
                colorStripDropDownButton.DropDownItems.Add(color);
            }
            for (int i = 0; i < colorStripDropDownButton.DropDownItems.Count; i++)
            {
                // Create KnownColor object
                KnownColor selectedColor;
                selectedColor = (KnownColor)System.Enum.Parse(typeof(KnownColor), colorList[i]);    // parse to a KnownColor
                colorStripDropDownButton.DropDownItems[i].BackColor = Color.FromKnownColor(selectedColor);    // set the BackColor to its appropriate list item

                // Set the text color depending on if the barkground is darker or lighter
                // create Color object
                Color col = Color.FromName(colorList[i]);

                // 255,255,255 = White and 0,0,0 = Black
                // Max sum of RGB values is 765 -> (255 + 255 + 255)
                // Middle sum of RGB values is 382 -> (765/2)
                // Color is considered darker if its <= 382
                // Color is considered lighter if its > 382
                sumRGB = ConvertToRGB(col);    // get the color objects sum of the RGB value
                if (sumRGB <= MIDDLE)    // Darker Background
                {
                    colorStripDropDownButton.DropDownItems[i].ForeColor = Color.White;    // set to White text
                }
                else if (sumRGB > MIDDLE)    // Lighter Background
                {
                    colorStripDropDownButton.DropDownItems[i].ForeColor = Color.Black;    // set to Black text
                }
            }
            InstalledFontCollection fonts = new InstalledFontCollection();
            foreach (FontFamily family in fonts.Families)
            {
                fontStripComboBox.Items.Add(family.Name);
            }

        }
        private int ConvertToRGB(System.Drawing.Color c)
        {
            int r = c.R, // RED component value
                g = c.G, // GREEN component value
                b = c.B; // BLUE component value
            int sum = 0;

            // calculate sum of RGB
            sum = r + g + b;

            return sum;
        }


        
        private void colorStripDropDownButton_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            KnownColor selectedColor;
            selectedColor = (KnownColor)System.Enum.Parse(typeof(KnownColor), e.ClickedItem.Text);
            richTextBox1.SelectionColor = Color.FromKnownColor(selectedColor);
        }
        
        private void fontSizeComboBox_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (richTextBox1.SelectionFont == null)
            {
                return;
            }
            // sets the font size when changed
            richTextBox1.SelectionFont = new Font(richTextBox1.SelectionFont.FontFamily, Convert.ToInt32(fontSizeComboBox.Text), richTextBox1.SelectionFont.Style);
        }

        private void fontStripComboBox_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (richTextBox1.SelectionFont == null)
            {
                // sets the Font Family style
                richTextBox1.SelectionFont = new Font(fontStripComboBox.Text, richTextBox1.Font.Size);
            }
            // sets the selected font famly style
            richTextBox1.SelectionFont = new Font(fontStripComboBox.Text, richTextBox1.SelectionFont.Size);
        }
        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void сохранитьКакToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
               return;
            string filePath = saveFileDialog1.FileName;
            File.WriteAllText(filePath, richTextBox1.Text);  
            open_path = filePath;
            MessageBox.Show("Файл сохранён!");
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                
                if (!string.IsNullOrEmpty(open_path))
                {
                    
                    File.WriteAllText(open_path, richTextBox1.Text);
                    MessageBox.Show("Файл сохранён!");
                }
                else
                {
                    
                    сохранитьКакToolStripMenuItem_Click(sender, e);
                }
            }
            catch (Exception)
            {
                MessageBox.Show(
                    "Ошибка сохранения файла!",
                    "Ошибка!",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void открытьФайлToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            string filePath = openFileDialog1.FileName;
            string fileText = File.ReadAllText(filePath);
            richTextBox1.Text = fileText;  
            open_path = filePath;  
            MessageBox.Show("Файл открыт!");
        }

        private void копироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(richTextBox1.TextLength != 0)
            {
                richTextBox1.Copy();
            }
        }

        private void вставитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox1.TextLength != 0)
            {
                richTextBox1.Paste();
            }
        }

        private void вырезатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox1.TextLength != 0)
            {
                richTextBox1.Cut();
            }
        }

        private void настройкиШрифтаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fontDialog1.ShowDialog();
            richTextBox1.Font = fontDialog1.Font;
        }

        private void настройкаФонаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            richTextBox1.BackColor = colorDialog1.Color;
        }

        private void выделитьВсеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox1.TextLength != 0)
            {
                richTextBox1.SelectAll();
            }
        }


        private void richTextBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                
            }
        }

        private void richTextBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                
            }
        }

        private void создатьФайлToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(richTextBox1.Text))
            {
                
                richTextBox1.Clear();
                open_path = "";  
                MessageBox.Show("Новый файл создан.");
            }
            else
            {
                
                MessageBox.Show("Текущий файл уже пуст.");
            }
        }

        
    }
}
