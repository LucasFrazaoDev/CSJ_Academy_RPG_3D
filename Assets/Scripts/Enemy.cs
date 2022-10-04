using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float totalHealth, currentHealth, attackDamage, movementSpeed;

    public void GetHit()
    {
        Debug.Log("Faleci");
        Destroy(gameObject);
    }
}
