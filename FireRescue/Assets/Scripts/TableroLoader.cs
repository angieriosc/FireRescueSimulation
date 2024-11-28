using UnityEngine;
using System.IO;
using System.Collections;

public class TableroLoader : MonoBehaviour
{
    public GameObject paredHPrefab, paredVPrefab, basuraGPrefab, basuraCPrefab;
    public GameObject puntoInteresPrefab, victimaPrefab; 
    public GameObject puertaAbiertaPrefab, puertaCerradaPrefab;
    public GameObject player1, player2, player3, player4, player5, player6;

    private string filePath;


    public void CargarContenido(string content)
    {
        string[] lines = content.Split('\n'); // Divide el contenido en líneas
        Debug.Log("Contenido recibido por TableroLoader:");
        CargarTablero(lines);
    }


    void CargarTablero(string[] lines)
    {
        LimpiarEscena();
        int index = 0;

        // 1. Leer la descripción de las paredes 
        for (int i = 0; i < 6; i++)
        {
            string[] celdas = lines[index].Split(' ');
            for (int j = 0; j < 8; j++)
            {
                string paredes = celdas[j];
                CrearParedes(j, i, paredes);
            }
            index++;
        }

        // 2. Leer los puntos de interés
        for (int i = 0; i < 3; i++)
        {
            string[] partes = lines[index].Split(' ');
            float r = int.Parse(partes[0]);
            float c = int.Parse(partes[1]);
            char tipo = partes[2][0];
            char revelado = partes[3][0];

            Vector3 posicion = new Vector3(((c * 5) - 2.5f), 1f, ((-r * 5) + 2.5f));

            if (tipo == 'v') // Es una víctima
            {   
                if (revelado == 'v')
                {
                    Instantiate(victimaPrefab, posicion, Quaternion.identity);
                }
                else if (revelado == 'f')
                {
                    Instantiate(puntoInteresPrefab, posicion, Quaternion.identity);
                }
            }
            else if (tipo == 'f') // Es una falsa alarma
            {
                if (revelado == 'f')
                {
                    Instantiate(puntoInteresPrefab, posicion, Quaternion.identity);
                }
            }
            index++;
        }

        int n = int.Parse(lines[index]);
        index++;

        // Instanciar los marcadores de humo
        for (int i = 0; i < n; i++)
        {
            string[] parts = lines[index].Split(' ');
            int fila = int.Parse(parts[0]); // Fila
            int columna = int.Parse(parts[1]); // Columna
            char categoria = parts[2][0];
            Vector3 posicion = new Vector3((columna * 5) - 5, 0, (-fila * 5) + 5);

            if (categoria == 'h'){
                Instantiate(basuraCPrefab, posicion, Quaternion.identity);
            }
            else if (categoria == 'f'){
                Instantiate(basuraGPrefab, posicion, Quaternion.identity);
            }
            index++;
        }

        int o = int.Parse(lines[index]);
        index++;

        // 5. Leer las puertas
        for (int i = 0; i < o; i++)
        {
            string[] part = lines[index].Split(' ');
            int r1 = int.Parse(part[0])-1;
            int c1 = int.Parse(part[1])-1;
            int r2 = int.Parse(part[2])-1;
            int c2 = int.Parse(part[3])-1;
            int estado = int.Parse(part[4]);

            // Determinar si la puerta está en horizontal o vertical
            bool esHorizontal = c1 == c2;
            bool esVertical = r1 == r2; 

            // Rotación para la puerta
            Quaternion rotacion = esHorizontal ? Quaternion.identity : Quaternion.Euler(0, 90, 0);

            // Quitar la pared que separa las dos celdas
            if (esHorizontal & estado == 0){
                Vector3 posicion = new Vector3((c1 * 5)+0.25f, 0, (r1 * -5) - 5f); 
                Instantiate(puertaCerradaPrefab, posicion, rotacion);
            }
            else if (esHorizontal & estado == 1){
                Vector3 posicion = new Vector3((c1 * 5)+0.25f, 0, (r1 * -5) - 5f); 
                Instantiate(puertaAbiertaPrefab, posicion, rotacion);
            }
            else if (esVertical  & estado == 0){
                Vector3 posicion = new Vector3((c1 * 5) + 5f, 0, (r1 * -5)-0.25f); 
                Instantiate(puertaCerradaPrefab, posicion, rotacion);
            }
            else if (esVertical & estado == 1){
                Vector3 posicion = new Vector3((c1 * 5) + 5f, 0, (r1 * -5)-0.25f); 
                Instantiate(puertaAbiertaPrefab, posicion, rotacion);
            }

            index++;
        }
                
        int p = int.Parse(lines[index]);
        index++;


        // 5. Leer los puntos de entrada
        for (int i = 0; i < p; i++)
        {
            string[] partess = lines[index].Split(' ');
            int rr = int.Parse(partess[0]) -1;
            int cc = int.Parse(partess[1]) -1;

            // Determinar en que parte del tablero va la entrada
            bool esArriba = rr == 0 ;
            bool esAbajo = rr == 5; 
            bool esIzquierda = cc == 0;
            bool esDerecha = cc == 7 ; 
            
            // Rotación para la entrada
            Quaternion rotacion = esArriba || esAbajo ? Quaternion.identity : Quaternion.Euler(0, 90, 0);

            if (esAbajo)
            {
                Vector3 posicion = new Vector3((cc * 5)+0.25f, 0, (rr * -5)-5f); 
                Instantiate(puertaAbiertaPrefab, posicion, rotacion);
            }
            else if (esArriba)
            {
                Vector3 posicion = new Vector3((cc * 5)+0.25f, 0, (rr * -5)); 
                Instantiate(puertaAbiertaPrefab, posicion, rotacion);
            }
            else if (esDerecha)
            {
                Vector3 posicion = new Vector3((cc * 5)+5f, 0, (rr * -5)-0.25f); 
                Instantiate(puertaAbiertaPrefab, posicion, rotacion);
            }
            else if (esIzquierda)
            {
                Vector3 posicion = new Vector3((cc * 5), 0, (rr * -5)-0.25f); 
                Instantiate(puertaAbiertaPrefab, posicion, rotacion);
            }
            index++;
        }
        // Colocar jugadores
        int h = int.Parse(lines[index]);
        index++;


        // 5. Leer los puntos de entrada
        for (int i = 0; i < h; i++){
            GameObject[] players = { player1, player2, player3, player4, player5, player6 };
            string[] partesss = lines[index].Split(' ');
            int ccc = int.Parse(partesss[0]);  // Columna
            int rrr = int.Parse(partesss[1]);  // Fila
            // Columna int ccc = int.Parse(partesss[1]);  
            int id = int.Parse(partesss[2]); // Id del jugador

            GameObject player = players[id]; // Ajustamos el id para que coincida con el índice
            Vector3 nuevaPosicion = new Vector3((ccc * 5) - 2.5f, 0, (-rrr * 5) + 2.5f);

                // Iniciar movimiento hacia la nueva posición
            StartCoroutine(MoverJugador(player, nuevaPosicion, 2f));
            index++;
            }
    }
        // Corutina para mover un jugador

