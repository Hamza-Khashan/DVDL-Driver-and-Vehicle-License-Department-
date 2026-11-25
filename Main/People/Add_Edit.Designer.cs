namespace Main
{
    partial class Add_Edit
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
            this.label1 = new System.Windows.Forms.Label();
            this.ctrlAddPerson1 = new Main.ctrlAddPerson();
            this.label2 = new System.Windows.Forms.Label();
            this.PersonID = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Brown;
            this.label1.Location = new System.Drawing.Point(301, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(246, 33);
            this.label1.TabIndex = 1;
            this.label1.Text = "Add New Person";
            // 
            // ctrlAddPerson1
            // 
            this.ctrlAddPerson1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctrlAddPerson1.Location = new System.Drawing.Point(14, 90);
            this.ctrlAddPerson1.Name = "ctrlAddPerson1";
            this.ctrlAddPerson1.Size = new System.Drawing.Size(825, 370);
            this.ctrlAddPerson1.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(39, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "Person ID:";
            // 
            // PersonID
            // 
            this.PersonID.AutoSize = true;
            this.PersonID.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PersonID.Location = new System.Drawing.Point(141, 60);
            this.PersonID.Name = "PersonID";
            this.PersonID.Size = new System.Drawing.Size(39, 16);
            this.PersonID.TabIndex = 4;
            this.PersonID.Text = "????";
            // 
            // Add_Edit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(851, 472);
            this.Controls.Add(this.PersonID);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ctrlAddPerson1);
            this.Controls.Add(this.label1);
            this.Name = "Add_Edit";
            this.Text = "Add/Edit Person Info.";
            this.Load += new System.EventHandler(this.Add_Edit_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private ctrlAddPerson ctrlAddPerson1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label PersonID;
    }
}