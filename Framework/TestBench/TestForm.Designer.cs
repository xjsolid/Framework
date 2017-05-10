namespace TestBench
{
    partial class TestForm
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
            this.button1 = new System.Windows.Forms.Button();
            this.button_LogTest = new System.Windows.Forms.Button();
            this.button_Exception = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_Counter = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "MessageBox";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button_LogTest
            // 
            this.button_LogTest.Location = new System.Drawing.Point(105, 12);
            this.button_LogTest.Name = "button_LogTest";
            this.button_LogTest.Size = new System.Drawing.Size(75, 23);
            this.button_LogTest.TabIndex = 1;
            this.button_LogTest.Text = "LogTest";
            this.button_LogTest.UseVisualStyleBackColor = true;
            this.button_LogTest.Click += new System.EventHandler(this.button_LogTest_Click);
            // 
            // button_Exception
            // 
            this.button_Exception.Location = new System.Drawing.Point(186, 12);
            this.button_Exception.Name = "button_Exception";
            this.button_Exception.Size = new System.Drawing.Size(99, 23);
            this.button_Exception.TabIndex = 2;
            this.button_Exception.Text = "Exception";
            this.button_Exception.UseVisualStyleBackColor = true;
            this.button_Exception.Click += new System.EventHandler(this.button_Exception_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 59);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "Counter Test:";
            // 
            // textBox_Counter
            // 
            this.textBox_Counter.Location = new System.Drawing.Point(101, 50);
            this.textBox_Counter.Name = "textBox_Counter";
            this.textBox_Counter.Size = new System.Drawing.Size(100, 21);
            this.textBox_Counter.TabIndex = 4;
            // 
            // TestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(525, 465);
            this.Controls.Add(this.textBox_Counter);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button_Exception);
            this.Controls.Add(this.button_LogTest);
            this.Controls.Add(this.button1);
            this.Name = "TestForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TestBench_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.TestForm_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button_LogTest;
        private System.Windows.Forms.Button button_Exception;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_Counter;
    }
}

