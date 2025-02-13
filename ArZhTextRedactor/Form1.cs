﻿using System;
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
        const int MIDDLE = 382;   
        int sumRGB;    
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

     
            foreach (string color in colorList)
            {
                colorStripDropDownButton.DropDownItems.Add(color);
            }
            for (int i = 0; i < colorStripDropDownButton.DropDownItems.Count; i++)
            {
                
                KnownColor selectedColor;
                selectedColor = (KnownColor)System.Enum.Parse(typeof(KnownColor), colorList[i]);    
                colorStripDropDownButton.DropDownItems[i].BackColor = Color.FromKnownColor(selectedColor);    

               
                Color col = Color.FromName(colorList[i]);

                
                
                sumRGB = ConvertToRGB(col);    
                if (sumRGB <= MIDDLE)    
                {
                    colorStripDropDownButton.DropDownItems[i].ForeColor = Color.White;    
                }
                else if (sumRGB > MIDDLE)    
                {
                    colorStripDropDownButton.DropDownItems[i].ForeColor = Color.Black;    
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
            int r = c.R, 
                g = c.G, 
                b = c.B; 
            int sum = 0;

            
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
           
            richTextBox1.SelectionFont = new Font(richTextBox1.SelectionFont.FontFamily, Convert.ToInt32(fontSizeComboBox.Text), richTextBox1.SelectionFont.Style);
        }

        private void fontStripComboBox_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (richTextBox1.SelectionFont == null)
            {
                
                richTextBox1.SelectionFont = new Font(fontStripComboBox.Text, richTextBox1.Font.Size);
            }
            
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

        private void CheckStyleButton()
        {
            if (richTextBox1.SelectionFont is null)
                return;


        }

        private void BoldStripButton1_Click(object sender, EventArgs e)
        {
            if (richTextBox1.SelectionFont is null)
                return;

            BoldStripButton1.Checked = !BoldStripButton1.Checked;  
           
            
            FontStyle style = richTextBox1.SelectionFont.Style;

            if (richTextBox1.SelectionFont.Bold)
                style &= ~FontStyle.Bold;
            else
                style |= FontStyle.Bold;

            richTextBox1.SelectionFont = new Font(richTextBox1.SelectionFont, style);
        }

        private void ItalicStripButton2_Click(object sender, EventArgs e)
        {
            if (ItalicStripButton2.Checked == false)
            {
                ItalicStripButton2.Checked = true;    
            }
            else if (ItalicStripButton2.Checked == true)
            {
                ItalicStripButton2.Checked = false;    
            }

            if (richTextBox1.SelectionFont == null)
            {
                return;
            }
           
            FontStyle style = richTextBox1.SelectionFont.Style;

           
            if (richTextBox1.SelectionFont.Italic)
            {
                style &= ~FontStyle.Italic;
            }
            else
            {
                style |= FontStyle.Italic;
            }
            richTextBox1.SelectionFont = new Font(richTextBox1.SelectionFont, style);
        }

        private void UnderlineStripButton3_Click(object sender, EventArgs e)
        {
            if (UnderlineStripButton3.Checked == false)
            {
                UnderlineStripButton3.Checked = true;     
            }
            else if (UnderlineStripButton3.Checked == true)
            {
                UnderlineStripButton3.Checked = false;    
            }

            if (richTextBox1.SelectionFont == null)
            {
                return;
            }

            
            FontStyle style = richTextBox1.SelectionFont.Style;

           
            if (richTextBox1.SelectionFont.Underline)
            {
                style &= ~FontStyle.Underline;
            }
            else
            {
                style |= FontStyle.Underline;
            }
            richTextBox1.SelectionFont = new Font(richTextBox1.SelectionFont, style);
        }

        private void leftAlignStripButton_Click(object sender, EventArgs e)
        {
            centerAlignStripButton.Checked = false;
            rightAlignStripButton.Checked = false;
            if (leftAlignStripButton.Checked == false)
            {
                leftAlignStripButton.Checked = true;   
            }
            else if (leftAlignStripButton.Checked == true)
            {
                leftAlignStripButton.Checked = false;    
            }
            richTextBox1.SelectionAlignment = HorizontalAlignment.Left;
        }

        private void centerAlignStripButton_Click(object sender, EventArgs e)
        {
            leftAlignStripButton.Checked = false;
            rightAlignStripButton.Checked = false;
            if (centerAlignStripButton.Checked == false)
            {
                centerAlignStripButton.Checked = true;   
            }
            else if (centerAlignStripButton.Checked == true)
            {
                centerAlignStripButton.Checked = false;    
            }
            richTextBox1.SelectionAlignment = HorizontalAlignment.Center;
        }

        private void rightAlignStripButton_Click(object sender, EventArgs e)
        {
            leftAlignStripButton.Checked = false;
            centerAlignStripButton.Checked = false;

            if (rightAlignStripButton.Checked == false)
            {
                rightAlignStripButton.Checked = true;    
            }
            else if (rightAlignStripButton.Checked == true)
            {
                rightAlignStripButton.Checked = false;    
            }
            richTextBox1.SelectionAlignment = HorizontalAlignment.Right;
        }
    }
}