    IEnumerator MoverJugador(GameObject jugador, Vector3 destino, float duracion)
    {
        Vector3 posicionInicial = jugador.transform.position;
        float tiempoTranscurrido = 0;

        while (tiempoTranscurrido < duracion)
        {
            tiempoTranscurrido += Time.deltaTime;
            float t = tiempoTranscurrido / duracion;

            // Interpolación de la posición usando Lerp
            jugador.transform.position = Vector3.Lerp(posicionInicial, destino, t);

            // Esperar hasta el siguiente frame
            yield return null;
        }
        // Asegurarse de que el jugador llegue a la posición exacta al final
        jugador.transform.position = destino;
        yield return new WaitForSeconds(2f);
    }

    void CrearParedes(int a, int b, string paredes)
    {
        float x = a * 5;
        float z = -b * 5;

        // Pared arriba (primer dígito)
        if (paredes[0] == '1')
            Instantiate(paredHPrefab, new Vector3(x, 0, z), Quaternion.identity);

        // Pared izquierda (segundo dígito)
        if (paredes[1] == '1')
            Instantiate(paredHPrefab, new Vector3(x, 0, z), Quaternion.Euler(0, 90, 0));

        // Pared abajo (tercer dígito)
        if (paredes[2] == '1')
            Instantiate(paredHPrefab, new Vector3(x, 0, z - 5f), Quaternion.identity);

        // Pared derecha (cuarto dígito)
        if (paredes[3] == '1')
            Instantiate(paredHPrefab, new Vector3(x + 5f, 0, z), Quaternion.Euler(0, 90, 0));
    }

    // Método para limpiar todos los prefabs excepto los jugadores
    void LimpiarEscena()
    {
        // Encuentra todos los objetos con la etiqueta "Prefab"
        GameObject[] objetos = GameObject.FindGameObjectsWithTag("object");

        foreach (GameObject obj in objetos)
        {
            // Si el objeto no es un jugador y está dentro del área, destrúyelo
            if (!obj.CompareTag("Player"))
            {
                Vector3 posicion = obj.transform.position;

                // Verificar si está dentro del área delnimitada
                if (posicion.x >= 0 && posicion.x <= 50 &&
                    posicion.z >= -50 && posicion.z <= 0)
                {
                    Destroy(obj);
                }
            }
        }
    }
}
