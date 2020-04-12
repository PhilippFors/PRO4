using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MultiplierManager : MonoBehaviour
{
    //Lists are dynamic in size. Array would technically work too.
    private List<Multiplier> enemyMultList = new List<Multiplier>();
    private List<Multiplier> playerMultList = new List<Multiplier>();
   
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
        enemyMultList = StatInit.InitEnemyMultipliers();
        playerMultList = StatInit.InitPlayerMultipliers();
    }

    public float GetEnemyMultValue(MultiplierName name)
    {
        if (enemyMultList.Exists(x => x.GetName().Equals(name)))
            return enemyMultList.Find(x => x.GetName().Equals(name)).GetValue() == 0 ? 1f : enemyMultList.Find(x => x.GetName().Equals(name)).GetValue();

        return 1;
    }
    public float GetPlayerMultValue(MultiplierName name)
    {
        if (playerMultList.Exists(x => x.GetName().Equals(name)))
            return playerMultList.Find(x => x.GetName().Equals(name)).GetValue() == 0 ? 1f : enemyMultList.Find(x => x.GetName().Equals(name)).GetValue();

        return 1;
    }

    public void SetEnemyMultValue(MultiplierName name, float value, float Skilltime)
    {
        enemyMultList.Find(x => x.GetName().Equals(name)).SetValue(value);
        StartCoroutine(ResetTimer(Skilltime));
        //Start Musik Filters
    }

    public void SetPlayerMultValue(MultiplierName name, float value)
    {
        playerMultList.Find(x => x.GetName().Equals(name)).SetValue(value);
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
        foreach (Multiplier mult in enemyMultList)
        {
            mult.ResetMod();
        }
    }
}
