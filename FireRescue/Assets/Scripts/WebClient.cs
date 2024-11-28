using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class WebClient : MonoBehaviour
{
    [SerializeField]
    private string url = "http://localhost:8585/"; // URL base del servidor

    [SerializeField]
    private TableroLoader tableroLoader; 

    // Método para realizar una solicitud GET
    public IEnumerator GetGameState(int step, System.Action<ResponseData, bool> callback)
    {
        string stepUrl = $"{url}/{step}";
        UnityWebRequest www = UnityWebRequest.Get(stepUrl);

        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError($"Error al conectar al servidor: {www.error}");
            callback(null, false);
        }
        else
        {
            Debug.Log($"Datos JSON recibidos: {www.downloadHandler.text}");
            // Deserializar el JSON en un objeto ResponseData
            ResponseData response = JsonUtility.FromJson<ResponseData>(www.downloadHandler.text);
            callback(response, true);
        }
    }

    // Corutina para seguir solicitando pasos hasta que se complete el proceso
    private IEnumerator FetchSteps()
    {
        int step = 0; // Comenzamos con el paso 1
        bool end = false;

        while (!end)
        {
            yield return StartCoroutine(GetGameState(step, (response, success) =>
            {
                if (success && response != null)
                {
                    // Procesar los datos recibidos
                    Debug.Log($"Paso {step} recibido: {response.content}");
                    tableroLoader.CargarContenido(response.content);

                    // Actualizar el estado de finalización
                    end = response.end;
                }
                else
                {
                    Debug.LogError("Error o respuesta vacía. Deteniendo solicitudes.");
                    end = true; // Termina en caso de error para evitar bucles infinitos
                }
            }));

            // Espera 5 segundos antes de continuar con el siguiente paso
            yield return new WaitForSeconds(5f);

            step++;
        }

        Debug.Log("Se completaron todos los pasos.");
    }

    // Start es llamado al iniciar el objeto
    void Start()
    {
        StartCoroutine(FetchSteps());
    }

    // Clase para deserializar la respuesta JSON
    [System.Serializable]
    public class ResponseData
    {
        public string content;   // El contenido completo del JSON
        public bool end;  // Indicador de finalización
    }
}
