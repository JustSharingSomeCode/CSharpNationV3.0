using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpNation.Visualizer.Waves
{
    public static class WaveTools
    {
        public static float Clamp(float value, float min, float max)
        {
            return (value < min) ? min : (value > max) ? max : value;
        }

        public static int Clamp(int min, int max, int value)
        {
            return (value < min) ? min : (value > max) ? max : value;
        }

        public static List<float> FixDiscontinuities(List<float> spectrum)
        {
            float[] fixedSpectrum = new float[spectrum.Count];

            fixedSpectrum[0] = spectrum[0];

            for (int i = 1; i < spectrum.Count - 1; i++)
            {
                if (spectrum[i] < fixedSpectrum[i - 1] && spectrum[i] < spectrum[i + 1])
                {
                    fixedSpectrum[i] = (fixedSpectrum[i - 1] + spectrum[i + 1]) / 2;
                }
                else
                {
                    fixedSpectrum[i] = spectrum[i];
                }
            }

            fixedSpectrum[fixedSpectrum.Length - 1] = spectrum[spectrum.Count - 1];

            return fixedSpectrum.ToList();
        }

        public static List<float> Normalize(List<float> spectrum)
        {
            float[] list = new float[spectrum.Count];
            float max = spectrum.Max();

            for (int i = 0; i < spectrum.Count; i++)
            {
                list[i] = spectrum[i] / max;
            }

            return list.ToList();
        }

        public static List<float> AvgWave(List<float> spectrum, int bars)
        {
            float[] avgSpectrum = new float[spectrum.Count];
            float max = spectrum.Max();

            float value;
            int count;

            for (int i = 0; i < spectrum.Count; i++)
            {
                count = 1;
                value = spectrum[i];

                for (int j = i - bars; j <= i + bars; j++)
                {
                    if (j < 0 || j >= spectrum.Count || j == i)
                    {
                        continue;
                    }

                    value += spectrum[j];
                    count++;
                }

                value /= count;
                avgSpectrum[i] = value;
            }

            List<float> result = Normalize(avgSpectrum.ToList());

            for (int i = 0; i < result.Count; i++)
            {
                result[i] = result[i] * max;
            }

            return result;
        }

        public static List<float> SmoothWave(List<float> actualSpectrum, List<float> previousSpectrum, float deltaFactor)
        {
            if (actualSpectrum == null || previousSpectrum == null)
            {
                return actualSpectrum;
            }

            float[] deltas = new float[actualSpectrum.Count];

            for (int i = 0; i < actualSpectrum.Count; i++)
            {
                deltas[i] = (actualSpectrum[i] - previousSpectrum[i]) * deltaFactor;
            }

            float[] result = new float[previousSpectrum.Count];

            for (int i = 0; i < result.Length; i++)
            {
                //previousSpectrum[i] += deltas[i];
                result[i] = previousSpectrum[i] + deltas[i];
            }

            return result.ToList();
        }

        public static List<float> NanToZero(List<float> spectrum)
        {
            for (int i = 0; i < spectrum.Count; i++)
            {
                if (float.IsNaN(spectrum[i]))
                {
                    spectrum[i] = 0;
                }
            }

            return spectrum;
        }
    }
}
