using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Classe che gestisce le barriere da eliminare per far muovere il player nel museo
/// </summary>
public class BarriereManager : MonoBehaviour
{
    public static BarriereManager instance;     // Istanza della classe

    private static GameObject[] gameObjects;    // Vettore di GameObject che contiene gli oggetti barriere

    // Metodo Awake rispetto a Start è chiamato al caricamento dell'oggetto e non all'esecuzione dello script
    private void Awake()
    {
        instance = this;

        // Salva nel vettore tutti i GameObject che hanno come tag "Barriera"
        gameObjects = GameObject.FindGameObjectsWithTag("Barriera");
    }
    
    /// <summary>
    /// Metodo che disattiva le barriere nel museo
    /// </summary>
    public void controlloBarriere()
    {
        // Itera su ogni oggetto barriera e lo disabilita
        foreach (GameObject barriera in gameObjects)
        {
            barriera.SetActive(false);
        }
    }
}
