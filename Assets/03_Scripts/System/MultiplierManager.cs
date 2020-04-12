using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MultiplierManager : MonoBehaviour
{
    //Lists are dynamic in size. Array would technically work too.
    private List<Multiplier> modList = new List<Multiplier>();
   
    //Singleton setup
    private static MultiplierManager _instance;
    public static MultiplierManager instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("null");

            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        modList = StatInit.InitMultipliers();
    }

    public float GetMultiplierValue(MultiplierName name)
    {
        if (modList.Exists(x => x.GetName().Equals(name)))
            return modList.Find(x => x.GetName().Equals(name)).GetValue() == 0 ? 1f : modList.Find(x => x.GetName().Equals(name)).GetValue();

        return 1;
    }

    public void SetMultiplierValue(MultiplierName name, float value, float Skilltime)
    {
        modList.Find(x => x.GetName().Equals(name)).SetValue(value);
        StartCoroutine(ResetTimer(Skilltime));
        //Start Musik Filters
    }

    IEnumerator ResetTimer(float Skilltime)
    {
        yield return new WaitForSeconds(Skilltime);
        ResetMultiplier();
        //Reset Music
        yield return null;
    }

    void ResetMultiplier()
    {
        //iterate over every mod with foreach
        foreach (Multiplier mult in modList)
        {
            mult.ResetMod();
        }
    }
}
