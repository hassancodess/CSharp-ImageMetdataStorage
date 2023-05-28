namespace GalleryFolderTest
{
    partial class Form1
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
            this.lbl_path = new System.Windows.Forms.Label();
            this.btn_openFolder = new System.Windows.Forms.Button();
            this.listbox_imageFiles = new System.Windows.Forms.ListBox();
            this.listbox_image_metadata = new System.Windows.Forms.ListBox();
            this.btninsertintodatabase = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbl_path
            // 
            this.lbl_path.AutoSize = true;
            this.lbl_path.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_path.Location = new System.Drawing.Point(57, 30);
            this.lbl_path.Name = "lbl_path";
            this.lbl_path.Size = new System.Drawing.Size(60, 24);
            this.lbl_path.TabIndex = 1;
            this.lbl_path.Text = "label2";
            // 
            // btn_openFolder
            // 
            this.btn_openFolder.Location = new System.Drawing.Point(886, 31);
            this.btn_openFolder.Name = "btn_openFolder";
            this.btn_openFolder.Size = new System.Drawing.Size(148, 26);
            this.btn_openFolder.TabIndex = 2;
            this.btn_openFolder.Text = "Open Folder";
            this.btn_openFolder.UseVisualStyleBackColor = true;
            this.btn_openFolder.Click += new System.EventHandler(this.btn_openFolder_Click);
            // 
            // listbox_imageFiles
            // 
            this.listbox_imageFiles.FormattingEnabled = true;
            this.listbox_imageFiles.Location = new System.Drawing.Point(61, 80);
            this.listbox_imageFiles.Name = "listbox_imageFiles";
            this.listbox_imageFiles.Size = new System.Drawing.Size(352, 316);
            this.listbox_imageFiles.TabIndex = 3;
            // 
            // listbox_image_metadata
            // 
            this.listbox_image_metadata.FormattingEnabled = true;
            this.listbox_image_metadata.Location = new System.Drawing.Point(682, 80);
            this.listbox_image_metadata.Name = "listbox_image_metadata";
            this.listbox_image_metadata.Size = new System.Drawing.Size(598, 316);
            this.listbox_image_metadata.TabIndex = 4;
            // 
            // btninsertintodatabase
            // 
            this.btninsertintodatabase.Location = new System.Drawing.Point(465, 407);
            this.btninsertintodatabase.Name = "btninsertintodatabase";
            this.btninsertintodatabase.Size = new System.Drawing.Size(162, 63);
            this.btninsertintodatabase.TabIndex = 5;
            this.btninsertintodatabase.Text = "Insert into database";
            this.btninsertintodatabase.UseVisualStyleBackColor = true;
            this.btninsertintodatabase.Click += new System.EventHandler(this.btninsertintodatabase_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1292, 638);
            this.Controls.Add(this.btninsertintodatabase);
            this.Controls.Add(this.listbox_image_metadata);
            this.Controls.Add(this.listbox_imageFiles);
            this.Controls.Add(this.btn_openFolder);
            this.Controls.Add(this.lbl_path);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_path;
        private System.Windows.Forms.Button btn_openFolder;
        private System.Windows.Forms.ListBox listbox_imageFiles;
        private System.Windows.Forms.ListBox listbox_image_metadata;
        private System.Windows.Forms.Button btninsertintodatabase;
    }
}

