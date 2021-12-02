using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

public class SmartContractInteractions : MonoBehaviour
{
    string abi;
    string testNetContract = "0x7a705FD3b63fee65C8D4D4E7C4307F44826dEDc6";
    string mainNetContract = "0x4c2f6BB56d50DD596C58Bc5656Fd0B035cDD474E";
    string contract;
    string chain = "polygon";
    string network = "testnet";
    string gasLimit = "";
    string gasPrice = "";
    string playerAccount;
    public bool webGLTest;
    [SerializeField] GamePlayEvents gpEvents;

    public void Start()
    {
        gpEvents.LoadingScreen(true);
        contract = testNetContract;
        var _abi = Resources.Load<TextAsset>("testnetABI");
        abi = _abi.ToString();
        if (webGLTest)
        {
            playerAccount = PlayerPrefs.GetString("Account");
            CheckPlots();
        }
        else
        {
            playerAccount = "0x1802426BE9a273174a25975a6d327668566b905b";
            gpEvents.plot = "2, 34, 345634, 236784527";
            gpEvents.HelloWorld();
        }

    }

    async public void CheckPlots()
    {

        string method = "getPlotIDs";
        string args = "[\"" + playerAccount + "\"]";
        string plots;

        try
        {
            plots = await EVM.Call(chain, network, contract, abi, method, args);
            gpEvents.plot = plots;
            gpEvents.BuyScreen(false);
            gpEvents.HelloWorld();

        }
        catch (Exception e)
        {
            Debug.LogException(e, this);
            // plots = "ERROR";
        }


    }

    async public void CheckPrice()
    {
        string method = "price";
        string args = "[]";
        try
        {
            string plotCost = await EVM.Call(chain, network, contract, abi, method, args);
            Debug.Log("Price is " + plotCost);
            BuyLand(plotCost);
        }
        catch (Exception e)
        {
            Debug.Log("Price check failed");
        }
    }

    async public void BuyLand(string _value)
    {
        string method = "addPlotID";
        string args = "[]";
        string value = _value;

        try
        {
            string response = await Web3GL.SendContract(method, abi, contract, args, value, gasLimit, gasPrice);
            Debug.Log(response);
            Invoke("ReCheck", 10);

        }
        catch (Exception e)
        {
            Debug.LogException(e, this);
        }

    }

    void ReCheck()
    { CheckPlots(); }
}
