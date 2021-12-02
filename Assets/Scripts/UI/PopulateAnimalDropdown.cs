using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopulateAnimalDropdown : MonoBehaviour
{
    [System.Serializable]
    public struct Spawns
    {
        public string animal;
        public int count;
        public int cost;
    }
    public List<Spawns> spawns;
    int spawnChoice;
    List<string> spawnList = new List<string>();
    public Environment environment;

    // public AnimalsToSpawn spawns;
    [SerializeField] Dropdown m_Dropdown;

    public void Start()
    {
        foreach (Spawns sp in spawns)
        {
            spawnList.Add("Create " + sp.count.ToString() + " " + sp.animal + " lives for " + sp.cost.ToString() + " LORD POINTS");
        }

        m_Dropdown.ClearOptions();
        m_Dropdown.AddOptions(spawnList);
    }

    public void SetSpawnChoice(int choice)
    {
        spawnChoice = choice;
        Debug.Log(choice);
    }

    public void Spawn()
    {
        environment.SpawnFromMenu(spawns[spawnChoice].count, spawns[spawnChoice].animal);
    }
}
