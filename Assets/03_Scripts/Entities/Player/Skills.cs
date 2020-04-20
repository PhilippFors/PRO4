using UnityEngine;

[CreateAssetMenu(fileName = "New Skill", menuName = "Skills")]
public class Skills : ScriptableObject
{
    public string skillName;
    public float current = 0;
    public float max;
    public float timer;
    public bool isActive = false;
    public float activeValue = 0.3f;
    public float deactiveValue = 1f;
    public float decreaseMultValue;
    public float increaseMultValue;
    
}