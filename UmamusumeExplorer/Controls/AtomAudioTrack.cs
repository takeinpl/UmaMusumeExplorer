using CriWareLibrary;
using NAudio.Wave;
using UmamusumeAudio;

namespace UmamusumeExplorer.Controls
{
    internal class AtomAudioTrack : IAudioTrack
    {
        private readonly FileStream acbFile;
        private readonly AwbReader awbReader;
        private readonly int index;

        public AtomAudioTrack(string acbPath, string awbPath, int waveIndex)
        {
            acbFile = File.OpenRead(acbPath);

            AcbParser acbNameLoader = new(acbFile);

            bool hasMemory = false;
            if (string.IsNullOrEmpty(awbPath))
            {
                AcbReader acbReader = new(acbFile);
                awbReader = acbReader.GetAwb();

                hasMemory = acbReader.HasMemoryAwb;
            }
            else
            {
                awbReader = new(File.OpenRead(awbPath));
            }

            index = waveIndex;

            Name = acbNameLoader.LoadWaveName(waveIndex, 0, hasMemory);
        }

        public string Name { get; }

        public WaveStream WaveStream
        {
            get
            {
                return new UmaWaveStream(awbReader, awbReader.Waves[index].WaveId);
            }
        }
    }
}
