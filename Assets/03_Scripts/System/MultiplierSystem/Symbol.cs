using UnityEngine;

[CreateAssetMenu(fileName = "Symbol", menuName = "Symbols/Symbol")]
public class Symbol : ScriptableObject
{
    public Shader shader;
    public Texture symbolSprite;
    public bool enhance;
    public MultiplierName main
    {
        get
        {
            return enhance ? buff : debuff;
        }
    }
    public MultiplierName buff;
    public MultiplierName debuff;
}
