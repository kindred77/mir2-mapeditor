namespace Map_Editor
{
    partial class MapConverter
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
            this.BackLibConvert_DGV = new System.Windows.Forms.DataGridView();
            this.BackLibIndex = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TargetLibFile = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ConvertToIndex = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IfExportBackImage = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.MiddleLibConvert_DGV = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IfExportMiddleImage = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.FrontLibConvert_DGV = new System.Windows.Forms.DataGridView();
            this.DoorLibConvert_DGV = new System.Windows.Forms.DataGridView();
            this.ApplyTo_BTN = new System.Windows.Forms.Button();
            this.SaveTo_BTN = new System.Windows.Forms.Button();
            this.BatchTilesProc_BTN = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IfExportObjectImage = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IfExportDoorImage = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.BackLibConvert_DGV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MiddleLibConvert_DGV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FrontLibConvert_DGV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DoorLibConvert_DGV)).BeginInit();
            this.SuspendLayout();
            // 
            // BackLibConvert_DGV
            // 
            this.BackLibConvert_DGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.BackLibConvert_DGV.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.BackLibIndex,
            this.TargetLibFile,
            this.ConvertToIndex,
            this.IfExportBackImage});
            this.BackLibConvert_DGV.Location = new System.Drawing.Point(71, 12);
            this.BackLibConvert_DGV.Name = "BackLibConvert_DGV";
            this.BackLibConvert_DGV.RowTemplate.Height = 23;
            this.BackLibConvert_DGV.Size = new System.Drawing.Size(442, 131);
            this.BackLibConvert_DGV.TabIndex = 0;
            this.BackLibConvert_DGV.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.DGV_CellBeginEdit);
            // 
            // BackLibIndex
            // 
            this.BackLibIndex.HeaderText = "原始libIndex";
            this.BackLibIndex.Name = "BackLibIndex";
            // 
            // TargetLibFile
            // 
            this.TargetLibFile.HeaderText = "指定Lib文件";
            this.TargetLibFile.Name = "TargetLibFile";
            // 
            // ConvertToIndex
            // 
            this.ConvertToIndex.HeaderText = "转换后的LibIndex";
            this.ConvertToIndex.Name = "ConvertToIndex";
            // 
            // IfExportBackImage
            // 
            this.IfExportBackImage.HeaderText = "图片提取";
            this.IfExportBackImage.Name = "IfExportBackImage";
            // 
            // MiddleLibConvert_DGV
            // 
            this.MiddleLibConvert_DGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.MiddleLibConvert_DGV.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.IfExportMiddleImage});
            this.MiddleLibConvert_DGV.Location = new System.Drawing.Point(71, 158);
            this.MiddleLibConvert_DGV.Name = "MiddleLibConvert_DGV";
            this.MiddleLibConvert_DGV.RowTemplate.Height = 23;
            this.MiddleLibConvert_DGV.Size = new System.Drawing.Size(442, 131);
            this.MiddleLibConvert_DGV.TabIndex = 1;
            this.MiddleLibConvert_DGV.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.DGV_CellBeginEdit);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "原始libIndex";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "指定Lib文件";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "转换后的LibIndex";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            // 
            // IfExportMiddleImage
            // 
            this.IfExportMiddleImage.HeaderText = "图片提取";
            this.IfExportMiddleImage.Name = "IfExportMiddleImage";
            // 
            // FrontLibConvert_DGV
            // 
            this.FrontLibConvert_DGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.FrontLibConvert_DGV.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn6,
            this.IfExportObjectImage});
            this.FrontLibConvert_DGV.Location = new System.Drawing.Point(71, 307);
            this.FrontLibConvert_DGV.Name = "FrontLibConvert_DGV";
            this.FrontLibConvert_DGV.RowTemplate.Height = 23;
            this.FrontLibConvert_DGV.Size = new System.Drawing.Size(442, 131);
            this.FrontLibConvert_DGV.TabIndex = 2;
            this.FrontLibConvert_DGV.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.DGV_CellBeginEdit);
            // 
            // DoorLibConvert_DGV
            // 
            this.DoorLibConvert_DGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DoorLibConvert_DGV.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn7,
            this.dataGridViewTextBoxColumn8,
            this.dataGridViewTextBoxColumn9,
            this.IfExportDoorImage});
            this.DoorLibConvert_DGV.Location = new System.Drawing.Point(71, 458);
            this.DoorLibConvert_DGV.Name = "DoorLibConvert_DGV";
            this.DoorLibConvert_DGV.RowTemplate.Height = 23;
            this.DoorLibConvert_DGV.Size = new System.Drawing.Size(442, 131);
            this.DoorLibConvert_DGV.TabIndex = 3;
            this.DoorLibConvert_DGV.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.DGV_CellBeginEdit);
            // 
            // ApplyTo_BTN
            // 
            this.ApplyTo_BTN.Location = new System.Drawing.Point(539, 184);
            this.ApplyTo_BTN.Name = "ApplyTo_BTN";
            this.ApplyTo_BTN.Size = new System.Drawing.Size(75, 23);
            this.ApplyTo_BTN.TabIndex = 4;
            this.ApplyTo_BTN.Text = "应用到地图";
            this.ApplyTo_BTN.UseVisualStyleBackColor = true;
            this.ApplyTo_BTN.Click += new System.EventHandler(this.ApplyTo_BTN_Click);
            // 
            // SaveTo_BTN
            // 
            this.SaveTo_BTN.Location = new System.Drawing.Point(539, 266);
            this.SaveTo_BTN.Name = "SaveTo_BTN";
            this.SaveTo_BTN.Size = new System.Drawing.Size(75, 23);
            this.SaveTo_BTN.TabIndex = 5;
            this.SaveTo_BTN.Text = "保存地图";
            this.SaveTo_BTN.UseVisualStyleBackColor = true;
            this.SaveTo_BTN.Click += new System.EventHandler(this.SaveTo_BTN_Click);
            // 
            // BatchTilesProc_BTN
            // 
            this.BatchTilesProc_BTN.Location = new System.Drawing.Point(539, 348);
            this.BatchTilesProc_BTN.Name = "BatchTilesProc_BTN";
            this.BatchTilesProc_BTN.Size = new System.Drawing.Size(100, 23);
            this.BatchTilesProc_BTN.TabIndex = 5;
            this.BatchTilesProc_BTN.Text = "批量分离Tiles";
            this.BatchTilesProc_BTN.UseVisualStyleBackColor = true;
            this.BatchTilesProc_BTN.Click += new System.EventHandler(this.BatchTilesProc_BTN_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 70);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "Back";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 214);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "Middle";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 366);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 12);
            this.label3.TabIndex = 8;
            this.label3.Text = "Front";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 522);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 9;
            this.label4.Text = "Door";
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "原始libIndex";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.HeaderText = "指定Lib文件";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.HeaderText = "转换后的LibIndex";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            // 
            // IfExportObjectImage
            // 
            this.IfExportObjectImage.HeaderText = "图片提取";
            this.IfExportObjectImage.Name = "IfExportObjectImage";
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.HeaderText = "原始libIndex";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.HeaderText = "指定Lib文件";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            // 
            // dataGridViewTextBoxColumn9
            // 
            this.dataGridViewTextBoxColumn9.HeaderText = "转换后的LibIndex";
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            // 
            // IfExportDoorImage
            // 
            this.IfExportDoorImage.HeaderText = "图片提取";
            this.IfExportDoorImage.Name = "IfExportDoorImage";
            // 
            // MapConverter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(626, 600);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.SaveTo_BTN);
            this.Controls.Add(this.ApplyTo_BTN);
            this.Controls.Add(this.BatchTilesProc_BTN);
            this.Controls.Add(this.DoorLibConvert_DGV);
            this.Controls.Add(this.FrontLibConvert_DGV);
            this.Controls.Add(this.MiddleLibConvert_DGV);
            this.Controls.Add(this.BackLibConvert_DGV);
            this.Name = "MapConverter";
            this.Text = "MapConverter";
            ((System.ComponentModel.ISupportInitialize)(this.BackLibConvert_DGV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MiddleLibConvert_DGV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FrontLibConvert_DGV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DoorLibConvert_DGV)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView BackLibConvert_DGV;
        private System.Windows.Forms.DataGridView MiddleLibConvert_DGV;
        private System.Windows.Forms.DataGridView FrontLibConvert_DGV;
        private System.Windows.Forms.DataGridView DoorLibConvert_DGV;
        private System.Windows.Forms.Button ApplyTo_BTN;
        private System.Windows.Forms.Button SaveTo_BTN;
        private System.Windows.Forms.Button BatchTilesProc_BTN;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridViewTextBoxColumn BackLibIndex;
        private System.Windows.Forms.DataGridViewTextBoxColumn TargetLibFile;
        private System.Windows.Forms.DataGridViewTextBoxColumn ConvertToIndex;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IfExportBackImage;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IfExportMiddleImage;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IfExportObjectImage;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IfExportDoorImage;
    }
}