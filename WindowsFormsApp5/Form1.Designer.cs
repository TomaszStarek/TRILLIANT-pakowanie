namespace WindowsFormsApp5
{
    partial class Form1
    {
        /// <summary>
        /// Wymagana zmienna projektanta.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Wyczyść wszystkie używane zasoby.
        /// </summary>
        /// <param name="disposing">prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kod generowany przez Projektanta formularzy systemu Windows

        /// <summary>
        /// Metoda wymagana do obsługi projektanta — nie należy modyfikować
        /// jej zawartości w edytorze kodu.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.labelCountVerifiedPiecesToBox = new System.Windows.Forms.Label();
            this.labelPozostalo = new System.Windows.Forms.Label();
            this.label300 = new System.Windows.Forms.Label();
            this.labelScanOut = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.labelStatusInfo = new System.Windows.Forms.Label();
            this.buttonWygenerujBarcode = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.labelBarcode1Accepted = new System.Windows.Forms.Label();
            this.labelBarcode2Accepted = new System.Windows.Forms.Label();
            this.buttonIfMesDoneThenPlcDone = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.labelUTR = new System.Windows.Forms.Label();
            this.labelPasswUTR = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.button7 = new System.Windows.Forms.Button();
            this.BtnErrorOccursAccept = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.buttonDoneBoxNoBarcode = new System.Windows.Forms.Button();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button9 = new System.Windows.Forms.Button();
            this.labelCountPackedPieces = new System.Windows.Forms.Label();
            this.labelZweryfikowano = new System.Windows.Forms.Label();
            this.label300_2 = new System.Windows.Forms.Label();
            this.button10 = new System.Windows.Forms.Button();
            this.button11 = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.labelScanOut2 = new System.Windows.Forms.Label();
            this.labelScanInfoPAK = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.textBox1.Location = new System.Drawing.Point(10, 42);
            this.textBox1.Margin = new System.Windows.Forms.Padding(2);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(180, 28);
            this.textBox1.TabIndex = 0;
            this.textBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.HighlightText;
            this.button1.Location = new System.Drawing.Point(14, 400);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(112, 19);
            this.button1.TabIndex = 1;
            this.button1.Text = "WŁ TRIGGER";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.SystemColors.HighlightText;
            this.button2.Location = new System.Drawing.Point(14, 423);
            this.button2.Margin = new System.Windows.Forms.Padding(2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(112, 19);
            this.button2.TabIndex = 2;
            this.button2.Text = "WYŁ TRIGGER";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(191, 24);
            this.label1.TabIndex = 3;
            this.label1.Text = "Zeskanowany Barkod";
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.SystemColors.HighlightText;
            this.button3.Location = new System.Drawing.Point(538, 400);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(130, 67);
            this.button3.TabIndex = 4;
            this.button3.Text = "Wyślij do sterownika:\r\nSKAN_OK\r\n";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // labelCountVerifiedPiecesToBox
            // 
            this.labelCountVerifiedPiecesToBox.AutoSize = true;
            this.labelCountVerifiedPiecesToBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelCountVerifiedPiecesToBox.Location = new System.Drawing.Point(309, 50);
            this.labelCountVerifiedPiecesToBox.Name = "labelCountVerifiedPiecesToBox";
            this.labelCountVerifiedPiecesToBox.Size = new System.Drawing.Size(18, 20);
            this.labelCountVerifiedPiecesToBox.TabIndex = 5;
            this.labelCountVerifiedPiecesToBox.Text = "0";
            this.labelCountVerifiedPiecesToBox.Click += new System.EventHandler(this.button7_Click);
            // 
            // labelPozostalo
            // 
            this.labelPozostalo.AutoSize = true;
            this.labelPozostalo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelPozostalo.Location = new System.Drawing.Point(244, 21);
            this.labelPozostalo.Name = "labelPozostalo";
            this.labelPozostalo.Size = new System.Drawing.Size(215, 20);
            this.labelPozostalo.TabIndex = 5;
            this.labelPozostalo.Text = "Pozostało do końca boxu:";
            this.labelPozostalo.Click += new System.EventHandler(this.button7_Click);
            // 
            // label300
            // 
            this.label300.AutoSize = true;
            this.label300.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label300.Location = new System.Drawing.Point(339, 50);
            this.label300.Name = "label300";
            this.label300.Size = new System.Drawing.Size(44, 20);
            this.label300.TabIndex = 5;
            this.label300.Text = "/ 300";
            this.label300.Click += new System.EventHandler(this.button7_Click);
            // 
            // labelScanOut
            // 
            this.labelScanOut.AutoSize = true;
            this.labelScanOut.Location = new System.Drawing.Point(12, 477);
            this.labelScanOut.Name = "labelScanOut";
            this.labelScanOut.Size = new System.Drawing.Size(145, 13);
            this.labelScanOut.TabIndex = 6;
            this.labelScanOut.Text = "                                              ";
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.SystemColors.HighlightText;
            this.button4.Location = new System.Drawing.Point(342, 410);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(77, 57);
            this.button4.TabIndex = 7;
            this.button4.Text = "Zerowanie";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // labelStatusInfo
            // 
            this.labelStatusInfo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.labelStatusInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelStatusInfo.Location = new System.Drawing.Point(13, 84);
            this.labelStatusInfo.Name = "labelStatusInfo";
            this.labelStatusInfo.Size = new System.Drawing.Size(741, 106);
            this.labelStatusInfo.TabIndex = 8;
            this.labelStatusInfo.Text = "                             ";
            // 
            // buttonWygenerujBarcode
            // 
            this.buttonWygenerujBarcode.BackColor = System.Drawing.Color.HotPink;
            this.buttonWygenerujBarcode.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.buttonWygenerujBarcode.ForeColor = System.Drawing.Color.LavenderBlush;
            this.buttonWygenerujBarcode.Location = new System.Drawing.Point(10, 193);
            this.buttonWygenerujBarcode.Name = "buttonWygenerujBarcode";
            this.buttonWygenerujBarcode.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.buttonWygenerujBarcode.Size = new System.Drawing.Size(180, 88);
            this.buttonWygenerujBarcode.TabIndex = 11;
            this.buttonWygenerujBarcode.Text = "Wygeneruj barkody";
            this.buttonWygenerujBarcode.UseVisualStyleBackColor = false;
            this.buttonWygenerujBarcode.Visible = false;
            this.buttonWygenerujBarcode.Click += new System.EventHandler(this.button6_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(13, 461);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(148, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "bufor RS232 ze skanera:";
            // 
            // labelBarcode1Accepted
            // 
            this.labelBarcode1Accepted.AutoSize = true;
            this.labelBarcode1Accepted.BackColor = System.Drawing.Color.LavenderBlush;
            this.labelBarcode1Accepted.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelBarcode1Accepted.Location = new System.Drawing.Point(289, 193);
            this.labelBarcode1Accepted.Name = "labelBarcode1Accepted";
            this.labelBarcode1Accepted.Padding = new System.Windows.Forms.Padding(10);
            this.labelBarcode1Accepted.Size = new System.Drawing.Size(140, 33);
            this.labelBarcode1Accepted.TabIndex = 13;
            this.labelBarcode1Accepted.Text = "niezaakceptowano1";
            this.labelBarcode1Accepted.Visible = false;
            // 
            // labelBarcode2Accepted
            // 
            this.labelBarcode2Accepted.AutoSize = true;
            this.labelBarcode2Accepted.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelBarcode2Accepted.Location = new System.Drawing.Point(289, 233);
            this.labelBarcode2Accepted.Name = "labelBarcode2Accepted";
            this.labelBarcode2Accepted.Padding = new System.Windows.Forms.Padding(10);
            this.labelBarcode2Accepted.Size = new System.Drawing.Size(140, 33);
            this.labelBarcode2Accepted.TabIndex = 13;
            this.labelBarcode2Accepted.Text = "niezaakceptowano2";
            this.labelBarcode2Accepted.Visible = false;
            // 
            // buttonIfMesDoneThenPlcDone
            // 
            this.buttonIfMesDoneThenPlcDone.BackColor = System.Drawing.Color.Plum;
            this.buttonIfMesDoneThenPlcDone.Location = new System.Drawing.Point(555, 193);
            this.buttonIfMesDoneThenPlcDone.Name = "buttonIfMesDoneThenPlcDone";
            this.buttonIfMesDoneThenPlcDone.Size = new System.Drawing.Size(180, 88);
            this.buttonIfMesDoneThenPlcDone.TabIndex = 14;
            this.buttonIfMesDoneThenPlcDone.Text = "Sprawdź płytki w MES i wyslij sygnał końca boxu do PLC";
            this.buttonIfMesDoneThenPlcDone.UseVisualStyleBackColor = false;
            this.buttonIfMesDoneThenPlcDone.Visible = false;
            this.buttonIfMesDoneThenPlcDone.Click += new System.EventHandler(this.buttonIfMesDoneThenPlcDone_Click);
            // 
            // button6
            // 
            this.button6.BackColor = System.Drawing.SystemColors.HighlightText;
            this.button6.Location = new System.Drawing.Point(429, 404);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(103, 63);
            this.button6.TabIndex = 15;
            this.button6.Text = "Wyślij do sterownika: \r\nKoniec partii\r\n";
            this.button6.UseVisualStyleBackColor = false;
            this.button6.Click += new System.EventHandler(this.button6_Click_2);
            // 
            // button5
            // 
            this.button5.BackColor = System.Drawing.SystemColors.Window;
            this.button5.Location = new System.Drawing.Point(246, 410);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(82, 57);
            this.button5.TabIndex = 16;
            this.button5.Text = "Odkryj okienka";
            this.button5.UseVisualStyleBackColor = false;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // labelUTR
            // 
            this.labelUTR.Location = new System.Drawing.Point(7, 392);
            this.labelUTR.Name = "labelUTR";
            this.labelUTR.Size = new System.Drawing.Size(969, 138);
            this.labelUTR.TabIndex = 17;
            this.labelUTR.Text = "       ";
            // 
            // labelPasswUTR
            // 
            this.labelPasswUTR.AutoSize = true;
            this.labelPasswUTR.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelPasswUTR.Location = new System.Drawing.Point(21, 348);
            this.labelPasswUTR.Name = "labelPasswUTR";
            this.labelPasswUTR.Size = new System.Drawing.Size(97, 16);
            this.labelPasswUTR.TabIndex = 18;
            this.labelPasswUTR.Text = "Podaj hasło:";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(17, 369);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(102, 20);
            this.textBox2.TabIndex = 19;
            this.textBox2.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // button7
            // 
            this.button7.BackColor = System.Drawing.Color.Transparent;
            this.button7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.button7.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.button7.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button7.ForeColor = System.Drawing.Color.Violet;
            this.button7.Location = new System.Drawing.Point(238, 5);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(226, 76);
            this.button7.TabIndex = 20;
            this.button7.UseVisualStyleBackColor = false;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // BtnErrorOccursAccept
            // 
            this.BtnErrorOccursAccept.BackColor = System.Drawing.Color.Fuchsia;
            this.BtnErrorOccursAccept.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.BtnErrorOccursAccept.ForeColor = System.Drawing.Color.LavenderBlush;
            this.BtnErrorOccursAccept.Location = new System.Drawing.Point(10, 295);
            this.BtnErrorOccursAccept.Name = "BtnErrorOccursAccept";
            this.BtnErrorOccursAccept.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.BtnErrorOccursAccept.Size = new System.Drawing.Size(725, 39);
            this.BtnErrorOccursAccept.TabIndex = 11;
            this.BtnErrorOccursAccept.Text = "Potwierdż wystąpienie błędów!";
            this.BtnErrorOccursAccept.UseVisualStyleBackColor = false;
            this.BtnErrorOccursAccept.Visible = false;
            this.BtnErrorOccursAccept.Click += new System.EventHandler(this.BtnErrorOccursAccept_Click);
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(679, 444);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(75, 23);
            this.button8.TabIndex = 21;
            this.button8.Text = "button8";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Visible = false;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // buttonDoneBoxNoBarcode
            // 
            this.buttonDoneBoxNoBarcode.BackColor = System.Drawing.Color.DarkMagenta;
            this.buttonDoneBoxNoBarcode.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.buttonDoneBoxNoBarcode.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.buttonDoneBoxNoBarcode.Location = new System.Drawing.Point(10, 287);
            this.buttonDoneBoxNoBarcode.Margin = new System.Windows.Forms.Padding(2);
            this.buttonDoneBoxNoBarcode.Name = "buttonDoneBoxNoBarcode";
            this.buttonDoneBoxNoBarcode.Size = new System.Drawing.Size(731, 58);
            this.buttonDoneBoxNoBarcode.TabIndex = 22;
            this.buttonDoneBoxNoBarcode.Text = "Zakończ pakowanie rolki";
            this.buttonDoneBoxNoBarcode.UseVisualStyleBackColor = false;
            this.buttonDoneBoxNoBarcode.Visible = false;
            this.buttonDoneBoxNoBarcode.Click += new System.EventHandler(this.button9_Click);
            // 
            // textBox3
            // 
            this.textBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.textBox3.Location = new System.Drawing.Point(780, 42);
            this.textBox3.Margin = new System.Windows.Forms.Padding(2);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(194, 28);
            this.textBox3.TabIndex = 0;
            this.textBox3.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label3.Location = new System.Drawing.Point(783, 16);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(188, 24);
            this.label3.TabIndex = 3;
            this.label3.Text = "Skaner PAKOWANIE";
            // 
            // button9
            // 
            this.button9.BackColor = System.Drawing.Color.White;
            this.button9.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.button9.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.button9.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button9.ForeColor = System.Drawing.Color.Violet;
            this.button9.Location = new System.Drawing.Point(511, 5);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(226, 76);
            this.button9.TabIndex = 20;
            this.button9.UseVisualStyleBackColor = false;
            this.button9.Click += new System.EventHandler(this.button7_Click);
            // 
            // labelCountPackedPieces
            // 
            this.labelCountPackedPieces.AutoSize = true;
            this.labelCountPackedPieces.BackColor = System.Drawing.Color.White;
            this.labelCountPackedPieces.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelCountPackedPieces.Location = new System.Drawing.Point(585, 50);
            this.labelCountPackedPieces.Name = "labelCountPackedPieces";
            this.labelCountPackedPieces.Size = new System.Drawing.Size(18, 20);
            this.labelCountPackedPieces.TabIndex = 5;
            this.labelCountPackedPieces.Text = "0";
            this.labelCountPackedPieces.Click += new System.EventHandler(this.button7_Click);
            // 
            // labelZweryfikowano
            // 
            this.labelZweryfikowano.AutoSize = true;
            this.labelZweryfikowano.BackColor = System.Drawing.Color.White;
            this.labelZweryfikowano.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelZweryfikowano.Location = new System.Drawing.Point(534, 21);
            this.labelZweryfikowano.Name = "labelZweryfikowano";
            this.labelZweryfikowano.Size = new System.Drawing.Size(185, 20);
            this.labelZweryfikowano.TabIndex = 5;
            this.labelZweryfikowano.Text = "Zweryfikowano płytek:";
            this.labelZweryfikowano.Click += new System.EventHandler(this.button7_Click);
            // 
            // label300_2
            // 
            this.label300_2.AutoSize = true;
            this.label300_2.BackColor = System.Drawing.Color.White;
            this.label300_2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label300_2.Location = new System.Drawing.Point(615, 50);
            this.label300_2.Name = "label300_2";
            this.label300_2.Size = new System.Drawing.Size(44, 20);
            this.label300_2.TabIndex = 5;
            this.label300_2.Text = "/ 300";
            this.label300_2.Click += new System.EventHandler(this.button7_Click);
            // 
            // button10
            // 
            this.button10.BackColor = System.Drawing.SystemColors.HighlightText;
            this.button10.Location = new System.Drawing.Point(780, 400);
            this.button10.Margin = new System.Windows.Forms.Padding(2);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(112, 19);
            this.button10.TabIndex = 1;
            this.button10.Text = "WŁ TRIGGER";
            this.button10.UseVisualStyleBackColor = false;
            this.button10.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // button11
            // 
            this.button11.BackColor = System.Drawing.SystemColors.HighlightText;
            this.button11.Location = new System.Drawing.Point(780, 423);
            this.button11.Margin = new System.Windows.Forms.Padding(2);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(112, 19);
            this.button11.TabIndex = 2;
            this.button11.Text = "WYŁ TRIGGER";
            this.button11.UseVisualStyleBackColor = false;
            this.button11.Click += new System.EventHandler(this.button2_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label7.Location = new System.Drawing.Point(779, 461);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(176, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "bufor RS232 ze skanera PAK:";
            // 
            // labelScanOut2
            // 
            this.labelScanOut2.AutoSize = true;
            this.labelScanOut2.Location = new System.Drawing.Point(777, 477);
            this.labelScanOut2.Name = "labelScanOut2";
            this.labelScanOut2.Size = new System.Drawing.Size(145, 13);
            this.labelScanOut2.TabIndex = 6;
            this.labelScanOut2.Text = "                                              ";
            // 
            // labelScanInfoPAK
            // 
            this.labelScanInfoPAK.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelScanInfoPAK.Location = new System.Drawing.Point(777, 84);
            this.labelScanInfoPAK.Name = "labelScanInfoPAK";
            this.labelScanInfoPAK.Size = new System.Drawing.Size(194, 106);
            this.labelScanInfoPAK.TabIndex = 23;
            // 
            // Form1
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LavenderBlush;
            this.ClientSize = new System.Drawing.Size(994, 543);
            this.Controls.Add(this.labelScanInfoPAK);
            this.Controls.Add(this.buttonDoneBoxNoBarcode);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.labelPasswUTR);
            this.Controls.Add(this.labelScanOut2);
            this.Controls.Add(this.labelScanOut);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.labelUTR);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.buttonIfMesDoneThenPlcDone);
            this.Controls.Add(this.labelBarcode2Accepted);
            this.Controls.Add(this.labelBarcode1Accepted);
            this.Controls.Add(this.BtnErrorOccursAccept);
            this.Controls.Add(this.buttonWygenerujBarcode);
            this.Controls.Add(this.labelStatusInfo);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.label300_2);
            this.Controls.Add(this.labelZweryfikowano);
            this.Controls.Add(this.label300);
            this.Controls.Add(this.labelCountPackedPieces);
            this.Controls.Add(this.labelPozostalo);
            this.Controls.Add(this.labelCountVerifiedPiecesToBox);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button11);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.button10);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button9);
            this.Controls.Add(this.button7);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.ShowIcon = false;
            this.Text = "Aplikacja Skaner";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label labelCountVerifiedPiecesToBox;
        private System.Windows.Forms.Label labelPozostalo;
        private System.Windows.Forms.Label label300;
        private System.Windows.Forms.Label labelScanOut;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label labelStatusInfo;
        private System.Windows.Forms.Button buttonWygenerujBarcode;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelBarcode1Accepted;
        private System.Windows.Forms.Label labelBarcode2Accepted;
        private System.Windows.Forms.Button buttonIfMesDoneThenPlcDone;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Label labelUTR;
        private System.Windows.Forms.Label labelPasswUTR;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button BtnErrorOccursAccept;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button buttonDoneBoxNoBarcode;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Label labelCountPackedPieces;
        private System.Windows.Forms.Label labelZweryfikowano;
        private System.Windows.Forms.Label label300_2;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.Button button11;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label labelScanOut2;
        private System.Windows.Forms.Label labelScanInfoPAK;
    }
}

