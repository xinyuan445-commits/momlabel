﻿﻿﻿﻿﻿namespace Mom_Label
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            tabControl1 = new TabControl();
            tabPage7 = new TabPage();
            tabPage1 = new TabPage();
            button2 = new Button();
            label1 = new Label();
            textBox1 = new TextBox();
            dataGridView1 = new DataGridView();
            button1 = new Button();
            tabPage2 = new TabPage();
            tabPage3 = new TabPage();
            tabPage4 = new TabPage();
            tabPage5 = new TabPage();
            textBox2 = new TextBox();
            label2 = new Label();
            button4 = new Button();
            button3 = new Button();
            dataGridView2 = new DataGridView();
            tabPage6 = new TabPage();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            tabPage5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).BeginInit();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage7);
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Controls.Add(tabPage3);
            tabControl1.Controls.Add(tabPage4);
            tabControl1.Controls.Add(tabPage5);
            tabControl1.Controls.Add(tabPage6);
            tabControl1.Location = new Point(2, 2);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(1202, 672);
            tabControl1.TabIndex = 0;
            // 
            // tabPage7
            // 
            tabPage7.Location = new Point(4, 26);
            tabPage7.Name = "tabPage7";
            tabPage7.Padding = new Padding(3);
            tabPage7.Size = new Size(1194, 642);
            tabPage7.TabIndex = 6;
            tabPage7.Text = "Main";
            tabPage7.UseVisualStyleBackColor = true;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(button2);
            tabPage1.Controls.Add(label1);
            tabPage1.Controls.Add(textBox1);
            tabPage1.Controls.Add(dataGridView1);
            tabPage1.Controls.Add(button1);
            tabPage1.Location = new Point(4, 26);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(1194, 642);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "生产订单";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.Location = new Point(423, 73);
            button2.Name = "button2";
            button2.Size = new Size(149, 44);
            button2.TabIndex = 4;
            button2.Text = "打印";
            button2.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(37, 46);
            label1.Name = "label1";
            label1.Size = new Size(68, 17);
            label1.TabIndex = 3;
            label1.Text = "生产订单：";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(134, 43);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(213, 23);
            textBox1.TabIndex = 2;
            textBox1.Text = "SCDD23010069";
            textBox1.TextChanged += textBox1_TextChanged;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(3, 123);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(1188, 516);
            dataGridView1.TabIndex = 1;
            // 
            // button1
            // 
            button1.Location = new Point(423, 6);
            button1.Name = "button1";
            button1.Size = new Size(149, 44);
            button1.TabIndex = 0;
            button1.Text = "查询";
            button1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            tabPage2.Location = new Point(4, 26);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(1194, 642);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "用料核单";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            tabPage3.Location = new Point(4, 26);
            tabPage3.Name = "tabPage3";
            tabPage3.Padding = new Padding(3);
            tabPage3.Size = new Size(1194, 642);
            tabPage3.TabIndex = 2;
            tabPage3.Text = "领料核单";
            tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            tabPage4.Location = new Point(4, 26);
            tabPage4.Name = "tabPage4";
            tabPage4.Size = new Size(1194, 642);
            tabPage4.TabIndex = 3;
            tabPage4.Text = "产成品入库单";
            tabPage4.UseVisualStyleBackColor = true;
            // 
            // tabPage5
            // 
            tabPage5.Controls.Add(textBox2);
            tabPage5.Controls.Add(label2);
            tabPage5.Controls.Add(button4);
            tabPage5.Controls.Add(button3);
            tabPage5.Controls.Add(dataGridView2);
            tabPage5.Location = new Point(4, 26);
            tabPage5.Name = "tabPage5";
            tabPage5.Size = new Size(1194, 642);
            tabPage5.TabIndex = 4;
            tabPage5.Text = "工序流转单";
            tabPage5.UseVisualStyleBackColor = true;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(134, 43);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(213, 23);
            textBox2.TabIndex = 4;
            textBox2.Text = "SCDD23010069";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(37, 46);
            label2.Name = "label2";
            label2.Size = new Size(68, 17);
            label2.TabIndex = 3;
            label2.Text = "生产订单：";
            label2.Click += label2_Click;
            // 
            // button4
            // 
            button4.Location = new Point(423, 73);
            button4.Name = "button4";
            button4.Size = new Size(149, 44);
            button4.TabIndex = 2;
            button4.Text = "预览";
            button4.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            button3.Location = new Point(423, 6);
            button3.Name = "button3";
            button3.Size = new Size(149, 44);
            button3.TabIndex = 1;
            button3.Text = "查询";
            button3.UseVisualStyleBackColor = true;
            // 
            // dataGridView2
            // 
            dataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView2.Location = new Point(0, 123);
            dataGridView2.Name = "dataGridView2";
            dataGridView2.Size = new Size(1194, 519);
            dataGridView2.TabIndex = 0;
            // 
            // tabPage6
            // 
            tabPage6.Location = new Point(4, 26);
            tabPage6.Name = "tabPage6";
            tabPage6.Size = new Size(1194, 642);
            tabPage6.TabIndex = 5;
            tabPage6.Text = "开料标签";
            tabPage6.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1206, 676);
            Controls.Add(tabControl1);
            Name = "Form1";
            Text = "生产表单打印工具";
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            tabPage5.ResumeLayout(false);
            tabPage5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private TabPage tabPage3;
        private TabPage tabPage4;
        private TabPage tabPage5;
        private TabPage tabPage6;
        private Button button1;
        private Label label1;
        private TextBox textBox1;
        private DataGridView dataGridView1;
        private Button button2;
        private Label label2;
        private Button button4;
        private Button button3;
        private DataGridView dataGridView2;
        private TextBox textBox2;
        private TabPage tabPage7;
    }
}
