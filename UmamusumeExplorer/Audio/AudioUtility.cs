using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UmamusumeExplorer.Audio
{
    internal static class AudioUtility
    {
        public static long BytesToSamples(long bytes, WaveFormat waveFormat)
        {
            return bytes / waveFormat.Channels / (waveFormat.BitsPerSample / 8);
        }

        public static long SamplesToBytes(long samples, WaveFormat waveFormat)
        {
            return samples * waveFormat.Channels * (waveFormat.BitsPerSample / 8);
        }
    }
}
