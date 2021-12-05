using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewGame : MonoBehaviour
{

    public int spawnPoints;
    [SerializeField] Image infoImage;
    [SerializeField] Text infoLabel;
    [SerializeField] Text infoText;
    [SerializeField] Text spawnPointsText;
    [SerializeField] GamePlayEvents gpEvents;

    [System.Serializable]
    public class AnimalInfo
    {
        public string animal;
        public string animalLabel;
        public Sprite animalImage;
        public string animalInfo;
        public int animalPerSpawnPoint;
    }
    [SerializeField] List<AnimalInfo> infoForUI;

    [SerializeField] Slider frogSlider;
    [SerializeField] Slider mouseSlider;
    [SerializeField] Slider pigeonSlider;
    [SerializeField] Slider squirrelSlider;
    [SerializeField] Slider duckSlider;
    [SerializeField] Slider rabbitSlider;
    [SerializeField] Slider turtleSlider;
    [SerializeField] Slider possumSlider;
    [SerializeField] Slider raccoonSlider;
    [SerializeField] Slider pigSlider;
    [SerializeField] Slider weaselSlider;
    [SerializeField] Slider badgerSlider;
    [SerializeField] Slider foxSlider;
    [SerializeField] Text frogCount;
    [SerializeField] Text mouseCount;
    [SerializeField] Text pigeonCount;
    [SerializeField] Text squirrelCount;
    [SerializeField] Text duckCount;
    [SerializeField] Text rabbitCount;
    [SerializeField] Text turtleCount;
    [SerializeField] Text possumCount;
    [SerializeField] Text raccoonCount;
    [SerializeField] Text pigCount;
    [SerializeField] Text weaselCount;
    [SerializeField] Text badgerCount;
    [SerializeField] Text foxCount;

    Dictionary<string, int> spawnList = new Dictionary<string, int>();
    Dictionary<string, int> validSpawnList = new Dictionary<string, int>();


    public void Start()
    {
        foreach (AnimalInfo animalInfo in infoForUI)
        {
            spawnList.Add(animalInfo.animal, 0);
            Debug.Log(animalInfo.animal);
            validSpawnList.Add(animalInfo.animal, 1);
        }

        for (int i = 0; i < infoForUI.Count; i++)
        {
            UpdateSpawnList(i);

        }
    }

    public void UpdateInfo(int animalIndex)
    {
        infoLabel.text = infoForUI[animalIndex].animalLabel;
        infoText.text = infoForUI[animalIndex].animalInfo;
        infoImage.GetComponent<Image>().sprite = infoForUI[animalIndex].animalImage;
        UpdateSpawnList(animalIndex);
    }

    public void UpdateSpawnList(int animalIndex)
    {
        if (animalIndex == 0)
        {
            spawnList["Frog"] = (int)frogSlider.value * infoForUI[animalIndex].animalPerSpawnPoint;
            frogCount.text = ((int)frogSlider.value * infoForUI[animalIndex].animalPerSpawnPoint).ToString();
        }
        if (animalIndex == 1)
        {
            spawnList["Mouse"] = (int)mouseSlider.value * infoForUI[animalIndex].animalPerSpawnPoint;
            mouseCount.text = ((int)mouseSlider.value * infoForUI[animalIndex].animalPerSpawnPoint).ToString();
        }
        if (animalIndex == 2)
        {
            spawnList["Pigeon"] = (int)pigeonSlider.value * infoForUI[animalIndex].animalPerSpawnPoint;
            pigeonCount.text = ((int)pigeonSlider.value * infoForUI[animalIndex].animalPerSpawnPoint).ToString();
        }
        if (animalIndex == 3)
        {
            spawnList["Squirrel"] = (int)squirrelSlider.value * infoForUI[animalIndex].animalPerSpawnPoint;
            squirrelCount.text = ((int)squirrelSlider.value * infoForUI[animalIndex].animalPerSpawnPoint).ToString();
        }
        if (animalIndex == 4)
        {
            spawnList["Duck"] = (int)duckSlider.value * infoForUI[animalIndex].animalPerSpawnPoint;
            duckCount.text = ((int)duckSlider.value * infoForUI[animalIndex].animalPerSpawnPoint).ToString();
        }
        if (animalIndex == 5)
        {
            spawnList["Rabbit"] = (int)rabbitSlider.value * infoForUI[animalIndex].animalPerSpawnPoint;
            rabbitCount.text = ((int)rabbitSlider.value * infoForUI[animalIndex].animalPerSpawnPoint).ToString();
        }
        if (animalIndex == 6)
        {
            spawnList["Turtle"] = (int)turtleSlider.value * infoForUI[animalIndex].animalPerSpawnPoint;
            turtleCount.text = ((int)turtleSlider.value * infoForUI[animalIndex].animalPerSpawnPoint).ToString();
        }
        if (animalIndex == 7)
        {
            spawnList["Possum"] = (int)possumSlider.value * infoForUI[animalIndex].animalPerSpawnPoint;
            possumCount.text = ((int)possumSlider.value * infoForUI[animalIndex].animalPerSpawnPoint).ToString();
        }
        if (animalIndex == 8)
        {
            spawnList["Raccoon"] = (int)raccoonSlider.value * infoForUI[animalIndex].animalPerSpawnPoint;
            raccoonCount.text = ((int)raccoonSlider.value * infoForUI[animalIndex].animalPerSpawnPoint).ToString();
        }
        if (animalIndex == 9)
        {
            spawnList["Pig"] = (int)pigSlider.value * infoForUI[animalIndex].animalPerSpawnPoint;
            pigCount.text = ((int)pigSlider.value * infoForUI[animalIndex].animalPerSpawnPoint).ToString();
        }
        if (animalIndex == 10)
        {
            spawnList["Weasel"] = (int)weaselSlider.value * infoForUI[animalIndex].animalPerSpawnPoint;
            weaselCount.text = ((int)weaselSlider.value * infoForUI[animalIndex].animalPerSpawnPoint).ToString();
        }
        if (animalIndex == 11)
        {
            spawnList["Badger"] = (int)badgerSlider.value * infoForUI[animalIndex].animalPerSpawnPoint;
            badgerCount.text = ((int)badgerSlider.value * infoForUI[animalIndex].animalPerSpawnPoint).ToString();
        }
        if (animalIndex == 12)
        {
            spawnList["Fox"] = (int)foxSlider.value * infoForUI[animalIndex].animalPerSpawnPoint;
            foxCount.text = ((int)foxSlider.value * infoForUI[animalIndex].animalPerSpawnPoint).ToString();
        }
        spawnPoints = 38 - (int)frogSlider.value - (int)mouseSlider.value - (int)pigeonSlider.value - (int)squirrelSlider.value - (int)duckSlider.value - (int)rabbitSlider.value - (int)turtleSlider.value - (int)possumSlider.value - (int)raccoonSlider.value - (int)pigSlider.value - (int)weaselSlider.value - (int)badgerSlider.value - (int)foxSlider.value;
        if (spawnPoints >= 0)
        {
            spawnPointsText.text = spawnPoints.ToString();
            validSpawnList["Frog"] = (int)frogSlider.value;
            validSpawnList["Mouse"] = (int)mouseSlider.value;
            validSpawnList["Pigeon"] = (int)pigeonSlider.value;
            validSpawnList["Squirrel"] = (int)squirrelSlider.value;
            validSpawnList["Duck"] = (int)duckSlider.value;
            validSpawnList["Rabbit"] = (int)rabbitSlider.value;
            validSpawnList["Turtle"] = (int)turtleSlider.value;
            validSpawnList["Possum"] = (int)possumSlider.value;
            validSpawnList["Raccoon"] = (int)raccoonSlider.value;
            validSpawnList["Pig"] = (int)pigSlider.value;
            validSpawnList["Weasel"] = (int)weaselSlider.value;
            validSpawnList["Badger"] = (int)badgerSlider.value;
            validSpawnList["Fox"] = (int)foxSlider.value;
        }
        else
        {
            frogSlider.value = validSpawnList["Frog"];
            mouseSlider.value = validSpawnList["Mouse"];
            pigeonSlider.value = validSpawnList["Pigeon"];
            squirrelSlider.value = validSpawnList["Squirrel"];
            duckSlider.value = validSpawnList["Duck"];
            rabbitSlider.value = validSpawnList["Rabbit"];
            turtleSlider.value = validSpawnList["Turtle"];
            possumSlider.value = validSpawnList["Possum"];
            raccoonSlider.value = validSpawnList["Raccoon"];
            pigSlider.value = validSpawnList["Pig"];
            weaselSlider.value = validSpawnList["Weasel"];
            badgerSlider.value = validSpawnList["Badger"];
            foxSlider.value = validSpawnList["Fox"];
        }
    }

    public void StartGame()
    { gpEvents.StartWorld(spawnList); }
}
