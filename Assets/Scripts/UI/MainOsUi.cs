using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainOsUi : MonoBehaviour
{
    public Text lordPoints;
    float updateTime;

    public void Start()
    { lordPoints.text = GlobalScript.Instance.lordPoints.ToString("n0"); }
    public void Update()
    {
        updateTime += Time.deltaTime;
        if (updateTime > 1)
        { lordPoints.text = GlobalScript.Instance.lordPoints.ToString("n0"); }
    }

}
