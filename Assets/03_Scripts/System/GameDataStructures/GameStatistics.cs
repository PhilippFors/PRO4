public enum StatName
{
    undefined,
    MaxHealth,
    Speed,
    Range,
    TurnSpeed,
    Defense,
    AttackSpeed,
    AttackRate,
    BaseDmg
    
}
public class GameStatistics
{
    private float v;
    private StatName _name;

    public GameStatistics(float value, StatName name){
        v = value;
        _name = name;
    }

    public void SetValue(float value)
    {
        v = value;
    }

    public float GetValue()
    {
        return v;
    }

    public StatName GetName()
    {
        return _name;
    }
}

