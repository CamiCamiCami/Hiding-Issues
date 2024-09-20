using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PocionVida : MonoBehaviour
{
    public GameObject pocion;            
    public GameObject player;            
    public Slider healthBar;             
    public int vida;                     

    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Jugador ha colisionado con la poción.");
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        
        if (collision.gameObject.CompareTag("Player") && Input.GetKeyDown(KeyCode.R))
        {
            
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                
                playerHealth.TakeDamage(-vida); // Usamos TakeDamage con un valor negativo para curar

                
                if (healthBar != null)
                {
                    healthBar.value += vida;
                }

                // Desactiva la poción una vez usada
                pocion.SetActive(false);
                Debug.Log("Poción consumida. Vida añadida: " + vida);
            }
            else
            {
                Debug.LogWarning("El jugador no tiene el componente PlayerHealth.");
            }
        }
    }
}