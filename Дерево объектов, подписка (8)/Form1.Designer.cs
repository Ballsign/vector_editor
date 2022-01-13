namespace Дерево_объектов__подписка__8_ {
    partial class Form1 {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btLine = new System.Windows.Forms.Button();
            this.btCircle = new System.Windows.Forms.Button();
            this.btRectangle = new System.Windows.Forms.Button();
            this.btTriangle = new System.Windows.Forms.Button();
            this.btCursor = new System.Windows.Forms.Button();
            this.btColor = new System.Windows.Forms.Button();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.btSave = new System.Windows.Forms.Button();
            this.btLoad = new System.Windows.Forms.Button();
            this.btGroup = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.btUngroup = new System.Windows.Forms.Button();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.chSticky = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.Location = new System.Drawing.Point(147, 65);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(930, 451);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
            // 
            // btLine
            // 
            this.btLine.Image = ((System.Drawing.Image)(resources.GetObject("btLine.Image")));
            this.btLine.Location = new System.Drawing.Point(12, 12);
            this.btLine.Name = "btLine";
            this.btLine.Size = new System.Drawing.Size(25, 25);
            this.btLine.TabIndex = 1;
            this.btLine.UseVisualStyleBackColor = true;
            this.btLine.Click += new System.EventHandler(this.btLine_Click);
            // 
            // btCircle
            // 
            this.btCircle.Image = ((System.Drawing.Image)(resources.GetObject("btCircle.Image")));
            this.btCircle.Location = new System.Drawing.Point(38, 12);
            this.btCircle.Name = "btCircle";
            this.btCircle.Size = new System.Drawing.Size(25, 25);
            this.btCircle.TabIndex = 2;
            this.btCircle.UseVisualStyleBackColor = true;
            this.btCircle.Click += new System.EventHandler(this.btCircle_Click);
            // 
            // btRectangle
            // 
            this.btRectangle.Image = ((System.Drawing.Image)(resources.GetObject("btRectangle.Image")));
            this.btRectangle.Location = new System.Drawing.Point(64, 12);
            this.btRectangle.Name = "btRectangle";
            this.btRectangle.Size = new System.Drawing.Size(25, 25);
            this.btRectangle.TabIndex = 3;
            this.btRectangle.UseVisualStyleBackColor = true;
            this.btRectangle.Click += new System.EventHandler(this.btRectangle_Click);
            // 
            // btTriangle
            // 
            this.btTriangle.Image = ((System.Drawing.Image)(resources.GetObject("btTriangle.Image")));
            this.btTriangle.Location = new System.Drawing.Point(90, 12);
            this.btTriangle.Name = "btTriangle";
            this.btTriangle.Size = new System.Drawing.Size(25, 25);
            this.btTriangle.TabIndex = 4;
            this.btTriangle.UseVisualStyleBackColor = true;
            this.btTriangle.Click += new System.EventHandler(this.btTriangle_Click);
            // 
            // btCursor
            // 
            this.btCursor.Image = ((System.Drawing.Image)(resources.GetObject("btCursor.Image")));
            this.btCursor.Location = new System.Drawing.Point(116, 12);
            this.btCursor.Name = "btCursor";
            this.btCursor.Size = new System.Drawing.Size(25, 25);
            this.btCursor.TabIndex = 5;
            this.btCursor.UseVisualStyleBackColor = true;
            this.btCursor.Click += new System.EventHandler(this.btCursor_Click);
            // 
            // btColor
            // 
            this.btColor.Location = new System.Drawing.Point(201, 12);
            this.btColor.Name = "btColor";
            this.btColor.Size = new System.Drawing.Size(25, 25);
            this.btColor.TabIndex = 6;
            this.btColor.UseVisualStyleBackColor = true;
            this.btColor.Click += new System.EventHandler(this.btColor_Click);
            // 
            // btSave
            // 
            this.btSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btSave.Location = new System.Drawing.Point(913, 9);
            this.btSave.Name = "btSave";
            this.btSave.Size = new System.Drawing.Size(75, 23);
            this.btSave.TabIndex = 7;
            this.btSave.Text = "Сохранить";
            this.btSave.UseVisualStyleBackColor = true;
            this.btSave.Click += new System.EventHandler(this.btSave_Click);
            // 
            // btLoad
            // 
            this.btLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btLoad.Location = new System.Drawing.Point(994, 9);
            this.btLoad.Name = "btLoad";
            this.btLoad.Size = new System.Drawing.Size(75, 23);
            this.btLoad.TabIndex = 8;
            this.btLoad.Text = "Загрузить";
            this.btLoad.UseVisualStyleBackColor = true;
            this.btLoad.Click += new System.EventHandler(this.btLoad_Click);
            // 
            // btGroup
            // 
            this.btGroup.Location = new System.Drawing.Point(412, 9);
            this.btGroup.Name = "btGroup";
            this.btGroup.Size = new System.Drawing.Size(107, 23);
            this.btGroup.TabIndex = 9;
            this.btGroup.Text = "Сгруппировать";
            this.btGroup.UseVisualStyleBackColor = true;
            this.btGroup.Click += new System.EventHandler(this.btGroup_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            this.saveFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFileDialog1_FileOk);
            // 
            // btUngroup
            // 
            this.btUngroup.Location = new System.Drawing.Point(525, 9);
            this.btUngroup.Name = "btUngroup";
            this.btUngroup.Size = new System.Drawing.Size(113, 23);
            this.btUngroup.TabIndex = 10;
            this.btUngroup.Text = "Разгруппировать";
            this.btUngroup.UseVisualStyleBackColor = true;
            this.btUngroup.Click += new System.EventHandler(this.btUngroup_Click);
            // 
            // treeView1
            // 
            this.treeView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.treeView1.Location = new System.Drawing.Point(12, 65);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(129, 451);
            this.treeView1.TabIndex = 11;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            this.treeView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.treeView1_KeyDown);
            // 
            // chSticky
            // 
            this.chSticky.AutoSize = true;
            this.chSticky.Location = new System.Drawing.Point(748, 12);
            this.chSticky.Name = "chSticky";
            this.chSticky.Size = new System.Drawing.Size(68, 19);
            this.chSticky.TabIndex = 12;
            this.chSticky.Text = "Липкий";
            this.chSticky.UseVisualStyleBackColor = true;
            this.chSticky.CheckedChanged += new System.EventHandler(this.chSticky_CheckedChanged);
            this.chSticky.Click += new System.EventHandler(this.chSticky_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1081, 519);
            this.Controls.Add(this.chSticky);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.btUngroup);
            this.Controls.Add(this.btGroup);
            this.Controls.Add(this.btLoad);
            this.Controls.Add(this.btSave);
            this.Controls.Add(this.btColor);
            this.Controls.Add(this.btCursor);
            this.Controls.Add(this.btTriangle);
            this.Controls.Add(this.btRectangle);
            this.Controls.Add(this.btCircle);
            this.Controls.Add(this.btLine);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResizeEnd += new System.EventHandler(this.Form1_ResizeEnd);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private PictureBox pictureBox1;
        private Button btLine;
        private Button btCircle;
        private Button btRectangle;
        private Button btTriangle;
        private Button btCursor;
        private Button btColor;
        private ColorDialog colorDialog1;
        private Button btSave;
        private Button btLoad;
        private Button btGroup;
        private OpenFileDialog openFileDialog1;
        private SaveFileDialog saveFileDialog1;
        private Button btUngroup;
        private TreeView treeView1;
        private CheckBox chSticky;
    }
}