using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public float colliderRadius;
    public bool isOpened;

    public List<Item> items = new List<Item>();

    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        GetPlayer();
    }

    private void GetPlayer()
    {
        if (!isOpened)
        {
            foreach (Collider collider in Physics.OverlapSphere((transform.position + transform.forward * colliderRadius), colliderRadius))
            {
                if (collider.gameObject.CompareTag("Player"))
                {
                    if (Input.GetMouseButtonDown(0))
                        OpenChest();
                }
            }
        }
    }

    private void OpenChest()
    {
        foreach (Item i in items)
        {
            i.GetAction();
        }
        anim.SetTrigger("Open");
        isOpened = true;
    }
}
