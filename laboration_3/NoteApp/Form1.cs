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
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Text Files (*.txt) | *.txt";
                saveFileDialog.DefaultExt = "txt";
                saveFileDialog.AddExtension = true;

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    using (StreamWriter streamWriter = new StreamWriter(saveFileDialog.FileName))
                    {
                        streamWriter.Write(richTextBox.Text);
                    }

                    openedFile = saveFileDialog.FileName;
                    unsavedText = false;
                    UpdateFormTitle();
                }
            }
            else
            {
                using (StreamWriter streamWriter = new StreamWriter(openedFile))
                {
                    streamWriter.Write(richTextBox.Text);
                }
                unsavedText = false;
                UpdateFormTitle();
            }
        }

        private void richTextBox_TextChanged(object sender, EventArgs e)
        {
            unsavedText = true;
            UpdateFormTitle();
            UpdateStatusStrip();
        }

        private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text Files (*.txt) | *.txt";
            openFileDialog.Title = "Open Text File";

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
        private void UpdateFormTitle()
        {
            if (unsavedText && string.IsNullOrEmpty(openedFile))
            {
                this.Text = "* Untitled.txt"; // Uppdaterar filnamnet i titel. Lägger till * före filnamnet. Kanske borde använda det som standard namn.. återkommer, kanske.
            } 
            else
            {
                this.Text = "*" + Path.GetFileName(openedFile);
            }
        }

        private void UpdateStatusStrip()
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

            if(unsavedText)
            {
                DialogResult dialog = MessageBox.Show("Save changes before exit?", "Unsaved changes", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                if (dialog == DialogResult.Yes)
                {
                    saveToolStripMenuItem_Click(sender, e);
                }
                else if (dialog == DialogResult.No)
                {
                    closeByMenu = true;
                    Application.Exit();
                } else
                {
                    closeByMenu = false;
                    return;
                }
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
                DialogResult dialog = MessageBox.Show("Save changes before exit?", "Unsaved changes", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dialog == DialogResult.Yes)
                {
                    saveToolStripMenuItem_Click(sender, e);
                }
            }
        }
    }
}
