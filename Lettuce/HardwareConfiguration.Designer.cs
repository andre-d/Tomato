namespace Lettuce
{
    partial class HardwareConfiguration
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.hardwareSelectionListBox = new System.Windows.Forms.CheckedListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.setAsDefaultCheckBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(177, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Hardware Configuration";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(12, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(264, 41);
            this.label2.TabIndex = 1;
            this.label2.Text = "You can choose what hardware to enable here.  Start Lettuce with --hw [hardware i" +
    "d] if you wish to skip this step.";
            // 
            // hardwareSelectionListBox
            // 
            this.hardwareSelectionListBox.FormattingEnabled = true;
            this.hardwareSelectionListBox.Location = new System.Drawing.Point(12, 73);
            this.hardwareSelectionListBox.Name = "hardwareSelectionListBox";
            this.hardwareSelectionListBox.Size = new System.Drawing.Size(264, 139);
            this.hardwareSelectionListBox.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(198, 218);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // setAsDefaultCheckBox
            // 
            this.setAsDefaultCheckBox.AutoSize = true;
            this.setAsDefaultCheckBox.Location = new System.Drawing.Point(12, 222);
            this.setAsDefaultCheckBox.Name = "setAsDefaultCheckBox";
            this.setAsDefaultCheckBox.Size = new System.Drawing.Size(93, 17);
            this.setAsDefaultCheckBox.TabIndex = 4;
            this.setAsDefaultCheckBox.Text = "Set as Default";
            this.setAsDefaultCheckBox.UseVisualStyleBackColor = true;
            // 
            // HardwareConfiguration
            // 
            this.AcceptButton = this.button1;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(285, 249);
            this.Controls.Add(this.setAsDefaultCheckBox);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.hardwareSelectionListBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "HardwareConfiguration";
            this.Text = "Configuration";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckedListBox hardwareSelectionListBox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox setAsDefaultCheckBox;
    }
}

