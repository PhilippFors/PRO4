using UnityEngine;

[Author(mainAuthor = "Philipp Forstner")]
[CreateAssetMenu(fileName = "Symbol", menuName = "Symbols/Symbol")]
public class Symbol : ScriptableObject
{
    public Material mat;
    public Texture tex => mat.GetTexture("_Texture");
    // public Texture symbolSprite;
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
