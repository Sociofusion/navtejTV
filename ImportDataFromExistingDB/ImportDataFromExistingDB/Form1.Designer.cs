namespace ImportDataFromExistingDB
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
            this.btnImportCategory = new System.Windows.Forms.Button();
            this.btnImportPost = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnImportCategory
            // 
            this.btnImportCategory.Location = new System.Drawing.Point(157, 44);
            this.btnImportCategory.Name = "btnImportCategory";
            this.btnImportCategory.Size = new System.Drawing.Size(181, 48);
            this.btnImportCategory.TabIndex = 0;
            this.btnImportCategory.Text = "Import Category";
            this.btnImportCategory.UseVisualStyleBackColor = true;
            this.btnImportCategory.Click += new System.EventHandler(this.btnImportCategory_Click);
            // 
            // btnImportPost
            // 
            this.btnImportPost.Location = new System.Drawing.Point(382, 44);
            this.btnImportPost.Name = "btnImportPost";
            this.btnImportPost.Size = new System.Drawing.Size(181, 48);
            this.btnImportPost.TabIndex = 1;
            this.btnImportPost.Text = "Import Post";
            this.btnImportPost.UseVisualStyleBackColor = true;
            this.btnImportPost.Click += new System.EventHandler(this.btnImportPost_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnImportPost);
            this.Controls.Add(this.btnImportCategory);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnImportCategory;
        private System.Windows.Forms.Button btnImportPost;
    }
}

