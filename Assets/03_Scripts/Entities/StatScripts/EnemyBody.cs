using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBody : AStats, IHasHealth, IKnockback
{
    public StatTemplate statTemplate;
    public StateMachineController controller => GetComponent<StateMachineController>();
    public Rigidbody rb => GetComponent<Rigidbody>();

    public Coroutine stunCoroutine { get; set; }
    public float currentStun { get; set; }
    public float stunCooldown = 0.5f;
    public Symbol symbolInfo;
    public GameObject parent;
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
        // symbol.GetComponent<MeshRenderer>().material = symbolInfo.mat;
        MeshRenderer[] rend = symbol.GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer r in rend)
            r.material = symbolInfo.mat;
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
        MyEventSystem.instance.OnEnemyDeath(this);
        Destroy(parent);
        Destroy(symbol);
    }
    #endregion

    public void ApplyKnockback(float force)
    {
        Vector3 direction = (transform.position - controller.aiManager.playerTarget.position).normalized;
        rb.AddForce(direction * force, ForceMode.Impulse);
    }

    public void ApplyStun(float stun)
    {
        if (stunCoroutine != null)
            StopCoroutine(stunCoroutine);

        stunCoroutine = StartCoroutine(StunCooldown());
        currentStun += stun;

        if (currentStun > GetStatValue(StatName.StunResist))
            controller.Stun();
    }

    public IEnumerator StunCooldown()
    {
        yield return new WaitForSeconds(stunCooldown);
        while (currentStun >= 0)
            currentStun -= Time.deltaTime;
    }
}
