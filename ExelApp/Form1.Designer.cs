
namespace ExelApp
{
    partial class Excel
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.File = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutProgramToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.enterButton = new System.Windows.Forms.Button();
            this.textBox = new System.Windows.Forms.TextBox();
            this.label_Row = new System.Windows.Forms.Label();
            this.addRowBtn = new System.Windows.Forms.Button();
            this.deleteRowBtn = new System.Windows.Forms.Button();
            this.labelColumn = new System.Windows.Forms.Label();
            this.addColumnBtn = new System.Windows.Forms.Button();
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            this.deleteColumnBtn = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.Control;
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.File,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(920, 33);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // File
            // 
            this.File.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToolStripMenuItem,
            this.openToolStripMenuItem});
            this.File.Name = "File";
            this.File.Size = new System.Drawing.Size(54, 29);
            this.File.Text = "File";
            this.File.Click += new System.EventHandler(this.bjToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(270, 34);
            this.saveToolStripMenuItem.Text = "Save as...";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(270, 34);
            this.openToolStripMenuItem.Text = "Open...";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutProgramToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(65, 29);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutProgramToolStripMenuItem
            // 
            this.aboutProgramToolStripMenuItem.Name = "aboutProgramToolStripMenuItem";
            this.aboutProgramToolStripMenuItem.Size = new System.Drawing.Size(238, 34);
            this.aboutProgramToolStripMenuItem.Text = "About Program";
            this.aboutProgramToolStripMenuItem.Click += new System.EventHandler(this.aboutProgramToolStripMenuItem_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ActiveBorder;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(0, 40);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 62;
            this.dataGridView1.RowTemplate.Height = 28;
            this.dataGridView1.Size = new System.Drawing.Size(931, 515);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // enterButton
            // 
            this.enterButton.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.enterButton.Location = new System.Drawing.Point(432, 2);
            this.enterButton.Name = "enterButton";
            this.enterButton.Size = new System.Drawing.Size(72, 32);
            this.enterButton.TabIndex = 3;
            this.enterButton.Text = "Enter";
            this.enterButton.UseVisualStyleBackColor = true;
            this.enterButton.Click += new System.EventHandler(this.EnterBtn_Click);
            // 
            // textBox
            // 
            this.textBox.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.textBox.Location = new System.Drawing.Point(149, 6);
            this.textBox.Name = "textBox";
            this.textBox.Size = new System.Drawing.Size(268, 26);
            this.textBox.TabIndex = 4;
            this.textBox.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // label_Row
            // 
            this.label_Row.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label_Row.AutoSize = true;
            this.label_Row.Location = new System.Drawing.Point(553, 9);
            this.label_Row.Name = "label_Row";
            this.label_Row.Size = new System.Drawing.Size(41, 20);
            this.label_Row.TabIndex = 5;
            this.label_Row.Text = "Row";
            // 
            // addRowBtn
            // 
            this.addRowBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.addRowBtn.Location = new System.Drawing.Point(600, 6);
            this.addRowBtn.Name = "addRowBtn";
            this.addRowBtn.Size = new System.Drawing.Size(28, 28);
            this.addRowBtn.TabIndex = 6;
            this.addRowBtn.Text = "+";
            this.addRowBtn.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.addRowBtn.UseVisualStyleBackColor = true;
            this.addRowBtn.Click += new System.EventHandler(this.addRowBtn_Click);
            // 
            // deleteRowBtn
            // 
            this.deleteRowBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.deleteRowBtn.Location = new System.Drawing.Point(634, 6);
            this.deleteRowBtn.Name = "deleteRowBtn";
            this.deleteRowBtn.Size = new System.Drawing.Size(28, 28);
            this.deleteRowBtn.TabIndex = 7;
            this.deleteRowBtn.Text = "-";
            this.deleteRowBtn.UseVisualStyleBackColor = true;
            this.deleteRowBtn.Click += new System.EventHandler(this.deleteRowBtn_Click);
            // 
            // labelColumn
            // 
            this.labelColumn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelColumn.AutoSize = true;
            this.labelColumn.Location = new System.Drawing.Point(680, 10);
            this.labelColumn.Name = "labelColumn";
            this.labelColumn.Size = new System.Drawing.Size(63, 20);
            this.labelColumn.TabIndex = 8;
            this.labelColumn.Text = "Column";
            // 
            // addColumnBtn
            // 
            this.addColumnBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.addColumnBtn.Location = new System.Drawing.Point(749, 5);
            this.addColumnBtn.Name = "addColumnBtn";
            this.addColumnBtn.Size = new System.Drawing.Size(28, 28);
            this.addColumnBtn.TabIndex = 9;
            this.addColumnBtn.Text = "+";
            this.addColumnBtn.UseVisualStyleBackColor = true;
            this.addColumnBtn.Click += new System.EventHandler(this.addColumnBtn_Click);
            // 
            // deleteColumnBtn
            // 
            this.deleteColumnBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.deleteColumnBtn.Location = new System.Drawing.Point(783, 5);
            this.deleteColumnBtn.Name = "deleteColumnBtn";
            this.deleteColumnBtn.Size = new System.Drawing.Size(28, 28);
            this.deleteColumnBtn.TabIndex = 10;
            this.deleteColumnBtn.Text = "-";
            this.deleteColumnBtn.UseVisualStyleBackColor = true;
            this.deleteColumnBtn.Click += new System.EventHandler(this.deleteColumnBtn_Click);
            // 
            // Excel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(920, 554);
            this.Controls.Add(this.deleteColumnBtn);
            this.Controls.Add(this.addColumnBtn);
            this.Controls.Add(this.labelColumn);
            this.Controls.Add(this.deleteRowBtn);
            this.Controls.Add(this.addRowBtn);
            this.Controls.Add(this.label_Row);
            this.Controls.Add(this.textBox);
            this.Controls.Add(this.enterButton);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(942, 442);
            this.Name = "Excel";
            this.Text = "Excel";
            this.Load += new System.EventHandler(this.Exel_Load);
            this.Click += new System.EventHandler(this.FormClick);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormKeyDown);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem File;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ToolStripMenuItem aboutProgramToolStripMenuItem;
        private System.Windows.Forms.Button enterButton;
        private System.Windows.Forms.TextBox textBox;
        private System.Windows.Forms.Label label_Row;
        private System.Windows.Forms.Button addRowBtn;
        private System.Windows.Forms.Button deleteRowBtn;
        private System.Windows.Forms.Label labelColumn;
        private System.Windows.Forms.Button addColumnBtn;
        private System.Windows.Forms.HelpProvider helpProvider1;
        private System.Windows.Forms.Button deleteColumnBtn;
    }
}

