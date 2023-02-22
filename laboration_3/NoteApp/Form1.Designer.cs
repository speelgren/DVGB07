namespace NoteApp
{
    partial class NoteApp
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.richTextBox = new System.Windows.Forms.RichTextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip_counter = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel_withSpaces = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel_withoutSpaces = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel_wordCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel_rowCounter = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1.SuspendLayout();
            this.statusStrip_counter.SuspendLayout();
            this.SuspendLayout();
            // 
            // richTextBox
            // 
            this.richTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox.Location = new System.Drawing.Point(0, 27);
            this.richTextBox.Name = "richTextBox";
            this.richTextBox.Size = new System.Drawing.Size(565, 393);
            this.richTextBox.TabIndex = 2;
            this.richTextBox.Text = "";
            this.richTextBox.TextChanged += new System.EventHandler(this.richTextBox_TextChanged);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(565, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToolStripMenuItem,
            this.openFileToolStripMenuItem,
            this.clearFileToolStripMenuItem,
            this.exitFileToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.saveToolStripMenuItem.Text = "Save File";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // openFileToolStripMenuItem
            // 
            this.openFileToolStripMenuItem.Name = "openFileToolStripMenuItem";
            this.openFileToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.openFileToolStripMenuItem.Text = "Open File";
            this.openFileToolStripMenuItem.Click += new System.EventHandler(this.openFileToolStripMenuItem_Click);
            // 
            // clearFileToolStripMenuItem
            // 
            this.clearFileToolStripMenuItem.Name = "clearFileToolStripMenuItem";
            this.clearFileToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.clearFileToolStripMenuItem.Text = "Clear File";
            this.clearFileToolStripMenuItem.Click += new System.EventHandler(this.clearFileToolStripMenuItem_Click);
            // 
            // exitFileToolStripMenuItem
            // 
            this.exitFileToolStripMenuItem.Name = "exitFileToolStripMenuItem";
            this.exitFileToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.exitFileToolStripMenuItem.Text = "Exit File";
            this.exitFileToolStripMenuItem.Click += new System.EventHandler(this.exitFileToolStripMenuItem_Click);
            // 
            // statusStrip_counter
            // 
            this.statusStrip_counter.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel_withSpaces,
            this.toolStripStatusLabel_withoutSpaces,
            this.toolStripStatusLabel_wordCount,
            this.toolStripStatusLabel_rowCounter});
            this.statusStrip_counter.Location = new System.Drawing.Point(0, 421);
            this.statusStrip_counter.Name = "statusStrip_counter";
            this.statusStrip_counter.Size = new System.Drawing.Size(565, 22);
            this.statusStrip_counter.TabIndex = 4;
            this.statusStrip_counter.Text = "statusStrip_counter";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            // 
            // toolStripStatusLabel_withSpaces
            // 
            this.toolStripStatusLabel_withSpaces.Name = "toolStripStatusLabel_withSpaces";
            this.toolStripStatusLabel_withSpaces.Size = new System.Drawing.Size(60, 17);
            this.toolStripStatusLabel_withSpaces.Text = "w spaces: ";
            // 
            // toolStripStatusLabel_withoutSpaces
            // 
            this.toolStripStatusLabel_withoutSpaces.Name = "toolStripStatusLabel_withoutSpaces";
            this.toolStripStatusLabel_withoutSpaces.Size = new System.Drawing.Size(72, 17);
            this.toolStripStatusLabel_withoutSpaces.Text = "w/o spaces: ";
            // 
            // toolStripStatusLabel_wordCount
            // 
            this.toolStripStatusLabel_wordCount.Name = "toolStripStatusLabel_wordCount";
            this.toolStripStatusLabel_wordCount.Size = new System.Drawing.Size(74, 17);
            this.toolStripStatusLabel_wordCount.Text = "word count: ";
            // 
            // toolStripStatusLabel_rowCounter
            // 
            this.toolStripStatusLabel_rowCounter.Name = "toolStripStatusLabel_rowCounter";
            this.toolStripStatusLabel_rowCounter.Size = new System.Drawing.Size(67, 17);
            this.toolStripStatusLabel_rowCounter.Text = "row count: ";
            // 
            // NoteApp
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(565, 443);
            this.Controls.Add(this.statusStrip_counter);
            this.Controls.Add(this.richTextBox);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(328, 398);
            this.Name = "NoteApp";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "NoteApp";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.form_Closed);
            this.Load += new System.EventHandler(this.form_onLoad);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Form_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Form_DragEnter);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip_counter.ResumeLayout(false);
            this.statusStrip_counter.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.RichTextBox richTextBox;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitFileToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip_counter;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel_withSpaces;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel_withoutSpaces;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel_wordCount;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel_rowCounter;
    }
}

