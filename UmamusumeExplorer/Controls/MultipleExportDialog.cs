using NAudio.Wave;
using System.ComponentModel;
using UmamusumeAudio;

namespace UmamusumeExplorer.Controls
{
    public partial class MultipleExportDialog : Form
    {
        private readonly AudioSource[] sources;
        private readonly string outputPath;
        private readonly Dictionary<AudioSource, Exception> exceptions = [];

        private int completed = 0;

        public MultipleExportDialog(AudioSource[] audioSources, string outputDirectory)
        {
            InitializeComponent();

            sources = audioSources;
            outputPath = outputDirectory;

            exportBackgroundWorker.RunWorkerAsync();
        }

        private void ExportBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            int currentFile = 1;

            foreach (var audioSource in sources)
            {
                if (exportBackgroundWorker.CancellationPending)
                {
                    e.Cancel = true;
                    break;
                }

                State state;
                state.Name = audioSource.Name;
                state.CurrentFile = currentFile;

                exportBackgroundWorker.ReportProgress((int)(((float)currentFile / sources.Length) * 100), state);

                try
                {
                    if (audioSource.Tracks.Length > 1)
                    {
                        string directory = Path.Combine(outputPath, audioSource.Name);
                        Directory.CreateDirectory(directory);

                        foreach (var track in audioSource.Tracks)
                        {
                            UmaWaveStream waveStream = (UmaWaveStream)track.WaveStream;
                            waveStream.Loop = false;

                            string fileName = Path.Combine(directory, track.Name + ".wav");
                            if (fileName.Length > 128)
                                fileName = fileName[..128];

                            WaveFileWriter.CreateWaveFile16(fileName, waveStream.ToSampleProvider());
                        }
                    }
                    else
                    {
                        IAudioTrack audioTrack = audioSource.Tracks[0];
                        UmaWaveStream waveStream = (UmaWaveStream)audioTrack.WaveStream;
                        waveStream.Loop = false;

                        string fileName = Path.Combine(outputPath, audioTrack.Name + ".wav");
                        if (fileName.Length > 128)
                            fileName = fileName[..128];

                        WaveFileWriter.CreateWaveFile16(fileName, waveStream.ToSampleProvider());
                    }

                    completed++;
                }
                catch (Exception ex)
                {
                    exceptions.Add(audioSource, ex);
                    progressLabel.ForeColor = Color.DarkOrange;
                }

                currentFile++;
            }
        }

        private void ExportBackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage;

            if (e.UserState is not State state) return;

            Text = $"Exporting... ({e.ProgressPercentage}%)";
            currentFileLabel.Text = "Exporting " + state.Name;
            progressLabel.Text = $"{state.CurrentFile} of {sources.Length}";
        }

        private void ExportBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
                MessageBox.Show("Export cancelled.", "Cancelled", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else if (completed != sources.Length)
                MessageBox.Show($"Export incomplete. ", "Incomplete", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            else
                MessageBox.Show("Export complete.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            if (exceptions.Count > 0)
            {
                ControlHelpers.ShowFormDialogCenter(new IncompleteExportForm(exceptions), this);
            }

            Close();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            exportBackgroundWorker.CancelAsync();
        }

        struct State
        {
            public string Name;
            public int CurrentFile;
        }
    }
}
