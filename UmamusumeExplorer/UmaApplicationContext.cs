using UmamsumeData;
using UmamusumeData.DataDirectories;
using UmamusumeExplorer.Assets;
using UmamusumeExplorer.Controls;

namespace UmamusumeExplorer
{
    class UmaApplicationContext : ApplicationContext
    {
        private const string pathTextFile = "UmamusumePath.txt";

        public UmaApplicationContext()
        {
            if (File.Exists(pathTextFile))
                UmaDataHelper.UmamusumeDirectory = File.ReadAllText(pathTextFile).Trim();

            List<DataDirectory> dataDirectories = UmaDataHelper.ScanDirectories();
            if (dataDirectories.Count > 1)
            {
                InstallationSelectForm installationSelectForm = new(dataDirectories);
                DialogResult installationSelectResult = installationSelectForm.ShowDialog();

                if (installationSelectResult == DialogResult.OK)
                {
                    UmaDataHelper.UmamusumeDirectory = installationSelectForm.Path;
                }
                else
                {
                    Environment.Exit(1);
                    return;
                }    
            }
            else
            {
                UmaDataHelper.UmamusumeDirectory = dataDirectories.First().DataDirectoryPath;
            }

            if (!UmaDataHelper.CheckDirectory())
            {
                DialogResult result = MessageBox.Show(
                    "Unable to find the required files in the default known directories.\n\n" +
                    "If you have not installed, launched, or updated the game, please do so. " +
                    "Otherwise, please manually specify the data directory.\n\n" +
                    "Would you like to continue?",
                    "Missing files",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Information);
                if (result == DialogResult.Yes)
                {
                    while (true)
                    {
                        FolderBrowserDialog browserDialog = new();
                        DialogResult browseResult = browserDialog.ShowDialog();

                        if (browseResult == DialogResult.OK)
                        {
                            UmaDataHelper.UmamusumeDirectory = browserDialog.SelectedPath;

                            if (UmaDataHelper.CheckDirectory())
                            {
                                File.WriteAllText(pathTextFile, UmaDataHelper.UmamusumeDirectory);
                                break;
                            }

                            MessageBox.Show("Please select a valid data directory containing the \"dat\" folder, " +
                                "the \"meta\" file, and the \"master\" folder with the \"master.mdb\" file.",
                                "Missing files",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                        }
                        else
                        {
                            Environment.Exit(1);
                            return;
                        }
                    }
                }
                else
                {
                    Environment.Exit(1);
                    return;
                }
            }

            SplashForm splashForm = new();
            if (splashForm.LoadAndClose())
            {
                MainForm mainForm = new();
                mainForm.Show();
                MainForm = mainForm;
                //mainForm.FormClosed += (s, e) => ExitThread();

                GameAssets.MainForm = mainForm;
            }
        }
    }
}
