namespace RuleSetDesignerLauncher
{
    partial class Launcher
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
            this.LaunchButton = new System.Windows.Forms.Button();
            this.TypeFilterTextBox = new System.Windows.Forms.TextBox();
            this.RuleSetFileNameBrowseButton = new System.Windows.Forms.Button();
            this.ActivityTypesListBox = new System.Windows.Forms.ListBox();
            this.BrowseForActivityTypeAssembliesButton = new System.Windows.Forms.Button();
            this.RuleSetFileNameTextBox = new System.Windows.Forms.TextBox();
            this.RuleSetFileNameLabel = new System.Windows.Forms.Label();
            this.RuleSetFileNameDialog = new System.Windows.Forms.OpenFileDialog();
            this.ActivityTypeAssembliesFolderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.TypeFilterLabel = new System.Windows.Forms.Label();
            this.RefreshButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // LaunchButton
            // 
            this.LaunchButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.LaunchButton.Location = new System.Drawing.Point(379, 255);
            this.LaunchButton.Name = "LaunchButton";
            this.LaunchButton.Size = new System.Drawing.Size(75, 23);
            this.LaunchButton.TabIndex = 1;
            this.LaunchButton.Text = "Launch";
            this.LaunchButton.UseVisualStyleBackColor = true;
            this.LaunchButton.Click += new System.EventHandler(this.LaunchButton_Click);
            // 
            // TypeFilterTextBox
            // 
            this.TypeFilterTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TypeFilterTextBox.Location = new System.Drawing.Point(12, 77);
            this.TypeFilterTextBox.Name = "TypeFilterTextBox";
            this.TypeFilterTextBox.Size = new System.Drawing.Size(280, 20);
            this.TypeFilterTextBox.TabIndex = 2;
            this.TypeFilterTextBox.Text = "Context";
            this.TypeFilterTextBox.TextChanged += new System.EventHandler(this.TypeFilterTextBox_TextChanged);
            // 
            // RuleSetFileNameBrowseButton
            // 
            this.RuleSetFileNameBrowseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.RuleSetFileNameBrowseButton.Location = new System.Drawing.Point(379, 32);
            this.RuleSetFileNameBrowseButton.Name = "RuleSetFileNameBrowseButton";
            this.RuleSetFileNameBrowseButton.Size = new System.Drawing.Size(75, 23);
            this.RuleSetFileNameBrowseButton.TabIndex = 3;
            this.RuleSetFileNameBrowseButton.Text = "Browse";
            this.RuleSetFileNameBrowseButton.UseVisualStyleBackColor = true;
            this.RuleSetFileNameBrowseButton.Click += new System.EventHandler(this.RuleSetFileNameBrowseButton_Click);
            // 
            // ActivityTypesListBox
            // 
            this.ActivityTypesListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ActivityTypesListBox.FormattingEnabled = true;
            this.ActivityTypesListBox.Location = new System.Drawing.Point(12, 103);
            this.ActivityTypesListBox.Name = "ActivityTypesListBox";
            this.ActivityTypesListBox.Size = new System.Drawing.Size(442, 147);
            this.ActivityTypesListBox.TabIndex = 4;
            this.ActivityTypesListBox.SelectedIndexChanged += new System.EventHandler(this.ActivityTypesListBox_SelectedIndexChanged);
            this.ActivityTypesListBox.DoubleClick += new System.EventHandler(this.ActivityTypesListBox_DoubleClick);
            // 
            // BrowseForActivityTypeAssembliesButton
            // 
            this.BrowseForActivityTypeAssembliesButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BrowseForActivityTypeAssembliesButton.Location = new System.Drawing.Point(379, 74);
            this.BrowseForActivityTypeAssembliesButton.Name = "BrowseForActivityTypeAssembliesButton";
            this.BrowseForActivityTypeAssembliesButton.Size = new System.Drawing.Size(75, 23);
            this.BrowseForActivityTypeAssembliesButton.TabIndex = 5;
            this.BrowseForActivityTypeAssembliesButton.Text = "Browse";
            this.BrowseForActivityTypeAssembliesButton.UseVisualStyleBackColor = true;
            this.BrowseForActivityTypeAssembliesButton.Click += new System.EventHandler(this.BrowseForActivityTypeAssembliesButton_Click);
            // 
            // RuleSetFileNameTextBox
            // 
            this.RuleSetFileNameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RuleSetFileNameTextBox.Location = new System.Drawing.Point(12, 32);
            this.RuleSetFileNameTextBox.Name = "RuleSetFileNameTextBox";
            this.RuleSetFileNameTextBox.Size = new System.Drawing.Size(361, 20);
            this.RuleSetFileNameTextBox.TabIndex = 6;
            this.RuleSetFileNameTextBox.TextChanged += new System.EventHandler(this.RuleSetFileNameTextBox_TextChanged);
            // 
            // RuleSetFileNameLabel
            // 
            this.RuleSetFileNameLabel.AutoSize = true;
            this.RuleSetFileNameLabel.Location = new System.Drawing.Point(13, 13);
            this.RuleSetFileNameLabel.Name = "RuleSetFileNameLabel";
            this.RuleSetFileNameLabel.Size = new System.Drawing.Size(101, 13);
            this.RuleSetFileNameLabel.TabIndex = 7;
            this.RuleSetFileNameLabel.Text = "Rule Set File Name:";
            // 
            // RuleSetFileNameDialog
            // 
            this.RuleSetFileNameDialog.CheckFileExists = false;
            this.RuleSetFileNameDialog.CheckPathExists = false;
            this.RuleSetFileNameDialog.DefaultExt = "xml";
            this.RuleSetFileNameDialog.FileName = "ruleset.xml";
            this.RuleSetFileNameDialog.Filter = "Rule Set files|*.xml|All files|*.*";
            this.RuleSetFileNameDialog.Title = "Select Rule Set File Name";
            // 
            // ActivityTypeAssembliesFolderBrowserDialog
            // 
            this.ActivityTypeAssembliesFolderBrowserDialog.Description = "Select Activity Type Assembly Folder";
            // 
            // TypeFilterLabel
            // 
            this.TypeFilterLabel.AutoSize = true;
            this.TypeFilterLabel.Location = new System.Drawing.Point(13, 61);
            this.TypeFilterLabel.Name = "TypeFilterLabel";
            this.TypeFilterLabel.Size = new System.Drawing.Size(59, 13);
            this.TypeFilterLabel.TabIndex = 8;
            this.TypeFilterLabel.Text = "Type Filter:";
            // 
            // RefreshButton
            // 
            this.RefreshButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.RefreshButton.Location = new System.Drawing.Point(298, 74);
            this.RefreshButton.Name = "RefreshButton";
            this.RefreshButton.Size = new System.Drawing.Size(75, 23);
            this.RefreshButton.TabIndex = 9;
            this.RefreshButton.Text = "Refresh";
            this.RefreshButton.UseVisualStyleBackColor = true;
            this.RefreshButton.Click += new System.EventHandler(this.RefreshButton_Click);
            // 
            // Launcher
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(466, 290);
            this.Controls.Add(this.RefreshButton);
            this.Controls.Add(this.TypeFilterLabel);
            this.Controls.Add(this.RuleSetFileNameLabel);
            this.Controls.Add(this.RuleSetFileNameTextBox);
            this.Controls.Add(this.BrowseForActivityTypeAssembliesButton);
            this.Controls.Add(this.ActivityTypesListBox);
            this.Controls.Add(this.RuleSetFileNameBrowseButton);
            this.Controls.Add(this.TypeFilterTextBox);
            this.Controls.Add(this.LaunchButton);
            this.Name = "Launcher";
            this.Text = "Rule Set Designer Launcher";
            this.Load += new System.EventHandler(this.Launcher_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button LaunchButton;
        private System.Windows.Forms.TextBox TypeFilterTextBox;
        private System.Windows.Forms.Button RuleSetFileNameBrowseButton;
        private System.Windows.Forms.ListBox ActivityTypesListBox;
        private System.Windows.Forms.Button BrowseForActivityTypeAssembliesButton;
        private System.Windows.Forms.TextBox RuleSetFileNameTextBox;
        private System.Windows.Forms.Label RuleSetFileNameLabel;
        private System.Windows.Forms.OpenFileDialog RuleSetFileNameDialog;
        private System.Windows.Forms.FolderBrowserDialog ActivityTypeAssembliesFolderBrowserDialog;
        private System.Windows.Forms.Label TypeFilterLabel;
        private System.Windows.Forms.Button RefreshButton;
    }
}

