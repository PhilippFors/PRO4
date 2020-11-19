using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Author(mainAuthor = "Philipp Forstner")]
/// <summary>
/// Manages all of the numerical statistics for any Entity that inherits from it.
/// </summary>
public abstract class StatisticController : MonoBehaviour
{
    public List<GameStatistics> statList;
    public List<Multiplier> multList;
    public StatTemplate statTemplate;
    private void Awake()
    {
        InitStats();
    }
    virtual protected void InitStats()
    {
        multList = new List<Multiplier>();
        statList = new List<GameStatistics>();
        foreach (FloatReference f in statTemplate.statList)
        {
            StatVariable s = (StatVariable)f.Variable;
            statList.Add(new GameStatistics(f.Value, s.statName));
        }
    }

    /// <summary>
    /// Sets any available Statistic value to the value provided
    /// </summary>
    /// <param name="name">The Enum name for the Stat value</param>
    /// <param name="value">The value</param>
    virtual public void SetStatValue(StatName name, float value)
    {
        statList.Find(x => x.GetName().Equals(name)).SetValue(value);
    }

    /// <summary>
    /// Returns any available Statistic value as a float.
    /// If Statistic is not availaible, will return 1.
    /// </summary>
    /// <param name="stat">The Enum name for the Stat value</param>
    /// <returns></returns>
    virtual public float GetStatValue(StatName stat)
    {
        if (statList.Exists(x => x.GetName().Equals(stat)))
        {
            return statList.Find(x => x.GetName().Equals(stat)).GetValue();
        }
        else
        {
            return 1f;
        }
    }

    /// <summary>
    /// Adds a Multiplier that is used to buff or debuff existing Statistics.
    /// </summary>
    /// <param name="name">Enum for the Multiplier name</param>
    /// <param name="value">Can be any float value. Doesn't need to be higher than 1, can be negative.</param>
    /// <param name="time">The amount of time the multiplier should be active</param>
    virtual public void AddMultiplier(MultiplierName name, float value, float time)
    {
        multList.Add(new Multiplier(value, name));

        StartCoroutine(MultiplierTimer(time, multList.FindIndex(x => x.GetName().Equals(name))));
    }

    /// <summary>
    /// Finds all instances of the specified multiplier, adds them together and returns them as a float.
    /// Returns 1 if none are found.
    /// </summary>
    /// <param name="name">The Multiplier Enum name</param>
    /// <returns></returns>
    virtual public float GetMultValue(MultiplierName name)
    {
        float value = 1f;
        if (multList.Exists(x => x.GetName().Equals(name)))
        {
            List<Multiplier> list = multList.FindAll(x => x.GetName().Equals(name));
            for (int i = 0; i < list.Count; i++)
            {
                value += list[i].GetValue();
            }
            return value;
        }

        return value;
    }

    virtual public void ResetMultipliers()
    {
        multList.Clear();
    }

    /// <summary>
    /// Times the Multiplier and automatically removes it after the given time.
    /// </summary>
    /// <param name="time"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    virtual protected IEnumerator MultiplierTimer(float time, int id)
    {
        yield return new WaitForSeconds(time);
        multList.RemoveAt(id);
    }
}
