namespace PikachuGame
{
    partial class frmTopcs
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTopcs));
            this.dgTop = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgTop)).BeginInit();
            this.SuspendLayout();
            // 
            // dgTop
            // 
            this.dgTop.AllowUserToAddRows = false;
            this.dgTop.AllowUserToDeleteRows = false;
            this.dgTop.AllowUserToResizeRows = false;
            this.dgTop.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgTop.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgTop.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3});
            this.dgTop.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dgTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgTop.Location = new System.Drawing.Point(0, 0);
            this.dgTop.Name = "dgTop";
            this.dgTop.Size = new System.Drawing.Size(376, 192);
            this.dgTop.TabIndex = 0;
            this.dgTop.TabStop = false;
            this.dgTop.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgTop_CellContentClick);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Tên";
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Tuổi";
            this.Column2.Name = "Column2";
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Điểm";
            this.Column3.Name = "Column3";
            // 
            // frmTopcs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(376, 192);
            this.Controls.Add(this.dgTop);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmTopcs";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Danh sách cao thủ";
            this.Load += new System.EventHandler(this.frmTopcs_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgTop)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgTop;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
    }
}