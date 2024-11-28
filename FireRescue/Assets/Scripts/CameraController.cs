using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera mainCamera; 
    public Camera cameraClose; 
    public Camera cameraPlayer1; 
    public Camera cameraPlayer2; 
    public Camera cameraPlayer3; 
    public Camera cameraPlayer4; 
    public Camera cameraPlayer5; 
    public Camera cameraPlayer6; 

    void Start()
    {
        // Asegurarse de que solo una cámara esté activa al inicio
        ActivateCamera(mainCamera);
    }

    /// <summary>
    /// Escucha las entradas del teclado para alternar entre cámaras.
    /// </summary>
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            // Alternar entre la cámara principal y la cámara cercana
            if (mainCamera.enabled)
                ActivateCamera(cameraClose);
            else
                ActivateCamera(mainCamera);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1)) // Tecla 1
        {
            ActivateCamera(cameraPlayer1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2)) // Tecla 2
        {
            ActivateCamera(cameraPlayer2);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3)) // Tecla 3
        {
            ActivateCamera(cameraPlayer3);
        }

        if (Input.GetKeyDown(KeyCode.Alpha4)) // Tecla 4
        {
            ActivateCamera(cameraPlayer4);
        }

        if (Input.GetKeyDown(KeyCode.Alpha5)) // Tecla 5
        {
            ActivateCamera(cameraPlayer5);
        }

        if (Input.GetKeyDown(KeyCode.Alpha6)) // Tecla 6
        {
            ActivateCamera(cameraPlayer6);
        }
    }

    /// <summary>
    /// Activa la cámara especificada y desactiva todas las demás.
    /// </summary>
    /// <param name="cameraToActivate">La cámara que debe activarse.</param>
    private void ActivateCamera(Camera cameraToActivate)
    {
        // Desactivar todas las cámaras
        mainCamera.enabled = false;
        cameraClose.enabled = false;
        cameraPlayer1.enabled = false;
        cameraPlayer2.enabled = false;
        cameraPlayer3.enabled = false;
        cameraPlayer4.enabled = false;
        cameraPlayer5.enabled = false;
        cameraPlayer6.enabled = false;

        // Activar solo la cámara especificada
        cameraToActivate.enabled = true;
    }
}
