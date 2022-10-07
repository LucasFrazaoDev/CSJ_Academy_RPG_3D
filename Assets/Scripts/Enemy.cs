using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float totalHealth = 100f, currentHealth, attackDamage, movementSpeed;
    public float colliderRadius;
    public Image healthBar;

    [Header("Path")]
    public List<Transform> pathPoints = new List<Transform>();
    public int currentPathIndex = 0;


    // NavMesh settings
    public float lookRadius = 10f;
    public Transform target;
    private NavMeshAgent agent;

    private bool isReady;
    private bool playerIsAlive;
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        currentHealth = totalHealth;
        playerIsAlive = true;
    }

    void MoveToNextPoint()
    {
        if (pathPoints.Count > 0)
        {
            float distance = Vector3.Distance(pathPoints[currentPathIndex].position, transform.position);
            agent.destination = pathPoints[currentPathIndex].position;

            if (distance <= 4f)
            {
                //currentPathIndex++;
                currentPathIndex = Random.Range(0, pathPoints.Count);
                currentPathIndex %= pathPoints.Count;
            }

            anim.SetInteger("Transition", 2);
            anim.SetBool("SkeletonWalking", true);
        }
    }

    private void Update()
    {
        if (currentHealth > 0)
        {
            float distance = Vector3.Distance(target.position, transform.position);

            if (distance <= lookRadius)
            {
                agent.isStopped = false;
                if (!anim.GetBool("SkeletonAttacking"))
                {
                    agent.SetDestination(target.position);
                    anim.SetInteger("Transition", 2);
                    anim.SetBool("SkeletonWalking", true);
                }

                if (distance <= agent.stoppingDistance)
                {
                    // attack execution
                    StartCoroutine("Attack");
                    LookTarget();
                }

                if (distance >= agent.stoppingDistance)
                {
                    anim.SetBool("SkeletonAttacking", false);
                }
            }
            else
            {
                anim.SetInteger("Transition", 0);
                anim.SetBool("SkeletonWalking", false);
                anim.SetBool("SkeletonAttacking", false);
                //agent.isStopped = true;
                MoveToNextPoint();
            }
        }
    }

    IEnumerator Attack()
    {
        if (!isReady && playerIsAlive && !anim.GetBool("isHitting"))
        {
            isReady = true;
            anim.SetBool("SkeletonAttacking", true);
            anim.SetBool("SkeletonWalking", false);
            anim.SetInteger("Transition", 1);

            yield return new WaitForSeconds(1f);

            GetEnemy();
            yield return new WaitForSeconds(1.7f);
            isReady = false;
        }
        if(!playerIsAlive)
        {
            anim.SetInteger("Transition", 0);
            anim.SetBool("SkeletonWalking", false);
            anim.SetBool("SkeletonAttacking", false);
            agent.isStopped = true;
        }
    }

    private void GetEnemy()
    {
        foreach (Collider collider in Physics.OverlapSphere((transform.position + transform.forward * colliderRadius), colliderRadius))
        {
            if (collider.gameObject.CompareTag("Player"))
            {
                // detecta o player
                collider.gameObject.GetComponent<Player>().GetHit(25);
                playerIsAlive = collider.gameObject.GetComponent<Player>().isAlive;
            }
        }
    }

    private void LookTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

    public void GetHit(float damage)
    {
        currentHealth -= damage;

        healthBar.fillAmount = currentHealth / totalHealth;

        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            StopCoroutine("Attack");
            anim.SetInteger("Transition", 3);
            anim.SetBool("isHitting", true);
            StartCoroutine(RecoveryFromhit());
        }
    }

    IEnumerator RecoveryFromhit()
    {
        yield return new WaitForSeconds(0.9f);
        anim.SetInteger("Transition", 0);
        anim.SetBool("isHitting", false);
        isReady = false;
    }

    private void Die()
    {
        anim.SetInteger("Transition", 4);
        Destroy(gameObject, 2f);
    }
}
