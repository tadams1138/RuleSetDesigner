using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Workflow.Activities.Rules;
using RuleSetDesigner;

namespace RuleSetDesignerLauncher
{
    public partial class Launcher : Form
    {
        private List<System.Reflection.Assembly> _assemblies;

        public Launcher()
        {
            InitializeComponent();
            ActivityTypeAssembliesFolderBrowserDialog.SelectedPath = System.IO.Directory.GetCurrentDirectory();
            RuleSetFileNameDialog.InitialDirectory = System.IO.Directory.GetCurrentDirectory();
        }

        public Launcher(string ruleSetFileName, string activityTypeAssemblyFolder) : this()
        {
            ActivityTypeAssembliesFolderBrowserDialog.SelectedPath = activityTypeAssemblyFolder;
            RuleSetFileNameTextBox.Text = ruleSetFileName;
        }

        public string[] AssemblySearchPatterns = { "*.dll", "*.exe" };
        
        #region UI Events

        private void ActivityTypesListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                RefreshLaunchEnabled();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BrowseForActivityTypeAssembliesButton_Click(object sender, EventArgs e)
        {
            try
            {
                BrowseForActivityTypeAssemblies();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DialogOnSave(object sender, EventArgs e)
        {
            try
            {
                SaveRuleSet(sender);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LaunchButton_Click(object sender, EventArgs e)
        {
            try
            {
                Launch();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Launcher_Load(object sender, EventArgs e)
        {
            try
            {
                RefreshAssembliesList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void RuleSetFileNameBrowseButton_Click(object sender, EventArgs e)
        {
            try
            {
                BrowseForRuleSetFileName();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void RuleSetFileNameTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                RefreshLaunchEnabled();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void TypeFilterTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                RefreshActivityTypeList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        private void BrowseForActivityTypeAssemblies()
        {
            if (ActivityTypeAssembliesFolderBrowserDialog.ShowDialog(this) == DialogResult.OK)
            {
                RefreshAssembliesList();
            }
        }

        private void BrowseForRuleSetFileName()
        {
            if (RuleSetFileNameDialog.ShowDialog() == DialogResult.OK)
            {
                RuleSetFileNameTextBox.Text = RuleSetFileNameDialog.FileName;
            }
        }

        private RuleSetDialog GetDialog(Type activityType, RuleSet ruleSet)
        {
            var retval = new RuleSetDialog(activityType, null, ruleSet);
            retval.Save += DialogOnSave;
            return retval;
        }

        private void Launch()
        {
            var ruleSet = RuleSetGateway.GetRuleSet(RuleSetFileNameTextBox.Text);
            using (var dialog = GetDialog((Type)ActivityTypesListBox.SelectedItem, ruleSet))
            {
                Hide();
                dialog.ShowDialog(this);
                Show();
            }
        }

        private void RefreshActivityTypeList()
        {
            var regex = Regex.TryCreate(TypeFilterTextBox.Text, RegexOptions.IgnoreCase);
            var types = _assemblies.SelectMany(x => x.GetTypes())
                .Where(x => x.IsClass && !x.IsAbstract && x.IsPublic && (regex?.IsMatch(x.Name) ?? false))
                .Cast<object>().ToArray();
            ActivityTypesListBox.Items.Clear();
            if (types.Any())
            {
                ActivityTypesListBox.Items.AddRange(types);
                ActivityTypesListBox.SelectedIndex = 0;
            }
            RefreshLaunchEnabled();
        }

        private void RefreshAssembliesList()
        {
            var files = Directory.GetFiles(ActivityTypeAssembliesFolderBrowserDialog.SelectedPath, AssemblySearchPatterns, SearchOption.AllDirectories);
            _assemblies = files.Select(Assembly.TryLoadFile).Where(x => x != null).ToList();
            RefreshActivityTypeList();
        }

        private void RefreshLaunchEnabled()
        {
            LaunchButton.Enabled = !string.IsNullOrWhiteSpace(RuleSetFileNameTextBox.Text) &&
                                 ActivityTypesListBox.SelectedItem != null;
        }

        private void SaveRuleSet(object sender)
        {
            var dialog = (RuleSetDialog)sender;
            RuleSetGateway.SaveRuleSet(dialog.RuleSet, RuleSetFileNameTextBox.Text);
        }
    }
}
