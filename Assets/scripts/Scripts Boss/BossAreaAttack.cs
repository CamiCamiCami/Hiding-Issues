using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossAreaAttack : MonoBehaviour
{
    public float attackRadius = 5f;        
    public int attackDamage = 20;          
    public float attackInterval = 10f;     
    public float attackDuration = 3f;      
    private float nextAreaAttackTime = 0f; 
    private Transform player;              
    private PlayerHealth playerHealth;     
    private NavMeshAgent agent;            
    public ParticleSystem shockParticles;  
    public CameraShake cameraShake;        
    public float shakeDuration = 0.5f;     
    public float shakeMagnitude = 0.2f;    
    private Animator animator;             

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerHealth = player.GetComponent<PlayerHealth>();
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        
        if (shockParticles != null)
        {
            shockParticles.Stop();  
            shockParticles.transform.SetParent(transform);
        }
    }

    void Update()
    {
        // Verifica si es tiempo de realizar el ataque en área
        if (Time.time >= nextAreaAttackTime)
        {
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("AttackArea"))
            {
                StartCoroutine(PerformAreaAttack());
            }
            nextAreaAttackTime = Time.time + attackInterval; // Configura el tiempo para el próximo ataque
        }
    }

    private IEnumerator PerformAreaAttack()
    {
        
        if (agent != null)
        {
            agent.isStopped = true;
        }

        
        animator.SetBool("isAttacking", true);

        
        yield return new WaitForSeconds(1f);

        
        if (shockParticles != null)
        {
            shockParticles.Play();
        }

        if (cameraShake != null)
        {
            StartCoroutine(cameraShake.Shake(shakeDuration, shakeMagnitude));
        }

        
        if (Vector3.Distance(transform.position, player.position) <= attackRadius)
        {
            playerHealth.TakeDamage(attackDamage);
            Debug.Log("Ataque en área realizado. Daño aplicado al jugador: " + attackDamage);
        }

        
        yield return new WaitForSeconds(2f);

        
        if (shockParticles != null)
        {
            shockParticles.Stop();
        }

        
        animator.SetBool("isAttacking", false);

        
        if (agent != null)
        {
            agent.isStopped = false;
            agent.SetDestination(player.position); 
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}