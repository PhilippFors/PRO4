using UnityEngine;
public enum WeaponNames
{
    Dagger,
    Hammer,
}

[CreateAssetMenu(fileName = "WeaponSettings", menuName = "WeaponSettings", order = 0)]
public class WeaponSettings : ScriptableObject
{
    public WeaponNames weaponName;
    public float bsdmg = 20.0f;
    public float knockbackForce = 3f;
    public int weaponID;

    [Range(0, 100)]
    public int stunChance = 50;
}
