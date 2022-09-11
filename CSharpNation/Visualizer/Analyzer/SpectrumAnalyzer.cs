using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Un4seen.Bass;
using Un4seen.BassWasapi;

namespace CSharpNation.Visualizer.Analyzer
{
    public class SpectrumAnalyzer
    {
        public float[] _fft;               //buffer for fft data
        private WASAPIPROC _process;        //callback function to obtain data
        public List<float> _spectrumdata;   //spectrum data buffer

        private int previousDevice = -1;

        public float multiplier = 1;
        public int _lines = 50;

        #region Constructor
        public SpectrumAnalyzer()
        {
            _fft = new float[8192];
            _process = new WASAPIPROC(Process);
            _spectrumdata = new List<float>();

            InitializeSpectrumData();

            Init();
            CaptureDevice(0);
        }
        #endregion

        #region Initialize
        private void InitializeSpectrumData()
        {
            for (int i = 0; i < _lines; i++)
            {
                _spectrumdata.Add(0);
            }
        }

        private void Init()
        {
            Bass.BASS_SetConfig(BASSConfig.BASS_CONFIG_UPDATETHREADS, false);
            if (!Bass.BASS_Init(0, 44100, BASSInit.BASS_DEVICE_DEFAULT, IntPtr.Zero))
            {
                throw new Exception("Init Error");
            }
        }
        #endregion

        #region Devices
        public List<string> GetDevices()
        {
            List<string> Devices = new List<string>();

            for (int i = 0; i < BassWasapi.BASS_WASAPI_GetDeviceCount(); i++)
            {
                BASS_WASAPI_DEVICEINFO device = BassWasapi.BASS_WASAPI_GetDeviceInfo(i);
                if (device.IsEnabled && device.IsLoopback)
                {
                    Devices.Add(string.Format("{0} - {1}", i, device.name));
                }
            }

            return Devices;
        }

        private void CaptureDevice(int deviceIndex)
        {
            //Console.WriteLine(GetDevices()[deviceIndex]);
            int device = Convert.ToInt32(GetDevices()[deviceIndex].Split(' ')[0]);

            if (!BassWasapi.BASS_WASAPI_Init(device, 0, 0, BASSWASAPIInit.BASS_WASAPI_BUFFER, 1f, 0.05f, _process, IntPtr.Zero))
            {
                throw new Exception(Bass.BASS_ErrorGetCode().ToString());
            }

            BassWasapi.BASS_WASAPI_Start();

            previousDevice = deviceIndex;
        }

        public void ResumeCapture()
        {
            BassWasapi.BASS_WASAPI_Start();
        }

        public void PauseCapture()
        {
            BassWasapi.BASS_WASAPI_Stop(true);
        }

        public void ChangeDevice(int deviceIndex)
        {
            if (deviceIndex != previousDevice && deviceIndex < GetDevices().Count && deviceIndex >= 0)
            {
                Free();

                Init();
                CaptureDevice(deviceIndex);
            }
        }
        #endregion

        #region Spectrum
        public List<float> GetSpectrum()
        {
            int ret = BassWasapi.BASS_WASAPI_GetData(_fft, (int)BASSData.BASS_DATA_FFT8192);
            if (ret < -1) { return _spectrumdata; }
            else
            {
                int x;
                float y;
                int b0 = 0;

                for (x = 0; x < _lines; x++)
                {
                    float peak = 0;
                    int b1 = (int)Math.Pow(2, x * 10.0 / (_lines - 1));
                    if (b1 > 1023) b1 = 1023;
                    if (b1 <= b0) b1 = b0 + 1;
                    for (; b0 < b1; b0++)
                    {
                        if (peak < _fft[1 + b0]) peak = _fft[1 + b0];
                    }

                    y = peak * multiplier;
                    if (y < 0) y = 0;
                    _spectrumdata[x] = y;
                }

                return new List<float>(_spectrumdata);
            }
        }
        #endregion

        #region Utils
        private int Process(IntPtr buffer, int length, IntPtr user)
        {
            return length;
        }

        //cleanup
        public void Free()
        {
            _ = BassWasapi.BASS_WASAPI_Free();
            _ = Bass.BASS_Free();
        }
        #endregion
    }
}
