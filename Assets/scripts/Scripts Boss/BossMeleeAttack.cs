using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMeleeAttack : MonoBehaviour
{
    public int attackDamage = 10;         
    public float attackCooldown = 1f;     
    private float nextAttackTime = 0f;   

    void OnCollisionEnter(Collision collision)
    {
            Debug.Log("Colisión detectada con: " + collision.gameObject.name); 
        Debug.Log($"Etiqueta del objeto colisionado: {collision.gameObject.tag}"); 
        Debug.Log($"Tiempo actual: {Time.time}, Próximo ataque permitido a partir de: {nextAttackTime}");

        // Verifica la condición completa
        if (collision.gameObject.CompareTag("Player") && Time.time >= nextAttackTime)
        {
        PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            Debug.Log("Aplicando daño al jugador."); 
            playerHealth.TakeDamage(attackDamage);  
            nextAttackTime = Time.time + attackCooldown;  // Actualiza el tiempo del próximo ataque
            Debug.Log($"Próximo ataque posible en: {nextAttackTime} segundos.");
        }
        else
        {
            Debug.LogWarning("PlayerHealth no encontrado en el jugador."); 
        }
        }
         else
        {
        // Depuración
        if (!collision.gameObject.CompareTag("Player"))
        {
            Debug.LogWarning("La etiqueta del objeto colisionado no es 'Player'.");
        }
        if (Time.time < nextAttackTime)
        {
            Debug.LogWarning("El ataque aún está en cooldown. Esperando a que pase el tiempo de cooldown.");
        }
        }
        }
}
