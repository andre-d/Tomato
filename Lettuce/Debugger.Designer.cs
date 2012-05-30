namespace Lettuce
{
    partial class Debugger
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
            this.components = new System.ComponentModel.Container();
            Tomato.DCPU dcpu1 = new Tomato.DCPU();
            Tomato.DCPU dcpu2 = new Tomato.DCPU();
            Tomato.DCPU dcpu3 = new Tomato.DCPU();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Debugger));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.checkBoxOnFire = new System.Windows.Forms.CheckBox();
            this.label16 = new System.Windows.Forms.Label();
            this.buttonStepOver = new System.Windows.Forms.Button();
            this.buttonStepInto = new System.Windows.Forms.Button();
            this.checkBoxInterruptQueue = new System.Windows.Forms.CheckBox();
            this.textBoxRegisterPC = new System.Windows.Forms.TextBox();
            this.textBoxRegisterIA = new System.Windows.Forms.TextBox();
            this.textBoxRegisterEX = new System.Windows.Forms.TextBox();
            this.textBoxRegisterSP = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.textBoxRegisterJ = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.textBoxRegisterI = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.textBoxRegisterZ = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.textBoxRegisterC = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textBoxRegisterY = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxRegisterB = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxRegisterX = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxRegisterA = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.checkBoxRunning = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.buttonEditDevice = new System.Windows.Forms.Button();
            this.labelVersion = new System.Windows.Forms.Label();
            this.labelManufacturer = new System.Windows.Forms.Label();
            this.labelHardwareID = new System.Windows.Forms.Label();
            this.listBoxConnectedDevices = new System.Windows.Forms.ListBox();
            this.label17 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.emulationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.debugToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stepIntoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stepOverToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.memoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gotoAddressToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resetToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.loadListingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.organicToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.blueDASToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stackDisplay = new Lettuce.MemoryDisplay();
            this.disassemblyDisplay1 = new Lettuce.DisassemblyDisplay();
            this.rawMemoryDisplay = new Lettuce.MemoryDisplay();
            this.defineValueToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.stackDisplay);
            this.groupBox1.Controls.Add(this.label18);
            this.groupBox1.Controls.Add(this.disassemblyDisplay1);
            this.groupBox1.Controls.Add(this.rawMemoryDisplay);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 27);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(589, 563);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Memory";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(6, 16);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(35, 13);
            this.label18.TabIndex = 6;
            this.label18.Text = "Stack";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 273);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Disassembly";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(126, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Raw Memory";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.checkBoxOnFire);
            this.groupBox2.Controls.Add(this.label16);
            this.groupBox2.Controls.Add(this.buttonStepOver);
            this.groupBox2.Controls.Add(this.buttonStepInto);
            this.groupBox2.Controls.Add(this.checkBoxInterruptQueue);
            this.groupBox2.Controls.Add(this.textBoxRegisterPC);
            this.groupBox2.Controls.Add(this.textBoxRegisterIA);
            this.groupBox2.Controls.Add(this.textBoxRegisterEX);
            this.groupBox2.Controls.Add(this.textBoxRegisterSP);
            this.groupBox2.Controls.Add(this.label15);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.textBoxRegisterJ);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.textBoxRegisterI);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.textBoxRegisterZ);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.textBoxRegisterC);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.textBoxRegisterY);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.textBoxRegisterB);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.textBoxRegisterX);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.textBoxRegisterA);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.checkBoxRunning);
            this.groupBox2.Location = new System.Drawing.Point(607, 27);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(167, 278);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Machine";
            // 
            // checkBoxOnFire
            // 
            this.checkBoxOnFire.AutoSize = true;
            this.checkBoxOnFire.Location = new System.Drawing.Point(6, 229);
            this.checkBoxOnFire.Name = "checkBoxOnFire";
            this.checkBoxOnFire.Size = new System.Drawing.Size(60, 17);
            this.checkBoxOnFire.TabIndex = 30;
            this.checkBoxOnFire.Text = "On Fire";
            this.checkBoxOnFire.UseVisualStyleBackColor = true;
            this.checkBoxOnFire.CheckedChanged += new System.EventHandler(this.checkBoxOnFire_CheckedChanged);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(6, 213);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(104, 13);
            this.label16.TabIndex = 29;
            this.label16.Text = "Queued Interrupts: 0";
            // 
            // buttonStepOver
            // 
            this.buttonStepOver.Location = new System.Drawing.Point(86, 247);
            this.buttonStepOver.Name = "buttonStepOver";
            this.buttonStepOver.Size = new System.Drawing.Size(75, 23);
            this.buttonStepOver.TabIndex = 28;
            this.buttonStepOver.Text = "Step Over";
            this.buttonStepOver.UseVisualStyleBackColor = true;
            this.buttonStepOver.Click += new System.EventHandler(this.buttonStepOver_Click);
            // 
            // buttonStepInto
            // 
            this.buttonStepInto.Location = new System.Drawing.Point(3, 247);
            this.buttonStepInto.Name = "buttonStepInto";
            this.buttonStepInto.Size = new System.Drawing.Size(75, 23);
            this.buttonStepInto.TabIndex = 27;
            this.buttonStepInto.Text = "Step Into";
            this.buttonStepInto.UseVisualStyleBackColor = true;
            this.buttonStepInto.Click += new System.EventHandler(this.buttonStepInto_Click);
            // 
            // checkBoxInterruptQueue
            // 
            this.checkBoxInterruptQueue.AutoSize = true;
            this.checkBoxInterruptQueue.Location = new System.Drawing.Point(6, 193);
            this.checkBoxInterruptQueue.Name = "checkBoxInterruptQueue";
            this.checkBoxInterruptQueue.Size = new System.Drawing.Size(100, 17);
            this.checkBoxInterruptQueue.TabIndex = 26;
            this.checkBoxInterruptQueue.Text = "Interrupt Queue";
            this.checkBoxInterruptQueue.UseVisualStyleBackColor = true;
            // 
            // textBoxRegisterPC
            // 
            this.textBoxRegisterPC.Location = new System.Drawing.Point(24, 144);
            this.textBoxRegisterPC.MaxLength = 4;
            this.textBoxRegisterPC.Name = "textBoxRegisterPC";
            this.textBoxRegisterPC.Size = new System.Drawing.Size(57, 20);
            this.textBoxRegisterPC.TabIndex = 19;
            this.textBoxRegisterPC.Text = "0000";
            this.textBoxRegisterPC.TextChanged += new System.EventHandler(this.textBoxRegisterPC_TextChanged);
            this.textBoxRegisterPC.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxRegisterX_KeyDown);
            // 
            // textBoxRegisterIA
            // 
            this.textBoxRegisterIA.Location = new System.Drawing.Point(104, 167);
            this.textBoxRegisterIA.MaxLength = 4;
            this.textBoxRegisterIA.Name = "textBoxRegisterIA";
            this.textBoxRegisterIA.Size = new System.Drawing.Size(57, 20);
            this.textBoxRegisterIA.TabIndex = 25;
            this.textBoxRegisterIA.Text = "0000";
            this.textBoxRegisterIA.TextChanged += new System.EventHandler(this.textBoxRegisterIA_TextChanged);
            this.textBoxRegisterIA.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxRegisterX_KeyDown);
            // 
            // textBoxRegisterEX
            // 
            this.textBoxRegisterEX.Location = new System.Drawing.Point(104, 144);
            this.textBoxRegisterEX.MaxLength = 4;
            this.textBoxRegisterEX.Name = "textBoxRegisterEX";
            this.textBoxRegisterEX.Size = new System.Drawing.Size(57, 20);
            this.textBoxRegisterEX.TabIndex = 23;
            this.textBoxRegisterEX.Text = "0000";
            this.textBoxRegisterEX.TextChanged += new System.EventHandler(this.textBoxRegisterEX_TextChanged);
            this.textBoxRegisterEX.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxRegisterX_KeyDown);
            // 
            // textBoxRegisterSP
            // 
            this.textBoxRegisterSP.Location = new System.Drawing.Point(24, 167);
            this.textBoxRegisterSP.MaxLength = 4;
            this.textBoxRegisterSP.Name = "textBoxRegisterSP";
            this.textBoxRegisterSP.Size = new System.Drawing.Size(57, 20);
            this.textBoxRegisterSP.TabIndex = 21;
            this.textBoxRegisterSP.Text = "0000";
            this.textBoxRegisterSP.TextChanged += new System.EventHandler(this.textBoxRegisterSP_TextChanged);
            this.textBoxRegisterSP.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxRegisterX_KeyDown);
            // 
            // label15
            // 
            this.label15.Location = new System.Drawing.Point(83, 170);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(23, 17);
            this.label15.TabIndex = 24;
            this.label15.Text = "IA: ";
            // 
            // label14
            // 
            this.label14.Location = new System.Drawing.Point(80, 147);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(36, 17);
            this.label14.TabIndex = 22;
            this.label14.Text = "EX: ";
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(0, 170);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(26, 17);
            this.label13.TabIndex = 20;
            this.label13.Text = "SP: ";
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(0, 147);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(39, 17);
            this.label12.TabIndex = 18;
            this.label12.Text = "PC: ";
            // 
            // textBoxRegisterJ
            // 
            this.textBoxRegisterJ.Location = new System.Drawing.Point(104, 121);
            this.textBoxRegisterJ.MaxLength = 4;
            this.textBoxRegisterJ.Name = "textBoxRegisterJ";
            this.textBoxRegisterJ.Size = new System.Drawing.Size(57, 20);
            this.textBoxRegisterJ.TabIndex = 17;
            this.textBoxRegisterJ.Text = "0000";
            this.textBoxRegisterJ.TextChanged += new System.EventHandler(this.textBoxRegisterJ_TextChanged);
            this.textBoxRegisterJ.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxRegisterX_KeyDown);
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(86, 124);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(20, 17);
            this.label11.TabIndex = 16;
            this.label11.Text = "J: ";
            // 
            // textBoxRegisterI
            // 
            this.textBoxRegisterI.Location = new System.Drawing.Point(24, 121);
            this.textBoxRegisterI.MaxLength = 4;
            this.textBoxRegisterI.Name = "textBoxRegisterI";
            this.textBoxRegisterI.Size = new System.Drawing.Size(57, 20);
            this.textBoxRegisterI.TabIndex = 15;
            this.textBoxRegisterI.Text = "0000";
            this.textBoxRegisterI.TextChanged += new System.EventHandler(this.textBoxRegisterI_TextChanged);
            this.textBoxRegisterI.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxRegisterX_KeyDown);
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(6, 124);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(20, 17);
            this.label10.TabIndex = 14;
            this.label10.Text = "I: ";
            // 
            // textBoxRegisterZ
            // 
            this.textBoxRegisterZ.Location = new System.Drawing.Point(104, 98);
            this.textBoxRegisterZ.MaxLength = 4;
            this.textBoxRegisterZ.Name = "textBoxRegisterZ";
            this.textBoxRegisterZ.Size = new System.Drawing.Size(57, 20);
            this.textBoxRegisterZ.TabIndex = 13;
            this.textBoxRegisterZ.Text = "0000";
            this.textBoxRegisterZ.TextChanged += new System.EventHandler(this.textBoxRegisterZ_TextChanged);
            this.textBoxRegisterZ.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxRegisterX_KeyDown);
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(86, 101);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(20, 17);
            this.label9.TabIndex = 12;
            this.label9.Text = "Z: ";
            // 
            // textBoxRegisterC
            // 
            this.textBoxRegisterC.Location = new System.Drawing.Point(24, 98);
            this.textBoxRegisterC.MaxLength = 4;
            this.textBoxRegisterC.Name = "textBoxRegisterC";
            this.textBoxRegisterC.Size = new System.Drawing.Size(57, 20);
            this.textBoxRegisterC.TabIndex = 11;
            this.textBoxRegisterC.Text = "0000";
            this.textBoxRegisterC.TextChanged += new System.EventHandler(this.textBoxRegisterC_TextChanged);
            this.textBoxRegisterC.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxRegisterX_KeyDown);
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(6, 101);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(20, 17);
            this.label8.TabIndex = 10;
            this.label8.Text = "C: ";
            // 
            // textBoxRegisterY
            // 
            this.textBoxRegisterY.Location = new System.Drawing.Point(104, 75);
            this.textBoxRegisterY.MaxLength = 4;
            this.textBoxRegisterY.Name = "textBoxRegisterY";
            this.textBoxRegisterY.Size = new System.Drawing.Size(57, 20);
            this.textBoxRegisterY.TabIndex = 9;
            this.textBoxRegisterY.Text = "0000";
            this.textBoxRegisterY.TextChanged += new System.EventHandler(this.textBoxRegisterY_TextChanged);
            this.textBoxRegisterY.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxRegisterX_KeyDown);
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(86, 78);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(20, 17);
            this.label7.TabIndex = 8;
            this.label7.Text = "Y: ";
            // 
            // textBoxRegisterB
            // 
            this.textBoxRegisterB.Location = new System.Drawing.Point(24, 75);
            this.textBoxRegisterB.MaxLength = 4;
            this.textBoxRegisterB.Name = "textBoxRegisterB";
            this.textBoxRegisterB.Size = new System.Drawing.Size(57, 20);
            this.textBoxRegisterB.TabIndex = 7;
            this.textBoxRegisterB.Text = "0000";
            this.textBoxRegisterB.TextChanged += new System.EventHandler(this.textBoxRegisterB_TextChanged);
            this.textBoxRegisterB.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxRegisterX_KeyDown);
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(6, 78);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(20, 17);
            this.label6.TabIndex = 6;
            this.label6.Text = "B: ";
            // 
            // textBoxRegisterX
            // 
            this.textBoxRegisterX.Location = new System.Drawing.Point(104, 52);
            this.textBoxRegisterX.MaxLength = 4;
            this.textBoxRegisterX.Name = "textBoxRegisterX";
            this.textBoxRegisterX.Size = new System.Drawing.Size(57, 20);
            this.textBoxRegisterX.TabIndex = 5;
            this.textBoxRegisterX.Text = "0000";
            this.textBoxRegisterX.TextChanged += new System.EventHandler(this.textBoxRegisterX_TextChanged);
            this.textBoxRegisterX.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxRegisterX_KeyDown);
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(86, 55);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(20, 17);
            this.label5.TabIndex = 4;
            this.label5.Text = "X: ";
            // 
            // textBoxRegisterA
            // 
            this.textBoxRegisterA.Location = new System.Drawing.Point(24, 52);
            this.textBoxRegisterA.MaxLength = 4;
            this.textBoxRegisterA.Name = "textBoxRegisterA";
            this.textBoxRegisterA.Size = new System.Drawing.Size(57, 20);
            this.textBoxRegisterA.TabIndex = 3;
            this.textBoxRegisterA.Text = "0000";
            this.textBoxRegisterA.TextChanged += new System.EventHandler(this.textBoxRegisterA_TextChanged);
            this.textBoxRegisterA.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxRegisterX_KeyDown);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(6, 55);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(20, 17);
            this.label4.TabIndex = 2;
            this.label4.Text = "A: ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(6, 39);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Registers";
            // 
            // checkBoxRunning
            // 
            this.checkBoxRunning.AutoSize = true;
            this.checkBoxRunning.Location = new System.Drawing.Point(6, 19);
            this.checkBoxRunning.Name = "checkBoxRunning";
            this.checkBoxRunning.Size = new System.Drawing.Size(66, 17);
            this.checkBoxRunning.TabIndex = 0;
            this.checkBoxRunning.Text = "Running";
            this.checkBoxRunning.UseVisualStyleBackColor = true;
            this.checkBoxRunning.CheckedChanged += new System.EventHandler(this.checkBoxRunning_CheckedChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.buttonEditDevice);
            this.groupBox3.Controls.Add(this.labelVersion);
            this.groupBox3.Controls.Add(this.labelManufacturer);
            this.groupBox3.Controls.Add(this.labelHardwareID);
            this.groupBox3.Controls.Add(this.listBoxConnectedDevices);
            this.groupBox3.Controls.Add(this.label17);
            this.groupBox3.Location = new System.Drawing.Point(607, 311);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(167, 279);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Hardware";
            // 
            // buttonEditDevice
            // 
            this.buttonEditDevice.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonEditDevice.Location = new System.Drawing.Point(9, 246);
            this.buttonEditDevice.Name = "buttonEditDevice";
            this.buttonEditDevice.Size = new System.Drawing.Size(152, 23);
            this.buttonEditDevice.TabIndex = 5;
            this.buttonEditDevice.Text = "Edit Device";
            this.buttonEditDevice.UseVisualStyleBackColor = true;
            this.buttonEditDevice.Click += new System.EventHandler(this.buttonEditDevice_Click);
            // 
            // labelVersion
            // 
            this.labelVersion.AutoSize = true;
            this.labelVersion.Location = new System.Drawing.Point(6, 182);
            this.labelVersion.Name = "labelVersion";
            this.labelVersion.Size = new System.Drawing.Size(45, 13);
            this.labelVersion.TabIndex = 4;
            this.labelVersion.Text = "Version:";
            // 
            // labelManufacturer
            // 
            this.labelManufacturer.AutoSize = true;
            this.labelManufacturer.Location = new System.Drawing.Point(6, 169);
            this.labelManufacturer.Name = "labelManufacturer";
            this.labelManufacturer.Size = new System.Drawing.Size(73, 13);
            this.labelManufacturer.TabIndex = 3;
            this.labelManufacturer.Text = "Manufacturer:";
            // 
            // labelHardwareID
            // 
            this.labelHardwareID.AutoSize = true;
            this.labelHardwareID.Location = new System.Drawing.Point(6, 156);
            this.labelHardwareID.Name = "labelHardwareID";
            this.labelHardwareID.Size = new System.Drawing.Size(73, 13);
            this.labelHardwareID.TabIndex = 2;
            this.labelHardwareID.Text = "Hardware ID: ";
            // 
            // listBoxConnectedDevices
            // 
            this.listBoxConnectedDevices.FormattingEnabled = true;
            this.listBoxConnectedDevices.Location = new System.Drawing.Point(6, 32);
            this.listBoxConnectedDevices.Name = "listBoxConnectedDevices";
            this.listBoxConnectedDevices.Size = new System.Drawing.Size(155, 121);
            this.listBoxConnectedDevices.TabIndex = 1;
            this.listBoxConnectedDevices.SelectedIndexChanged += new System.EventHandler(this.listBoxConnectedDevices_SelectedIndexChanged);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(6, 16);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(104, 13);
            this.label17.TabIndex = 0;
            this.label17.Text = "Connected Devices:";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.emulationToolStripMenuItem,
            this.debugToolStripMenuItem,
            this.memoryToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(783, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // emulationToolStripMenuItem
            // 
            this.emulationToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stopToolStripMenuItem,
            this.resetToolStripMenuItem});
            this.emulationToolStripMenuItem.Name = "emulationToolStripMenuItem";
            this.emulationToolStripMenuItem.Size = new System.Drawing.Size(73, 20);
            this.emulationToolStripMenuItem.Text = "Emulation";
            // 
            // stopToolStripMenuItem
            // 
            this.stopToolStripMenuItem.Name = "stopToolStripMenuItem";
            this.stopToolStripMenuItem.ShortcutKeyDisplayString = "F5";
            this.stopToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.stopToolStripMenuItem.Text = "Stop";
            this.stopToolStripMenuItem.Click += new System.EventHandler(this.checkBoxRunning_CheckedChanged);
            // 
            // resetToolStripMenuItem
            // 
            this.resetToolStripMenuItem.Name = "resetToolStripMenuItem";
            this.resetToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.resetToolStripMenuItem.Text = "Reset";
            this.resetToolStripMenuItem.Click += new System.EventHandler(this.resetToolStripMenuItem_Click);
            // 
            // debugToolStripMenuItem
            // 
            this.debugToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stepIntoToolStripMenuItem,
            this.stepOverToolStripMenuItem,
            this.loadListingToolStripMenuItem,
            this.defineValueToolStripMenuItem});
            this.debugToolStripMenuItem.Name = "debugToolStripMenuItem";
            this.debugToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
            this.debugToolStripMenuItem.Text = "Debug";
            // 
            // stepIntoToolStripMenuItem
            // 
            this.stepIntoToolStripMenuItem.Name = "stepIntoToolStripMenuItem";
            this.stepIntoToolStripMenuItem.ShortcutKeyDisplayString = "F6";
            this.stepIntoToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.stepIntoToolStripMenuItem.Text = "Step Into";
            this.stepIntoToolStripMenuItem.Click += new System.EventHandler(this.buttonStepInto_Click);
            // 
            // stepOverToolStripMenuItem
            // 
            this.stepOverToolStripMenuItem.Name = "stepOverToolStripMenuItem";
            this.stepOverToolStripMenuItem.ShortcutKeyDisplayString = "F7";
            this.stepOverToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.stepOverToolStripMenuItem.Text = "Step Over";
            this.stepOverToolStripMenuItem.Click += new System.EventHandler(this.buttonStepOver_Click);
            // 
            // memoryToolStripMenuItem
            // 
            this.memoryToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gotoAddressToolStripMenuItem,
            this.resetToolStripMenuItem1});
            this.memoryToolStripMenuItem.Name = "memoryToolStripMenuItem";
            this.memoryToolStripMenuItem.Size = new System.Drawing.Size(64, 20);
            this.memoryToolStripMenuItem.Text = "Memory";
            // 
            // gotoAddressToolStripMenuItem
            // 
            this.gotoAddressToolStripMenuItem.Name = "gotoAddressToolStripMenuItem";
            this.gotoAddressToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+G";
            this.gotoAddressToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.gotoAddressToolStripMenuItem.Text = "Goto Address";
            this.gotoAddressToolStripMenuItem.Click += new System.EventHandler(this.gotoAddressToolStripMenuItem_Click);
            // 
            // resetToolStripMenuItem1
            // 
            this.resetToolStripMenuItem1.Name = "resetToolStripMenuItem1";
            this.resetToolStripMenuItem1.Size = new System.Drawing.Size(187, 22);
            this.resetToolStripMenuItem1.Text = "Reset";
            this.resetToolStripMenuItem1.Click += new System.EventHandler(this.resetToolStripMenuItem1_Click);
            // 
            // loadListingToolStripMenuItem
            // 
            this.loadListingToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.organicToolStripMenuItem,
            this.blueDASToolStripMenuItem});
            this.loadListingToolStripMenuItem.Name = "loadListingToolStripMenuItem";
            this.loadListingToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.loadListingToolStripMenuItem.Text = "Load Listing";
            // 
            // organicToolStripMenuItem
            // 
            this.organicToolStripMenuItem.Name = "organicToolStripMenuItem";
            this.organicToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
            this.organicToolStripMenuItem.Text = "Organic";
            this.organicToolStripMenuItem.Click += new System.EventHandler(this.organicToolStripMenuItem_Click);
            // 
            // blueDASToolStripMenuItem
            // 
            this.blueDASToolStripMenuItem.Name = "blueDASToolStripMenuItem";
            this.blueDASToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
            this.blueDASToolStripMenuItem.Text = "BlueDAS";
            // 
            // stackDisplay
            // 
            this.stackDisplay.AsStack = true;
            this.stackDisplay.CPU = dcpu1;
            this.stackDisplay.Font = new System.Drawing.Font("Courier New", 12F);
            this.stackDisplay.Location = new System.Drawing.Point(8, 32);
            this.stackDisplay.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.stackDisplay.Name = "stackDisplay";
            this.stackDisplay.SelectedAddress = ((ushort)(0));
            this.stackDisplay.Size = new System.Drawing.Size(113, 238);
            this.stackDisplay.TabIndex = 7;
            // 
            // disassemblyDisplay1
            // 
            this.disassemblyDisplay1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.disassemblyDisplay1.CPU = dcpu2;
            this.disassemblyDisplay1.EnableUpdates = true;
            this.disassemblyDisplay1.EndAddress = ((ushort)(0));
            this.disassemblyDisplay1.Font = new System.Drawing.Font("Courier New", 12F);
            this.disassemblyDisplay1.Location = new System.Drawing.Point(6, 289);
            this.disassemblyDisplay1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.disassemblyDisplay1.Name = "disassemblyDisplay1";
            this.disassemblyDisplay1.SelectedAddress = ((ushort)(0));
            this.disassemblyDisplay1.Size = new System.Drawing.Size(574, 264);
            this.disassemblyDisplay1.TabIndex = 5;
            // 
            // rawMemoryDisplay
            // 
            this.rawMemoryDisplay.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rawMemoryDisplay.AsStack = false;
            this.rawMemoryDisplay.CPU = dcpu3;
            this.rawMemoryDisplay.Font = new System.Drawing.Font("Courier New", 12F);
            this.rawMemoryDisplay.Location = new System.Drawing.Point(129, 32);
            this.rawMemoryDisplay.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.rawMemoryDisplay.Name = "rawMemoryDisplay";
            this.rawMemoryDisplay.SelectedAddress = ((ushort)(0));
            this.rawMemoryDisplay.Size = new System.Drawing.Size(451, 238);
            this.rawMemoryDisplay.TabIndex = 4;
            // 
            // defineValueToolStripMenuItem
            // 
            this.defineValueToolStripMenuItem.Name = "defineValueToolStripMenuItem";
            this.defineValueToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.defineValueToolStripMenuItem.Text = "Define Value";
            this.defineValueToolStripMenuItem.Click += new System.EventHandler(this.defineValueToolStripMenuItem_Click);
            // 
            // Debugger
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(783, 602);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Debugger";
            this.Text = "Debugger";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Debugger_KeyDown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Button buttonStepOver;
        private System.Windows.Forms.Button buttonStepInto;
        private System.Windows.Forms.CheckBox checkBoxInterruptQueue;
        private System.Windows.Forms.TextBox textBoxRegisterPC;
        private System.Windows.Forms.TextBox textBoxRegisterIA;
        private System.Windows.Forms.TextBox textBoxRegisterEX;
        private System.Windows.Forms.TextBox textBoxRegisterSP;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox textBoxRegisterJ;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox textBoxRegisterI;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBoxRegisterZ;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBoxRegisterC;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBoxRegisterY;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxRegisterB;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxRegisterX;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxRegisterA;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox checkBoxRunning;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button buttonEditDevice;
        private System.Windows.Forms.Label labelVersion;
        private System.Windows.Forms.Label labelManufacturer;
        private System.Windows.Forms.Label labelHardwareID;
        private System.Windows.Forms.ListBox listBoxConnectedDevices;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.CheckBox checkBoxOnFire;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem emulationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stopToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem debugToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stepIntoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stepOverToolStripMenuItem;
        private MemoryDisplay rawMemoryDisplay;
        private System.Windows.Forms.ToolStripMenuItem memoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gotoAddressToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resetToolStripMenuItem1;
        private DisassemblyDisplay disassemblyDisplay1;
        private MemoryDisplay stackDisplay;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.ToolStripMenuItem loadListingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem organicToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem blueDASToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem defineValueToolStripMenuItem;

    }
}