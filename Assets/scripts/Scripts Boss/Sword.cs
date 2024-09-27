using UnityEngine;

public class PlayerEquipWeapon : MonoBehaviour
{
    public GameObject swordPrefab; // El prefab de la espada
    public Transform swordHolder;  // El punto donde se equipará la espada (SwordHolder)
    private GameObject equippedSword;

    private void Start()
    {
        EquipSword();
    }

    // Función para equipar la espada
    void EquipSword()
    {
        if (swordPrefab != null && swordHolder != null)
        {
            // Instanciar la espada y hacer que sea hija del punto de la mano
            equippedSword = Instantiate(swordPrefab, swordHolder.position, swordHolder.rotation, swordHolder);
            Debug.Log("Espada equipada.");
        }
        else
        {
            Debug.LogWarning("No se ha asignado la espada o el punto de equipamiento.");
        }
    }

    // Puedes añadir un método si quieres permitir cambiar de espada o desequiparla
    public void UnequipSword()
    {
        if (equippedSword != null)
        {
            Destroy(equippedSword);
            Debug.Log("Espada desequipada.");
        }
    }
}
