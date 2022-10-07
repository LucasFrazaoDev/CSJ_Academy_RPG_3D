using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public static GameObject itemBegginDragged; // Item selecionado
    public Item item; // Classe item com os atributos

    private Vector3 startPos;
    private Transform startParent;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Image>().sprite = item.icon;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        itemBegginDragged = gameObject;
        startPos = transform.position;
        startParent = transform.parent;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        itemBegginDragged = null;
        GetComponent<CanvasGroup>().blocksRaycasts = true;

        if (transform.parent == startParent)
        {
            transform.position = startPos;
        }
    }

    public void SetParent(Transform slotTransform, Slots slot)
    {
        if (item.SlotType.ToString() == slot.SlotType.ToString())
        {
            transform.SetParent(slotTransform);
            item.GetAction();
        }
        else if (slot.SlotType.ToString() == "inventory")
        {
            transform.SetParent(slotTransform);
            item.RemoveStats();
        }
    }
}
