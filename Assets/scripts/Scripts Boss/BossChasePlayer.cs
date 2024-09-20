using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;  // Necesario para usar NavMeshAgent

public class BossChasePlayer : MonoBehaviour
{
    private NavMeshAgent agent;       
    private Transform player;         
    public float chaseRange = 200f;    

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();  
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        // Solo persigue al jugador si el agente no está detenido
        if (agent != null && !agent.isStopped)
        {

            float distanceToPlayer = Vector3.Distance(player.position, transform.position);
            if (distanceToPlayer <= chaseRange)
            {
                agent.SetDestination(player.position);  
            }
            else
            {
                agent.SetDestination(transform.position); 
            }

        }
    }
}
