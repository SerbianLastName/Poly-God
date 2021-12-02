using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalScript : MonoBehaviour
{
    public static GlobalScript Instance { get; private set; }

    // Population Modifiers

    public float hornyMod;
    public float hungerMod;
    public float mutMod;
    public float diseaseMod;
    public float cannibalMod;

    // Population Stats

    public int population;
    public int births;
    public int deaths;
    public int prey;
    public int pred;
    public int plants;
    public int cannibals;


    // Player Variables

    public int lordPoints;
    public int startingLp;
    public List<string> globalEffects = new List<string>();


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            lordPoints = startingLp;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
