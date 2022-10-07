using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    public GameObject inventoryButton;
    public GameObject itemPrefab;

    private void Awake()
    {
        instance = this;
    }

    public void ActiveGameObject(GameObject go)
    {
        go.SetActive(true);
        inventoryButton.SetActive(false);
    }

    public void DisableGameObject(GameObject go)
    {
        go.SetActive(false);
        inventoryButton.SetActive(true);
    }
}
