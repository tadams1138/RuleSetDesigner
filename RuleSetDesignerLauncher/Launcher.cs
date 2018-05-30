using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using RuleSetDesigner;
using RuleSetDesignerLauncher.Properties;

namespace RuleSetDesignerLauncher
{
    public partial class Launcher : Form
    {
        private readonly string[] _assemblySearchPatterns = { "*.dll", "*.exe" };
        private List<System.Reflection.Assembly> _assemblies;

        public Launcher()
        {
            InitializeComponent();
            ActivityTypeAssembliesFolderBrowserDialog.SelectedPath = System.IO.Directory.GetCurrentDirectory();
            RuleSetFileNameDialog.InitialDirectory = System.IO.Directory.GetCurrentDirectory();            
        }

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
                TypeFilterTextBox.Text = Settings.Default.TypFilter;
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

        public void SetRuleSetFileName(string ruleSetFileName)
        {
            RuleSetFileNameTextBox.Text = ruleSetFileName;
        }

        private void BrowseForActivityTypeAssemblies()
        {
            if (ActivityTypeAssembliesFolderBrowserDialog.ShowDialog(this) == DialogResult.OK)
            {
                System.IO.Directory.SetCurrentDirectory(ActivityTypeAssembliesFolderBrowserDialog.SelectedPath);
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

        private static System.Type GetActivityType(object item)
        {
            var reflectionOnlyType = (System.Type) item;
            var retval = Type.GetTypeFromReflectionOnlyType(reflectionOnlyType);
            return retval;
        }

        private RuleSetDialog GetDialog()
        {
            var ruleSet = RuleSetGateway.GetRuleSet(RuleSetFileNameTextBox.Text);
            var activityType = GetActivityType(ActivityTypesListBox.SelectedItem);
            var retval = new RuleSetDialog(activityType, null, ruleSet);
            retval.Save += DialogOnSave;
            return retval;
        }

        private void Launch()
        {
            using (var dialog = GetDialog())
            {
                Hide();
                dialog.ShowDialog(this);
                Show();
            }
        }

        private void RefreshActivityTypeList()
        {
            var regex = Regex.TryCreate(TypeFilterTextBox.Text, RegexOptions.IgnoreCase);
            // ReSharper disable once AssignNullToNotNullAttribute
            var types = _assemblies.SelectMany(x => x.GetTypes())
                .Where(x => regex?.IsMatch(x.FullName) ?? false)
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
            var files = Directory.GetFiles(_assemblySearchPatterns);
            _assemblies = files.Select(Assembly.TryReflectionOnlyLoadFrom).Where(x => x != null).ToList();
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
