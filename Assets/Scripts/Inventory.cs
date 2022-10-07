using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    public GameObject slotParent;
    public List<Slots> slots = new List<Slots>();

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        GetSlots();
    }

    public void GetSlots()
    {
        foreach (Slots s in slotParent.GetComponentsInChildren<Slots>())
        {
            slots.Add(s);
        }

        //CreateItem();
    }

    public void CreateItem(Item item)
    {
        foreach (Slots s in slots)
        {
            if (s.transform.childCount == 0)
            {
                GameObject currentItem = Instantiate(GameController.instance.itemPrefab, s.transform);
                currentItem.GetComponent<DragItem>().item = item;

                return;
            }
        }
    }
}
