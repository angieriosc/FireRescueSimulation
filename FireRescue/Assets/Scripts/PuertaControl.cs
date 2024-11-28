using UnityEngine;

public class PuertaControl : MonoBehaviour
{
    // Este script debe estar en la puerta cerrada

    void OnTriggerEnter(Collider other)
    {
        // Verificar si el objeto con el que ha colisionado tiene el tag "Pared"
        if (other.CompareTag("Pared"))
        {
            // Destruir la pared
            Destroy(other.gameObject);
        }
    }
}
