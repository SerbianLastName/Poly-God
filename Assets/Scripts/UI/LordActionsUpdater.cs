using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LordActionsUpdater : MonoBehaviour
{
    public Button slowBirth;
    public Button fastHunger;
    public Button fastDisease;
    public Button theSnap;
    public Button theRapture;
    public Button fastBirth;
    public Button slowHunger;
    public Button slowDisease;
    public Button blessLand;
    public Button sendSon;
    public Dropdown spawnList;
    float updateTime;

    public void Update()
    {
        updateTime += Time.deltaTime;
        if (updateTime > 1)
        { UpdateButtons(); }
    }

    void UpdateButtons()
    {
        if (GlobalScript.Instance.lordPoints >= 1000 && !GlobalScript.Instance.globalEffects.Contains("LessHorny") && !GlobalScript.Instance.globalEffects.Contains("MoreHorny"))
        {
            fastBirth.interactable = true;
            slowBirth.interactable = true;
        }
        else
        {
            fastBirth.interactable = false;
            slowBirth.interactable = false;
        }
        if (GlobalScript.Instance.lordPoints >= 1500 && !GlobalScript.Instance.globalEffects.Contains("LessHungry") && !GlobalScript.Instance.globalEffects.Contains("MoreHungry"))
        {
            fastHunger.interactable = true;
            slowHunger.interactable = true;
        }
        else
        {
            fastHunger.interactable = false;
            slowHunger.interactable = false;
        }
        if (GlobalScript.Instance.lordPoints >= 2500 && !GlobalScript.Instance.globalEffects.Contains("LessSick") && !GlobalScript.Instance.globalEffects.Contains("MoreSick"))
        {
            fastDisease.interactable = true;
            slowDisease.interactable = true;
        }
        else
        {
            fastDisease.interactable = false;
            slowDisease.interactable = false;
        }
        if (GlobalScript.Instance.lordPoints >= 5000)
        { blessLand.interactable = true; }
        else
        { blessLand.interactable = false; }
        if (GlobalScript.Instance.lordPoints >= 10000)
        { theSnap.interactable = true; }
        else
        { theSnap.interactable = false; }
        if (GlobalScript.Instance.lordPoints >= 100000)
        { sendSon.interactable = true; }
        else
        { sendSon.interactable = false; }
        if (GlobalScript.Instance.lordPoints >= 1000000)
        { theRapture.interactable = true; }
        else
        { theRapture.interactable = false; }
    }
}




