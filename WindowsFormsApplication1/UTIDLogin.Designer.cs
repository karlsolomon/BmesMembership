using System;

namespace WindowsFormsApplication1
{
    partial class UTIDLogin
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
            this.utIDTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.noID = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // utIDTextBox
            // 
            this.utIDTextBox.AcceptsReturn = true;
            this.utIDTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.utIDTextBox.Location = new System.Drawing.Point(69, 44);
            this.utIDTextBox.Name = "utIDTextBox";
            this.utIDTextBox.Size = new System.Drawing.Size(203, 26);
            this.utIDTextBox.TabIndex = 0;
            this.utIDTextBox.TextChanged += new System.EventHandler(this.utID_Send);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(43, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(184, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Please Swipe your UT ID";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "UT ID";
            // 
            // noID
            // 
            this.noID.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.noID.Location = new System.Drawing.Point(36, 88);
            this.noID.Name = "noID";
            this.noID.Size = new System.Drawing.Size(203, 38);
            this.noID.TabIndex = 3;
            this.noID.Text = "I don\'t have my UT ID";
            this.noID.UseVisualStyleBackColor = true;
            this.noID.Click += new System.EventHandler(this.noID_Click);
            // 
            // UTIDLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 138);
            this.Controls.Add(this.noID);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.utIDTextBox);
            this.Name = "UTIDLogin";
            this.Text = "UT ID Login";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox utIDTextBox;
        private System.Windows.Forms.Button noID;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}

