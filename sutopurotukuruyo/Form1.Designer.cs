namespace sutopurotukuruyo
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
            FilePathTextBox = new TextBox();
            LoadFileButton = new Button();
            textBox1 = new TextBox();
            MethodGenerateButton = new Button();
            SuspendLayout();
            // 
            // FilePathTextBox
            // 
            FilePathTextBox.BorderStyle = BorderStyle.FixedSingle;
            FilePathTextBox.Location = new Point(12, 12);
            FilePathTextBox.Name = "FilePathTextBox";
            FilePathTextBox.Size = new Size(231, 27);
            FilePathTextBox.TabIndex = 0;
            // 
            // LoadFileButton
            // 
            LoadFileButton.Location = new Point(249, 12);
            LoadFileButton.Name = "LoadFileButton";
            LoadFileButton.Size = new Size(94, 29);
            LoadFileButton.TabIndex = 1;
            LoadFileButton.Text = "ファイル読込";
            LoadFileButton.UseVisualStyleBackColor = true;
            LoadFileButton.Click += LoadFileButton_Click;
            // 
            // textBox1
            // 
            textBox1.BorderStyle = BorderStyle.FixedSingle;
            textBox1.Location = new Point(12, 45);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(776, 393);
            textBox1.TabIndex = 2;
            // 
            // MethodGenerateButton
            // 
            MethodGenerateButton.Location = new Point(349, 12);
            MethodGenerateButton.Name = "MethodGenerateButton";
            MethodGenerateButton.Size = new Size(94, 29);
            MethodGenerateButton.TabIndex = 3;
            MethodGenerateButton.Text = "メソッド作成";
            MethodGenerateButton.UseVisualStyleBackColor = true;
            MethodGenerateButton.Click += MethodGenerateButton_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(MethodGenerateButton);
            Controls.Add(textBox1);
            Controls.Add(LoadFileButton);
            Controls.Add(FilePathTextBox);
            Name = "Form1";
            Text = "ぽえ～";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox FilePathTextBox;
        private Button LoadFileButton;
        private TextBox textBox1;
        private Button MethodGenerateButton;
    }
}
