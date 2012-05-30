namespace Lettuce.HardwareControllers
{
    partial class LEM1802Controller
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
            this.deviceIndexLabel = new System.Windows.Forms.Label();
            this.hardwareIdLabel = new System.Windows.Forms.Label();
            this.manufacturerIdLabel = new System.Windows.Forms.Label();
            this.versionLabel = new System.Windows.Forms.Label();
            this.lcdMapTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.fontMapTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.paletteMapTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.borderColorTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "LEM 1802";
            // 
            // deviceIndexLabel
            // 
            this.deviceIndexLabel.AutoSize = true;
            this.deviceIndexLabel.Location = new System.Drawing.Point(13, 29);
            this.deviceIndexLabel.Name = "deviceIndexLabel";
            this.deviceIndexLabel.Size = new System.Drawing.Size(76, 13);
            this.deviceIndexLabel.TabIndex = 1;
            this.deviceIndexLabel.Text = "Device Index: ";
            // 
            // hardwareIdLabel
            // 
            this.hardwareIdLabel.AutoSize = true;
            this.hardwareIdLabel.Location = new System.Drawing.Point(13, 42);
            this.hardwareIdLabel.Name = "hardwareIdLabel";
            this.hardwareIdLabel.Size = new System.Drawing.Size(73, 13);
            this.hardwareIdLabel.TabIndex = 2;
            this.hardwareIdLabel.Text = "Hardware ID: ";
            // 
            // manufacturerIdLabel
            // 
            this.manufacturerIdLabel.AutoSize = true;
            this.manufacturerIdLabel.Location = new System.Drawing.Point(13, 55);
            this.manufacturerIdLabel.Name = "manufacturerIdLabel";
            this.manufacturerIdLabel.Size = new System.Drawing.Size(90, 13);
            this.manufacturerIdLabel.TabIndex = 3;
            this.manufacturerIdLabel.Text = "Manufacturer ID: ";
            // 
            // versionLabel
            // 
            this.versionLabel.AutoSize = true;
            this.versionLabel.Location = new System.Drawing.Point(13, 68);
            this.versionLabel.Name = "versionLabel";
            this.versionLabel.Size = new System.Drawing.Size(48, 13);
            this.versionLabel.TabIndex = 4;
            this.versionLabel.Text = "Version: ";
            // 
            // lcdMapTextBox
            // 
            this.lcdMapTextBox.Location = new System.Drawing.Point(87, 92);
            this.lcdMapTextBox.MaxLength = 4;
            this.lcdMapTextBox.Name = "lcdMapTextBox";
            this.lcdMapTextBox.Size = new System.Drawing.Size(131, 20);
            this.lcdMapTextBox.TabIndex = 5;
            this.lcdMapTextBox.TextChanged += new System.EventHandler(this.lcdMapTextBox_TextChanged);
            this.lcdMapTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxRegisterX_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 95);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "LCD Map:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 121);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Font Map:";
            // 
            // fontMapTextBox
            // 
            this.fontMapTextBox.Location = new System.Drawing.Point(87, 118);
            this.fontMapTextBox.MaxLength = 4;
            this.fontMapTextBox.Name = "fontMapTextBox";
            this.fontMapTextBox.Size = new System.Drawing.Size(131, 20);
            this.fontMapTextBox.TabIndex = 7;
            this.fontMapTextBox.TextChanged += new System.EventHandler(this.fontMapTextBox_TextChanged);
            this.fontMapTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxRegisterX_KeyDown);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 147);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Palette Map:";
            // 
            // paletteMapTextBox
            // 
            this.paletteMapTextBox.Location = new System.Drawing.Point(87, 144);
            this.paletteMapTextBox.MaxLength = 4;
            this.paletteMapTextBox.Name = "paletteMapTextBox";
            this.paletteMapTextBox.Size = new System.Drawing.Size(131, 20);
            this.paletteMapTextBox.TabIndex = 9;
            this.paletteMapTextBox.TextChanged += new System.EventHandler(this.paletteMapTextBox_TextChanged);
            this.paletteMapTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxRegisterX_KeyDown);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 173);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Border Color:";
            // 
            // borderColorTextBox
            // 
            this.borderColorTextBox.Location = new System.Drawing.Point(87, 170);
            this.borderColorTextBox.MaxLength = 4;
            this.borderColorTextBox.Name = "borderColorTextBox";
            this.borderColorTextBox.Size = new System.Drawing.Size(131, 20);
            this.borderColorTextBox.TabIndex = 11;
            this.borderColorTextBox.TextChanged += new System.EventHandler(this.borderColorTextBox_TextChanged);
            this.borderColorTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxRegisterX_KeyDown);
            // 
            // LEM1802Controller
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(230, 206);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.borderColorTextBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.paletteMapTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.fontMapTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lcdMapTextBox);
            this.Controls.Add(this.versionLabel);
            this.Controls.Add(this.manufacturerIdLabel);
            this.Controls.Add(this.hardwareIdLabel);
            this.Controls.Add(this.deviceIndexLabel);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "LEM1802Controller";
            this.Text = "LEM1802 Controller";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label deviceIndexLabel;
        private System.Windows.Forms.Label hardwareIdLabel;
        private System.Windows.Forms.Label manufacturerIdLabel;
        private System.Windows.Forms.Label versionLabel;
        private System.Windows.Forms.TextBox lcdMapTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox fontMapTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox paletteMapTextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox borderColorTextBox;
    }
}