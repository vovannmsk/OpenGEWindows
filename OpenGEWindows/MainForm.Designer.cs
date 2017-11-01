namespace OpenGEWindows
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            this.buttonOpenWindowGE = new System.Windows.Forms.Button();
            this.buttonReOpenWindowGE = new System.Windows.Forms.Button();
            this.labelInformation = new System.Windows.Forms.Label();
            this.buttonSuperSell = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.button_StandUp = new System.Windows.Forms.Button();
            this.buttonWarning = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.checkBox4 = new System.Windows.Forms.CheckBox();
            this.checkBox5 = new System.Windows.Forms.CheckBox();
            this.checkBox10 = new System.Windows.Forms.CheckBox();
            this.checkBox9 = new System.Windows.Forms.CheckBox();
            this.checkBox8 = new System.Windows.Forms.CheckBox();
            this.checkBox7 = new System.Windows.Forms.CheckBox();
            this.checkBox6 = new System.Windows.Forms.CheckBox();
            this.checkBox19 = new System.Windows.Forms.CheckBox();
            this.checkBox18 = new System.Windows.Forms.CheckBox();
            this.checkBox17 = new System.Windows.Forms.CheckBox();
            this.checkBox16 = new System.Windows.Forms.CheckBox();
            this.checkBox15 = new System.Windows.Forms.CheckBox();
            this.checkBox14 = new System.Windows.Forms.CheckBox();
            this.checkBox13 = new System.Windows.Forms.CheckBox();
            this.checkBox12 = new System.Windows.Forms.CheckBox();
            this.checkBox11 = new System.Windows.Forms.CheckBox();
            this.buttonGotoTradeTest = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.buttonNewAcc = new System.Windows.Forms.Button();
            this.RunToCrater = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonOpenWindowGE
            // 
            this.buttonOpenWindowGE.Location = new System.Drawing.Point(6, 385);
            this.buttonOpenWindowGE.Name = "buttonOpenWindowGE";
            this.buttonOpenWindowGE.Size = new System.Drawing.Size(163, 35);
            this.buttonOpenWindowGE.TabIndex = 0;
            this.buttonOpenWindowGE.Text = "Открыть окна";
            this.buttonOpenWindowGE.UseVisualStyleBackColor = true;
            this.buttonOpenWindowGE.Click += new System.EventHandler(this.buttonOpenWindowGE_Click);
            // 
            // buttonReOpenWindowGE
            // 
            this.buttonReOpenWindowGE.BackColor = System.Drawing.Color.Orange;
            this.buttonReOpenWindowGE.Location = new System.Drawing.Point(9, 53);
            this.buttonReOpenWindowGE.Name = "buttonReOpenWindowGE";
            this.buttonReOpenWindowGE.Size = new System.Drawing.Size(163, 35);
            this.buttonReOpenWindowGE.TabIndex = 1;
            this.buttonReOpenWindowGE.Text = "Восстановить Окна";
            this.buttonReOpenWindowGE.UseVisualStyleBackColor = false;
            this.buttonReOpenWindowGE.Click += new System.EventHandler(this.buttonReOpenWindowGE_Click);
            // 
            // labelInformation
            // 
            this.labelInformation.AutoSize = true;
            this.labelInformation.Location = new System.Drawing.Point(19, -14);
            this.labelInformation.Name = "labelInformation";
            this.labelInformation.Size = new System.Drawing.Size(35, 13);
            this.labelInformation.TabIndex = 2;
            this.labelInformation.Text = "label1";
            this.labelInformation.Visible = false;
            // 
            // buttonSuperSell
            // 
            this.buttonSuperSell.BackColor = System.Drawing.Color.Aqua;
            this.buttonSuperSell.Location = new System.Drawing.Point(9, 94);
            this.buttonSuperSell.Name = "buttonSuperSell";
            this.buttonSuperSell.Size = new System.Drawing.Size(163, 37);
            this.buttonSuperSell.TabIndex = 3;
            this.buttonSuperSell.Text = "Продажа одного окна";
            this.buttonSuperSell.UseVisualStyleBackColor = false;
            this.buttonSuperSell.Click += new System.EventHandler(this.buttonSuperSell_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(9, 246);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(112, 88);
            this.button1.TabIndex = 5;
            this.button1.Text = "T E S T";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button_StandUp
            // 
            this.button_StandUp.BackColor = System.Drawing.Color.PaleGreen;
            this.button_StandUp.Location = new System.Drawing.Point(178, 53);
            this.button_StandUp.Name = "button_StandUp";
            this.button_StandUp.Size = new System.Drawing.Size(198, 35);
            this.button_StandUp.TabIndex = 20;
            this.button_StandUp.Text = "Восстановить окна после вылета. Универсальный помощник";
            this.button_StandUp.UseVisualStyleBackColor = false;
            this.button_StandUp.Click += new System.EventHandler(this.button_StandUp_Click);
            // 
            // buttonWarning
            // 
            this.buttonWarning.BackColor = System.Drawing.Color.SkyBlue;
            this.buttonWarning.Location = new System.Drawing.Point(178, 12);
            this.buttonWarning.Name = "buttonWarning";
            this.buttonWarning.Size = new System.Drawing.Size(198, 35);
            this.buttonWarning.TabIndex = 22;
            this.buttonWarning.Text = "АВТОРЕЖИМ.  ВЫКЛЮЧЕН.";
            this.buttonWarning.UseVisualStyleBackColor = false;
            this.buttonWarning.Click += new System.EventHandler(this.buttonWarning_Click);
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.MediumPurple;
            this.button4.Location = new System.Drawing.Point(178, 94);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(198, 37);
            this.button4.TabIndex = 25;
            this.button4.Text = "Передача песо";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(6, 339);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(32, 17);
            this.checkBox1.TabIndex = 26;
            this.checkBox1.Text = "1";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(44, 339);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(32, 17);
            this.checkBox2.TabIndex = 27;
            this.checkBox2.Text = "2";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Location = new System.Drawing.Point(82, 339);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(32, 17);
            this.checkBox3.TabIndex = 28;
            this.checkBox3.Text = "3";
            this.checkBox3.UseVisualStyleBackColor = true;
            this.checkBox3.CheckedChanged += new System.EventHandler(this.checkBox3_CheckedChanged);
            // 
            // checkBox4
            // 
            this.checkBox4.AutoSize = true;
            this.checkBox4.Location = new System.Drawing.Point(120, 339);
            this.checkBox4.Name = "checkBox4";
            this.checkBox4.Size = new System.Drawing.Size(32, 17);
            this.checkBox4.TabIndex = 29;
            this.checkBox4.Text = "4";
            this.checkBox4.UseVisualStyleBackColor = true;
            this.checkBox4.CheckedChanged += new System.EventHandler(this.checkBox4_CheckedChanged);
            // 
            // checkBox5
            // 
            this.checkBox5.AutoSize = true;
            this.checkBox5.Location = new System.Drawing.Point(158, 339);
            this.checkBox5.Name = "checkBox5";
            this.checkBox5.Size = new System.Drawing.Size(32, 17);
            this.checkBox5.TabIndex = 30;
            this.checkBox5.Text = "5";
            this.checkBox5.UseVisualStyleBackColor = true;
            this.checkBox5.CheckedChanged += new System.EventHandler(this.checkBox5_CheckedChanged);
            // 
            // checkBox10
            // 
            this.checkBox10.AutoSize = true;
            this.checkBox10.Location = new System.Drawing.Point(348, 339);
            this.checkBox10.Name = "checkBox10";
            this.checkBox10.Size = new System.Drawing.Size(38, 17);
            this.checkBox10.TabIndex = 35;
            this.checkBox10.Text = "10";
            this.checkBox10.UseVisualStyleBackColor = true;
            this.checkBox10.CheckedChanged += new System.EventHandler(this.checkBox10_CheckedChanged);
            // 
            // checkBox9
            // 
            this.checkBox9.AutoSize = true;
            this.checkBox9.Location = new System.Drawing.Point(310, 339);
            this.checkBox9.Name = "checkBox9";
            this.checkBox9.Size = new System.Drawing.Size(32, 17);
            this.checkBox9.TabIndex = 34;
            this.checkBox9.Text = "9";
            this.checkBox9.UseVisualStyleBackColor = true;
            this.checkBox9.CheckedChanged += new System.EventHandler(this.checkBox9_CheckedChanged);
            // 
            // checkBox8
            // 
            this.checkBox8.AutoSize = true;
            this.checkBox8.Location = new System.Drawing.Point(272, 339);
            this.checkBox8.Name = "checkBox8";
            this.checkBox8.Size = new System.Drawing.Size(32, 17);
            this.checkBox8.TabIndex = 33;
            this.checkBox8.Text = "8";
            this.checkBox8.UseVisualStyleBackColor = true;
            this.checkBox8.CheckedChanged += new System.EventHandler(this.checkBox8_CheckedChanged);
            // 
            // checkBox7
            // 
            this.checkBox7.AutoSize = true;
            this.checkBox7.Location = new System.Drawing.Point(234, 339);
            this.checkBox7.Name = "checkBox7";
            this.checkBox7.Size = new System.Drawing.Size(32, 17);
            this.checkBox7.TabIndex = 32;
            this.checkBox7.Text = "7";
            this.checkBox7.UseVisualStyleBackColor = true;
            this.checkBox7.CheckedChanged += new System.EventHandler(this.checkBox7_CheckedChanged);
            // 
            // checkBox6
            // 
            this.checkBox6.AutoSize = true;
            this.checkBox6.Location = new System.Drawing.Point(196, 339);
            this.checkBox6.Name = "checkBox6";
            this.checkBox6.Size = new System.Drawing.Size(32, 17);
            this.checkBox6.TabIndex = 31;
            this.checkBox6.Text = "6";
            this.checkBox6.UseVisualStyleBackColor = true;
            this.checkBox6.CheckedChanged += new System.EventHandler(this.checkBox6_CheckedChanged);
            // 
            // checkBox19
            // 
            this.checkBox19.AutoSize = true;
            this.checkBox19.Location = new System.Drawing.Point(310, 362);
            this.checkBox19.Name = "checkBox19";
            this.checkBox19.Size = new System.Drawing.Size(38, 17);
            this.checkBox19.TabIndex = 44;
            this.checkBox19.Text = "19";
            this.checkBox19.UseVisualStyleBackColor = true;
            this.checkBox19.CheckedChanged += new System.EventHandler(this.checkBox19_CheckedChanged);
            // 
            // checkBox18
            // 
            this.checkBox18.AutoSize = true;
            this.checkBox18.Location = new System.Drawing.Point(272, 362);
            this.checkBox18.Name = "checkBox18";
            this.checkBox18.Size = new System.Drawing.Size(38, 17);
            this.checkBox18.TabIndex = 43;
            this.checkBox18.Text = "18";
            this.checkBox18.UseVisualStyleBackColor = true;
            this.checkBox18.CheckedChanged += new System.EventHandler(this.checkBox18_CheckedChanged);
            // 
            // checkBox17
            // 
            this.checkBox17.AutoSize = true;
            this.checkBox17.Location = new System.Drawing.Point(234, 362);
            this.checkBox17.Name = "checkBox17";
            this.checkBox17.Size = new System.Drawing.Size(38, 17);
            this.checkBox17.TabIndex = 42;
            this.checkBox17.Text = "17";
            this.checkBox17.UseVisualStyleBackColor = true;
            this.checkBox17.CheckedChanged += new System.EventHandler(this.checkBox17_CheckedChanged);
            // 
            // checkBox16
            // 
            this.checkBox16.AutoSize = true;
            this.checkBox16.Location = new System.Drawing.Point(196, 362);
            this.checkBox16.Name = "checkBox16";
            this.checkBox16.Size = new System.Drawing.Size(38, 17);
            this.checkBox16.TabIndex = 41;
            this.checkBox16.Text = "16";
            this.checkBox16.UseVisualStyleBackColor = true;
            this.checkBox16.CheckedChanged += new System.EventHandler(this.checkBox16_CheckedChanged);
            // 
            // checkBox15
            // 
            this.checkBox15.AutoSize = true;
            this.checkBox15.Location = new System.Drawing.Point(158, 362);
            this.checkBox15.Name = "checkBox15";
            this.checkBox15.Size = new System.Drawing.Size(38, 17);
            this.checkBox15.TabIndex = 40;
            this.checkBox15.Text = "15";
            this.checkBox15.UseVisualStyleBackColor = true;
            this.checkBox15.CheckedChanged += new System.EventHandler(this.checkBox15_CheckedChanged);
            // 
            // checkBox14
            // 
            this.checkBox14.AutoSize = true;
            this.checkBox14.Location = new System.Drawing.Point(120, 362);
            this.checkBox14.Name = "checkBox14";
            this.checkBox14.Size = new System.Drawing.Size(38, 17);
            this.checkBox14.TabIndex = 39;
            this.checkBox14.Text = "14";
            this.checkBox14.UseVisualStyleBackColor = true;
            this.checkBox14.CheckedChanged += new System.EventHandler(this.checkBox14_CheckedChanged);
            // 
            // checkBox13
            // 
            this.checkBox13.AutoSize = true;
            this.checkBox13.Location = new System.Drawing.Point(82, 362);
            this.checkBox13.Name = "checkBox13";
            this.checkBox13.Size = new System.Drawing.Size(38, 17);
            this.checkBox13.TabIndex = 38;
            this.checkBox13.Text = "13";
            this.checkBox13.UseVisualStyleBackColor = true;
            this.checkBox13.CheckedChanged += new System.EventHandler(this.checkBox13_CheckedChanged);
            // 
            // checkBox12
            // 
            this.checkBox12.AutoSize = true;
            this.checkBox12.Location = new System.Drawing.Point(44, 362);
            this.checkBox12.Name = "checkBox12";
            this.checkBox12.Size = new System.Drawing.Size(38, 17);
            this.checkBox12.TabIndex = 37;
            this.checkBox12.Text = "12";
            this.checkBox12.UseVisualStyleBackColor = true;
            this.checkBox12.CheckedChanged += new System.EventHandler(this.checkBox12_CheckedChanged);
            // 
            // checkBox11
            // 
            this.checkBox11.AutoSize = true;
            this.checkBox11.Location = new System.Drawing.Point(6, 362);
            this.checkBox11.Name = "checkBox11";
            this.checkBox11.Size = new System.Drawing.Size(38, 17);
            this.checkBox11.TabIndex = 36;
            this.checkBox11.Text = "11";
            this.checkBox11.UseVisualStyleBackColor = true;
            this.checkBox11.CheckedChanged += new System.EventHandler(this.checkBox11_CheckedChanged);
            // 
            // buttonGotoTradeTest
            // 
            this.buttonGotoTradeTest.Location = new System.Drawing.Point(9, 12);
            this.buttonGotoTradeTest.Name = "buttonGotoTradeTest";
            this.buttonGotoTradeTest.Size = new System.Drawing.Size(163, 35);
            this.buttonGotoTradeTest.TabIndex = 45;
            this.buttonGotoTradeTest.Text = "Направить окно на продажу";
            this.buttonGotoTradeTest.UseVisualStyleBackColor = true;
            this.buttonGotoTradeTest.Click += new System.EventHandler(this.buttonGotoTradeTest_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(264, 246);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(112, 88);
            this.button2.TabIndex = 46;
            this.button2.Text = "GotoWorkTest";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.Coral;
            this.button3.Location = new System.Drawing.Point(137, 246);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(112, 88);
            this.button3.TabIndex = 47;
            this.button3.Text = "Aion";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button5
            // 
            this.button5.BackColor = System.Drawing.Color.LightCoral;
            this.button5.Location = new System.Drawing.Point(178, 137);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(198, 35);
            this.button5.TabIndex = 48;
            this.button5.Text = "Найти окна";
            this.button5.UseVisualStyleBackColor = false;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // buttonNewAcc
            // 
            this.buttonNewAcc.BackColor = System.Drawing.Color.Pink;
            this.buttonNewAcc.Location = new System.Drawing.Point(9, 137);
            this.buttonNewAcc.Name = "buttonNewAcc";
            this.buttonNewAcc.Size = new System.Drawing.Size(163, 35);
            this.buttonNewAcc.TabIndex = 49;
            this.buttonNewAcc.Text = "Новые аккаунты";
            this.buttonNewAcc.UseVisualStyleBackColor = false;
            this.buttonNewAcc.Click += new System.EventHandler(this.buttonNewAcc_Click);
            // 
            // RunToCrater
            // 
            this.RunToCrater.BackColor = System.Drawing.Color.Lime;
            this.RunToCrater.Location = new System.Drawing.Point(9, 178);
            this.RunToCrater.Name = "RunToCrater";
            this.RunToCrater.Size = new System.Drawing.Size(163, 35);
            this.RunToCrater.TabIndex = 50;
            this.RunToCrater.Text = "Бежим до кратера";
            this.RunToCrater.UseVisualStyleBackColor = false;
            this.RunToCrater.Click += new System.EventHandler(this.RunToCrater_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(388, 219);
            this.Controls.Add(this.RunToCrater);
            this.Controls.Add(this.buttonNewAcc);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.buttonGotoTradeTest);
            this.Controls.Add(this.checkBox19);
            this.Controls.Add(this.checkBox18);
            this.Controls.Add(this.checkBox17);
            this.Controls.Add(this.checkBox16);
            this.Controls.Add(this.checkBox15);
            this.Controls.Add(this.checkBox14);
            this.Controls.Add(this.checkBox13);
            this.Controls.Add(this.checkBox12);
            this.Controls.Add(this.checkBox11);
            this.Controls.Add(this.checkBox10);
            this.Controls.Add(this.checkBox9);
            this.Controls.Add(this.checkBox8);
            this.Controls.Add(this.checkBox7);
            this.Controls.Add(this.checkBox6);
            this.Controls.Add(this.checkBox5);
            this.Controls.Add(this.checkBox4);
            this.Controls.Add(this.checkBox3);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.buttonWarning);
            this.Controls.Add(this.button_StandUp);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.buttonSuperSell);
            this.Controls.Add(this.labelInformation);
            this.Controls.Add(this.buttonReOpenWindowGE);
            this.Controls.Add(this.buttonOpenWindowGE);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.KeyPreview = true;
            this.Location = new System.Drawing.Point(1650, 740);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Америка  V 6.3";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonOpenWindowGE;
        private System.Windows.Forms.Button buttonReOpenWindowGE;
        private System.Windows.Forms.Label labelInformation;
        private System.Windows.Forms.Button buttonSuperSell;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button button_StandUp;
        private System.Windows.Forms.Button buttonWarning;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.CheckBox checkBox4;
        private System.Windows.Forms.CheckBox checkBox5;
        private System.Windows.Forms.CheckBox checkBox10;
        private System.Windows.Forms.CheckBox checkBox9;
        private System.Windows.Forms.CheckBox checkBox8;
        private System.Windows.Forms.CheckBox checkBox7;
        private System.Windows.Forms.CheckBox checkBox6;
        private System.Windows.Forms.CheckBox checkBox19;
        private System.Windows.Forms.CheckBox checkBox18;
        private System.Windows.Forms.CheckBox checkBox17;
        private System.Windows.Forms.CheckBox checkBox16;
        private System.Windows.Forms.CheckBox checkBox15;
        private System.Windows.Forms.CheckBox checkBox14;
        private System.Windows.Forms.CheckBox checkBox13;
        private System.Windows.Forms.CheckBox checkBox12;
        private System.Windows.Forms.CheckBox checkBox11;
        private System.Windows.Forms.Button buttonGotoTradeTest;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button buttonNewAcc;
        private System.Windows.Forms.Button RunToCrater;
    
    }
}

