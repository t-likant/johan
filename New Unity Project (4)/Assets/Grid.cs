using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Grid : MonoBehaviour
{
    public int width = 10;// Variable que representa el ancho
    public int height = 10;// Variable que representa la altura
    GameObject[,] matriz;// matriz de del campo de esferas
    public GameObject Win; // Variable que representa imagen asignada a bloquear la clase
    public Color baseColor;// Variable que representa los colores del campo de esferas
    public Color PlayerOne;// Variable que representa al jugador 1
    public Color PlayerTwo;// Variable que representa al jugador 2
    public Color IA; // Variable que representa color del contador de reto que sobre escribe en el campo de esferas 
    public TextMesh m3Dtext;// Variable cuadro de texto que nos muestra los puntos del jugador 1
    public TextMesh ronda;// Variable cuadro de texto que nos muestra los turnos "move"
    public float distance = 0; // Variable que representa la distancia entre los objetos
    bool move;//interupto de  muestra los turnos "move"
    bool End = true;//interupto abierto  asignado a "end"
    int turnos = 1;//  asignado al contador de turnos 
    int X = 0;// variable asignada a cordenadas del campo de esferaspara la "ia"
    int Y = 0;// variable asignada a cordenadas del campo de esferaspara la "ia"
    void Start()
    {//instancia la mariz que crea el campo de esferas
        matriz = new GameObject[width, height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                GameObject Sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere) as GameObject;
                Sphere.GetComponent<Renderer>().material.color = baseColor;
                Sphere.transform.position = new Vector3(x * distance, y * distance, 0f);
                matriz[x, y] = Sphere;
            }
        }
    }
    void Update()
    {
        if (End == true)
        {//variable para selecionar
            Vector3 mPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (!move)
            {
                m3Dtext.text = "Player = 1.";//texto asignado
            }
            else
                m3Dtext.text = "Player = 2.";//texto asignado
            ronda.text = ("Move = " + turnos);//texto asignado
            PintarFicha(mPosition);
        }
    }//instancia el sector de cambio de color
    void PintarFicha(Vector3 position)
    {//posisionamiento del selector
        int x = (int)((position.x + 0.5f) / distance);
        int y = (int)((position.y + 0.5f) / distance);
        if (Input.GetMouseButtonDown(0))
        {
            if (x >= 0 && y >= 0 && x < width && y < height)
            {//ubicacion de la matriz
                GameObject esfera = matriz[x, y];
                if (esfera.GetComponent<Renderer>().material.color == baseColor)
                {//selctor usado para no repetir el color asignado
                    Color colorAUsar = Color.clear;
                    if (!move)
                    {//color asignado al jugador1
                        colorAUsar = PlayerOne;
                        esfera.GetComponent<Renderer>().material.color = PlayerOne;
                        move = true;
                        Verificar(x, y, colorAUsar);
                        Verificar2(x, y, colorAUsar);
                    }
                    else
                    {//color asignado al jugador1
                        colorAUsar = PlayerTwo;
                        esfera.GetComponent<Renderer>().material.color = PlayerTwo;
                        move = false;
                        Verificar(x, y, colorAUsar);
                        Verificar2(x, y, colorAUsar);
                    }
                    if (turnos % 2 == 0)
                    {
                        Rule();
                    }
                }
                turnos++;
            }
        }
    } //verificadores de los colores repetidos 4 veces 
    public void Verificar(int s, int q, Color colorVerificar)
    {
        int gana, verifica;
        verifica = 0;
        gana = 0;
        //verificador horizontal 
        if (verifica == 0)
        {
            for (int i = s - 3; i < s + 4; i++)
            {
                if (i < 0 || i >= width)
                    continue;
                GameObject esfera = matriz[i, q];
                if (esfera.GetComponent<Renderer>().material.color == colorVerificar)
                {
                    gana++;
                    if (gana == 4)
                    {
                        Win.SetActive(true);
                        End = false;
                    }
                }
                else
                {
                    gana = 0;
                }
            }
            verifica++;
            gana = 0;
        }
        //verificador vertical
        if (verifica == 1)
        {
            for (int j = q - 3; j < q + 4; j++)
            {
                if (j < 0 || j >= height)
                    continue;
                GameObject esfera = matriz[s, j];

                if (esfera.GetComponent<Renderer>().material.color == colorVerificar)
                {
                    gana++;
                    if (gana == 4)
                    {
                        Win.SetActive(true);
                        End = false;
                    }
                }
                else
                {
                    gana = 0;
                }
            }
            gana = 0;
        }
    }
    public void Verificar2(int X, int Y, Color colorVerificar)
    {
        int gana, verifica;
        verifica = 0;
        gana = 0;

        //verificador diagonal ariba
        if (verifica == 0)
        {
            int j = Y - 3;
            for (int i = X - 3; i <= X + 3; i++)
            {
                if ((i >= 0 && i < width) && (j >= 0 && j < height))
                {
                    GameObject esfera = matriz[i, j];
                    if (esfera.GetComponent<Renderer>().material.color == colorVerificar)
                    {
                        gana++;
                        if (gana == 4)
                        {
                            Win.SetActive(true);
                            End = false;
                        }
                    }
                    else
                    {
                        gana = 0;
                    }
                }
                if (j < 0 || j < width)
                    j++;
            }

            verifica++;
            gana = 0;
        }

        //verificador diagonal abajo
        if (verifica == 1)
        {
            int k = Y + 3;
            for (int i = X - 3; i <= X + 3; i++)
            {
                if ((i >= 0 && i < width) && (k >= 0 && k < height))
                {
                    GameObject esfera = matriz[i, k];
                    if (esfera.GetComponent<Renderer>().material.color == colorVerificar)
                    {
                        gana++;
                        if (gana == 4)
                        {
                            Win.SetActive(true);
                            End = false;
                        }
                    }
                    else
                    {
                        gana = 0;
                    }
                }
                if (k < 0 || k <= width)
                    k--;
            }

            gana = 0;
        }
    }//regla creada que rellena el campo de esferas asta no completar el 4 en linea finaliza el juego
    public void Rule()
    {
        if (X < width)
        {

        }
        else
        {
            Y++;
            X = 0;
        }
        if (Y == 3)
        {
            print("GAME OVER");
        }
        GameObject Sphere = matriz[X, Y];

        if (Sphere.GetComponent<Renderer>().material.color == baseColor)
        {
            Sphere.GetComponent<Renderer>().material.color = IA;
            X++;
        }
        else
            X++;
        Sphere.GetComponent<Renderer>().material.color = IA;

    }
}