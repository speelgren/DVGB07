using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace NoteApp
{
    public partial class NoteApp : Form
    {
        // Flaggar för att se om text inom richTextBox har ändrats.
        private bool unsavedText = false;
        // Flaggar för att stänga programmet och inte trigga dialogrutan från form_Closed().
        private bool closeByMenu = false;
        // Spara filnamnet, för att komma åt det senare i programmet.
        private string fileName = string.Empty;

        public NoteApp()
        {
            InitializeComponent();
        }

        // När programmet startas tilldelas filnamnet i titelraden "Untitled.txt".
        private void form_Load(object sender, EventArgs e)
        {
            this.Text = "Untitled.txt";
        }

        // Uppdatering av filnamnet i titeln.
        private void formTitle_Update(string fileName)
        {
            if (!unsavedText && string.IsNullOrEmpty(fileName))
            {
                this.Text = "Untitled.txt";
            }
            if (unsavedText && string.IsNullOrEmpty(fileName))
            {
                this.Text = "* Untitled.txt";
            }
            if (!unsavedText && !string.IsNullOrEmpty(fileName))
            {
                this.Text = Path.GetFileName(fileName);
            }
            if (unsavedText && !string.IsNullOrEmpty(fileName))
            {
                this.Text = "* " + Path.GetFileName(fileName);
            }
        }

        // "Lyssnar" efter ändringar i richTextBox.
        private void richTextBox_TextChanged(object sender, EventArgs e)
        {
            unsavedText = true;
            formTitle_Update(fileName);
            statusStrip_Update(null, null);
        }
        
        // Uppdatering av informationen i statusStrip.
        private void statusStrip_Update(object sender, EventArgs e)
        {
            var charCountWithSpaces = richTextBox.Text.Replace("\r", "").Replace("\n", "").Length;
            var charCountWithoutSpaces = richTextBox.Text.Replace(" ", "").Replace("\r", "").Replace("\n", "").Length; // Komplettering
            var wordCount = Regex.Matches(richTextBox.Text, @"\w+").Count.ToString();
            var rowCount = richTextBox.Lines.Length; 

            toolStripStatusLabel_withSpaces.Text = "w spaces: " + charCountWithSpaces;
            toolStripStatusLabel_withoutSpaces.Text = "w/o spaces: " + charCountWithoutSpaces;
            toolStripStatusLabel_wordCount.Text = "word count: " + wordCount;
            toolStripStatusLabel_rowCounter.Text = "row count: " + rowCount;
        }

        // Skapa fil.
        private void writeFile(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Text Files (*.txt) | *.txt",
                DefaultExt = "txt",
                FileName = "Untitled",
                AddExtension = true
            })
            {
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    using (StreamWriter streamWriter = new StreamWriter(saveFileDialog.FileName))
                    {
                        streamWriter.Write(richTextBox.Text);
                    }
                    fileName = Path.GetFileName(saveFileDialog.FileName);
                    unsavedText = false;
                    formTitle_Update(fileName);
                }
            }
        }

        // Skriv till fil.
        private void saveFile(object sender, EventArgs e)
        {
            using (StreamWriter streamWriter = new StreamWriter(fileName))
            {
                streamWriter.Write(richTextBox.Text);
            }
            unsavedText = false;
            formTitle_Update(fileName);
        }

        // Läs fil.
        private void readFile(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Text Files (*.txt) | *.txt",
                Title = "Open Text File."
            })

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    using (StreamReader streamReader = new StreamReader(openFileDialog.FileName))
                    {
                        richTextBox.Text = streamReader.ReadToEnd();
                        fileName = openFileDialog.FileName;
                    }
                    this.Text = openFileDialog.SafeFileName;
                    unsavedText = false;
                }
        }

        // Spara fil.
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Om ingen fil finns (fileName är tom). Spara en ny.
            if (string.IsNullOrEmpty(fileName))
            {
                writeFile(null, null);
            }
            // Skriv till öppnad fil.
            else
            {
                saveFile(null, null);
            }
        }

        // Öppna fil.
        private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (unsavedText)
            {
                DialogResult dialog = MessageBox.Show(
                    "Save before open?",
                    "Unsaved changes", 
                    MessageBoxButtons.YesNoCancel, 
                    MessageBoxIcon.Question
                    );

                switch (dialog)
                {
                    case DialogResult.Yes:
                        // Om ingen fil finns (fileName är tom). Spara en ny fil och öppna readFile.
                        if (string.IsNullOrEmpty(fileName))
                        {
                            writeFile(null, null);
                            readFile(null, null);
                        }
                        else
                        {
                            saveFile(null, null);
                            readFile(null, null);
                        }
                        break;
                    case DialogResult.No:
                        readFile(null, null);
                        break;
                    default:
                        return;
                }
            }
            else
            {
                readFile(null, null);
            }
        }

        private void clearFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox.Clear();
            unsavedText = false;
            formTitle_Update(fileName = string.Empty);
        }

        private void exitFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Om det finns osparad text i dokumentet när programmet ska stängas via menyvalet "Exit File".
            if (unsavedText)
            {
                DialogResult dialog = MessageBox.Show(
                    "Save changes before exit?", 
                    "Unsaved changes", 
                    MessageBoxButtons.YesNoCancel, 
                    MessageBoxIcon.Question
                    );

                switch (dialog)
                {
                    case DialogResult.Yes:
                        saveToolStripMenuItem_Click(null, null);
                        break;
                    case DialogResult.No:
                        closeByMenu = true;
                        Application.Exit();
                        break;
                    default:
                        closeByMenu = false;
                        return;
                }
            }
            else
            {
                Application.Exit();
            }
        }

        private void form_Closing(object sender, FormClosingEventArgs e)
        {
            // Om det finns osparad text i dokumentet när programmet ska stängas via X-knappen.
            if (!closeByMenu && unsavedText)
            {
                DialogResult dialog = MessageBox.Show(
                    "Save changes before exit?", 
                    "Unsaved changes", 
                    MessageBoxButtons.YesNoCancel, 
                    MessageBoxIcon.Question
                    );

                switch (dialog)
                {
                    case DialogResult.Yes:
                        saveToolStripMenuItem_Click(null, null);
                        break;
                    case DialogResult.Cancel:
                        e.Cancel = true;
                        return;
                }
            }
        }
    }
}