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
            SelectButton = new Button();
            CopyStatusLabel = new Label();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            groupBox4.SuspendLayout();
            SuspendLayout();
            // 
            // FilePathTextBox
            // 
            FilePathTextBox.AllowDrop = true;
            FilePathTextBox.BorderStyle = BorderStyle.FixedSingle;
            FilePathTextBox.Location = new Point(14, 20);
            FilePathTextBox.Margin = new Padding(3, 2, 3, 2);
            FilePathTextBox.Name = "FilePathTextBox";
            FilePathTextBox.Size = new Size(261, 23);
            FilePathTextBox.TabIndex = 0;
            // 
            // LoadFileButton
            // 
            LoadFileButton.Location = new Point(280, 20);
            LoadFileButton.Margin = new Padding(3, 2, 3, 2);
            LoadFileButton.Name = "LoadFileButton";
            LoadFileButton.Size = new Size(82, 22);
            LoadFileButton.TabIndex = 1;
            LoadFileButton.Text = "ファイル読込";
            LoadFileButton.UseVisualStyleBackColor = true;
            LoadFileButton.Click += LoadFileButton_Click;
            // 
            // MethodTextBox
            // 
            MethodTextBox.BackColor = SystemColors.Window;
            MethodTextBox.BorderStyle = BorderStyle.FixedSingle;
            MethodTextBox.Location = new Point(10, 136);
            MethodTextBox.Margin = new Padding(3, 2, 3, 2);
            MethodTextBox.Multiline = true;
            MethodTextBox.Name = "MethodTextBox";
            MethodTextBox.ReadOnly = true;
            MethodTextBox.ScrollBars = ScrollBars.Both;
            MethodTextBox.Size = new Size(679, 285);
            MethodTextBox.TabIndex = 2;
            // 
            // MethodGenerateButton
            // 
            MethodGenerateButton.Location = new Point(607, 110);
            MethodGenerateButton.Margin = new Padding(3, 2, 3, 2);
            MethodGenerateButton.Name = "MethodGenerateButton";
            MethodGenerateButton.Size = new Size(82, 22);
            MethodGenerateButton.TabIndex = 3;
            MethodGenerateButton.Text = "メソッド作成";
            MethodGenerateButton.UseVisualStyleBackColor = true;
            MethodGenerateButton.Click += MethodGenerateButton_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(FilePathTextBox);
            groupBox1.Controls.Add(LoadFileButton);
            groupBox1.Location = new Point(10, 9);
            groupBox1.Margin = new Padding(3, 2, 3, 2);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(3, 2, 3, 2);
            groupBox1.Size = new Size(368, 50);
            groupBox1.TabIndex = 4;
            groupBox1.TabStop = false;
            groupBox1.Text = "ストアドプロシージャー指定";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(FunctionRadioButton2);
            groupBox2.Controls.Add(SubRadioButton);
            groupBox2.Location = new Point(10, 63);
            groupBox2.Margin = new Padding(3, 2, 3, 2);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new Padding(3, 2, 3, 2);
            groupBox2.Size = new Size(116, 68);
            groupBox2.TabIndex = 2;
            groupBox2.TabStop = false;
            groupBox2.Text = "メソッドの種類";
            // 
            // FunctionRadioButton2
            // 
            FunctionRadioButton2.AutoSize = true;
            FunctionRadioButton2.Location = new Point(5, 42);
            FunctionRadioButton2.Margin = new Padding(3, 2, 3, 2);
            FunctionRadioButton2.Name = "FunctionRadioButton2";
            FunctionRadioButton2.Size = new Size(71, 19);
            FunctionRadioButton2.TabIndex = 1;
            FunctionRadioButton2.TabStop = true;
            FunctionRadioButton2.Text = "Function";
            FunctionRadioButton2.UseVisualStyleBackColor = true;
            // 
            // SubRadioButton
            // 
            SubRadioButton.AutoSize = true;
            SubRadioButton.Location = new Point(5, 20);
            SubRadioButton.Margin = new Padding(3, 2, 3, 2);
            SubRadioButton.Name = "SubRadioButton";
            SubRadioButton.Size = new Size(45, 19);
            SubRadioButton.TabIndex = 0;
            SubRadioButton.TabStop = true;
            SubRadioButton.Text = "Sub";
            SubRadioButton.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(UseTransactionRadioButton);
            groupBox3.Controls.Add(NotUseTransactionRadioButton);
            groupBox3.Location = new Point(131, 63);
            groupBox3.Margin = new Padding(3, 2, 3, 2);
            groupBox3.Name = "groupBox3";
            groupBox3.Padding = new Padding(3, 2, 3, 2);
            groupBox3.Size = new Size(154, 68);
            groupBox3.TabIndex = 0;
            groupBox3.TabStop = false;
            groupBox3.Text = "トランザクション有無";
            // 
            // UseTransactionRadioButton
            // 
            UseTransactionRadioButton.AutoSize = true;
            UseTransactionRadioButton.Location = new Point(5, 42);
            UseTransactionRadioButton.Margin = new Padding(3, 2, 3, 2);
            UseTransactionRadioButton.Name = "UseTransactionRadioButton";
            UseTransactionRadioButton.Size = new Size(113, 19);
            UseTransactionRadioButton.TabIndex = 2;
            UseTransactionRadioButton.TabStop = true;
            UseTransactionRadioButton.Text = "トランザクションあり";
            UseTransactionRadioButton.UseVisualStyleBackColor = true;
            // 
            // NotUseTransactionRadioButton
            // 
            NotUseTransactionRadioButton.AutoSize = true;
            NotUseTransactionRadioButton.Location = new Point(5, 20);
            NotUseTransactionRadioButton.Margin = new Padding(3, 2, 3, 2);
            NotUseTransactionRadioButton.Name = "NotUseTransactionRadioButton";
            NotUseTransactionRadioButton.Size = new Size(113, 19);
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
            groupBox4.Location = new Point(290, 63);
            groupBox4.Margin = new Padding(3, 2, 3, 2);
            groupBox4.Name = "groupBox4";
            groupBox4.Padding = new Padding(3, 2, 3, 2);
            groupBox4.Size = new Size(312, 68);
            groupBox4.TabIndex = 1;
            groupBox4.TabStop = false;
            groupBox4.Text = "変数名指定";
            // 
            // SqlCommandNameTextBox
            // 
            SqlCommandNameTextBox.BorderStyle = BorderStyle.FixedSingle;
            SqlCommandNameTextBox.Location = new Point(116, 40);
            SqlCommandNameTextBox.Margin = new Padding(3, 2, 3, 2);
            SqlCommandNameTextBox.Name = "SqlCommandNameTextBox";
            SqlCommandNameTextBox.Size = new Size(191, 23);
            SqlCommandNameTextBox.TabIndex = 5;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(5, 42);
            label2.Name = "label2";
            label2.Size = new Size(89, 15);
            label2.TabIndex = 4;
            label2.Text = "■SqlCommand";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(5, 17);
            label1.Name = "label1";
            label1.Size = new Size(96, 15);
            label1.TabIndex = 3;
            label1.Text = "■SqlConnection";
            // 
            // SqlConnectionNameTextBox
            // 
            SqlConnectionNameTextBox.BorderStyle = BorderStyle.FixedSingle;
            SqlConnectionNameTextBox.Location = new Point(116, 16);
            SqlConnectionNameTextBox.Margin = new Padding(3, 2, 3, 2);
            SqlConnectionNameTextBox.Name = "SqlConnectionNameTextBox";
            SqlConnectionNameTextBox.Size = new Size(191, 23);
            SqlConnectionNameTextBox.TabIndex = 2;
            // 
            // SelectButton
            // 
            SelectButton.Location = new Point(614, 426);
            SelectButton.Name = "SelectButton";
            SelectButton.Size = new Size(75, 23);
            SelectButton.TabIndex = 5;
            SelectButton.Text = "コードをコピー";
            SelectButton.UseVisualStyleBackColor = true;
            SelectButton.Click += SelectButton_Click;
            // 
            // CopyStatusLabel
            // 
            CopyStatusLabel.AutoSize = true;
            CopyStatusLabel.Location = new Point(530, 430);
            CopyStatusLabel.Name = "CopyStatusLabel";
            CopyStatusLabel.Size = new Size(78, 15);
            CopyStatusLabel.TabIndex = 6;
            CopyStatusLabel.Text = "コピーしました！";
            CopyStatusLabel.Visible = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(700, 461);
            Controls.Add(CopyStatusLabel);
            Controls.Add(SelectButton);
            Controls.Add(groupBox4);
            Controls.Add(groupBox3);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Controls.Add(MethodGenerateButton);
            Controls.Add(MethodTextBox);
            Margin = new Padding(3, 2, 3, 2);
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
        private Button SelectButton;
        private Label CopyStatusLabel;
    }
}
