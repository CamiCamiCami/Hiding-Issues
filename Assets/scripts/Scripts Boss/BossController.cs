using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossController : MonoBehaviour
{
    private Animator animator;
    private NavMeshAgent agent;

    public float walkSpeed = 3.5f;
    public float attackInterval = 10f;
    private float nextAttackTime = 0f;

    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.speed = walkSpeed;
    }

    void Update()
    {
        // Verifica si el jefe está en movimiento
        bool isMoving = agent.velocity.magnitude > 0;
        animator.SetBool("isWalking", isMoving);

        if (!isMoving && Time.time >= nextAttackTime)
        {
            StartCoroutine(PerformAreaAttack());
            nextAttackTime = Time.time + attackInterval; // Configura el tiempo para el próximo ataque
        }
    }

    private IEnumerator PerformAreaAttack()
    {
        
        animator.SetBool("isAttacking", true);

        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        animator.SetBool("isAttacking", false);
    }

}