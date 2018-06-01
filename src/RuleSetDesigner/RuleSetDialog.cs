using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Workflow.Activities.Rules;
using System.Workflow.ComponentModel;
using System.Workflow.ComponentModel.Compiler;

// ReSharper disable UnusedMember.Global
namespace RuleSetDesigner
{
    public class RuleSetDialog : System.Workflow.Activities.Rules.Design.RuleSetDialog
    {
        private const string RulesListViewControlName = "rulesListView";

        public RuleSetDialog(Activity activity, RuleSet ruleSet) : base(activity, ruleSet)
        {
            InitializeComponents();
        }

        public RuleSetDialog(Type activityType, ITypeProvider typeProvider, RuleSet ruleSet) : base(activityType, typeProvider, ruleSet)
        {
            InitializeComponents();
        }

        public event EventHandler Save;

        private static void RulesListViewOnResize(object sender, EventArgs e)
        {
            SizeRulesListViewColumns((ListView)sender);
        }

        private static void SizeRulesListViewColumns(ListView lv)
        {
            var widthRatio = (double)lv.Width / 523;
            lv.Columns[0].Width = (int)(102 * widthRatio);
            lv.Columns[1].Width = (int)(60 * widthRatio);
            lv.Columns[2].Width = (int)(91 * widthRatio);
            lv.Columns[3].Width = (int)(59 * widthRatio);
            lv.Columns[4].Width = (int)(211 * widthRatio);
        }

        private void DialogOnLoad(object sender, EventArgs e)
        {
            var rulesListView = GetControl(RulesListViewControlName);
            SizeRulesListViewColumns((ListView)rulesListView);
        }

        private Control GetControl(string key)
        {
            return Controls.Find(key, true).Single();
        }

