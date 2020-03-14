using System;
using UnityEngine;
using System.Runtime.InteropServices;


[RequireComponent(typeof(FMODUnity.StudioEventEmitter))]
class FMODAudioPeer : MonoBehaviour
{


    FMODUnity.StudioEventEmitter emitter;
    FMOD.Studio.EventInstance musicInstance;
    FMOD.DSP fft;
    FMOD.ChannelGroup channelGroup;



    const int WindowSize = 512;



    //____
    //public float[] _samples = new float[3000];
    float[] _freqBand = new float[8];
    float[] _bandBuffer = new float[8];
    float[] _bufferDecrease = new float[8];

   public float _bufferDecreaseValue = 1;
   public float _bufferIncreaseValue = 1;


    float[] _freqBandHighest = new float[8];
    public static float[] _audioBand = new float[8];
    public static float[] _audioBandBuffer = new float[8];
    //_____



    void Start()
    {


        emitter = GetComponent<FMODUnity.StudioEventEmitter>();
        musicInstance = emitter.getEvent();

        //set up fft dsp
        FMODUnity.RuntimeManager.CoreSystem.createDSPByType(FMOD.DSP_TYPE.FFT, out fft);
        fft.setParameterInt((int)FMOD.DSP_FFT.WINDOWTYPE, (int)FMOD.DSP_FFT_WINDOW.BLACKMAN);
        fft.setParameterInt((int)FMOD.DSP_FFT.WINDOWSIZE, WindowSize * 2);

        //assign the dsp to a channel
       
        musicInstance.getChannelGroup(out channelGroup);


       
       
        //FMODUnity.RuntimeManager.CoreSystem.getMasterChannelGroup(out channelGroup);

        channelGroup.addDSP(FMOD.CHANNELCONTROL_DSP_INDEX.TAIL, fft);
       




        
    }



    void Update()
    {
        
        musicInstance.getChannelGroup(out channelGroup);
        channelGroup.isPlaying(out bool isPlaying);
        if (!isPlaying)
        {
            channelGroup.addDSP(FMOD.CHANNELCONTROL_DSP_INDEX.HEAD, fft);
        }


        IntPtr unmanagedData;
        uint length;
        fft.getParameterData((int)FMOD.DSP_FFT.SPECTRUMDATA, out unmanagedData, out length);
        FMOD.DSP_PARAMETER_FFT fftData = (FMOD.DSP_PARAMETER_FFT)Marshal.PtrToStructure(unmanagedData, typeof(FMOD.DSP_PARAMETER_FFT));
        var spectrum = fftData.spectrum;


        
        if (fftData.numchannels == 0){
            Debug.Log("keine FFT Channels vorhanden");
        }
        if (fftData.numchannels > 0)
        {

            
            // Debug.Log(fftData.spectrum[0].Length);

            //--- Frequency Bands
            //cant call the methdo because i cant convert the var to an actual array

            int count = 0;
            
            for (int i = 0; i < 8; i++)
            {


                float average = 0;
                int sampleCount = (int)Mathf.Pow(2, i) * 2;

                if (i == 7)
                {
                    sampleCount += 2;
                }
                for (int j = 0; j < sampleCount; j++)
                {
                    average += spectrum[0][count] * (count + 1);
                    count++;
                }

                //_____
            

                average /= count;
                _freqBand[i] = average * 10;
            }
            //-----------------------------------------------
            
            BandBuffer();
            CreateAudioBands();

           
           

        }

         
    }

    void MakeFrequencyBands()
    {
        /*
         * 
         * 22050 / 512 = 43 Hertz per Sample
         * 
         * 20   - 60
         * 60   - 250
         * 250  - 500 
         * 500  - 2000
         * 2000 - 4000
         * 4000 - 6000
         * 6000 - 20000
         * 
         * 
         * 0 - 2 = 86
         * 1 - 4 = 172  - 87 - 258
         * 2 - 8 = 344  - 259 - 602
         * 3 - 16 = 688 - 603 - 1290
         * 4 - 32 = 1376 - 1291 - 2666
         * 5 - 64 = 2752 - 2667 - 5418
         * 6 - 128 = 5504 - 5419 - 10922
         * 7 - 256 = 11008 - 10923 - 21930
         * 
         * 510
         * 
         * 
         */

        int count = 0;

        for (int i = 0; i < 8; i++)
        {
            float average = 0;
            int sampleCount = (int)Mathf.Pow(2, i) * 2;

            if (i == 7)
            {
                sampleCount += 2;
            }
            for (int j = 0; j < sampleCount; j++)
            {
                // average += _samples[count] * (count + 1);
                count++;
            }

            average /= count;

            _freqBand[i] = average * 10;
        }
    }



    void CreateAudioBands()
    {
        for (int i = 0; i < 8; i++)
        {
            if (_freqBand[i] > _freqBandHighest[i])
            {
                _freqBandHighest[i] = _freqBand[i];
            }
            _audioBand[i] = (_freqBand[i] / _freqBandHighest[i]);
            _audioBandBuffer[i] = (_bandBuffer[i] / _freqBandHighest[i]);
        }
    }

    void BandBuffer()
    {
        for (int g = 0; g < 8; ++g)
        {
            if (_freqBand[g] > _bandBuffer[g])
            {
                _bandBuffer[g] = _freqBand[g];
                _bufferDecrease[g] = 0.005f * _bufferDecreaseValue;
            }

            if (_freqBand[g] < _bandBuffer[g])
            {
                _bandBuffer[g] -= _bufferDecrease[g];
                _bufferDecrease[g] *= 2f * _bufferIncreaseValue;
            }
        }
    }
}
