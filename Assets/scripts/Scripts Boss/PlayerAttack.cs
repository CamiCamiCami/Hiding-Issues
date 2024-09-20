using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public int attackDamage = 10;    
    public float attackRange = 2f;   
    public LayerMask bossLayer;      

    void Update()
    {
        if (Input.GetMouseButtonDown(0))  
        {
            Attack();
        }
    }

    void Attack()
    {
        
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, attackRange, bossLayer))
        {
            BossHealth bossHealth = hit.collider.GetComponent<BossHealth>();
            if (bossHealth != null)
            {
                bossHealth.TakeDamage(attackDamage);  
                Debug.Log("Ataque realizado. Daño al jefe: " + attackDamage);
            }
        }
    }
}