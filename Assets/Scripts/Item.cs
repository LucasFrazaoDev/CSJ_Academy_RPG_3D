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

    [System.Serializable]
    public enum SlotsType
    {
        helmet,
        shield,
        armor
    }

    public SlotsType SlotType;

    public Player player;

    public void GetAction()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        switch (ItemType)
        {
            case Type.Potion:
                //Debug.Log("Health: " + value);
                player.IncreaseStats(value, 0f);
                break;

            case Type.Elixir:
                //Debug.Log("Elixir: " + value);
                player.IncreaseStats(0, value);
                break;

            case Type.Crystal:
                Debug.Log("Crystal: " + value);
                break;

            default:
                break;
        }
    }

    public void RemoveStats()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        switch (ItemType)
        {
            case Type.Potion:
                //Debug.Log("Health: " + value);
                player.DecreaseStats(value, 0f);
                break;

            case Type.Elixir:
                //Debug.Log("Elixir: " + value);
                player.DecreaseStats(0, value);
                break;

            case Type.Crystal:
                Debug.Log("Crystal: " + value);
                break;

            default:
                break;
        }
    }
}
