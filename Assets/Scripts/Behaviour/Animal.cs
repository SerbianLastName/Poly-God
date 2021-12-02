using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Animal : LivingEntity
{

    public const int maxViewDistance = 25;
    [EnumFlags]
    public Species diet;
    public CreatureAction currentAction;
    public Genes genes;
    public Color maleColour;
    public Color femaleColour;

    [Header("Settings")]
    public bool isPrey;
    public float timeBetweenActionChoices = 1.5f;
    float moveSpeed;
    [SerializeField] Vector2 moveSpeedMinMax;
    float sexDrive;
    [SerializeField] Vector2 sexDriveMinMax;
    int litterSize;
    [SerializeField] Vector2 litterMinMax;
    float size;
    [SerializeField] Vector2 sizeMinMax;
    [SerializeField] Vector2 jumpMinMax;
    [SerializeField] Vector2 geneHealthMinMax;
    [SerializeField] Vector2 oldAgeMinMax;
    [SerializeField] Vector2 thinkMinMax;
    [SerializeField] List<string> poopSpawns;
    float timeToDeathByHunger = 200;
    float timeToDeathByThirst = 200;
    float timeToNeedSex = 200;
    float drinkDuration = 6;
    float eatDuration = 10;
    float criticalPercent = 0.7f;
    float oldAge;
    bool pregnant;
    bool isCannibal;

    // Visual settings:
    float moveArcHeight = .2f;

    // State:
    [Header("State")]
    public string animalType;
    public float hunger;
    public float thirst;
    public float horny;
    public float disease;
    public float geneHealth;
    public float cannibalChance;
    float hornyLimit = 2.5f;
    public float age;
    public float term;
    float stateCheck;
    public float poopCheck;

    protected LivingEntity foodTarget;
    protected LivingEntity mateTarget;
    protected LivingEntity preyTarget;
    protected Coord waterTarget;
    Dictionary<string, float> geneStats;
    // Dictionary<string, Vector2> genesToPass;

    // Move data:
    bool animatingMovement;
    Coord moveFromCoord;
    Coord moveTargetCoord;
    Vector3 moveStartPos;
    Vector3 moveTargetPos;
    float moveTime;
    float moveSpeedFactor;
    float moveArcHeightFactor;
    Coord[] path;
    int pathIndex;

    // Other
    float lastActionChooseTime;
    const float sqrtTwo = 1.4142f;
    const float oneOverSqrtTwo = 1 / sqrtTwo;

    // Global Modifiers
    public float hornyMod;
    public float hungerMod;
    public float mutMod;
    public float diseaseMod;

    // Disease Modifiers

    float disHunger = 1;

    // Food Values

    Dictionary<string, float> foodValues = new Dictionary<string, float>() { { "Rabbit", 0.15f }, { "Mouse", 0.05f }, { "Raccoon", 0.25f }, { "Fox", 0.25f } };

    public override void Init(Coord coord)
    {
        base.Init(coord);
        moveFromCoord = coord;
        genes = Genes.RandomGenes(1);
        moveSpeed = UnityEngine.Random.Range(moveSpeedMinMax.x, moveSpeedMinMax.y);
        sexDrive = UnityEngine.Random.Range(sexDriveMinMax.x, sexDriveMinMax.y);
        litterSize = UnityEngine.Random.Range((int)litterMinMax.x, (int)litterMinMax.y);
        size = UnityEngine.Random.Range(sizeMinMax.x, sizeMinMax.y);
        geneHealth = UnityEngine.Random.Range(geneHealthMinMax.x, geneHealthMinMax.y);
        moveArcHeightFactor = UnityEngine.Random.Range(jumpMinMax.x, jumpMinMax.y);
        oldAge = UnityEngine.Random.Range(oldAgeMinMax.x, oldAgeMinMax.y);
        timeBetweenActionChoices = UnityEngine.Random.Range(thinkMinMax.x, thinkMinMax.y);
        transform.localScale = Vector3.one * size;
        Color _color = (genes.isMale) ? maleColour : femaleColour;
        _color += new Color(Mathf.Clamp(_color.r + UnityEngine.Random.Range(-0.05f, 0.025f), 0, 1), Mathf.Clamp(_color.g + UnityEngine.Random.Range(-0.05f, 0.025f), 0, 1), Mathf.Clamp(_color.b + UnityEngine.Random.Range(-0.05f, 0.05f), 0, 1));
        material.color = _color;

        StateCheck();
        ChooseNextAction();
    }

    public void NewInit(Coord coord, Dictionary<string, Vector2> newMinMax)
    {
        base.Init(coord);
        moveFromCoord = coord;
        genes = Genes.RandomGenes(1);
        moveSpeed = UnityEngine.Random.Range(newMinMax["Speed"].x, newMinMax["Speed"].y) + UnityEngine.Random.Range(-0.25f, 0.25f);
        sexDrive = UnityEngine.Random.Range(newMinMax["Sex"].x, newMinMax["Sex"].y) + UnityEngine.Random.Range(-0.25f, 0.25f);
        litterSize = UnityEngine.Random.Range((int)litterMinMax.x, (int)litterMinMax.y);
        size = UnityEngine.Random.Range(sizeMinMax.x, sizeMinMax.y);
        geneHealth = UnityEngine.Random.Range(newMinMax["Health"].x, newMinMax["Health"].y) + UnityEngine.Random.Range(-0.25f, 0.25f);
        moveArcHeightFactor = UnityEngine.Random.Range(jumpMinMax.x, jumpMinMax.y);
        oldAge = UnityEngine.Random.Range(oldAgeMinMax.x, oldAgeMinMax.y);
        timeBetweenActionChoices = UnityEngine.Random.Range(newMinMax["Think"].x, newMinMax["Think"].y) + UnityEngine.Random.Range(-0.25f, 0.25f);
        transform.localScale = Vector3.one * size;
        Color _color = (genes.isMale) ? maleColour : femaleColour;
        _color += new Color(Mathf.Clamp(_color.r + UnityEngine.Random.Range(-0.05f, 0.025f), 0, 1), Mathf.Clamp(_color.g + UnityEngine.Random.Range(-0.05f, 0.025f), 0, 1), Mathf.Clamp(_color.b + UnityEngine.Random.Range(-0.05f, 0.05f), 0, 1));
        material.color = _color;

        StateCheck();
        ChooseNextAction();
    }

    protected virtual void StateCheck()
    {
        hornyMod = GlobalScript.Instance.hornyMod;
        hungerMod = GlobalScript.Instance.hungerMod;
        mutMod = GlobalScript.Instance.mutMod;
        diseaseMod = GlobalScript.Instance.diseaseMod;
    }

    protected void Gestate()
    {
        term += Time.deltaTime * 1 / 5;
        transform.localScale += Vector3.one * (Time.deltaTime * 1 / 5);
        if (term > 0.05f && pregnant)
        {
            Environment.Birth(coord, animalType, litterSize, SetGenes());
            term = 0;
            horny = 0;
            currentAction = CreatureAction.Exploring;
            pregnant = false;
        }
    }
    public void StoreFatherGenes(Dictionary<string, float> dadGenes)
    {
        geneStats = dadGenes;
    }

    public Dictionary<string, Vector2> SetGenes()
    {
        Dictionary<string, Vector2> genesToPass = new Dictionary<string, Vector2>();
        if (geneStats["Speed"] >= moveSpeed)
        { genesToPass.Add("Speed", new Vector2(moveSpeed, geneStats["Speed"])); }
        else
        { genesToPass.Add("Speed", new Vector2(geneStats["Speed"], moveSpeed)); }
        if (geneStats["Sex"] >= sexDrive)
        { genesToPass.Add("Sex", new Vector2(sexDrive, geneStats["Sex"])); }
        else
        { genesToPass.Add("Sex", new Vector2(geneStats["Sex"], sexDrive)); }
        if (geneStats["Health"] >= geneHealth)
        { genesToPass.Add("Health", new Vector2(geneHealth, geneStats["Health"])); }
        else
        { genesToPass.Add("Health", new Vector2(geneStats["Health"], geneHealth)); }
        if (geneStats["Think"] >= timeBetweenActionChoices)
        { genesToPass.Add("Think", new Vector2(timeBetweenActionChoices, geneStats["Think"])); }
        else
        { genesToPass.Add("Think", new Vector2(geneStats["Think"], timeBetweenActionChoices)); }
        return genesToPass;
    }


    protected virtual void GiveDisease(Disease _disease)
    {
        if (_disease == Disease.Cannibal && !isCannibal)
        {
            isCannibal = true;
            // diet += (int)species;
        }
        if (_disease == Disease.SlowLoris && moveSpeed >= 0.2f)
        { moveSpeed = moveSpeed / 2; }
        if (_disease == Disease.Glutton)
        { disHunger += 0.25f; }
        if (_disease == Disease.Incel)
        { sexDrive = 0; }
        if (_disease == Disease.Weak && geneHealth < 5)
        { geneHealth = geneHealth * 2; }
    }

    protected virtual void RandomDisease()
    {
        int max = Enum.GetValues(typeof(Disease)).Length;
        int rand = UnityEngine.Random.Range(0, max);
        GiveDisease((Disease)rand);
        Debug.Log("A " + animalType + " is getting " + (Disease)rand);
    }

    protected virtual void Poop()
    {
        if (poopSpawns.Count != 0 && UnityEngine.Random.value < 0.5f)
        {
            int rand = UnityEngine.Random.Range(0, poopSpawns.Count - 1);
            Environment.NewPlant(coord, poopSpawns[rand]);
            GlobalScript.Instance.plants++;
        }
    }
    protected virtual void Update()
    {

        // Increase hunger and thirst over time
        hunger += (Time.deltaTime * 1 / timeToDeathByHunger) * hungerMod * disHunger;
        thirst += Time.deltaTime * 1 / timeToDeathByThirst;
        horny += (Time.deltaTime * sexDrive / timeToNeedSex) * hornyMod;
        disease += (Time.deltaTime * geneHealth / 200) * diseaseMod;
        age += Time.deltaTime * 1 / 200;
        stateCheck += Time.deltaTime;

        if (stateCheck > 5)
        {
            StateCheck();
        }

        // Animate movement. After moving a single tile, the animal will be able to choose its next action
        if (animatingMovement)
        {
            AnimateMove();
        }
        else
        {
            // Handle interactions with external things, like food, water, mates
            HandleInteractions();
            float timeSinceLastActionChoice = Time.time - lastActionChooseTime;
            if (timeSinceLastActionChoice > timeBetweenActionChoices)
            {
                ChooseNextAction();
            }
        }

        if (hunger >= 1)
        { Die(CauseOfDeath.Hunger); }
        if (hunger >= 0.9f && !isCannibal)
        { GiveDisease(Disease.Cannibal); }
        else if (thirst >= 1)
        { Die(CauseOfDeath.Thirst); }
        if (age >= oldAge)
        {
            if (UnityEngine.Random.value > 0.9999f)
            { Die(CauseOfDeath.Age); }
        }
        if (disease >= 2.5f)
        {
            disease = 0;
            if (UnityEngine.Random.value < geneHealth / 10)
            { RandomDisease(); }
        }
    }

    // Animals choose their next action after each movement step (1 tile),
    // or, when not moving (e.g interacting with food etc), at a fixed time interval
    protected virtual void ChooseNextAction()
    {
        lastActionChooseTime = Time.time;
        bool currentlyEating = currentAction == CreatureAction.Eating && foodTarget && hunger > 0;
        if (hunger >= thirst || currentlyEating && thirst < criticalPercent && horny < 1)
        {
            if (horny < hornyLimit && !isCannibal)
            { FindFood(); }
        }
        else
        {
            if (horny < hornyLimit && !isCannibal)
            { FindWater(); }
        }
        if (horny > hornyLimit && !pregnant && !isCannibal)
        { FindMate(); }
        if (pregnant && !isCannibal)
        { currentAction = CreatureAction.Pregnant; }
        if (isCannibal)
        { FindSelfFood(); }
        if (poopCheck >= 0.5f)
        {
            poopCheck = 0;
            Poop();
        }
        Act();

    }

    protected virtual void FindFood()
    {
        LivingEntity foodSource = Environment.SenseFood(coord, this, FoodPreferencePenalty);
        if (foodSource)
        {
            currentAction = CreatureAction.GoingToFood;
            foodTarget = foodSource;
            CreatePath(foodTarget.coord);
        }
        else
        { currentAction = CreatureAction.Exploring; }
    }

    protected virtual void FindSelfFood()
    {
        List<Animal> preySource = Environment.SensePotentialPrey(coord, this);
        if (preySource.Count > 0)
        {
            currentAction = CreatureAction.GoingToFoodCannibal;
            preyTarget = preySource[0];
            CreatePath(preyTarget.coord);
        }
        else
        { currentAction = CreatureAction.Exploring; }
    }

    protected virtual void FindWater()
    {
        Coord waterTile = Environment.SenseWater(coord);
        if (waterTile != Coord.invalid)
        {
            currentAction = CreatureAction.GoingToWater;
            waterTarget = waterTile;
            CreatePath(waterTarget);
        }
        else
        { currentAction = CreatureAction.Exploring; }
    }

    protected virtual void FindMate()
    {
        List<Animal> mateSource = Environment.SensePotentialMates(coord, this);
        if (mateSource.Count > 0)
        {
            currentAction = CreatureAction.GoingToMate;
            mateTarget = mateSource[0];
            CreatePath(mateTarget.coord);
        }
        else
        { currentAction = CreatureAction.SearchingForMate; }
    }

    // When choosing from multiple food sources, the one with the lowest penalty will be selected
    protected virtual int FoodPreferencePenalty(LivingEntity self, LivingEntity food)
    {
        return Coord.SqrDistance(self.coord, food.coord);
    }

    protected void Act()
    {
        switch (currentAction)
        {
            case CreatureAction.Exploring:
                StartMoveToCoord(Environment.GetNextTileWeighted(coord, moveFromCoord));
                break;

            case CreatureAction.Pregnant:
                Gestate();
                break;

            case CreatureAction.SearchingForMate:
                StartMoveToCoord(Environment.GetNextTileWeighted(coord, moveFromCoord));
                break;

            case CreatureAction.GoingToFood:
                if (Coord.AreNeighbours(coord, foodTarget.coord))
                {
                    LookAt(foodTarget.coord);
                    currentAction = CreatureAction.Eating;
                }
                else
                {
                    StartMoveToCoord(path[pathIndex]);
                    pathIndex++;
                }
                break;
            case CreatureAction.GoingToFoodCannibal:
                if (Coord.AreNeighbours(coord, preyTarget.coord))
                {
                    LookAt(preyTarget.coord);
                    currentAction = CreatureAction.EatingCannibal;
                }
                else
                {
                    StartMoveToCoord(path[pathIndex]);
                    pathIndex++;
                }
                break;

            case CreatureAction.GoingToWater:
                if (Coord.AreNeighbours(coord, waterTarget))
                {
                    LookAt(waterTarget);
                    currentAction = CreatureAction.Drinking;
                }
                else
                {
                    StartMoveToCoord(path[pathIndex]);
                    pathIndex++;
                }
                break;

            case CreatureAction.GoingToMate:
                if (Coord.AreNeighbours(coord, mateTarget.coord))
                {
                    LookAt(mateTarget.coord);
                    if (genes.isMale)
                    {
                        horny = 0;
                        currentAction = CreatureAction.Exploring;
                        Animal _mate = mateTarget.GetComponent<Animal>();
                        _mate.pregnant = true;
                        _mate.currentAction = CreatureAction.Pregnant;
                        Dictionary<string, float> genes = new Dictionary<string, float>() { { "Speed", moveSpeed }, { "Sex", sexDrive }, { "Health", geneHealth }, { "Think", timeBetweenActionChoices } };
                        _mate.StoreFatherGenes(genes);
                    }
                    else
                    {
                        // currentAction = CreatureAction.Pregnant;
                        // pregnant = true;
                    }
                }
                else
                {
                    StartMoveToCoord(path[pathIndex]);
                    pathIndex++;
                }
                break;
        }
    }

    protected void CreatePath(Coord target)
    {
        // Create new path if current is not already going to target
        if (path == null || pathIndex >= path.Length || (path[path.Length - 1] != target || path[pathIndex - 1] != moveTargetCoord))
        {
            path = EnvironmentUtility.GetPath(coord.x, coord.y, target.x, target.y);
            pathIndex = 0;
        }
    }

    protected void StartMoveToCoord(Coord target)
    {
        moveFromCoord = coord;
        moveTargetCoord = target;
        moveStartPos = transform.position;
        moveTargetPos = Environment.tileCentres[moveTargetCoord.x, moveTargetCoord.y];
        animatingMovement = true;

        bool diagonalMove = Coord.SqrDistance(moveFromCoord, moveTargetCoord) > 1;
        moveArcHeightFactor = (diagonalMove) ? sqrtTwo : 1;
        moveSpeedFactor = (diagonalMove) ? oneOverSqrtTwo : 1;

        LookAt(moveTargetCoord);
    }

    protected void LookAt(Coord target)
    {
        if (target != coord)
        {
            Coord offset = target - coord;
            transform.eulerAngles = Vector3.up * Mathf.Atan2(offset.x, offset.y) * Mathf.Rad2Deg;
        }
    }

    void HandleInteractions()
    {
        if (currentAction == CreatureAction.Eating)
        {
            if (foodTarget && hunger > 0 && foodTarget.isPlant)
            {
                float eatAmount = Mathf.Min(hunger, Time.deltaTime * 1 / eatDuration);
                eatAmount = ((Plant)foodTarget).Consume(eatAmount);
                hunger -= eatAmount;
                poopCheck += eatAmount;
            }
            if (foodTarget && hunger > 0 && !foodTarget.isPlant)
            {
                hunger -= foodValues[((Animal)foodTarget).animalType];
                poopCheck += foodValues[((Animal)foodTarget).animalType];
                foodTarget.Die(CauseOfDeath.Eaten);
            }
        }
        if (currentAction == CreatureAction.EatingCannibal)
        {
            hunger -= foodValues[((Animal)preyTarget).animalType];
            preyTarget.Die(CauseOfDeath.Eaten);
        }
        else if (currentAction == CreatureAction.Drinking)
        {
            if (thirst > 0)
            {
                thirst -= Time.deltaTime * 1 / drinkDuration;
                thirst = Mathf.Clamp01(thirst);
            }
        }
    }
    void AnimateMove()
    {
        // Move in an arc from start to end tile
        moveTime = Mathf.Min(1, moveTime + Time.deltaTime * moveSpeed * moveSpeedFactor);
        float height = (1 - 4 * (moveTime - .5f) * (moveTime - .5f)) * moveArcHeight * moveArcHeightFactor;
        transform.position = Vector3.Lerp(moveStartPos, moveTargetPos, moveTime) + Vector3.up * height;

        // Finished moving
        if (moveTime >= 1)
        {
            Environment.RegisterMove(this, coord, moveTargetCoord);
            coord = moveTargetCoord;
            animatingMovement = false;
            moveTime = 0;
            ChooseNextAction();
        }
    }

    // Extra functions    

    void OnDrawGizmosSelected()
    {
        if (Application.isPlaying)
        {
            var surroundings = Environment.Sense(coord);
            Gizmos.color = Color.white;
            if (surroundings.nearestFoodSource != null)
            {
                Gizmos.DrawLine(transform.position, surroundings.nearestFoodSource.transform.position);
            }
            if (surroundings.nearestWaterTile != Coord.invalid)
            {
                Gizmos.DrawLine(transform.position, Environment.tileCentres[surroundings.nearestWaterTile.x, surroundings.nearestWaterTile.y]);
            }

            if (currentAction == CreatureAction.GoingToFood)
            {
                var path = EnvironmentUtility.GetPath(coord.x, coord.y, foodTarget.coord.x, foodTarget.coord.y);
                Gizmos.color = Color.black;
                for (int i = 0; i < path.Length; i++)
                {
                    Gizmos.DrawSphere(Environment.tileCentres[path[i].x, path[i].y], .2f);
                }
            }
        }
    }
}