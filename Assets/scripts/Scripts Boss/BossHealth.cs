using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // Para cambiar de escena

public class BossHealth : MonoBehaviour
{
    public int maxHealth = 100;          
    public Slider healthBar;             
    private int currentHealth;           

    void Start()
    {
        currentHealth = maxHealth;       
        UpdateHealthBar();               
    }

    // Método para que el jefe reciba daño
    public void TakeDamage(int amount)
    {
        currentHealth -= amount;         
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); 
        UpdateHealthBar();               

        if (currentHealth <= 0)
        {
            WinGame();                   
        }
    }

    void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;  
            healthBar.value = currentHealth; 
        }
        else
        {
            Debug.LogWarning("El campo 'healthBar' no está asignado en el script BossHealth.");
        }
    }

    void WinGame()
    {
        Debug.Log("El jefe ha sido derrotado. Cargando la escena de victoria...");
        UnityEngine.SceneManagement.SceneManager.LoadScene("JEFE ABATIDO"); 
    }
}
