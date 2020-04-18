using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAudioSpectrum
{
    // Start is called before the first frame update


    float getFqBandBuffer8(int id);


    float getFqBand8(int id);


    float getAmplitudeBuffer();


    float getFqBandBuffer32(int id);


    float getFqBand32(int id);


}
