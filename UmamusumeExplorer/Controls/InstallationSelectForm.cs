using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UmamusumeData.DataDirectories;

namespace UmamusumeExplorer.Controls
{
    public partial class InstallationSelectForm : Form
    {
        private readonly IEnumerable<DataDirectory> dataDirectories;

        public InstallationSelectForm(IEnumerable<DataDirectory> dataDirectories)
        {
            InitializeComponent();

            foreach (var item in dataDirectories)
            {
                installationListBox.Items.Add(item.Name);
            }

            this.dataDirectories = dataDirectories;
        }

        public string Path { get; private set; } = "";

        private void OkButton_Click(object sender, EventArgs e)
        {
            Path = dataDirectories.ElementAt(installationListBox.SelectedIndex).DataDirectoryPath;
            DialogResult = DialogResult.OK;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
