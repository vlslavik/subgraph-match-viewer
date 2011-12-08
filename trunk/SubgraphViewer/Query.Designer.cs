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
            this.button2 = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.textBox2_EndVertex = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_StartVertex = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.buttonMatch = new System.Windows.Forms.Button();
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
            this.panel_bottom.Controls.Add(this.button3);
            this.panel_bottom.Controls.Add(this.textBox3);
            this.panel_bottom.Controls.Add(this.button2);
            this.panel_bottom.Controls.Add(this.textBox2);
            this.panel_bottom.Controls.Add(this.textBox1);
            this.panel_bottom.Controls.Add(this.button1);
            this.panel_bottom.Controls.Add(this.label5);
            this.panel_bottom.Controls.Add(this.label4);
            this.panel_bottom.Controls.Add(this.label3);
            this.panel_bottom.Controls.Add(this.listBox1);
            this.panel_bottom.Controls.Add(this.textBox2_EndVertex);
            this.panel_bottom.Controls.Add(this.label2);
            this.panel_bottom.Controls.Add(this.textBox_StartVertex);
            this.panel_bottom.Controls.Add(this.label1);
            this.panel_bottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel_bottom.Location = new System.Drawing.Point(40, 301);
            this.panel_bottom.Name = "panel_bottom";
            this.panel_bottom.Size = new System.Drawing.Size(544, 121);
            this.panel_bottom.TabIndex = 1;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(7, 32);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(63, 23);
            this.button2.TabIndex = 11;
            this.button2.Text = "add_edge";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(463, 68);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(76, 21);
            this.textBox2.TabIndex = 10;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(463, 32);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(76, 21);
            this.textBox1.TabIndex = 9;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(396, 93);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(69, 23);
            this.button1.TabIndex = 8;
            this.button1.Text = "add_label";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(400, 68);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 7;
            this.label5.Text = "label_name";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(400, 32);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "vertex_id";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(272, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "label_list";
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new System.Drawing.Point(226, 26);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(154, 88);
            this.listBox1.TabIndex = 4;
            // 
            // textBox2_EndVertex
            // 
            this.textBox2_EndVertex.Location = new System.Drawing.Point(86, 52);
            this.textBox2_EndVertex.Name = "textBox2_EndVertex";
            this.textBox2_EndVertex.Size = new System.Drawing.Size(86, 21);
            this.textBox2_EndVertex.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "end_vertex";
            // 
            // textBox_StartVertex
            // 
            this.textBox_StartVertex.Location = new System.Drawing.Point(86, 11);
            this.textBox_StartVertex.Name = "textBox_StartVertex";
            this.textBox_StartVertex.Size = new System.Drawing.Size(86, 21);
            this.textBox_StartVertex.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "start_vertex";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(86, 93);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(86, 21);
            this.textBox3.TabIndex = 12;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(7, 92);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(73, 23);
            this.button3.TabIndex = 13;
            this.button3.Text = "label_name";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // buttonMatch
            // 
            this.buttonMatch.Location = new System.Drawing.Point(40, 272);
            this.buttonMatch.Name = "buttonMatch";
            this.buttonMatch.Size = new System.Drawing.Size(544, 23);
            this.buttonMatch.TabIndex = 2;
            this.buttonMatch.Text = "Match!";
            this.buttonMatch.UseVisualStyleBackColor = true;
            this.buttonMatch.Click += new System.EventHandler(this.buttonMatch_Click);
            // 
            // Query
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 422);
            this.Controls.Add(this.buttonMatch);
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
        private System.Windows.Forms.TextBox textBox2_EndVertex;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_StartVertex;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Button buttonMatch;
    }
}

