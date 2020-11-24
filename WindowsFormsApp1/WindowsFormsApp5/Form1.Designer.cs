namespace WindowsFormsApp5
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblState = new System.Windows.Forms.ToolStripStatusLabel();
            this.txtIP = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnConIP = new System.Windows.Forms.Button();
            this.cmbServer = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnOPC = new System.Windows.Forms.Button();
            this.lstItems = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtValue = new System.Windows.Forms.TextBox();
            this.btnWrite = new System.Windows.Forms.Button();
            this.btnSetItem = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.btnGetGrps = new System.Windows.Forms.Button();
            this.txtMyValue = new System.Windows.Forms.TextBox();
            this.statusStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblState});
            this.statusStrip1.Location = new System.Drawing.Point(0, 452);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(887, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblState
            // 
            this.lblState.Name = "lblState";
            this.lblState.Size = new System.Drawing.Size(0, 17);
            // 
            // txtIP
            // 
            this.txtIP.Location = new System.Drawing.Point(79, 27);
            this.txtIP.Name = "txtIP";
            this.txtIP.Size = new System.Drawing.Size(175, 21);
            this.txtIP.TabIndex = 1;
            this.txtIP.Text = "127.0.0.1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "服务器IP";
            // 
            // btnConIP
            // 
            this.btnConIP.Location = new System.Drawing.Point(260, 27);
            this.btnConIP.Name = "btnConIP";
            this.btnConIP.Size = new System.Drawing.Size(75, 23);
            this.btnConIP.TabIndex = 3;
            this.btnConIP.Text = "连接服务器";
            this.btnConIP.UseVisualStyleBackColor = true;
            this.btnConIP.Click += new System.EventHandler(this.btnConIP_Click);
            // 
            // cmbServer
            // 
            this.cmbServer.FormattingEnabled = true;
            this.cmbServer.Location = new System.Drawing.Point(79, 68);
            this.cmbServer.Name = "cmbServer";
            this.cmbServer.Size = new System.Drawing.Size(175, 20);
            this.cmbServer.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(29, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "OPC服务";
            // 
            // btnOPC
            // 
            this.btnOPC.Enabled = false;
            this.btnOPC.Location = new System.Drawing.Point(260, 66);
            this.btnOPC.Name = "btnOPC";
            this.btnOPC.Size = new System.Drawing.Size(75, 23);
            this.btnOPC.TabIndex = 6;
            this.btnOPC.Text = "连接OPC";
            this.btnOPC.UseVisualStyleBackColor = true;
            this.btnOPC.Click += new System.EventHandler(this.btnOPC_Click);
            // 
            // lstItems
            // 
            this.lstItems.FormattingEnabled = true;
            this.lstItems.ItemHeight = 12;
            this.lstItems.Location = new System.Drawing.Point(79, 105);
            this.lstItems.Name = "lstItems";
            this.lstItems.Size = new System.Drawing.Size(244, 88);
            this.lstItems.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 108);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 8;
            this.label3.Text = "所有标签";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.txtValue);
            this.groupBox1.Controls.Add(this.btnWrite);
            this.groupBox1.Controls.Add(this.btnSetItem);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.btnGetGrps);
            this.groupBox1.Controls.Add(this.txtMyValue);
            this.groupBox1.Controls.Add(this.txtIP);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnConIP);
            this.groupBox1.Controls.Add(this.cmbServer);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.lstItems);
            this.groupBox1.Controls.Add(this.btnOPC);
            this.groupBox1.Location = new System.Drawing.Point(12, 21);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(851, 428);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "1.获取服务器OPC服务，并显示所有OPC标签";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(35, 207);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(41, 12);
            this.label11.TabIndex = 18;
            this.label11.Text = "标签值";
            // 
            // txtValue
            // 
            this.txtValue.Location = new System.Drawing.Point(79, 204);
            this.txtValue.Name = "txtValue";
            this.txtValue.Size = new System.Drawing.Size(175, 21);
            this.txtValue.TabIndex = 17;
            // 
            // btnWrite
            // 
            this.btnWrite.Enabled = false;
            this.btnWrite.Location = new System.Drawing.Point(260, 241);
            this.btnWrite.Name = "btnWrite";
            this.btnWrite.Size = new System.Drawing.Size(69, 23);
            this.btnWrite.TabIndex = 16;
            this.btnWrite.Text = "发送参数";
            this.btnWrite.UseVisualStyleBackColor = true;
            this.btnWrite.Click += new System.EventHandler(this.btnWrite_Click);
            // 
            // btnSetItem
            // 
            this.btnSetItem.Enabled = false;
            this.btnSetItem.Location = new System.Drawing.Point(329, 134);
            this.btnSetItem.Name = "btnSetItem";
            this.btnSetItem.Size = new System.Drawing.Size(75, 23);
            this.btnSetItem.TabIndex = 12;
            this.btnSetItem.Text = "定位标签";
            this.btnSetItem.UseVisualStyleBackColor = true;
            this.btnSetItem.Click += new System.EventHandler(this.btnSetItem_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(35, 246);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(41, 12);
            this.label10.TabIndex = 15;
            this.label10.Text = "发送值";
            // 
            // btnGetGrps
            // 
            this.btnGetGrps.Enabled = false;
            this.btnGetGrps.Location = new System.Drawing.Point(329, 105);
            this.btnGetGrps.Name = "btnGetGrps";
            this.btnGetGrps.Size = new System.Drawing.Size(75, 23);
            this.btnGetGrps.TabIndex = 9;
            this.btnGetGrps.Text = "获取标签";
            this.btnGetGrps.UseVisualStyleBackColor = true;
            this.btnGetGrps.Click += new System.EventHandler(this.btnGetGrps_Click);
            // 
            // txtMyValue
            // 
            this.txtMyValue.Location = new System.Drawing.Point(79, 243);
            this.txtMyValue.Name = "txtMyValue";
            this.txtMyValue.Size = new System.Drawing.Size(175, 21);
            this.txtMyValue.TabIndex = 14;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(887, 474);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.statusStrip1);
            this.Name = "Form1";
            this.Text = "OPCManage";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OPCManage_FormClosing);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblState;
        private System.Windows.Forms.TextBox txtIP;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnConIP;
        private System.Windows.Forms.ComboBox cmbServer;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnOPC;
        private System.Windows.Forms.ListBox lstItems;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnGetGrps;
        private System.Windows.Forms.Button btnWrite;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtMyValue;
        private System.Windows.Forms.Button btnSetItem;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtValue;
    }
}

