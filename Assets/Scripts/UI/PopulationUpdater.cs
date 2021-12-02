using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopulationUpdater : MonoBehaviour
{
    public List<string> preyList = new List<string>();
    public List<string> predList = new List<string>();
    public List<string> plantList = new List<string>();
    public Gradient gradient;
    public Slider preySlider;
    public Slider predSlider;
    public Slider plantSlider;
    public Text pop;
    public Text birth;
    public Text death;
    int preyPop;
    int predPop;
    int plantPop;
    int totalPop;
    int totalBirth;
    int totalDeath;
    public Image preyFill;
    public Image predFill;
    public Image plantFill;
    public float updateTime;

    public void UpdatePopulations(string _type, int _count)
    {
        if (preyList.Contains(_type))
        {
            if (_count > 0)
            { totalBirth += _count; }
            if (_count < 0)
            { totalDeath += _count; }
            preyPop += _count;
            totalPop += _count;
            preyFill.color = gradient.Evaluate(preySlider.normalizedValue);
        }
        if (predList.Contains(_type))
        {
            if (_count > 0)
            { totalBirth += _count; }
            if (_count < 0)
            { totalDeath += _count; }
            predPop += _count;
            totalPop += _count;
            predFill.color = gradient.Evaluate(predSlider.normalizedValue);
        }
        if (plantList.Contains(_type))
        {
            plantPop += _count;
            plantFill.color = gradient.Evaluate(plantSlider.normalizedValue);
        }
        preySlider.value = preyPop;
        predSlider.value = predPop;
        plantSlider.value = plantPop;

        if (_count > 0)
        {
            totalBirth += _count;
            birth.text = totalBirth.ToString();
        }
        if (_count < 0)
        {
            totalDeath += _count;
            death.text = totalDeath.ToString();
        }


        pop.text = totalPop.ToString();
    }

    public void SetMaxPopulation(string _type, int _count, int maxCount)
    {
        if (preyList.Contains(_type))
        {
            preyPop += _count;
            preySlider.maxValue += maxCount;
            preyFill.color = gradient.Evaluate(preySlider.normalizedValue);
        }
        if (predList.Contains(_type))
        {
            predPop += _count;
            predSlider.maxValue += maxCount;
            predFill.color = gradient.Evaluate(predSlider.normalizedValue);
        }
        if (plantList.Contains(_type))
        {
            plantPop += _count;
            plantSlider.maxValue += maxCount;
            plantFill.color = gradient.Evaluate(plantSlider.normalizedValue);
        }

    }

    void Update()
    {
        updateTime += Time.deltaTime;
        if (updateTime > 1)
        {
            totalPop = GlobalScript.Instance.population;
            totalBirth = GlobalScript.Instance.births;
            totalDeath = GlobalScript.Instance.deaths;
            pop.text = "TOTAL POPULATION - " + totalPop.ToString("n0");
            birth.text = "TOTAL BIRTHS - " + totalBirth.ToString("n0");
            death.text = "TOTAL DEATHS - " + totalDeath.ToString("n0");
            preySlider.value = GlobalScript.Instance.prey;
            predSlider.value = GlobalScript.Instance.pred;
            plantSlider.value = GlobalScript.Instance.plants;
            preyFill.color = gradient.Evaluate(preySlider.normalizedValue);
            predFill.color = gradient.Evaluate(predSlider.normalizedValue);
            plantFill.color = gradient.Evaluate(plantSlider.normalizedValue);
            updateTime = 0;

        }
    }



}
