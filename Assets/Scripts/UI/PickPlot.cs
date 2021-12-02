using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickPlot : MonoBehaviour
{
    List<string> plotList = new List<string>();
    [SerializeField] Dropdown plotDropdown;

    public void GenerateDropdown(List<int> plots)
    {
        Debug.Log(plots);
        foreach (int i in plots)
        {
            Debug.Log(i);
            plotList.Add("# " + i.ToString());
        }

        plotDropdown.ClearOptions();
        plotDropdown.AddOptions(plotList);
    }
}
