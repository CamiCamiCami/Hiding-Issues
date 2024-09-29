using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // Necesario para cambiar de escena

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;          
    public Slider healthBar;             
    private int currentHealth;           

    void Start()
    {
        currentHealth = maxHealth;       
        UpdateHealthBar();               
    }

    public void TakeDamage(int damage)
    {
        Debug.Log($"Recibiendo daño: {damage}, Salud actual antes de daño: {currentHealth}");
        currentHealth -= damage;         
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Asegura que la salud esté dentro del rango válido
        UpdateHealthBar();               
        Debug.Log($"Salud actual después de daño: {currentHealth}");

        
        if (currentHealth <= 0)
        {
            LoseGame();
        }
    }

    void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;  
            healthBar.value = currentHealth; 
            Debug.Log($"Actualizando Slider: Valor del Slider: {healthBar.value}, Salud actual: {currentHealth}");
        }
        else
        {
            Debug.LogWarning("El campo 'healthBar' no está asignado en el script PlayerHealth.");
        }
    }

    void LoseGame()
    {
        Debug.Log("Salud llegó a 0. Cargando la escena de perder...");
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameOver"); 
    }
}
