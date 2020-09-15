using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Runtime.InteropServices;


[RequireComponent(typeof(FMODUnity.StudioEventEmitter))]
class FMODAudioPeer : MonoBehaviour, IAudioSpectrum
{

    public static FMODAudioPeer _instance;
    public float _smoothBuffer = 0.005f;

    FMODUnity.StudioEventEmitter emitter;
    FMOD.Studio.EventInstance musicInstance;



    public event System.Action myAction;


    
    //---SPECTRUM ANALYZER---
    FMOD.DSP fft;
    FMOD.ChannelGroup channelGroup;

    //Werden benutzt um zu überprüfen ob Audio schon vorhanden ist
    bool isPlaying = false;
    bool ready = false;

    //Lenght of Sample Array
    const int WindowSize = 512;

    //Creating Analyzer for Environment/Game

    //variables for the 8band audio spectrum
    private float[] _freqBand8 = new float[8];
    private float[] _bandBuffer8 = new float[8];
    private float[] _bufferDecrease = new float[8];
    public float[] _freqBandHighest8 = new float[8];

    //variables for the 32band audio spectrum
    //this is used for the UI Spectrum
    private float[] _freqBand32 = new float[32];
    private float[] _bandBuffer32 = new float[32];
    private float[] _bufferDecrease32 = new float[32];
    private float[] _freqBandHighest32 = new float[32];



    [HideInInspector]
    public static float[] _audioBand8, _audioBandBuffer8;

    [HideInInspector]
    public static float[] _audioBand32, _audioBandBuffer32;

    [HideInInspector]
    public static float _amplitude, _amplitudeBuffer;
    private float _amplitudeHighest;
    public float _audioProfile;


    public enum _channel { Stereo, Left, Right };
    public _channel channel = new _channel();

    //GETTER

    //returned das fqBand mit Bufferfunkiont -> smoothed die werte
    //die id gibt dabei an welches Band man will
    // 0 - 7, je höher die Nummer desto höhere Frequenzen beherbergt das Band
    //will man denn Bass so nimmt man die 0, die Höhen bei 6 - 7
    public float getFqBandBuffer8(int id)
    {
        return _audioBandBuffer8[id];
    }

    public float getFqBand8(int id)
    {
        return _audioBand8[id];
    }

    public float getAmplitudeBuffer()
    {
        return _amplitudeBuffer;
    }

    public float getFqBandBuffer32(int id)
    {
        return _audioBandBuffer32[id];
    }

    public float getFqBand32(int id)
    {
        return _audioBand32[id];
    }



    public FMOD.Studio.EventInstance getMusicInstance()
    {
        return musicInstance;
    }

    void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        _audioBand8 = new float[8];
        _audioBandBuffer8 = new float[8];


        _audioBand32 = new float[32];
        _audioBandBuffer32 = new float[32];


        //AudioProfile(8, _audioProfile);
        setCustomAudioProfile();

        //fetch the musicEvent from the EventEmitter
        emitter = GetComponent<FMODUnity.StudioEventEmitter>();
        musicInstance = emitter.getEvent();

        //set up fft dsp
        FMODUnity.RuntimeManager.CoreSystem.createDSPByType(FMOD.DSP_TYPE.FFT, out fft);
        fft.setParameterInt((int)FMOD.DSP_FFT.WINDOWTYPE, (int)FMOD.DSP_FFT_WINDOW.BLACKMAN);
        fft.setParameterInt((int)FMOD.DSP_FFT.WINDOWSIZE, WindowSize * 2);

