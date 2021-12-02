using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIActions : MonoBehaviour
{
    Dictionary<string, int> populations = new Dictionary<string, int>();
    Dictionary<string, int> popTotals = new Dictionary<string, int>();
    [SerializeField] List<string> preyList = new List<string>();
    [SerializeField] List<string> predatorList = new List<string>();
    [SerializeField] List<string> plantList = new List<string>();
    int births;
    int deaths;
    int totalPop;
    public int preyMax;
    public int predatorMax;
    public int plantMax;
    [SerializeField] GamePlayEvents gpEvents;
    [SerializeField] CameraController cameraController;
    void Start()
    {
        popTotals.Add("all", 0);
        popTotals.Add("prey", 0);
        popTotals.Add("predator", 0);
        popTotals.Add("plant", 0);

    }

    public void AddPopulation(string _type, int _count)
    {
        if (populations.ContainsKey(_type))
        {
            populations[_type] += _count;
        }
        else
        {
            populations.Add(_type, _count);
        }
        UpdateTotals(_type, _count);
    }

    void UpdateTotals(string _type, int _count)
    {
        if (preyList.Contains(_type))
        {
            popTotals["prey"] += _count;
        }
        if (preyList.Contains(_type))
        {
            popTotals["predator"] += _count;
        }
        if (preyList.Contains(_type))
        {
            popTotals["plant"] += _count;
        }
        popTotals["all"] += _count;

    }

    void UpdateMoreTotals(int _adj)
    {
        if (_adj > 0)
        {
            births += _adj;
        }
        if (_adj < 0)
        {
            deaths += _adj;
        }
        totalPop += _adj;
    }

}
