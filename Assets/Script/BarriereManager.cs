using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestioneBarriere : MonoBehaviour
{
    public static GestioneBarriere instance;

    private static GameObject[] gameObjects;

    private void Awake()
    {

        // Troviamo tutti gli oggetti con il tag "Cordone"
        gameObjects = GameObject.FindGameObjectsWithTag("Barriera");

        if (instance == null)
        {
            instance = this;
            // Itera su ogni nemico e disabilitalo
            foreach (GameObject barriera in gameObjects)
            {
                DontDestroyOnLoad(barriera);
            }
        }
        else
        {
            foreach (GameObject barriera in gameObjects)
            {
                Destroy(barriera);
            }
        }
    }

    public void controlloBarriere()
    {
        // Itera su ogni oggetto barriera e lo disabilita
        foreach (GameObject barriera in gameObjects)
        {
            barriera.SetActive(false);
        }
    }
}
