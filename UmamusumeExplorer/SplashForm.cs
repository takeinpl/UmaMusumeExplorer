using UmamsumeData;
using UmamusumeExplorer.Assets;

namespace UmamusumeExplorer
{
    public partial class SplashForm : Form
    {
        private Thread? loadThread;
        private bool loadSuccess = false;

        public SplashForm()
        {
            InitializeComponent();
        }

        public bool LoadAndClose()
        {
            loadThread = new(() =>
            {
                try
                {
                    UmaDataHelper.Initialize();
                    AssetTables.UpdateProgress += UpdateProgress;
                    AssetTables.Initialize();
                    AssetTables.UpdateProgress -= UpdateProgress;
                    Invoke(() => Close());
                    loadSuccess = true;
                }
                catch (Exception e)
                {
                    string message = e.InnerException?.Message ?? e.Message;
                    MessageBox.Show($"Error reading tables.\n\nMessage:\n{message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.Exit();
                }
            });
            ShowDialog();

            return loadSuccess;
        }

        private void UpdateProgress(int progress, string name)
        {
            Invoke(() =>
            {
                loadingProgressBar.Value = progress;
                if (progress < 100)
                loadingLabel.Text = $"Loading {name}...";
                else
                    loadingLabel.Text = "Starting...";
            });
        }

        private void SplashForm_Load(object sender, EventArgs e)
        {
            loadThread?.Start();
        }
    }
}
