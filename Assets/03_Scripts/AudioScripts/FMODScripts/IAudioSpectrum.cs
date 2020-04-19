using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAudioSpectrum
{



    //Frequenices
    float getFqBandBuffer8(int id);

    float getFqBand8(int id);

    float getAmplitudeBuffer();

    float getFqBandBuffer32(int id);

    float getFqBand32(int id);


    //BPM
    float getBPM();

    bool getBeatFull();

    bool getBeatD8();

}