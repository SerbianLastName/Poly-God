using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GamePlayEvents : MonoBehaviour
{
    [SerializeField] SmartContractInteractions scScript;
    [SerializeField] GameObject buyLandUI;
    [SerializeField] GameObject pickPlotUI;
    [SerializeField] GameObject baseOSUI;
    [SerializeField] GameObject mainUI;
    [SerializeField] GameObject statsUI;
    [SerializeField] GameObject lordUI;
    [SerializeField] GameObject loadingUI;
    [SerializeField] GameObject buyWaitUI;
    [SerializeField] GameObject newGameUI;

    [SerializeField] GameObject bG;
    [SerializeField] Environment environment;
    [SerializeField] TerrainGeneration.TerrainGenerator terGen;
    [SerializeField] CameraController cameraController;
    public string plot;
    List<int> plots;
    int plotIndexToUse;

    // Start up functions

    public void HelloWorld()
    {
        pickPlotUI.GetComponent<Canvas>().enabled = false;
        lordUI.GetComponent<Canvas>().enabled = false;
        mainUI.GetComponent<Canvas>().enabled = false;
        bG.GetComponent<Canvas>().enabled = false;
        loadingUI.GetComponent<Canvas>().enabled = false;
        buyLandUI.GetComponent<Canvas>().enabled = false;
        terGen.Generate();
        plots = ConvertPlots(plot);
        if (plots.Count == 0)
        {
            baseOSUI.GetComponent<Canvas>().enabled = true;
            buyLandUI.GetComponent<Canvas>().enabled = true;
            bG.GetComponent<Canvas>().enabled = true;
        }
        else
        {
            cameraController.inMenu = true;
            baseOSUI.GetComponent<Canvas>().enabled = true;
            pickPlotUI.GetComponent<Canvas>().enabled = true;
            bG.GetComponent<Canvas>().enabled = true;
            pickPlotUI.GetComponent<PickPlot>().GenerateDropdown(plots);
            terGen.terrainNoise.seed = plots[0];
        }
    }

    public void StartWorld(Dictionary<string, int> spawns)
    {
        newGameUI.GetComponent<Canvas>().enabled = false;
        pickPlotUI.GetComponent<Canvas>().enabled = false;
        baseOSUI.GetComponent<Canvas>().enabled = false;
        bG.GetComponent<Canvas>().enabled = false;
        mainUI.GetComponent<Canvas>().enabled = true;
        cameraController.inMenu = false;
        environment.RealStart();
        foreach (KeyValuePair<string, int> animal in spawns)
        { environment.SpawnFromMenu(animal.Value, animal.Key); }
    }
    public void StartSpawns(Dictionary<string, int> spawns)
    {

    }

    // UI actions


    public void LoadingScreen(bool load)
    {
        if (load)
        { loadingUI.GetComponent<Canvas>().enabled = true; }
        else
        { loadingUI.GetComponent<Canvas>().enabled = false; }

    }
    public void BuyScreen(bool load)
    {
        if (load)
        { buyWaitUI.GetComponent<Canvas>().enabled = true; }
        else
        { buyWaitUI.GetComponent<Canvas>().enabled = false; }

    }

    public void SetWorldInt(int plotIndex)
    {
        plotIndexToUse = plotIndex;
        Debug.Log(plots[plotIndex]);
        terGen.terrainNoise.seed = plots[plotIndex];
        terGen.Generate();
    }

    public void NewGame()
    {
        pickPlotUI.GetComponent<Canvas>().enabled = false;
        newGameUI.GetComponent<Canvas>().enabled = true;

    }

    public void OpenStats()
    {
        baseOSUI.GetComponent<Canvas>().enabled = true;
        statsUI.GetComponent<Canvas>().enabled = true;
        mainUI.GetComponent<Canvas>().enabled = false;
        cameraController.inMenu = true;
    }

    public void OpenLordActions()
    {
        baseOSUI.GetComponent<Canvas>().enabled = true;
        lordUI.GetComponent<Canvas>().enabled = true;
        bG.GetComponent<Canvas>().enabled = true;
        mainUI.GetComponent<Canvas>().enabled = false;
        cameraController.inMenu = true;
    }

    public void CloseOS()
    {
        baseOSUI.GetComponent<Canvas>().enabled = false;
        statsUI.GetComponent<Canvas>().enabled = false;
        lordUI.GetComponent<Canvas>().enabled = false;
        bG.GetComponent<Canvas>().enabled = false;
        mainUI.GetComponent<Canvas>().enabled = true;
        cameraController.inMenu = false;
    }

    // Gameplay Actions

    public void UpBirthRate()
    {
        GlobalScript.Instance.hornyMod = 1.5f;
        GlobalScript.Instance.globalEffects.Add("MoreHorny");
        GlobalScript.Instance.lordPoints -= 1000;
        Invoke("ResetBirthRate", 60);
    }
    public void DownBirthRate()
    {
        GlobalScript.Instance.hornyMod = 0.5f;
        GlobalScript.Instance.globalEffects.Add("LessHorny");
        GlobalScript.Instance.lordPoints -= 1000;
        Invoke("ResetBirthRate", 60);
    }
    void ResetBirthRate()
    {
        GlobalScript.Instance.hornyMod = 1;
        GlobalScript.Instance.globalEffects.Remove("LessHorny");
        GlobalScript.Instance.globalEffects.Remove("MoreHorny");
    }

    public void UpHunger()
    {
        GlobalScript.Instance.hungerMod = 1.5f;
        GlobalScript.Instance.globalEffects.Add("MoreHungry");
        GlobalScript.Instance.lordPoints -= 1500;
        Invoke("ResetHunger", 60);
    }
    public void DownHunger()
    {
        GlobalScript.Instance.hungerMod = 0.5f;
        GlobalScript.Instance.globalEffects.Add("LessHungry");
        GlobalScript.Instance.lordPoints -= 1500;
        Invoke("ResetHunger", 60);
    }
    void ResetHunger()
    {
        GlobalScript.Instance.hungerMod = 1;
        GlobalScript.Instance.globalEffects.Remove("LessHungry");
        GlobalScript.Instance.globalEffects.Remove("MoreHungry");
    }
    public void UpDisease()
    {
        GlobalScript.Instance.diseaseMod = 1.5f;
        GlobalScript.Instance.globalEffects.Add("MoreSick");
        GlobalScript.Instance.lordPoints -= 2500;
        Invoke("ResetDisease", 60);
    }
    public void DownDisease()
    {
        GlobalScript.Instance.diseaseMod = 0.5f;
        GlobalScript.Instance.globalEffects.Add("LessSick");
        GlobalScript.Instance.lordPoints -= 2500;
        Invoke("ResetDisease", 60);
    }
    void ResetDisease()
    {
        GlobalScript.Instance.hungerMod = 1;
        GlobalScript.Instance.globalEffects.Remove("LessSick");
        GlobalScript.Instance.globalEffects.Remove("MoreSick");
    }
    public void TheSnap()
    {
        GlobalScript.Instance.lordPoints -= 10000;
        var animals = FindObjectsOfType<LivingEntity>();
        foreach (LivingEntity _animal in animals)
        {
            if (_animal.gameObject.activeSelf == true)
            {
                if (UnityEngine.Random.value > 0.5)
                { _animal.GetComponent<LivingEntity>().Die(CauseOfDeath.Snap); }
            }
        }
    }
    public void TheRapture()
    {
        GlobalScript.Instance.lordPoints -= 1000000;

    }
    public void BlessTheLand()
    {
        GlobalScript.Instance.lordPoints -= 5000;

    }


    // Crypto Functions

    public void BuyLand()
    {
        buyLandUI.GetComponent<Canvas>().enabled = false;
        BuyScreen(true);
        scScript.CheckPrice();

        // pickPlotUI.GetComponent<Canvas>().enabled = true;
    }

    // Utility Functions

    List<int> ConvertPlots(string str)
    {
        List<int> ints = new List<int>();

        string _str = str.Replace("[", "").Replace("]", "").Replace("\"", "");
        Debug.Log(_str);
        string[] splitString = _str.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
        foreach (string item in splitString)
        {
            try
            {
                ints.Add(Convert.ToInt32(item));
            }
            catch (System.Exception e)
            {
                Debug.LogException(e);
            }
        }
        return ints;
    }
}
