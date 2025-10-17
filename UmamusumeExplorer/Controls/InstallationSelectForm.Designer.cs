namespace UmamusumeExplorer.Controls
{
    partial class InstallationSelectForm
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
            installationListBox = new ListBox();
            listBoxLabel = new Label();
            okButton = new Button();
            cancelButton = new Button();
            SuspendLayout();
            // 
            // installationListBox
            // 
            installationListBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            installationListBox.FormattingEnabled = true;
            installationListBox.ItemHeight = 15;
            installationListBox.Location = new Point(12, 27);
            installationListBox.Name = "installationListBox";
            installationListBox.Size = new Size(404, 124);
            installationListBox.TabIndex = 1;
            // 
            // listBoxLabel
            // 
            listBoxLabel.AutoSize = true;
            listBoxLabel.Location = new Point(12, 9);
            listBoxLabel.Name = "listBoxLabel";
            listBoxLabel.Size = new Size(339, 15);
            listBoxLabel.TabIndex = 0;
            listBoxLabel.Text = "Multiple installations detected. Please select which one to load:";
            // 
            // okButton
            // 
            okButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            okButton.Location = new Point(260, 174);
            okButton.Name = "okButton";
            okButton.Size = new Size(75, 23);
            okButton.TabIndex = 2;
            okButton.Text = "OK";
            okButton.UseVisualStyleBackColor = true;
            okButton.Click += OkButton_Click;
            // 
            // cancelButton
            // 
            cancelButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            cancelButton.Location = new Point(341, 174);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(75, 23);
            cancelButton.TabIndex = 3;
            cancelButton.Text = "Cancel";
            cancelButton.UseVisualStyleBackColor = true;
            cancelButton.Click += cancelButton_Click;
            // 
            // InstallationSelectForm
            // 
            AcceptButton = okButton;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = cancelButton;
            ClientSize = new Size(428, 209);
            Controls.Add(cancelButton);
            Controls.Add(okButton);
            Controls.Add(listBoxLabel);
            Controls.Add(installationListBox);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "InstallationSelectForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Installation Select";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListBox installationListBox;
        private Label listBoxLabel;
        private Button okButton;
        private Button cancelButton;
    }
}