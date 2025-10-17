using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UmamusumeExplorer.Controls
{
    public partial class IncompleteExportForm : Form
    {
        public IncompleteExportForm(IDictionary<AudioSource, Exception> failedFiles)
        {
            InitializeComponent();

            foreach (var file in failedFiles)
            {
                failedFilesList.Items.Add(new ListViewItem([file.Key.Name, file.Value.Message]));
            }
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
