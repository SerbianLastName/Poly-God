using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoPlots : MonoBehaviour
{
    [SerializeField] GameObject buyLandButton;
    [SerializeField] GamePlayEvents gpEvents;

    void start()
    {
        Button btnBuyPlot = buyLandButton.GetComponent<Button>();
        btnBuyPlot.onClick.AddListener(BuyPlotClick);
    }
    void BuyPlotClick()
    {
        Debug.Log("Buy Button Clicked");
        gpEvents.BuyLand();
        gameObject.GetComponent<Canvas>().enabled = false;
    }


}
