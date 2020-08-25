using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum EnemyType
{
    undefinded,
    Avik,
    Shentau
}
public class EnemyBody : AStats
{
    // public List<GameStatistics> statList { get; set; }
    // public List<Multiplier> multList { get; set; }
    public StatTemplate statTemplate;
    public StateMachineController controller => GetComponent<StateMachineController>();
    public Rigidbody rb => GetComponent<Rigidbody>();
    public Symbol symbolInfo;
    // Material mat;
    [SerializeField] private GameObject parent;
    [SerializeField] private GameObject symbol;
    public float currentHealth;
    private void Awake()
    {
        InitSymbol();
        this.InitStats(statTemplate);
    }

    #region Init
    public override void InitStats(StatTemplate template)
    {
        multList = new List<Multiplier>();
        statList = new List<GameStatistics>();
        foreach (FloatReference f in statTemplate.statList)
        {
            StatVariable s = (StatVariable)f.Variable;
            statList.Add(new GameStatistics(f.Value, s.statName));
        }

        currentHealth = GetStatValue(StatName.MaxHealth);
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
    public override void CheckHealth()
    {
        if (currentHealth <= 0)
        {
            OnDeath();
        }
    }
    public override void TakeDamage(float damage)
    {
        float calcDamage = damage * GetMultValue(MultiplierName.damage);
        calcDamage = damage * (damage / (damage + (GetStatValue(StatName.Defense) * GetMultValue(MultiplierName.defense))));
        // SetStatValue(StatName.MaxHealth, (GetStatValue(StatName.MaxHealth) - calcDamage));

        Debug.Log(gameObject.name + " just took " + calcDamage + " damage.");
        currentHealth -= calcDamage;
        CheckHealth();
        // damage = damage * damage / (damage + (enemy.GetStatValue(StatName.defense) * MultiplierManager.instance.GetEnemyMultValue(MultiplierName.defense)));
    }

    public override void Heal(float healAmount)
    {

    }

    public override void OnDeath()
    {
        EventSystem.instance.OnEnemyDeath(this);
        Destroy(parent);
        Destroy(symbol);
    }
    #endregion

    #region Multipliers 
    // public void AddMultiplier(MultiplierName name, float value, float time)
    // {
    //     multList.Add(new Multiplier(value, name));
    //     StartCoroutine(MultiplierTimer(time, multList.FindIndex(x => x.GetName().Equals(name))));
    //     // multList.Find(x => x.GetName().Equals(name)).SetValue(GetMultValue(name) + value);
    // }
    // public float GetMultValue(MultiplierName name)
    // {
    //     float value = 0;
    //     if (multList.Exists(x => x.GetName().Equals(name)))
    //     {
    //         List<Multiplier> list = multList.FindAll(x => x.GetName().Equals(name));
    //         foreach (Multiplier mult in list)
    //         {
    //             value += mult.GetValue();
    //         }
    //         return value;
    //     }
    //     else
    //     {
    //         return 1f;
    //     }
    // }

    // public void ResetMultipliers()
    // {
    //     multList.Clear();
    // }
    // #endregion

    // #region Stats
    // public void SetStatValue(StatName stat, float value)
    // {
    //     statList.Find(x => x.GetName().Equals(stat)).SetValue(value);

    // }
    // public float GetStatValue(StatName stat)
    // {
    //     return statList.Find(x => x.GetName().Equals(stat)).GetValue();
    // }
    #endregion
    public void ApplyKnockback(float force, int stunChance)
    {
        // float totalStun = stunChance - GetStatValue(StatName.stunResist);
        int rand = Random.Range(0, 101);

        if (rand < stunChance)
        {
            controller.Stun();
        }

        Vector3 direction = Vector3.Normalize(transform.position - controller.settings.playerTarget.position);
        rb.AddForce(direction * force, ForceMode.Impulse);

    }
    // public IEnumerator MultiplierTimer(float time, int id)
    // {
    //     yield return new WaitForSeconds(time);
    //     multList.RemoveAt(id);
    // }

}