        private void InitializeComponents()
        {
            var chainingBehaviourComboBox = GetControl("chainingBehaviourComboBox");
            var chainingLabel = GetControl("chainingLabel");
            var closeButton = (Button)GetControl("buttonCancel");
            var conditionLabel = GetControl("conditionLabel");
            var conditionTextBox = GetControl("conditionTextBox");
            var elseLabel = GetControl("elseLabel");
            var elseTextBox = GetControl("elseTextBox");
            var okCancelTableLayoutPanel = (TableLayoutPanel)GetControl("okCancelTableLayoutPanel");
            var panel1 = GetControl("panel1");
            var ruleGroupBox = GetControl("ruleGroupBox");
            var rulesGroupBox = GetControl("rulesGroupBox");
            var rulesListView = GetControl(RulesListViewControlName);
            var saveAndCloseButton = GetControl("buttonOK");
            var thenLabel = GetControl("thenLabel");
            var thenTextBox = GetControl("thenTextBox");

            var ruleGroupBoxSize = ruleGroupBox.Size;
            var rulePanelSize = new Size(ruleGroupBoxSize.Width - 6, 69);
            var rulesGroupBoxSize = rulesGroupBox.Size;
            var splitContainerLocation = rulesGroupBox.Location;
            var splitContainerSize = new Size(rulesGroupBoxSize.Width,
                ruleGroupBox.Location.Y - rulesGroupBox.Location.Y + ruleGroupBoxSize.Height);
            var tableLayoutPanelLocation = new Point(3, conditionLabel.Location.Y);
            var tableLayoutPanelSize = new Size(ruleGroupBoxSize.Width - 6,
                ruleGroupBoxSize.Height - conditionLabel.Location.Y);

            var conditionPanel = new Panel();
            var elsePanel = new Panel();
            var saveButton = new Button();
            var splitContainer = new SplitContainer();
            var tableLayoutPanel = new TableLayoutPanel();
            var thenPanel = new Panel();

            SuspendLayout();
            okCancelTableLayoutPanel.SuspendLayout();
            rulesGroupBox.SuspendLayout();
            panel1.SuspendLayout();
            ruleGroupBox.SuspendLayout();
            ((ISupportInitialize)splitContainer).BeginInit();
            splitContainer.SuspendLayout();
            tableLayoutPanel.SuspendLayout();
            conditionPanel.SuspendLayout();
            thenPanel.SuspendLayout();
            elsePanel.SuspendLayout();

            //
            // chainingBehaviourComboBox
            //
            chainingBehaviourComboBox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            //
            // chainingLabel
            //
            chainingLabel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            //
            // closeButton
            //
            closeButton.Name = "closeButton";
            closeButton.Text = "Close";
            //
            // conditionLabel
            //
            conditionLabel.Location = new Point(7, 0);
            //
            // conditionPanel
            //
            conditionPanel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
            conditionPanel.Controls.Add(conditionLabel);
            conditionPanel.Controls.Add(conditionTextBox);
            conditionPanel.Size = rulePanelSize;
            //
            // conditionTextBox
            //
            conditionTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
            conditionTextBox.Location = new Point(7, 17);
            //
            // elseLabel
            //
            elseLabel.Location = new Point(7, 0);
            //
            // elseTextBox
            //
            elseTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
            elseTextBox.Location = new Point(7, 17);
            //
            // elsePanel
            //
            elsePanel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
            elsePanel.Controls.Add(elseLabel);
            elsePanel.Controls.Add(elseTextBox);
            elsePanel.Size = rulePanelSize;
            //
            // okCancelTableLayoutPanel
            //
            okCancelTableLayoutPanel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            okCancelTableLayoutPanel.ColumnCount = 3;
            okCancelTableLayoutPanel.ColumnStyles.Clear();
            okCancelTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            okCancelTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.34F));
            okCancelTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.35F));
            okCancelTableLayoutPanel.Controls.Clear();
            okCancelTableLayoutPanel.Controls.Add(saveButton, 0, 0);
            okCancelTableLayoutPanel.Controls.Add(saveAndCloseButton, 1, 0);
            okCancelTableLayoutPanel.Controls.Add(closeButton, 2, 0);
            //
            // ruleGroupBox
            //
            ruleGroupBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
            ruleGroupBox.Controls.Add(tableLayoutPanel);
            ruleGroupBox.Controls.Remove(conditionLabel);
            ruleGroupBox.Controls.Remove(conditionTextBox);
            ruleGroupBox.Controls.Remove(thenLabel);
            ruleGroupBox.Controls.Remove(thenTextBox);
            ruleGroupBox.Controls.Remove(elseLabel);
            ruleGroupBox.Controls.Remove(elseTextBox);
            ruleGroupBox.Location = new Point(0, 0);
            //
            // rulesGroupBox
            //
            rulesGroupBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
            rulesGroupBox.Location = new Point(0, 0);
            rulesGroupBox.Size = new Size(rulesGroupBoxSize.Width - 10, splitContainerSize.Height - 10);
            //
            // rulesListView
            //
            rulesListView.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
            rulesListView.Resize += RulesListViewOnResize;
            //
            // panel1
            //
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
            //
            // saveAndCloseButton
            //
            saveAndCloseButton.Click += SaveAndCloseButtonOnClick;
            saveAndCloseButton.Margin = new Padding(3, 0, 3, 0);
            saveAndCloseButton.Name = "saveAndCloseButton";
            saveAndCloseButton.Text = @"Save && Close";
            //
            // saveButton
            //
            saveButton.Click += SaveButtonOnClick;
            saveButton.Margin = new Padding(0, 0, 3, 0);
            saveButton.Name = "saveButton";
            saveButton.Text = @"Save";
            //
            // splitContainer
            //
            splitContainer.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
            splitContainer.Location = splitContainerLocation;
            splitContainer.Name = "splitContainer";
            splitContainer.Orientation = Orientation.Horizontal;
            splitContainer.Panel1.Controls.Add(rulesGroupBox);
            splitContainer.Panel2.Controls.Add(ruleGroupBox);
            splitContainer.Size = splitContainerSize;
            splitContainer.SplitterDistance = rulesGroupBoxSize.Height;
            //
            // tableLayoutPanel
            //
            tableLayoutPanel.ColumnCount = 1;
            tableLayoutPanel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel.Location = tableLayoutPanelLocation;
            tableLayoutPanel.Name = "tableLayoutPanel";
            tableLayoutPanel.RowCount = 3;
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 33.34F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33F));
            tableLayoutPanel.Controls.Add(conditionPanel, 0, 0);
            tableLayoutPanel.Controls.Add(thenPanel, 0, 1);
            tableLayoutPanel.Controls.Add(elsePanel, 0, 2);
            tableLayoutPanel.Size = tableLayoutPanelSize;
            //
            // thenLabel
            //
            thenLabel.Location = new Point(7, 0);
            //
            // thenPanel
            //
            thenPanel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
            thenPanel.Controls.Add(thenLabel);
            thenPanel.Controls.Add(thenTextBox);
            thenPanel.Size = rulePanelSize;
            //
            // thenTextBox
            //
            thenTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
            thenTextBox.Location = new Point(7, 17);
            //
            // RuleSetDialog
            //
            AcceptButton = null;
            CancelButton = null;
            Controls.Add(splitContainer);
            Controls.Remove(rulesGroupBox);
            Controls.Remove(ruleGroupBox);
            FormBorderStyle = FormBorderStyle.Sizable;
            Load += DialogOnLoad;
            MaximizeBox = true;
            MinimizeBox = true;
            MinimumSize = new Size(710, 594);
            ShowInTaskbar = true;
            SizeGripStyle = SizeGripStyle.Auto;
            StartPosition = FormStartPosition.CenterParent;
            ((ISupportInitialize)splitContainer).EndInit();
            conditionPanel.ResumeLayout(false);
            thenPanel.ResumeLayout(false);
            elsePanel.ResumeLayout(false);
            tableLayoutPanel.ResumeLayout(false);
            rulesGroupBox.ResumeLayout(false);
            panel1.ResumeLayout(false);
            ruleGroupBox.ResumeLayout(false);
            splitContainer.ResumeLayout(false);
            okCancelTableLayoutPanel.ResumeLayout(false);
            ResumeLayout(false);
        }

        private void SaveAndCloseButtonOnClick(object sender, EventArgs e)
        {
            Save?.Invoke(this, null);
        }

        private void SaveButtonOnClick(object sender, EventArgs e)
        {
            Save?.Invoke(this, null);
        }
    }
}