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
            this.buttonMatch = new System.Windows.Forms.Button();
            this.textBoxMaxMatchNum = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.textBoxVertexLabel = new System.Windows.Forms.TextBox();
            this.textBoxVertexID = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.listBoxLabelList = new System.Windows.Forms.ListBox();
            this.textBoxEndVertex = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxStartVertex = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
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
            this.panel_bottom.Controls.Add(this.buttonMatch);
            this.panel_bottom.Controls.Add(this.textBoxMaxMatchNum);
            this.panel_bottom.Controls.Add(this.label7);
            this.panel_bottom.Controls.Add(this.label6);
            this.panel_bottom.Controls.Add(this.button2);
            this.panel_bottom.Controls.Add(this.textBoxVertexLabel);
            this.panel_bottom.Controls.Add(this.textBoxVertexID);
            this.panel_bottom.Controls.Add(this.button1);
            this.panel_bottom.Controls.Add(this.label4);
            this.panel_bottom.Controls.Add(this.label3);
            this.panel_bottom.Controls.Add(this.listBoxLabelList);
            this.panel_bottom.Controls.Add(this.textBoxEndVertex);
            this.panel_bottom.Controls.Add(this.label2);
            this.panel_bottom.Controls.Add(this.textBoxStartVertex);
            this.panel_bottom.Controls.Add(this.label1);
            this.panel_bottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel_bottom.Location = new System.Drawing.Point(40, 257);
            this.panel_bottom.Name = "panel_bottom";
            this.panel_bottom.Size = new System.Drawing.Size(544, 165);
            this.panel_bottom.TabIndex = 1;
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
            this.textBoxMaxMatchNum.Location = new System.Drawing.Point(85, 112);
            this.textBoxMaxMatchNum.Name = "textBoxMaxMatchNum";
            this.textBoxMaxMatchNum.Size = new System.Drawing.Size(86, 21);
            this.textBoxMaxMatchNum.TabIndex = 15;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(5, 115);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(71, 12);
            this.label7.TabIndex = 14;
            this.label7.Text = "MaxMatchNum";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(413, 89);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 12);
            this.label6.TabIndex = 13;
            this.label6.Text = "label";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(3, 48);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(63, 23);
            this.button2.TabIndex = 11;
            this.button2.Text = "add_edge";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // textBoxVertexLabel
            // 
            this.textBoxVertexLabel.Location = new System.Drawing.Point(463, 80);
            this.textBoxVertexLabel.Name = "textBoxVertexLabel";
            this.textBoxVertexLabel.Size = new System.Drawing.Size(76, 21);
            this.textBoxVertexLabel.TabIndex = 10;
            // 
            // textBoxVertexID
            // 
            this.textBoxVertexID.Location = new System.Drawing.Point(463, 48);
            this.textBoxVertexID.Name = "textBoxVertexID";
            this.textBoxVertexID.Size = new System.Drawing.Size(76, 21);
            this.textBoxVertexID.TabIndex = 9;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(400, 115);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(69, 23);
            this.button1.TabIndex = 8;
            this.button1.Text = "set_label";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(398, 53);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "vertex_id";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(271, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "label_list";
            // 
            // listBoxLabelList
            // 
            this.listBoxLabelList.FormattingEnabled = true;
            this.listBoxLabelList.ItemHeight = 12;
            this.listBoxLabelList.Location = new System.Drawing.Point(233, 45);
            this.listBoxLabelList.Name = "listBoxLabelList";
            this.listBoxLabelList.Size = new System.Drawing.Size(154, 88);
            this.listBoxLabelList.TabIndex = 4;
            // 
            // textBoxEndVertex
            // 
            this.textBoxEndVertex.Location = new System.Drawing.Point(85, 71);
            this.textBoxEndVertex.Name = "textBoxEndVertex";
            this.textBoxEndVertex.Size = new System.Drawing.Size(86, 21);
            this.textBoxEndVertex.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "end_vertex";
            // 
            // textBoxStartVertex
            // 
            this.textBoxStartVertex.Location = new System.Drawing.Point(85, 30);
            this.textBoxStartVertex.Name = "textBoxStartVertex";
            this.textBoxStartVertex.Size = new System.Drawing.Size(86, 21);
            this.textBoxStartVertex.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(2, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "start_vertex";
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
        private System.Windows.Forms.TextBox textBoxEndVertex;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxStartVertex;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxVertexLabel;
        private System.Windows.Forms.TextBox textBoxVertexID;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox listBoxLabelList;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button buttonMatch;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxMaxMatchNum;
        private System.Windows.Forms.Label label7;
    }
}