        //assign the dsp to a channel
        musicInstance.getChannelGroup(out channelGroup);
        

    }

    void Update()
    {
        //BPM Calculation Methods
      


        if (!ready)
        {
            musicInstance.getChannelGroup(out channelGroup);
            channelGroup.isPlaying(out isPlaying);
        }


        if (isPlaying && !ready)
        {
            channelGroup.addDSP(FMOD.CHANNELCONTROL_DSP_INDEX.HEAD, fft);
            ready = true;
        }

        if (ready)
        {
         
            //Creating the spectrum with 512 samples
            IntPtr unmanagedData;
            uint length;
            fft.getParameterData((int)FMOD.DSP_FFT.SPECTRUMDATA, out unmanagedData, out length);
            FMOD.DSP_PARAMETER_FFT fftData = (FMOD.DSP_PARAMETER_FFT)Marshal.PtrToStructure(unmanagedData, typeof(FMOD.DSP_PARAMETER_FFT));
            var spectrum = fftData.spectrum;


            //Checking if the channels already loaded
            if (fftData.numchannels == 0)
            {
                Debug.Log("keine FFT Channels vorhanden");
            }
            //Checking if the channels already loaded
            if (fftData.numchannels > 0)
            {



                //--- Frequency Bands 8
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
                        if (channel == _channel.Stereo)
                        {
                            average += (spectrum[0][count] + spectrum[1][count]) * (count + 1);
                        }
                        if (channel == _channel.Left)
                        {
                            average += (spectrum[0][count]) * (count + 1);
                        }
                        if (channel == _channel.Right)
                        {
                            average += (spectrum[1][count]) * (count + 1);
                        }
                        count++;
                    }
                    average /= count;
                    _freqBand8[i] = average * 10;
                }




                /*
                Aufteilung der Samples
                0-7 	8	= 	4 sample = 32
                8-15 	8	=	8 sample = 64
                16-20 	5	= 16 sample = 	80
                21-25 	5	= 24 sample = 	120
                26-31 	6	= 36 sample = 	216
                */

                //--- Frequency Bands 32
                //cant call the methdo because i cant convert the var to an actual array
                count = 0;
                int sampleCount32 = 1;
                int power = 0;

                for (int i = 0; i < 32; i++)
                {
                    float average = 0;

                    if (i == 8 || i == 16 || i == 21 || i == 26)
                    {
                        power++;
                        sampleCount32 = (int)Mathf.Pow(2, power);
                        if (power == 3)
                        {
                            sampleCount32 -= 6;
                        }
                    }
                    for (int j = 0; j < sampleCount32; j++)
                    {
                        if (channel == _channel.Stereo)
                        {
                            average += (spectrum[0][count] + spectrum[1][count]) * (count + 1);

                        }
                        if (channel == _channel.Left)
                        {
                            average += (spectrum[0][count]) * (count + 1);
                        }
                        if (channel == _channel.Right)
                        {
                            average += (spectrum[1][count]) * (count + 1);
                        }
                        count++;
                    }
                    average /= count;
                    _freqBand32[i] = average * 40;
                }

                BandBuffer8();
                BandBuffer32();
                CreateAudioBands8();
                CreateAudioBands32();
                GetAmplitude(8);

            }

        }

    }

  

    

    //normalisiert die 8 Frequenzbänder
    //damit erhalten wir Werte zwischen 0 und 1 für jedes Frequenzband
    void CreateAudioBands8()
    {
        for (int i = 0; i < 8; i++)
        {
            /*
            if (_freqBand8[i] > _freqBandHighest8[i])
            {
                _freqBandHighest8[i] = _freqBand8[i];
            }
            */
            
            _audioBand8[i] = (_freqBand8[i] / _freqBandHighest8[i]);
            _audioBandBuffer8[i] = (_bandBuffer8[i] / _freqBandHighest8[i]);
        }
    }

    //normalisiert die 32 Frequenzbänder
    //damit erhalten wir Werte zwischen 0 und 1 für jedes Frequenzband
    void CreateAudioBands32()
    {
        for (int i = 0; i < 32; i++)
        {
            
            if (_freqBand32[i] > _freqBandHighest32[i])
            {
                _freqBandHighest32[i] = _freqBand32[i];
            }
            
            _audioBand32[i] = (_freqBand32[i] / _freqBandHighest32[i]);
            _audioBandBuffer32[i] = (_bandBuffer32[i] / _freqBandHighest32[i]);

        }
    }

    //smootht die Werte damit diese nicht so zappeln
    void BandBuffer8()
    {
        for (int g = 0; g < 8; ++g)
        {
            if (_freqBand8[g] > _bandBuffer8[g])
            {
                _bandBuffer8[g] = _freqBand8[g];
                _bufferDecrease[g] = _smoothBuffer;
            }

            if (_freqBand8[g] < _bandBuffer8[g])
            {
                _bufferDecrease[g] = (_bandBuffer8[g] - _freqBand8[g]) / 8;
                _bandBuffer8[g] -= _bufferDecrease[g];
            }
        }
    }

    //smootht die Werte damit diese nicht so zappeln
    void BandBuffer32()
    {
        for (int g = 0; g < 32; ++g)
        {
            if (_freqBand32[g] > _bandBuffer32[g])
            {
                _bandBuffer32[g] = _freqBand32[g];
                _bufferDecrease32[g] = _smoothBuffer;
            }

            if (_freqBand32[g] < _bandBuffer32[g])
            {
                _bufferDecrease32[g] = (_bandBuffer32[g] - _freqBand32[g]) / 64;
                _bandBuffer32[g] -= _bufferDecrease32[g];
            }
        }
    }

    //damit bekommen wir die Lautstärke
    void GetAmplitude(int bandAmount)
    {
        float _currentAmplitude = 0;
        float _CurrentAmplitudeBuffer = 0;
        for (int i = 0; i < bandAmount; i++)
        {
            _currentAmplitude += _audioBand8[i];
            _CurrentAmplitudeBuffer += _audioBandBuffer8[i];
        }
        if (_currentAmplitude > _amplitudeHighest)
        {
            _amplitudeHighest = _currentAmplitude;
        }
        _amplitude = _currentAmplitude / _amplitudeHighest;
        _amplitudeBuffer = _CurrentAmplitudeBuffer / _amplitudeHighest;
    }

    //kann für jeden Track angepasst werden
    //damit die Werte auch gleich am Anfang smooth sind, ohne dem AudioProfile brauchen diese ein bisschen bis sie sich einpendeln
    void AudioProfile(int bandAmount, float audioProfile)
    {
        for (int i = 0; i < bandAmount; i++)
        {
            _freqBandHighest8[i] = audioProfile;
        }
    }

    void setCustomAudioProfile()
    {
        float modifier = 0.05f;
        _freqBandHighest8[0] = 7.087749f;
        _freqBandHighest8[1] = 14.599951f;
        _freqBandHighest8[2] = 8.991749f;
        _freqBandHighest8[3] = 8.143146f;
        _freqBandHighest8[4] = 9.672077f;
        _freqBandHighest8[5] = 10.95406f;
        _freqBandHighest8[6] = 25.38993f;
        _freqBandHighest8[7] = 9.435572f;

        for (int i = 0; i < 8; i++)
        {
            _freqBandHighest8[i] *= modifier;
        }

       // _freqBandHighest8[0] = 0.5f;
        //_freqBandHighest8[1] = 0.5f;
    }



}


/*
    //--- Frequency Bands 64
    //cant call the methdo because i cant convert the var to an actual array

count = 0;
int sampleCount64 = 1;
int power = 0;

for (int i = 0; i < 64; i++)
{
   float average = 0;

   if (i == 16 || i == 32 || i == 40 || i == 48 || i == 56)
   {
       power++;
       sampleCount64 = (int)Mathf.Pow(2, power);
       if (power == 3)
       {
           sampleCount64 -= 2;
       }
   }
   for (int j = 0; j < sampleCount64; j++)
   {
       if (channel == _channel.Stereo)
       {
           average += (spectrum[0][count] + spectrum[1][count]) * (count + 1);

       }
       if (channel == _channel.Left)
       {
           average += (spectrum[0][count]) * (count + 1);
       }
       if (channel == _channel.Right)
       {
           average += (spectrum[1][count]) * (count + 1);
       }
       count++;
   }
   average /= count;
   _freqBand64[i] = average * 80;

*/





