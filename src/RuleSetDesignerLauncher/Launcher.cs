using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using RuleSetDesignerLauncher.Properties;

namespace RuleSetDesignerLauncher
{
    public partial class Launcher : Form
    {
        private readonly string[] _assemblySearchPatterns = {"*.dll", "*.exe"};
        private TypeInfo[] _typeInfo;

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
                ScanAssembliesForTypeInfo();
                TypeFilterTextBox.Text = Settings.Default.TypFilter;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void RefreshButton_Click(object sender, EventArgs e)
        {
            ScanAssembliesForTypeInfo();
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

        private void ActivityTypesListBox_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (CanLaunch())
                {
                    Launch();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BrowseForActivityTypeAssemblies()
        {
            if (ActivityTypeAssembliesFolderBrowserDialog.ShowDialog(this) == DialogResult.OK)
            {
                System.IO.Directory.SetCurrentDirectory(ActivityTypeAssembliesFolderBrowserDialog.SelectedPath);
                ScanAssembliesForTypeInfo();
            }
        }

        private void BrowseForRuleSetFileName()
        {
            if (RuleSetFileNameDialog.ShowDialog() == DialogResult.OK)
            {
                RuleSetFileNameTextBox.Text = RuleSetFileNameDialog.FileName;
            }
        }

        private bool CanLaunch()
        {
            return !string.IsNullOrWhiteSpace(RuleSetFileNameTextBox.Text) &&
                   ActivityTypesListBox.SelectedItem != null;
        }

        private void Launch()
        {
            var loaderDomain = AppDomainFactory.CreateWithShadowCopy();
            try
            {
                using (var dialog = RuleSetDialogFactory.Get(loaderDomain, (TypeInfo) ActivityTypesListBox.SelectedItem,
                    RuleSetFileNameTextBox.Text))
                {
                    Hide();
                    dialog.ShowDialog();
                }
            }
            finally
            {
                try
                {
                    Show();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                AppDomain.Unload(loaderDomain);
            }
        }

        private void RefreshActivityTypeList()
        {
            var regex = Regex.TryCreate(TypeFilterTextBox.Text, RegexOptions.IgnoreCase);
            // ReSharper disable once AssignNullToNotNullAttribute
            var types = _typeInfo.Where(x => regex?.IsMatch(x.FullName) ?? false)
                .Cast<object>().ToArray();
            ActivityTypesListBox.Items.Clear();
            if (types.Any())
            {
                ActivityTypesListBox.Items.AddRange(types);
                ActivityTypesListBox.SelectedIndex = 0;
            }

            RefreshLaunchEnabled();
        }

        private void RefreshLaunchEnabled()
        {
            LaunchButton.Enabled = CanLaunch();
        }

        private void ScanAssembliesForTypeInfo()
        {
            var files = Directory.GetFiles(_assemblySearchPatterns).ToList();
            _typeInfo = TypeInfoFactory.Get(files);
            RefreshActivityTypeList();
        }
    }
}