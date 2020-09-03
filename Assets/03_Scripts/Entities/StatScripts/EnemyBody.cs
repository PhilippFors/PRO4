using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBody : AStats, IHasHealth, IKnockback
{
    public StatTemplate statTemplate;
    public StateMachineController controller => GetComponent<StateMachineController>();
    public Rigidbody rb => GetComponent<Rigidbody>();
    public Symbol symbolInfo;
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
        symbol.GetComponent<MeshRenderer>().material = symbolInfo.mat;
    }

    #endregion

    #region health
    public void CheckHealth()
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

    public void ApplyKnockback(float force, int stunChance)
    {
        // float totalStun = stunChance - GetStatValue(StatName.stunResist);
        int rand = Random.Range(0, 101);
        float newstunchance = stunChance - GetStatValue(StatName.StunResist);
        if (newstunchance < 0)
            newstunchance = -1;

        if (rand < newstunchance)
            controller.Stun();

        Vector3 direction = Vector3.Normalize(transform.position - controller.settings.playerTarget.position);
        rb.AddForce(direction * force, ForceMode.Impulse);
    }

}
