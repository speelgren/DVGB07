using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace NoteApp
{
    public partial class NoteApp : Form
    {
        private bool unsavedText = false; // Flaggar för att se om text inom richTextBox har ändrats.
        private bool closeByMenu = false; // Flaggar för att stänga programmet och triggar inte dialogrutan från form_Closed().
        private string openedFile = ""; // Spara filnamnet, för att komma åt det senare i programmet utan savefiledialog eller openfiledialog.

        public NoteApp()
        {
            InitializeComponent();
            this.DragDrop += new DragEventHandler(form_DragDrop);
            this.DragEnter += new DragEventHandler(form_DragDrop);
        }

        /*
         * När programmet startas tilldelas filnamnet i titelraden "Untitled.txt".
         */
        private void form_onLoad(object sender, EventArgs e)
        {
            this.Text = "Untitled.txt";
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*
             * Om ingen fil finns (openedFile är tom). Spara en ny.
             */
            if(string.IsNullOrEmpty(openedFile))
            {
                using (SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "Text Files (*.txt) | *.txt",
                    DefaultExt = "txt",
                    AddExtension = true
                })

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    using (StreamWriter streamWriter = new StreamWriter(saveFileDialog.FileName))
                    {
                        streamWriter.Write(richTextBox.Text);
                    }
                    openedFile = saveFileDialog.FileName;
                    unsavedText = false;
                    formTitle_Update();
                }
            }
            else
            {
                using (StreamWriter streamWriter = new StreamWriter(openedFile))
                {
                    streamWriter.Write(richTextBox.Text);
                }
                unsavedText = false;
                formTitle_Update();
            }
        }

        /*
         * Lyssnar efter ändringar i richTextBox.
         */
        private void richTextBox_TextChanged(object sender, EventArgs e)
        {
            unsavedText = true;
            formTitle_Update();
            statusStrip_Update();
        }

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
                    openedFile = openFileDialog.FileName;
                }
                this.Text = openFileDialog.SafeFileName;
                unsavedText = false;
            }
        }

        /*
         * Uppdatering av filnamnet i titeln.
         */
        private void formTitle_Update()
        {
            if (unsavedText && string.IsNullOrEmpty(openedFile))
            {
                this.Text = "* Untitled.txt";
            } 
            else
            {
                this.Text = "*" + Path.GetFileName(openedFile);
            }
        }

        private void statusStrip_Update()
        {
            int charCountWithSpaces = richTextBox.Text.Length; // antal tkn inkl. mellanslag
            int charCountWithoutSpaces = richTextBox.Text.Replace(" ", "").Length; // antal tkn exkl. mellanslag
            int wordCount = richTextBox.Text.Split(new char[] {' ', '\n', '\r'}).Length; // antal ord. 
            int rowCount = richTextBox.Lines.Length; // antal rader

            /*
             * Uppdatering av informationen i statusStrip.
             */
            toolStripStatusLabel_withSpaces.Text = "w spaces: " + charCountWithSpaces;
            toolStripStatusLabel_withoutSpaces.Text = "w/o spaces: " + charCountWithoutSpaces;
            toolStripStatusLabel_wordCount.Text = "word count: " + wordCount;
            toolStripStatusLabel_rowCounter.Text = "row count: " + rowCount;
        }

        private void clearFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*
             * Töm richTextBox.
             * Sätt unsavedText till false. Finns ingen "unsavedText".
             */
            richTextBox.Clear();
            unsavedText = false;
        }

        private void exitFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*
             * Om det finns osparad text i dokumentet.
             */
            if(unsavedText)
            {
                DialogResult dialog = MessageBox.Show(
                    "Save changes before exit?", 
                    "Unsaved changes",
                    MessageBoxButtons.YesNoCancel, 
                    MessageBoxIcon.Question);

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
                /*
                 * Om det inte finns någon text i dokumentet så avslutas programmet.
                 */
                Application.Exit();
            }
        }
        
        /*
         * Använder eventet Closed för Form istället för Closing.
         * Fick inte Closing att fungera som jag ville (dubbla dialogrutor).
         */
        private void form_Closed(object sender, FormClosedEventArgs e)
        {
            if(!closeByMenu && unsavedText)
            {
                DialogResult dialog = MessageBox.Show(
                    "Save changes before exit?", 
                    "Unsaved changes", 
                    MessageBoxButtons.YesNo, 
                    MessageBoxIcon.Question);

                if (dialog == DialogResult.Yes)
                {
                    saveToolStripMenuItem_Click(sender, e);
                }
            }
        }

        /*
         * Version 3
         */
        private void form_DragDrop(object sender, DragEventArgs e)
        {
            /*
             * Check för att kolla om en fil droppats. Om inte return.
             */
            if (!e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                return;
            }

            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

            try
            {
                foreach (string file in files)
                {
                    /*
                     * För att endast ta mot .txt-filer.
                     */
                    if (Path.GetExtension(file).Equals(".txt", StringComparison.OrdinalIgnoreCase))
                    {
                        string fileContent = File.ReadAllText(file);

                        switch (ModifierKeys)
                        {
                            case Keys.Control:
                                richTextBox.Text += (fileContent + '\n');
                                break;
                            case Keys.Shift:
                                int mousePoint = richTextBox.GetCharIndexFromPosition(richTextBox.PointToClient(new Point(e.X, e.Y)));
                                richTextBox.Select(mousePoint, 0);
                                richTextBox.SelectedText = fileContent;
                                break;
                            default:
                                richTextBox.Text += (fileContent + '\n');
                                break;
                        }
                    }
                    else
                    {
                        throw new Exception ("Only Text Files.");
                    }
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        private void form_DragEnter(object sender, DragEventArgs e)
        {
            /*
             * Check för att kolla om shift eller ctrl på tangentbordet är nedtryckt.
             * Testat på tre olika tangentbord.
             */
            if (e.Data.GetDataPresent(DataFormats.FileDrop) && (Control.ModifierKeys & (Keys.Control | Keys.Shift)) == (Keys.Control | Keys.Shift))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }
    }
}
