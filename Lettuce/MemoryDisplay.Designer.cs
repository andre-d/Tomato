namespace Lettuce
{
    partial class MemoryDisplay
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.editValueToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gotoAddressToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editValueToolStripMenuItem,
            this.gotoAddressToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(153, 70);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // editValueToolStripMenuItem
            // 
            this.editValueToolStripMenuItem.Name = "editValueToolStripMenuItem";
            this.editValueToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.editValueToolStripMenuItem.Text = "Edit Value";
            this.editValueToolStripMenuItem.Click += new System.EventHandler(this.editValueToolStripMenuItem_Click);
            // 
            // gotoAddressToolStripMenuItem
            // 
            this.gotoAddressToolStripMenuItem.Name = "gotoAddressToolStripMenuItem";
            this.gotoAddressToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.gotoAddressToolStripMenuItem.Text = "Goto Address";
            this.gotoAddressToolStripMenuItem.Click += new System.EventHandler(this.gotoAddressToolStripMenuItem_Click);
            // 
            // MemoryDisplay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.DoubleBuffered = true;
            this.Name = "MemoryDisplay";
            this.Size = new System.Drawing.Size(401, 170);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.MemoryDisplay_Paint);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.MemoryDisplay_MouseClick);
            this.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.MemoryDisplay_MouseDoubleClick);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem editValueToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gotoAddressToolStripMenuItem;

    }
}
