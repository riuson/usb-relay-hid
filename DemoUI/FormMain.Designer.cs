namespace DemoUI
{
    partial class FormMain
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.buttonFindDevice = new System.Windows.Forms.Button();
            this.comboBoxPath = new System.Windows.Forms.ComboBox();
            this.buttonConnect = new System.Windows.Forms.Button();
            this.buttonOpenAll = new System.Windows.Forms.Button();
            this.buttonCloseAll = new System.Windows.Forms.Button();
            this.textBoxId = new System.Windows.Forms.TextBox();
            this.buttonSetId = new System.Windows.Forms.Button();
            this.buttonDisconnect = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Controls.Add(this.buttonFindDevice, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.comboBoxPath, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.buttonConnect, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.buttonOpenAll, 0, 10);
            this.tableLayoutPanel1.Controls.Add(this.buttonCloseAll, 2, 10);
            this.tableLayoutPanel1.Controls.Add(this.textBoxId, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.buttonSetId, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.buttonDisconnect, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(5, 5);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 11;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.09091F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.09091F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.09091F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.09091F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.09091F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.09091F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.09091F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.09091F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.09091F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.09091F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.09091F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(414, 440);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // buttonFindDevice
            // 
            this.buttonFindDevice.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonFindDevice.Location = new System.Drawing.Point(3, 3);
            this.buttonFindDevice.Name = "buttonFindDevice";
            this.buttonFindDevice.Size = new System.Drawing.Size(97, 33);
            this.buttonFindDevice.TabIndex = 0;
            this.buttonFindDevice.Text = "Find Device";
            this.buttonFindDevice.UseVisualStyleBackColor = true;
            this.buttonFindDevice.Click += new System.EventHandler(this.buttonFindDevice_Click);
            this.buttonFindDevice.Paint += new System.Windows.Forms.PaintEventHandler(this.OnControlPaint);
            // 
            // comboBoxPath
            // 
            this.comboBoxPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.SetColumnSpan(this.comboBoxPath, 3);
            this.comboBoxPath.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxPath.FormattingEnabled = true;
            this.comboBoxPath.Location = new System.Drawing.Point(106, 9);
            this.comboBoxPath.Name = "comboBoxPath";
            this.comboBoxPath.Size = new System.Drawing.Size(305, 21);
            this.comboBoxPath.TabIndex = 1;
            // 
            // buttonConnect
            // 
            this.buttonConnect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonConnect.Location = new System.Drawing.Point(3, 42);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(97, 33);
            this.buttonConnect.TabIndex = 2;
            this.buttonConnect.Text = "Open Device";
            this.buttonConnect.UseVisualStyleBackColor = true;
            this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click);
            this.buttonConnect.Paint += new System.Windows.Forms.PaintEventHandler(this.OnControlPaint);
            // 
            // buttonOpenAll
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.buttonOpenAll, 2);
            this.buttonOpenAll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonOpenAll.Location = new System.Drawing.Point(3, 393);
            this.buttonOpenAll.Name = "buttonOpenAll";
            this.buttonOpenAll.Size = new System.Drawing.Size(200, 44);
            this.buttonOpenAll.TabIndex = 4;
            this.buttonOpenAll.Text = "Open All";
            this.buttonOpenAll.UseVisualStyleBackColor = true;
            this.buttonOpenAll.Click += new System.EventHandler(this.buttonOpenAll_Click);
            this.buttonOpenAll.Paint += new System.Windows.Forms.PaintEventHandler(this.OnControlPaint);
            // 
            // buttonCloseAll
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.buttonCloseAll, 2);
            this.buttonCloseAll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonCloseAll.Location = new System.Drawing.Point(209, 393);
            this.buttonCloseAll.Name = "buttonCloseAll";
            this.buttonCloseAll.Size = new System.Drawing.Size(202, 44);
            this.buttonCloseAll.TabIndex = 5;
            this.buttonCloseAll.Text = "Close All";
            this.buttonCloseAll.UseVisualStyleBackColor = true;
            this.buttonCloseAll.Click += new System.EventHandler(this.buttonCloseAll_Click);
            this.buttonCloseAll.Paint += new System.Windows.Forms.PaintEventHandler(this.OnControlPaint);
            // 
            // textBoxId
            // 
            this.textBoxId.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxId.Location = new System.Drawing.Point(209, 42);
            this.textBoxId.MaxLength = 5;
            this.textBoxId.Name = "textBoxId";
            this.textBoxId.Size = new System.Drawing.Size(97, 20);
            this.textBoxId.TabIndex = 6;
            this.textBoxId.Text = "00000";
            this.textBoxId.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // buttonSetId
            // 
            this.buttonSetId.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonSetId.Location = new System.Drawing.Point(312, 42);
            this.buttonSetId.Name = "buttonSetId";
            this.buttonSetId.Size = new System.Drawing.Size(99, 33);
            this.buttonSetId.TabIndex = 7;
            this.buttonSetId.Text = "Rename";
            this.buttonSetId.UseVisualStyleBackColor = true;
            this.buttonSetId.Click += new System.EventHandler(this.buttonSetId_Click);
            this.buttonSetId.Paint += new System.Windows.Forms.PaintEventHandler(this.OnControlPaint);
            // 
            // buttonDisconnect
            // 
            this.buttonDisconnect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonDisconnect.Location = new System.Drawing.Point(106, 42);
            this.buttonDisconnect.Name = "buttonDisconnect";
            this.buttonDisconnect.Size = new System.Drawing.Size(97, 33);
            this.buttonDisconnect.TabIndex = 8;
            this.buttonDisconnect.Text = "Close Device";
            this.buttonDisconnect.UseVisualStyleBackColor = true;
            this.buttonDisconnect.Click += new System.EventHandler(this.buttonDisconnect_Click);
            this.buttonDisconnect.Paint += new System.Windows.Forms.PaintEventHandler(this.OnControlPaint);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(424, 450);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "FormMain";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.Text = "USB HID Relay Module Control Demo";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button buttonFindDevice;
        private System.Windows.Forms.ComboBox comboBoxPath;
        private System.Windows.Forms.Button buttonConnect;
        private System.Windows.Forms.Button buttonOpenAll;
        private System.Windows.Forms.Button buttonCloseAll;
        private System.Windows.Forms.TextBox textBoxId;
        private System.Windows.Forms.Button buttonSetId;
        private System.Windows.Forms.Button buttonDisconnect;
    }
}

