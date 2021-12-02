using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int lifePoints;
    public float populationHealth;

    public void AdjustLifePoints(int adj)
    {
        lifePoints += adj;
    }

}
