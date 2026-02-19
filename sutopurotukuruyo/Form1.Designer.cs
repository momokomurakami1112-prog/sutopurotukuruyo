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
            MethodTextBox = new TextBox();
            MethodGenerateButton = new Button();
            groupBox1 = new GroupBox();
            groupBox2 = new GroupBox();
            FunctionRadioButton2 = new RadioButton();
            SubRadioButton = new RadioButton();
            groupBox3 = new GroupBox();
            UseTransactionRadioButton = new RadioButton();
            NotUseTransactionRadioButton = new RadioButton();
            groupBox4 = new GroupBox();
            SqlCommandNameTextBox = new TextBox();
            label2 = new Label();
            label1 = new Label();
            SqlConnectionNameTextBox = new TextBox();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            groupBox4.SuspendLayout();
            SuspendLayout();
            // 
            // FilePathTextBox
            // 
            FilePathTextBox.BorderStyle = BorderStyle.FixedSingle;
            FilePathTextBox.Location = new Point(16, 26);
            FilePathTextBox.Name = "FilePathTextBox";
            FilePathTextBox.Size = new Size(298, 27);
            FilePathTextBox.TabIndex = 0;
            // 
            // LoadFileButton
            // 
            LoadFileButton.Location = new Point(320, 26);
            LoadFileButton.Name = "LoadFileButton";
            LoadFileButton.Size = new Size(94, 29);
            LoadFileButton.TabIndex = 1;
            LoadFileButton.Text = "ファイル読込";
            LoadFileButton.UseVisualStyleBackColor = true;
            LoadFileButton.Click += LoadFileButton_Click;
            // 
            // MethodTextBox
            // 
            MethodTextBox.BorderStyle = BorderStyle.FixedSingle;
            MethodTextBox.Location = new Point(12, 181);
            MethodTextBox.Multiline = true;
            MethodTextBox.Name = "MethodTextBox";
            MethodTextBox.ScrollBars = ScrollBars.Both;
            MethodTextBox.Size = new Size(776, 257);
            MethodTextBox.TabIndex = 2;
            // 
            // MethodGenerateButton
            // 
            MethodGenerateButton.Location = new Point(694, 146);
            MethodGenerateButton.Name = "MethodGenerateButton";
            MethodGenerateButton.Size = new Size(94, 29);
            MethodGenerateButton.TabIndex = 3;
            MethodGenerateButton.Text = "メソッド作成";
            MethodGenerateButton.UseVisualStyleBackColor = true;
            MethodGenerateButton.Click += MethodGenerateButton_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(FilePathTextBox);
            groupBox1.Controls.Add(LoadFileButton);
            groupBox1.Location = new Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(420, 66);
            groupBox1.TabIndex = 4;
            groupBox1.TabStop = false;
            groupBox1.Text = "ストアドプロシージャー指定";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(FunctionRadioButton2);
            groupBox2.Controls.Add(SubRadioButton);
            groupBox2.Location = new Point(12, 84);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(132, 91);
            groupBox2.TabIndex = 2;
            groupBox2.TabStop = false;
            groupBox2.Text = "メソッドの種類";
            // 
            // FunctionRadioButton2
            // 
            FunctionRadioButton2.AutoSize = true;
            FunctionRadioButton2.Location = new Point(6, 56);
            FunctionRadioButton2.Name = "FunctionRadioButton2";
            FunctionRadioButton2.Size = new Size(85, 24);
            FunctionRadioButton2.TabIndex = 1;
            FunctionRadioButton2.TabStop = true;
            FunctionRadioButton2.Text = "Function";
            FunctionRadioButton2.UseVisualStyleBackColor = true;
            // 
            // SubRadioButton
            // 
            SubRadioButton.AutoSize = true;
            SubRadioButton.Location = new Point(6, 26);
            SubRadioButton.Name = "SubRadioButton";
            SubRadioButton.Size = new Size(55, 24);
            SubRadioButton.TabIndex = 0;
            SubRadioButton.TabStop = true;
            SubRadioButton.Text = "Sub";
            SubRadioButton.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(UseTransactionRadioButton);
            groupBox3.Controls.Add(NotUseTransactionRadioButton);
            groupBox3.Location = new Point(150, 84);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(176, 91);
            groupBox3.TabIndex = 0;
            groupBox3.TabStop = false;
            groupBox3.Text = "トランザクション有無";
            // 
            // UseTransactionRadioButton
            // 
            UseTransactionRadioButton.AutoSize = true;
            UseTransactionRadioButton.Location = new Point(6, 56);
            UseTransactionRadioButton.Name = "UseTransactionRadioButton";
            UseTransactionRadioButton.Size = new Size(138, 24);
            UseTransactionRadioButton.TabIndex = 2;
            UseTransactionRadioButton.TabStop = true;
            UseTransactionRadioButton.Text = "トランザクションあり";
            UseTransactionRadioButton.UseVisualStyleBackColor = true;
            // 
            // NotUseTransactionRadioButton
            // 
            NotUseTransactionRadioButton.AutoSize = true;
            NotUseTransactionRadioButton.Location = new Point(6, 26);
            NotUseTransactionRadioButton.Name = "NotUseTransactionRadioButton";
            NotUseTransactionRadioButton.Size = new Size(139, 24);
            NotUseTransactionRadioButton.TabIndex = 2;
            NotUseTransactionRadioButton.TabStop = true;
            NotUseTransactionRadioButton.Text = "トランザクションなし";
            NotUseTransactionRadioButton.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(SqlCommandNameTextBox);
            groupBox4.Controls.Add(label2);
            groupBox4.Controls.Add(label1);
            groupBox4.Controls.Add(SqlConnectionNameTextBox);
            groupBox4.Location = new Point(332, 84);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(356, 91);
            groupBox4.TabIndex = 1;
            groupBox4.TabStop = false;
            groupBox4.Text = "変数名指定";
            // 
            // SqlCommandNameTextBox
            // 
            SqlCommandNameTextBox.BorderStyle = BorderStyle.FixedSingle;
            SqlCommandNameTextBox.Location = new Point(132, 53);
            SqlCommandNameTextBox.Name = "SqlCommandNameTextBox";
            SqlCommandNameTextBox.Size = new Size(218, 27);
            SqlCommandNameTextBox.TabIndex = 5;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(6, 56);
            label2.Name = "label2";
            label2.Size = new Size(114, 20);
            label2.TabIndex = 4;
            label2.Text = "■SqlCommand";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(6, 23);
            label1.Name = "label1";
            label1.Size = new Size(120, 20);
            label1.TabIndex = 3;
            label1.Text = "■SqlConnection";
            // 
            // SqlConnectionNameTextBox
            // 
            SqlConnectionNameTextBox.BorderStyle = BorderStyle.FixedSingle;
            SqlConnectionNameTextBox.Location = new Point(132, 21);
            SqlConnectionNameTextBox.Name = "SqlConnectionNameTextBox";
            SqlConnectionNameTextBox.Size = new Size(218, 27);
            SqlConnectionNameTextBox.TabIndex = 2;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(groupBox4);
            Controls.Add(groupBox3);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Controls.Add(MethodGenerateButton);
            Controls.Add(MethodTextBox);
            Name = "Form1";
            Text = "ぽえ～";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox FilePathTextBox;
        private Button LoadFileButton;
        private TextBox MethodTextBox;
        private Button MethodGenerateButton;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private GroupBox groupBox4;
        private RadioButton FunctionRadioButton2;
        private RadioButton SubRadioButton;
        private RadioButton UseTransactionRadioButton;
        private RadioButton NotUseTransactionRadioButton;
        private TextBox SqlCommandNameTextBox;
        private Label label2;
        private Label label1;
        private TextBox SqlConnectionNameTextBox;
    }
}
