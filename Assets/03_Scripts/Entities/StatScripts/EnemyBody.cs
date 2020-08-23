using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum EnemyType
{
    undefinded,
    Avik,
    Shentau
}
public class EnemyBody : MonoBehaviour, IStats, IMultipliers, IKnockback
{
    public List<GameStatistics> statList { get; set; }
    public List<Multiplier> multList { get; set; }
    public StatTemplate statTemplate;
    public StatTemplate multTemplate;
    public StateMachineController controller => GetComponent<StateMachineController>();
    public Rigidbody rb => GetComponent<Rigidbody>();
    public Symbol symbolInfo;
    // Material mat;
    [SerializeField] private GameObject parent;
    [SerializeField] private GameObject symbol;
    [SerializeField] public float currentHealth;
    private void Awake()
    {
        InitSymbol();
        InitStats();
        InitMultiplier();
    }


    #region Init
    public void InitStats()
    {
        statList = new List<GameStatistics>();
        foreach (FloatReference f in statTemplate.statList)
        {
            StatVariable s = (StatVariable)f.Variable;
            statList.Add(new GameStatistics(f.Value, s.statName));
        }

        currentHealth = GetStatValue(StatName.MaxHealth);
    }

    public void InitMultiplier()
    {
        multList = new List<Multiplier>();
        foreach (FloatReference f in multTemplate.statList)
        {
            MultVariable s = (MultVariable)f.Variable;
            multList.Add(new Multiplier(f.Value, s.multiplierName));
        }
    }

    void InitSymbol()
    {
        Material mat = new Material(symbolInfo.shader);
        mat.SetTexture("_Texture", symbolInfo.symbolSprite);
        mat.SetColor("_Color", Color.red);
        symbol.GetComponent<MeshRenderer>().material = mat;
    }

    #endregion

    #region health
    void CheckHealth()
    {
        if (currentHealth <= 0)
        {
            OnDeath();
        }
    }
    public void TakeDamage(float damage)
    {
        float calcDamage = damage * GetMultValue(MultiplierName.damage);
        calcDamage = damage * (damage / (damage + (GetStatValue(StatName.Defense) * GetMultValue(MultiplierName.defense))));
        // SetStatValue(StatName.MaxHealth, (GetStatValue(StatName.MaxHealth) - calcDamage));

        Debug.Log(gameObject.name + " just took " + calcDamage + " damage.");
        currentHealth -= calcDamage;
        CheckHealth();
        // damage = damage * damage / (damage + (enemy.GetStatValue(StatName.defense) * MultiplierManager.instance.GetEnemyMultValue(MultiplierName.defense)));
    }

    public void Heal(float healAmount)
    {

    }

    public void OnDeath()
    {
        EventSystem.instance.OnEnemyDeath(this);
        Destroy(parent);
        Destroy(symbol);
    }
    #endregion

    #region Multipliers 
    public void SetMultValue(MultiplierName name, float value)
    {
        multList.Find(x => x.GetName().Equals(name)).SetValue(GetMultValue(name) + value);
    }
    public float GetMultValue(MultiplierName name)
    {
        return multList.Find(x => x.GetName().Equals(name)).GetValue();
    }

    public void ResetMultipliers()
    {
        foreach (Multiplier mult in multList)
        {
            mult.ResetMultiplier();
        }
    }
    #endregion

    #region Stats
    public void SetStatValue(StatName stat, float value)
    {
        statList.Find(x => x.GetName().Equals(stat)).SetValue(value);

    }
    public float GetStatValue(StatName stat)
    {
        return statList.Find(x => x.GetName().Equals(stat)).GetValue();
    }

    public void ApplyKnockback(float force, int stunChance)
    {
        // float totalStun = stunChance - GetStatValue(StatName.stunResist);
        int rand = Random.Range(0, 101);

        if(rand < stunChance){
            controller.Stun();
        }
        
        Vector3 direction = Vector3.Normalize(transform.position - controller.settings.playerTarget.position);
        rb.AddForce(direction * force, ForceMode.Impulse);

    }
    #endregion
}
