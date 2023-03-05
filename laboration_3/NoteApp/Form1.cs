using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
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
            this.DragDrop += new DragEventHandler(formDrag_Drop);
            this.DragEnter += new DragEventHandler(formDrag_Drop);
        }

        // När programmet startas tilldelas filnamnet i titelraden "Untitled.txt".
        private void form_Load(object sender, EventArgs e)
        {
            this.Text = "Untitled.txt";
        }

        // Uppdatering av filnamnet i titeln.
        private void formTitle_Update(string fileName)
        {
            // Om: Ingen osparad text eller öppnad fil
            if (!unsavedText && string.IsNullOrEmpty(fileName))
            {
                this.Text = "Untitled.txt";
            }
            // Om: Osparad text och ingen öppnad fil
            if (unsavedText && string.IsNullOrEmpty(fileName))
            {
                this.Text = "* Untitled.txt";
            }
            // Om: Ingen osparad text i en öppnad fil
            if (!unsavedText && !string.IsNullOrEmpty(fileName))
            {
                this.Text = Path.GetFileName(fileName);
            }
            // Om: Osparad text i öppnad fil
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
            statusStrip_Update();
        }
        
        // Uppdatering av informationen i statusStrip.
        private void statusStrip_Update()
        {
            // Antal tkn inkl. mellanslag.
            var charCountWithSpaces = richTextBox.Text.Length;
            // Antal tkn exkl. mellanslag.
            var charCountWithoutSpaces = richTextBox.Text.Replace(" ", "").Length;
            // Antal ord. tinyurl.com/43jpfktk | Använder @"\w+" istället för @"[\W]+" för att räkna antal ord.
            var wordCount = Regex.Matches(richTextBox.Text, @"\w+").Count.ToString();
            // Antal rader.
            var rowCount = richTextBox.Lines.Length; 

            toolStripStatusLabel_withSpaces.Text = "w spaces: " + charCountWithSpaces;
            toolStripStatusLabel_withoutSpaces.Text = "w/o spaces: " + charCountWithoutSpaces;
            toolStripStatusLabel_wordCount.Text = "word count: " + wordCount;
            toolStripStatusLabel_rowCounter.Text = "row count: " + rowCount;
        }

        // Spara fil.
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Om ingen fil finns (fileName är tom). Spara en ny.
            if (string.IsNullOrEmpty(fileName))
            {
                using (SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "Text Files (*.txt) | *.txt",
                    DefaultExt = "txt",
                    AddExtension = true
                })
                {
                    // Placeholder filnamn.
                    saveFileDialog.FileName = "Untitled"; 
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
            // Skriv till öppnad fil.
            else
            {
                using (StreamWriter streamWriter = new StreamWriter(fileName))
                {
                    streamWriter.Write(richTextBox.Text);
                }
                unsavedText = false;
                formTitle_Update(fileName);
            }
        }

        // Öppna fil.
        private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void clearFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*
             * Töm richTextBox.
             * Sätt unsavedText till false.
             * Uppdatera formTitle.
             */
            richTextBox.Clear();
            unsavedText = false;
            formTitle_Update(fileName = string.Empty);
        }

        private void exitFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Om det finns osparad text i dokumentet när programmet ska stängas via menyvalet "Exit File".
            if (unsavedText)
            {
                DialogResult dialog = MessageBox.Show("Save changes before exit?", "Unsaved changes", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                switch (dialog)
                {
                    case DialogResult.Yes:
                        saveToolStripMenuItem_Click(sender, e);
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
                DialogResult dialog = MessageBox.Show("Save changes before exit?", "Unsaved changes", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                switch (dialog)
                {
                    case DialogResult.Yes:
                        saveToolStripMenuItem_Click(sender, e);
                        break;
                    case DialogResult.Cancel:
                        e.Cancel = true;
                        return;
                }
            }
        }

        private void formDrag_Enter(object sender, DragEventArgs e)
        {
            // Check för att kolla om shift eller ctrl på tangentbordet är nedtryckt.
            if (e.Data.GetDataPresent(DataFormats.FileDrop) && (Control.ModifierKeys & (Keys.Control | Keys.Shift)) == (Keys.Control | Keys.Shift))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void formDrag_Drop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

            try
            {
                foreach (string file in files)
                {
                    // För att endast ta mot .txt-filer.
                    if (Path.GetExtension(file).Equals(".txt", StringComparison.OrdinalIgnoreCase))
                    {
                        string fileContent = File.ReadAllText(file);
                        fileName = file;
                        var mousePoint = richTextBox.GetCharIndexFromPosition(richTextBox.PointToClient(new Point(e.X, e.Y)));

                        switch (ModifierKeys)
                        {
                            case Keys.Control:
                                richTextBox.Text += (fileContent + Environment.NewLine);
                                break;
                            case Keys.Shift:
                                richTextBox.Select(mousePoint, 0);
                                richTextBox.SelectedText = fileContent;
                                break;
                            default:
                                richTextBox.Clear();
                                richTextBox.Text += (fileContent);
                                unsavedText = false;
                                formTitle_Update(Path.GetFileName(file));
                                break;
                        }
                    }
                    else
                    {
                        throw new Exception("Can only import text files.");
                    }
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }
    }
}
