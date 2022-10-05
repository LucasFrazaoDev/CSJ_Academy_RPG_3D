using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float totalHealth = 100f, currentHealth, attackDamage, movementSpeed;

    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();

        currentHealth = totalHealth;
    }

    public void GetHit(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            anim.SetInteger("Transition", 3);
            StartCoroutine(RecoveryFromhit());
        }
    }

    IEnumerator RecoveryFromhit()
    {
        yield return new WaitForSeconds(0.9f);
        anim.SetInteger("Transition", 0);
    }

    private void Die()
    {
        anim.SetInteger("Transition", 4);
        Destroy(gameObject, 2f);
    }
}
