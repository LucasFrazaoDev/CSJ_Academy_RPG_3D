using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public Sprite icon;
    public string Name;
    public float value;

    [System.Serializable]
    public enum Type
    {
        Potion,
        Elixir,
        Crystal
    }

    public Type ItemType;

    public void GetAction()
    {
        switch (ItemType)
        {
            case Type.Potion:
                Debug.Log("Health" + value);
                break;
            case Type.Elixir:
                Debug.Log("Elixir" + value);
                break;
            case Type.Crystal:
                Debug.Log("Crystal" + value);
                break;
            default:
                break;
        }
    }
}
