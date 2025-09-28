using CriWareLibrary;

namespace UmamusumeExplorer.Controls
{
    internal class AtomAudioSource : AudioSource
    {
        private readonly string acb;
        private readonly string awb;

        public AtomAudioSource(string name, string acbPath, string awbPath)
        {
            Name = name;
            acb = acbPath;
            awb = awbPath;

            using var acbReader = new AcbReader(File.OpenRead(acbPath));

            if (string.IsNullOrEmpty(awbPath) && acbReader.HasMemoryAwb)
            {
                TrackCount = acbReader.GetAwb().Waves.Count;
            }
            else
            {
                if (string.IsNullOrEmpty(awbPath)) return;

                using var awbReader = new AwbReader(File.OpenRead(awbPath));
                TrackCount = awbReader.Waves.Count;
            }
        }

        public override string Name { get; }

        public override int TrackCount { get; }

        protected override IAudioTrack[] InitializeTracks()
        {
            AtomAudioTrack[] tracks = new AtomAudioTrack[TrackCount];

            for (int i = 0; i < TrackCount; i++)
            {
                tracks[i] = new AtomAudioTrack(acb, awb, i);
            }

            return tracks;
        }
    }
}