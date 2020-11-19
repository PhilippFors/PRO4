using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
[Author("Philipp Forstner")]
public class AIUtilities : MonoBehaviour
{
    //Simple Timer that counts down until 0 from a given float value
    public class Timer
    {
        float currentTime;
        float waitTime;
        bool timerDone;

        public Timer()
        {

        }

        public Timer(float time)
        {
            waitTime = time;
            currentTime = 0;
        }


        public void StartTimer()
        {
            timerDone = false;

            //Starting the async function
            Timing();
        }

        //Async operation that uses the UniTask async library
        //Subtracts deltatime every frame/playerloop
        async UniTask Timing()
        {
            while (currentTime <= waitTime)
            {
                currentTime += Time.deltaTime;
                await UniTask.Yield();
                timerDone = true;
            }
            currentTime -= waitTime;
        }

        //Can be called to check if the timer has counted down to 0
        public bool TimerDone()
        {
            return timerDone;
        }

        public void setWaitTime(float time)
        {
            waitTime = time;
            if (currentTime >= waitTime)
                currentTime -= waitTime;
            else
                currentTime = 0;
        }
    }
}
