using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public float rotation;
    public float gravity;
    public float colliderRadius;

    private List<Transform> enemiesList = new List<Transform>();
    private bool isReady;
    private float RotSpeed = 100f;
    private Vector3 moveDirection;
    private CharacterController controller;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        GetMouseInput();
    }

    void Move()
    {
        if (controller.isGrounded)
        {
            if (Input.GetKey(KeyCode.W))
            {
                if (!anim.GetBool("isAttacking"))
                {
                    anim.SetBool("isWalking", true);
                    anim.SetInteger("Transition", 1);

                    moveDirection = Vector3.forward * speed;
                    moveDirection = transform.TransformDirection(moveDirection);
                }
                else
                {
                    anim.SetBool("isWalking", false);

                    moveDirection = Vector3.zero;
                    StartCoroutine(Attack(1));
                }
            }

            if (Input.GetKeyUp(KeyCode.W))
            {
                anim.SetBool("isWalking", false);
                anim.SetInteger("Transition", 0);

                moveDirection = Vector3.zero;
            }
        }

        rotation += Input.GetAxis("Horizontal") * RotSpeed * Time.deltaTime;
        transform.eulerAngles = new Vector3(0, rotation, 0);

        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }

    private void GetMouseInput()
    {
        if (controller.isGrounded)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (anim.GetBool("isWalking"))
                {
                    anim.SetBool("isWalking", false);
                    anim.SetInteger("Transition", 0);
                }

                if (!anim.GetBool("isWalking"))
                {
                    // Executar ataque
                    StartCoroutine(Attack(0));
                }
            }
        }
    }

    IEnumerator Attack(int transitionValue)
    {
        if (!isReady)
        {
            isReady = true;
            anim.SetBool("isAttacking", true);
            anim.SetInteger("Transition", 2);

            yield return new WaitForSeconds(1.4f);

            GetEnemiesRange();

            foreach (Transform enemies in enemiesList)
            {
                // Ação de dano no inimigo
                Enemy enemy = enemies.GetComponent<Enemy>();

                if (enemy != null)
                {
                    enemy.GetHit();
                }
            }

            anim.SetInteger("Transition", transitionValue);
            anim.SetBool("isAttacking", false);
            isReady = false;
        }
    }

    private void GetEnemiesRange()
    {
        enemiesList.Clear();
        foreach (Collider collider in Physics.OverlapSphere((transform.position + transform.forward * colliderRadius), colliderRadius))
        {
            if (collider.gameObject.CompareTag("Enemy"))
            {
                enemiesList.Add(collider.transform);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + transform.forward, colliderRadius);
    }
}
