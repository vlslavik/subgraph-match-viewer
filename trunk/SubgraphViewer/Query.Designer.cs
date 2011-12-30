namespace SubgraphViewer
{
    partial class Query
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
            this.panel_left = new System.Windows.Forms.Panel();
            this.panel_bottom = new System.Windows.Forms.Panel();
            this.textBoxSetLabel = new System.Windows.Forms.TextBox();
            this.buttonSetLabel = new System.Windows.Forms.Button();
            this.buttonClear = new System.Windows.Forms.Button();
            this.buttonMatch = new System.Windows.Forms.Button();
            this.textBoxMaxMatchNum = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.listBoxLabelList = new System.Windows.Forms.ListBox();
            this.panel_bottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_left
            // 
            this.panel_left.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_left.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel_left.Location = new System.Drawing.Point(0, 0);
            this.panel_left.Name = "panel_left";
            this.panel_left.Size = new System.Drawing.Size(40, 422);
            this.panel_left.TabIndex = 0;
            this.panel_left.Paint += new System.Windows.Forms.PaintEventHandler(this.panel_left_Paint);
            this.panel_left.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel_left_MouseDown);
            this.panel_left.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PanelLeft_MouseMove);
            // 
            // panel_bottom
            // 
            this.panel_bottom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_bottom.Controls.Add(this.textBoxSetLabel);
            this.panel_bottom.Controls.Add(this.buttonSetLabel);
            this.panel_bottom.Controls.Add(this.buttonClear);
            this.panel_bottom.Controls.Add(this.buttonMatch);
            this.panel_bottom.Controls.Add(this.textBoxMaxMatchNum);
            this.panel_bottom.Controls.Add(this.label7);
            this.panel_bottom.Controls.Add(this.label3);
            this.panel_bottom.Controls.Add(this.listBoxLabelList);
            this.panel_bottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel_bottom.Location = new System.Drawing.Point(40, 257);
            this.panel_bottom.Name = "panel_bottom";
            this.panel_bottom.Size = new System.Drawing.Size(544, 165);
            this.panel_bottom.TabIndex = 1;
            this.panel_bottom.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel_bottom_MouseMove);
            // 
            // textBoxSetLabel
            // 
            this.textBoxSetLabel.Location = new System.Drawing.Point(182, 59);
            this.textBoxSetLabel.Name = "textBoxSetLabel";
            this.textBoxSetLabel.Size = new System.Drawing.Size(252, 21);
            this.textBoxSetLabel.TabIndex = 18;
            this.textBoxSetLabel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.textBoxSetLabel_MouseDown);
            // 
            // buttonSetLabel
            // 
            this.buttonSetLabel.Location = new System.Drawing.Point(445, 59);
            this.buttonSetLabel.Name = "buttonSetLabel";
            this.buttonSetLabel.Size = new System.Drawing.Size(75, 23);
            this.buttonSetLabel.TabIndex = 17;
            this.buttonSetLabel.Text = "set label";
            this.buttonSetLabel.UseVisualStyleBackColor = true;
            this.buttonSetLabel.Click += new System.EventHandler(this.buttonSetLabel_Click);
            // 
            // buttonClear
            // 
            this.buttonClear.Location = new System.Drawing.Point(0, 137);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(543, 23);
            this.buttonClear.TabIndex = 16;
            this.buttonClear.Text = "clear";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // buttonMatch
            // 
            this.buttonMatch.Location = new System.Drawing.Point(0, -1);
            this.buttonMatch.Name = "buttonMatch";
            this.buttonMatch.Size = new System.Drawing.Size(544, 23);
            this.buttonMatch.TabIndex = 2;
            this.buttonMatch.Text = "Match!";
            this.buttonMatch.UseVisualStyleBackColor = true;
            this.buttonMatch.Click += new System.EventHandler(this.buttonMatch_Click);
            // 
            // textBoxMaxMatchNum
            // 
            this.textBoxMaxMatchNum.Location = new System.Drawing.Point(371, 101);
            this.textBoxMaxMatchNum.Name = "textBoxMaxMatchNum";
            this.textBoxMaxMatchNum.Size = new System.Drawing.Size(63, 21);
            this.textBoxMaxMatchNum.TabIndex = 15;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(443, 104);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(101, 12);
            this.label7.TabIndex = 14;
            this.label7.Text = "max match number";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(39, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "label_list";
            // 
            // listBoxLabelList
            // 
            this.listBoxLabelList.FormattingEnabled = true;
            this.listBoxLabelList.ItemHeight = 12;
            this.listBoxLabelList.Location = new System.Drawing.Point(5, 43);
            this.listBoxLabelList.Name = "listBoxLabelList";
            this.listBoxLabelList.Size = new System.Drawing.Size(154, 88);
            this.listBoxLabelList.TabIndex = 4;
            // 
            // Query
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 422);
            this.Controls.Add(this.panel_bottom);
            this.Controls.Add(this.panel_left);
            this.Name = "Query";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Query_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Query_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Query_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Query_MouseMove);
            this.panel_bottom.ResumeLayout(false);
            this.panel_bottom.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel_left;
        private System.Windows.Forms.Panel panel_bottom;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox listBoxLabelList;
        private System.Windows.Forms.Button buttonMatch;
        private System.Windows.Forms.TextBox textBoxMaxMatchNum;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.TextBox textBoxSetLabel;
        private System.Windows.Forms.Button buttonSetLabel;
    }
}

