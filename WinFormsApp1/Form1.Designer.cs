namespace WinFormsApp1
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
            txtCommand = new TextBox();
            btnExecute = new Button();
            listBox = new ListBox();
            panel1 = new Panel();
            label1 = new Label();
            Text1 = new Label();
            ExitBtn = new Button();
            Setting = new Button();
            LauChoce = new Button();
            btnProfile = new Button();
            AvatarName = new Label();
            label2 = new Label();
            button11 = new Button();
            textBox11 = new TextBox();
            button1 = new Button();
            textBox1 = new TextBox();
            listBox1 = new ListBox();
            comboBox = new ComboBox();
            radioVoiceMode = new RadioButton();
            btnStart = new Button();
            radioTextMode = new RadioButton();
            panel2 = new Panel();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // txtCommand
            // 
            txtCommand.Location = new Point(394, 777);
            txtCommand.Name = "txtCommand";
            txtCommand.Size = new Size(125, 27);
            txtCommand.TabIndex = 0;
            // 
            // btnExecute
            // 
            btnExecute.Location = new Point(549, 776);
            btnExecute.Name = "btnExecute";
            btnExecute.Size = new Size(101, 29);
            btnExecute.TabIndex = 1;
            btnExecute.Text = "Выполнить";
            btnExecute.UseVisualStyleBackColor = true;
            btnExecute.Click += btnExecute_Click;
            // 
            // listBox
            // 
            listBox.BackColor = SystemColors.InactiveBorder;
            listBox.BorderStyle = BorderStyle.None;
            listBox.ForeColor = SystemColors.ControlText;
            listBox.FormattingEnabled = true;
            listBox.Location = new Point(2, 87);
            listBox.Name = "listBox";
            listBox.Size = new Size(313, 460);
            listBox.TabIndex = 2;
            // 
            // panel1
            // 
            panel1.Controls.Add(label1);
            panel1.Controls.Add(Text1);
            panel1.Controls.Add(ExitBtn);
            panel1.Controls.Add(Setting);
            panel1.Controls.Add(LauChoce);
            panel1.Location = new Point(8, 550);
            panel1.Name = "panel1";
            panel1.Size = new Size(307, 235);
            panel1.TabIndex = 3;
            panel1.Visible = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Century Gothic", 10F);
            label1.Location = new Point(13, 70);
            label1.Name = "label1";
            label1.Size = new Size(278, 21);
            label1.TabIndex = 4;
            label1.Text = "Чтобы пользоваться функциями";
            // 
            // Text1
            // 
            Text1.AutoSize = true;
            Text1.Font = new Font("Century Gothic", 12F);
            Text1.Location = new Point(53, 47);
            Text1.Name = "Text1";
            Text1.Size = new Size(191, 23);
            Text1.TabIndex = 3;
            Text1.Text = "Войдите в аккаунт";
            // 
            // ExitBtn
            // 
            ExitBtn.Location = new Point(35, 94);
            ExitBtn.Name = "ExitBtn";
            ExitBtn.Size = new Size(241, 51);
            ExitBtn.TabIndex = 2;
            ExitBtn.Text = "button1";
            ExitBtn.UseVisualStyleBackColor = true;
            // 
            // Setting
            // 
            Setting.Location = new Point(35, 151);
            Setting.Name = "Setting";
            Setting.Size = new Size(241, 29);
            Setting.TabIndex = 1;
            Setting.Text = "button1";
            Setting.UseVisualStyleBackColor = true;
            // 
            // LauChoce
            // 
            LauChoce.Location = new Point(35, 186);
            LauChoce.Name = "LauChoce";
            LauChoce.Size = new Size(241, 29);
            LauChoce.TabIndex = 0;
            LauChoce.Text = "button1";
            LauChoce.UseVisualStyleBackColor = true;
            // 
            // btnProfile
            // 
            btnProfile.FlatStyle = FlatStyle.Popup;
            btnProfile.ImageAlign = ContentAlignment.BottomRight;
            btnProfile.Location = new Point(77, 791);
            btnProfile.Name = "btnProfile";
            btnProfile.Size = new Size(50, 50);
            btnProfile.TabIndex = 0;
            btnProfile.Text = "Avatar";
            btnProfile.UseVisualStyleBackColor = true;
            btnProfile.Click += btnProfile_Click;
            // 
            // AvatarName
            // 
            AvatarName.AutoSize = true;
            AvatarName.Font = new Font("Century Gothic", 18F, FontStyle.Regular, GraphicsUnit.Point, 204);
            AvatarName.ImageAlign = ContentAlignment.BottomRight;
            AvatarName.Location = new Point(133, 795);
            AvatarName.Name = "AvatarName";
            AvatarName.Size = new Size(96, 37);
            AvatarName.TabIndex = 4;
            AvatarName.Text = "None";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Century Gothic", 25F);
            label2.Location = new Point(77, 21);
            label2.Name = "label2";
            label2.Size = new Size(107, 51);
            label2.TabIndex = 5;
            label2.Text = "Eiva";
            // 
            // button11
            // 
            button11.Location = new Point(549, 722);
            button11.Name = "button11";
            button11.Size = new Size(101, 29);
            button11.TabIndex = 7;
            button11.Text = "Выполнить";
            button11.UseVisualStyleBackColor = true;
            button11.Click += button11_Click_1;
            // 
            // textBox11
            // 
            textBox11.Location = new Point(394, 723);
            textBox11.Name = "textBox11";
            textBox11.Size = new Size(125, 27);
            textBox11.TabIndex = 6;
            // 
            // button1
            // 
            button1.Location = new Point(502, 781);
            button1.Name = "button1";
            button1.Size = new Size(124, 29);
            button1.TabIndex = 11;
            button1.Text = "Выполнить";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(12, 783);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(484, 27);
            textBox1.TabIndex = 10;
            textBox1.KeyDown += textBox1_KeyDown;
            // 
            // listBox1
            // 
            listBox1.BackColor = SystemColors.InactiveBorder;
            listBox1.BorderStyle = BorderStyle.None;
            listBox1.ForeColor = SystemColors.ControlText;
            listBox1.FormattingEnabled = true;
            listBox1.Location = new Point(12, 21);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(614, 740);
            listBox1.TabIndex = 12;
            // 
            // comboBox
            // 
            comboBox.FormattingEnabled = true;
            comboBox.Location = new Point(635, 162);
            comboBox.Name = "comboBox";
            comboBox.Size = new Size(151, 28);
            comboBox.TabIndex = 13;
            comboBox.SelectedIndexChanged += comboBox_SelectedIndexChanged;
            // 
            // radioVoiceMode
            // 
            radioVoiceMode.AutoSize = true;
            radioVoiceMode.Location = new Point(657, 255);
            radioVoiceMode.Name = "radioVoiceMode";
            radioVoiceMode.Size = new Size(117, 24);
            radioVoiceMode.TabIndex = 15;
            radioVoiceMode.TabStop = true;
            radioVoiceMode.Text = "radioButton1";
            radioVoiceMode.UseVisualStyleBackColor = true;
            radioVoiceMode.CheckedChanged += radioVoiceMode_CheckedChanged;
            // 
            // btnStart
            // 
            btnStart.Location = new Point(667, 285);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(94, 29);
            btnStart.TabIndex = 16;
            btnStart.Text = "button2";
            btnStart.UseVisualStyleBackColor = true;
            btnStart.Click += btnStart_Click;
            // 
            // radioTextMode
            // 
            radioTextMode.AutoSize = true;
            radioTextMode.Location = new Point(657, 225);
            radioTextMode.Name = "radioTextMode";
            radioTextMode.Size = new Size(117, 24);
            radioTextMode.TabIndex = 17;
            radioTextMode.TabStop = true;
            radioTextMode.Text = "radioButton2";
            radioTextMode.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            panel2.Controls.Add(listBox1);
            panel2.Controls.Add(textBox1);
            panel2.Controls.Add(button1);
            panel2.Location = new Point(932, 12);
            panel2.Name = "panel2";
            panel2.Size = new Size(638, 829);
            panel2.TabIndex = 18;
            panel2.Visible = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1582, 853);
            Controls.Add(panel2);
            Controls.Add(radioTextMode);
            Controls.Add(btnStart);
            Controls.Add(radioVoiceMode);
            Controls.Add(comboBox);
            Controls.Add(button11);
            Controls.Add(textBox11);
            Controls.Add(label2);
            Controls.Add(AvatarName);
            Controls.Add(btnProfile);
            Controls.Add(panel1);
            Controls.Add(listBox);
            Controls.Add(btnExecute);
            Controls.Add(txtCommand);
            Name = "Form1";
            Text = " ";
            Load += Form1_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtCommand;
        private Button btnExecute;
        private ListBox listBox;
        private Panel panel1;
        private Button btnProfile;
        private Label AvatarName;
        private Label label1;
        private Label Text1;
        private Button ExitBtn;
        private Button Setting;
        private Button LauChoce;
        private Label label2;
        private Button button11;
        private TextBox textBox11;
        private Button button1;
        private TextBox textBox1;
        private ListBox listBox1;
        private ComboBox comboBox;
        private RadioButton radioVoiceMode;
        private Button btnStart;
        private RadioButton radioTextMode;
        private Panel panel2;
    }
}