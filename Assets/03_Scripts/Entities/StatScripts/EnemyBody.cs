using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum EnemyType
{
    undefinded,
    Avik,
    Shentau
}
public class EnemyBody : AStats, IHasHealth, IKnockback
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

        if (rand < stunChance)
        {
            controller.Stun();
        }

        Vector3 direction = Vector3.Normalize(transform.position - controller.settings.playerTarget.position);
        rb.AddForce(direction * force, ForceMode.Impulse);

    }

}
