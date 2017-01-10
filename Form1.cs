using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace BiPad
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
            richTextBox1.SelectionFont = new Font("Courier New", 12, FontStyle.Regular);
            richTextBox1.SelectionColor = System.Drawing.Color.Black;
        }
        /// <summary>
        /// Global Variable FileName
        /// </summary>
        public string FileName;

        // File -> Close - File Menu Action
        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
            // Create StreamWriter Object to open the file
            using (StreamReader sr = new StreamReader(openFileDialog1.FileName))
            {
                
                FileName = openFileDialog1.FileName;
                // Read the entire file into buffer
                String lineChk = sr.ReadToEnd();
           
                // Put the file contents in the richTextBox1
                if (FileName != null && richTextBox1.Text.Equals(lineChk))
                {
                    MessageBox.Show("They don't match");
                }
            

            if (FileName == null)
                {
                    // Display a MsgBox asking the user to save changes or abort. 
                    if (MessageBox.Show("Do you want to save changes to your text?", "BiPad",
                       MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        // Open Save As Dialog
                        SaveFileAs();
                    }
 
                    else
                    {
                        // The user clicked NO and wants to exit the application. 
                        // Close the StreamReader
                        sr.Close();
                        // Garbage Collection
                        GC.Collect();
                        // Close everything down.
                        Application.Exit();

                        
                    }
                }
                else if ((FileName == null) && (richTextBox1.Text == ""))
                {
                    // The user wants to exit the application. There is no FilName and the richTextBox is empty. 
                    // Close everything down.
                    // Close the StreamReader
                    sr.Close();
                    // Garbage Collection
                    GC.Collect();
                    Application.Exit();
                  
                }
                else
                {
                    // The user wants to exit the application. Close everything down.
                    // Close the StreamReader
                    sr.Close();
                    // Garbage Collection
                    GC.Collect();
                    Application.Exit();
                     
                }
            } // Close the StreamReader
        } //Close menu strip

        // File -> File Save - File Menu Action
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // If the Global FileName exists
            if (FileName != null)
            {
                
                // Write text to file
                using (StreamWriter sw = new StreamWriter(FileName))
                {
                    sw.WriteLine(richTextBox1.Text);

                    // Close the StreamWriter
                    sw.Close();
                    // Garbage Collection
                    GC.Collect();
                }
            }
            else
            {
                // Open Save As Dialog
                SaveFileAs();
             }

        }

        // File -> Save As - File Menu Action
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Open Save As Dialog
            SaveFileAs();
        }

        // File -> Open - File Menu Action
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Stream myStream = null;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            if (FileName != null)
            {
                // Open last directory opened
                openFileDialog1.RestoreDirectory = true;
            }
            else
            {
                // Open Default Directory "My Documents"
                openFileDialog1.InitialDirectory =
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            }
            
            // Open Type: All Files and Text .txt Files
            openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;

            // If the user clicks OK
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Create StreamWriter Object to open the file
                    using (StreamReader sr = new StreamReader(openFileDialog1.FileName))
                    {
                        // setting the global variable
                        FileName = openFileDialog1.FileName;
                        // Read the entire file into buffer
                        String line = sr.ReadToEnd();
                        // Put the file contents in the richTextBox1
                        richTextBox1.Text = line;
                        // Close the StreamReader
                        sr.Close();
                        // Garbage Collection
                        GC.Collect();
                    }
                }
                    // If there is any problem reading the file
                catch (Exception ex)
                {
                    MessageBox.Show("The file could not be read:" + ex.Message, "Cannot read the File");
                }
            }
            else
            {
                // Close Dialog and Reset
                openFileDialog1.Reset();
            }
        }

        
        /// <summary>
        /// Save As Method:
        /// Invokes the Save As Dialog
        /// </summary>
 
        public void SaveFileAs()
        {
            //  This is for "Save As" dialog
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            if (FileName == null)
            {
                // Default FileName
                saveFileDialog1.FileName = "*.txt";
            }
            else
            {
                // Default FileName
                saveFileDialog1.FileName = FileName;
            }
            // Save as Type: All Files and Text .txt Files
            saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*"; 
            // Default File Extension
            saveFileDialog1.DefaultExt = "txt";

            if (FileName != null)
            {
                // Open last directory opened
                saveFileDialog1.RestoreDirectory = true;
            }
            else
            {
                // Open Default Directory "My Documents"
                saveFileDialog1.InitialDirectory =
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            }
            saveFileDialog1.FilterIndex = 2;
 
            // If user clicked OK
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {

                // Create StreamWriter Object to write the file
                using (StreamWriter sw = new StreamWriter(saveFileDialog1.FileName))
                {
                    sw.WriteLine(richTextBox1.Text);
                    // setting the global variable
                    FileName = saveFileDialog1.FileName;

                    // Close the StreamWriter
                    sw.Close();
                    // Garbage Collection
                    GC.Collect();
                }

            }
            else
            {
                // Close Dialog and Reset
                saveFileDialog1.Reset();
            }

        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FileName = null;
            richTextBox1.Text = "";
        }

        private void aboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(" BiPad Text Editor \r\n Version 1.0 \r\n Blake Stiller Copyright © 2015 \r\n BlakeStiller.com", "About");
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(" If there are any issues, errors, or suggestions \r\n You can email me at: \r\n me@BlakeStiller.com", "Help");
        }

        private void colourToolStripMenuItem_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            richTextBox1.ForeColor = colorDialog1.Color;
        }

        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fontDialog1.ShowDialog();
            richTextBox1.Font = fontDialog1.Font;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (FileName == null)
            {
                SaveFileAs();
            }
        }
    }
}


