using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarriereManager : MonoBehaviour
{
    public static BarriereManager instance;

    private static GameObject[] gameObjects;

    private void Awake()
    {
        instance = this;

        // Troviamo tutti gli oggetti con il tag "Cordone"
        gameObjects = GameObject.FindGameObjectsWithTag("Barriera");
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
