namespace GalleryFolderTest
{
    partial class Form2
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
            this.btninsertintodatabase = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btninsertintodatabase
            // 
            this.btninsertintodatabase.Location = new System.Drawing.Point(296, 112);
            this.btninsertintodatabase.Name = "btninsertintodatabase";
            this.btninsertintodatabase.Size = new System.Drawing.Size(75, 23);
            this.btninsertintodatabase.TabIndex = 1;
            this.btninsertintodatabase.Text = "Insert into database";
            this.btninsertintodatabase.UseVisualStyleBackColor = true;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btninsertintodatabase);
            this.Name = "Form2";
            this.Text = "Form2";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btninsertintodatabase;
    }
}