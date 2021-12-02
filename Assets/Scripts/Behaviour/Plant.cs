using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : LivingEntity
{
    float amountRemaining = 10;
    const float consumeSpeed = 16;
    public float waitTime;
    public float respawnTime;
    public string plantType;

    public void Start()
    { isPlant = true; }


    protected virtual void Update()
    {
        waitTime += Time.deltaTime * 1 / 200;
        if (waitTime >= respawnTime)
        {
            Regrow();
        }

    }

    public float Consume(float amount)
    {

        float amountConsumed = Mathf.Max(0, Mathf.Min(amountRemaining, amount));
        Mathf.Clamp(amountRemaining -= amount * consumeSpeed, 0, 1);
        transform.localScale = Vector3.one * amountRemaining / 10;

        if (amountRemaining <= 0)
        {
            Die(CauseOfDeath.Eaten);
            // GlobalScript.Instance.plants -= 1;
            Destroy(gameObject);
        }

        return amountConsumed;
    }

    protected virtual void Regrow()
    {
        amountRemaining += 0.05f;
        transform.localScale = Vector3.one * amountRemaining / 10;

        if (amountRemaining >= 10)
        {
            waitTime = 0;
        }
    }

    public float AmountRemaining
    {
        get
        {
            return amountRemaining;
        }
    }

}