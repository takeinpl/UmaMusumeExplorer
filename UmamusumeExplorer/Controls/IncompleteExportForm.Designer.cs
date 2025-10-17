namespace UmamusumeExplorer.Controls
{
    partial class IncompleteExportForm
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
            exportFailedLabel = new Label();
            failedFilesList = new ListView();
            nameHeader = new ColumnHeader();
            reasonHeader = new ColumnHeader();
            okButton = new Button();
            SuspendLayout();
            // 
            // exportFailedLabel
            // 
            exportFailedLabel.AutoSize = true;
            exportFailedLabel.Location = new Point(12, 9);
            exportFailedLabel.Name = "exportFailedLabel";
            exportFailedLabel.Size = new Size(198, 15);
            exportFailedLabel.TabIndex = 0;
            exportFailedLabel.Text = "Export failed for the following items:";
            // 
            // failedFilesList
            // 
            failedFilesList.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            failedFilesList.Columns.AddRange(new ColumnHeader[] { nameHeader, reasonHeader });
            failedFilesList.FullRowSelect = true;
            failedFilesList.Location = new Point(12, 27);
            failedFilesList.Name = "failedFilesList";
            failedFilesList.Size = new Size(641, 275);
            failedFilesList.TabIndex = 1;
            failedFilesList.UseCompatibleStateImageBehavior = false;
            failedFilesList.View = View.Details;
            // 
            // nameHeader
            // 
            nameHeader.Text = "File";
            nameHeader.Width = 240;
            // 
            // reasonHeader
            // 
            reasonHeader.Text = "Reason";
            reasonHeader.Width = 397;
            // 
            // okButton
            // 
            okButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            okButton.Location = new Point(578, 308);
            okButton.Name = "okButton";
            okButton.Size = new Size(75, 23);
            okButton.TabIndex = 2;
            okButton.Text = "OK";
            okButton.UseVisualStyleBackColor = true;
            okButton.Click += OkButton_Click;
            // 
            // IncompleteExportForm
            // 
            AcceptButton = okButton;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(665, 343);
            Controls.Add(okButton);
            Controls.Add(failedFilesList);
            Controls.Add(exportFailedLabel);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "IncompleteExportForm";
            StartPosition = FormStartPosition.Manual;
            Text = "Incomplete Export";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label exportFailedLabel;
        private ListView failedFilesList;
        private Button retryButton;
        private ColumnHeader nameHeader;
        private ColumnHeader reasonHeader;
        private Button okButton;
    }
}