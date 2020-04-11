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
        modList = StatInit.InitModifiers();
    }

    public float GetModValue(MultiplierName name)
    {
        if (modList.Exists(x => x.GetName().Equals(name)))
            return modList.Find(x => x.GetName().Equals(name)).GetModValue() == 0 ? 1f : modList.Find(x => x.GetName().Equals(name)).GetModValue();

        return 1;
    }

    public void SetModValue(MultiplierName name, float value, float Skilltime)
    {
        modList.Find(x => x.GetName().Equals(name)).SetMod(value);
        StartCoroutine(ResetTimer(Skilltime));
        //Start Musik Filters
    }

    IEnumerator ResetTimer(float Skilltime)
    {
        yield return new WaitForSeconds(Skilltime);
        ResetMods();
        //Reset Music
        yield return null;
    }

    void ResetMods()
    {
        //iterate over every mod with foreach
        foreach (Multiplier mod in modList)
        {
            mod.ResetMod();
        }

    }
}
