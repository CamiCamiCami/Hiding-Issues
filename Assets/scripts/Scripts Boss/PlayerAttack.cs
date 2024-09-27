
/*
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

*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public int attackDamage = 10;           // Daño que inflige el ataque
    public float attackRange = 6f;
    public GameObject swordPrefab;          // Prefab de la espada
    public Transform swordHolder;           // Punto donde se sostiene la espada (en la mano)
    private GameObject equippedSword;       // La espada equipada
    private bool isAttacking = false;       // Estado de ataque

    void Start()
    {
        EquipSword();  // Equipar la espada al inicio del juego
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))  // Cuando el jugador presiona el botón izquierdo del ratón
        {
            StartCoroutine(Attack());  // Inicia el ataque
        }
    }

    // Método para equipar la espada desde un prefab
    void EquipSword()
    {
        if (swordPrefab != null && swordHolder != null)
        {
            // Instanciar la espada y hacer que sea hija del punto de la mano
            equippedSword = Instantiate(swordPrefab, swordHolder.position, swordHolder.rotation, swordHolder);

            // Añadir un Collider a la espada si no tiene uno
            Collider swordCollider = equippedSword.GetComponent<Collider>();
            if (swordCollider == null)
            {
                swordCollider = equippedSword.AddComponent<BoxCollider>();
                swordCollider.isTrigger = true;  // Hacer el collider un trigger para detectar colisiones
            }

            // Añadir el script para que la espada pueda dañar al jefe
            equippedSword.AddComponent<SwordAttack>().attackDamage = attackDamage;

            Debug.Log("Espada equipada.");
        }
        else
        {
            Debug.LogWarning("No se ha asignado el prefab de la espada o el punto de equipamiento.");
        }
    }

    // Coroutine para manejar la duración del ataque
    IEnumerator Attack()
    {
        isAttacking = true;

        // Activar el collider de la espada temporalmente para detectar colisiones
        Collider swordCollider = equippedSword.GetComponent<Collider>();
        swordCollider.enabled = true;

        Debug.Log("Ataque iniciado.");

        // Espera un pequeño tiempo (simulando el tiempo de un ataque)
        yield return new WaitForSeconds(0.5f);

        // Desactivar el collider de la espada después del ataque
        swordCollider.enabled = false;

        isAttacking = false;

        Debug.Log("Ataque terminado.");
    }
}

public class SwordAttack : MonoBehaviour
{
    public int attackDamage = 10;  // Daño que inflige la espada
    public float attackRange = 6f;

    // Detectar cuando la espada golpea al jefe
    private void OnTriggerEnter(Collider other)
    {
        // Verificar si el objeto golpeado tiene el componente "BossHealth"
        if (other.gameObject.CompareTag("Boss"))
        {
            BossHealth bossHealth = other.GetComponent<BossHealth>();
            if (bossHealth != null)
            {
                bossHealth.TakeDamage(attackDamage);  // Aplicar daño al jefe
                Debug.Log("Golpeado al jefe con la espada. Daño: " + attackDamage);
            }
        }
    }
}
