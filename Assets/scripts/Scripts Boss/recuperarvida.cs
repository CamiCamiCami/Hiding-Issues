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
    public AudioSource audioSource;
    public AudioClip soundEffect;

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
                playerHealth.TakeDamage(-vida);
                audioSource.PlayOneShot(soundEffect);

                if (healthBar != null)
                {
                    healthBar.value += vida;
                }
                foreach (Renderer renderer in pocion.GetComponentsInChildren<Renderer>())
                {
                    renderer.enabled = false;
                }
                pocion.GetComponent<Collider>().enabled = false;
                // Inicia una corutina para desactivar la poción después de la duración del sonido
                StartCoroutine(DesactivarPocionDespuesDeSonido());
                Debug.Log("Poción consumida. Vida añadida: " + vida);
            }
            else
            {
                Debug.LogWarning("El jugador no tiene el componente PlayerHealth.");
            }
        }
    }

    private IEnumerator DesactivarPocionDespuesDeSonido()
    {
        // Espera la duración del sonido antes de desactivar el objeto
        yield return new WaitForSeconds(soundEffect.length);
        pocion.SetActive(false);
    }
}