using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ModifierManager : MonoBehaviour
{
    //Struct is more memory effecient...I think
    //Also no Monobehaviour needed.
    public struct Modifier
    {
        private readonly float RESET_VALUE;
        public string name;
        private float v;
        public Modifier(float value, float resetValue, string n)
        {
            v = value;
            RESET_VALUE = resetValue;
            name = n;
        }

        //Adding functionality to the individual Modifiers can make using it easier
        public void ResetModifier()
        {
            v = RESET_VALUE;
        }
        public float GetModValue()
        {
            return v;
        }

        public void SetModifer(float value)
        {
            v = value;
        }
    }

    private const float RESET_VALUE = 1.0f;
    private float someotherModifier = 1.0f;
    private float someMod = 1.0f;

    //Lists are dynamic in size. Array would technically work too.
    private List<Modifier> modList = new List<Modifier>();

    //Singleton setup
    private static ModifierManager _instance;
    public static ModifierManager instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("null");

            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }
    private void Start()
    {
        //Example for making List with mods. Would be ideal to instantiate in a seperate class and return the List to keep it clean.
        Modifier someMod = new Modifier(1f, 1f, "someMod");
        Modifier someOtherMod = new Modifier(1f, 1f, "someOtherMod");
        modList.Add(someMod);
        modList.Add(someOtherMod);

        //How to use "List.Find"
        Debug.Log(modList.Find(x => x.name.Equals("someMod")).GetModValue());

    }

    //Getter and Setter for every modifier if this is somehow a better idea. idk
    public void setSomeMod(float value, float Skilltime)
    {
        someMod = value;
        StartCoroutine(ResetTimer(Skilltime));
    }

    public float getSomeMod()
    {
        return someMod;
    }


    IEnumerator ResetTimer(float Skilltime)
    {
        yield return new WaitForSeconds(Skilltime);
        ResetMods();

    }

    void ResetMods()
    {
        //Either reset mods individually
        someMod = RESET_VALUE;

        //or iterate over every mod with foreach
        foreach (Modifier mod in modList)
        {
            mod.ResetModifier();
        }

    }
}
