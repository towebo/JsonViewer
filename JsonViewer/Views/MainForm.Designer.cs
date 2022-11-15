namespace JsonViewer
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.JsonTree = new System.Windows.Forms.TreeView();
            this.MainMnu = new System.Windows.Forms.MenuStrip();
            this.FileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshFromClipboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openJsonFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeAppToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenJsonFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.MainMnu.SuspendLayout();
            this.SuspendLayout();
            // 
            // JsonTree
            // 
            this.JsonTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.JsonTree.Location = new System.Drawing.Point(0, 24);
            this.JsonTree.Name = "JsonTree";
            this.JsonTree.Size = new System.Drawing.Size(800, 426);
            this.JsonTree.TabIndex = 0;
            // 
            // MainMnu
            // 
            this.MainMnu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileMenuItem});
            this.MainMnu.Location = new System.Drawing.Point(0, 0);
            this.MainMnu.Name = "MainMnu";
            this.MainMnu.Size = new System.Drawing.Size(800, 24);
            this.MainMnu.TabIndex = 1;
            this.MainMnu.Text = "Main Menu";
            // 
            // FileMenuItem
            // 
            this.FileMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.refreshFromClipboardToolStripMenuItem,
            this.openJsonFileToolStripMenuItem,
            this.closeAppToolStripMenuItem});
            this.FileMenuItem.Name = "FileMenuItem";
            this.FileMenuItem.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.FileMenuItem.Size = new System.Drawing.Size(37, 20);
            this.FileMenuItem.Text = "&File";
            // 
            // refreshFromClipboardToolStripMenuItem
            // 
            this.refreshFromClipboardToolStripMenuItem.Name = "refreshFromClipboardToolStripMenuItem";
            this.refreshFromClipboardToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.refreshFromClipboardToolStripMenuItem.Size = new System.Drawing.Size(218, 22);
            this.refreshFromClipboardToolStripMenuItem.Text = "&Refresh From Clipboard";
            this.refreshFromClipboardToolStripMenuItem.Click += new System.EventHandler(this.refreshFromClipboardToolStripMenuItem_Click);
            // 
            // openJsonFileToolStripMenuItem
            // 
            this.openJsonFileToolStripMenuItem.Name = "openJsonFileToolStripMenuItem";
            this.openJsonFileToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openJsonFileToolStripMenuItem.Size = new System.Drawing.Size(218, 22);
            this.openJsonFileToolStripMenuItem.Text = "&Open Json File";
            this.openJsonFileToolStripMenuItem.Click += new System.EventHandler(this.openJsonFileToolStripMenuItem_Click);
            // 
            // closeAppToolStripMenuItem
            // 
            this.closeAppToolStripMenuItem.Name = "closeAppToolStripMenuItem";
            this.closeAppToolStripMenuItem.Size = new System.Drawing.Size(218, 22);
            this.closeAppToolStripMenuItem.Text = "&Close";
            this.closeAppToolStripMenuItem.Click += new System.EventHandler(this.closeAppToolStripMenuItem_Click);
            // 
            // OpenJsonFileDialog
            // 
            this.OpenJsonFileDialog.DefaultExt = "json";
            this.OpenJsonFileDialog.Filter = "Json Files (*.json)|*.json|All Files (*.)|*.*";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.JsonTree);
            this.Controls.Add(this.MainMnu);
            this.MainMenuStrip = this.MainMnu;
            this.Name = "MainForm";
            this.Text = "JsonViewer";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.MainMnu.ResumeLayout(false);
            this.MainMnu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TreeView JsonTree;
        private MenuStrip MainMnu;
        private ToolStripMenuItem FileMenuItem;
        private ToolStripMenuItem closeAppToolStripMenuItem;
        private ToolStripMenuItem refreshFromClipboardToolStripMenuItem;
        private OpenFileDialog OpenJsonFileDialog;
        private ToolStripMenuItem openJsonFileToolStripMenuItem;
    }
}