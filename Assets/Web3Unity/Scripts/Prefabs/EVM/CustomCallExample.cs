using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

public class CustomCallExample : MonoBehaviour
{
    string abi;
    async void Start()
    {
        var _abi = Resources.Load<TextAsset>("ABI");
        abi = _abi.ToString();
        /*
        // SPDX-License-Identifier: MIT
        pragma solidity ^0.8.0;

        contract AddTotal {
            uint256 public myTotal = 0;

            function addTotal(uint8 _myArg) public {
                myTotal = myTotal + _myArg;
            }
        }
        */
        // set chain: ethereum, moonbeam, polygon etc
        string chain = "polygon";
        // set network mainnet, testnet
        string network = "testnet";
        // smart contract method to call
        string method = "price";
        // abi in json format
        // string abi = "[ { \"inputs\": [ { \"internalType\": \"uint8\", \"name\": \"_myArg\", \"type\": \"uint8\" } ], \"name\": \"addTotal\", \"outputs\": [], \"stateMutability\": \"nonpayable\", \"type\": \"function\" }, { \"inputs\": [], \"name\": \"myTotal\", \"outputs\": [ { \"internalType\": \"uint256\", \"name\": \"\", \"type\": \"uint256\" } ], \"stateMutability\": \"view\", \"type\": \"function\" } ]";
        // address of contract
        string contract = "0x7a705FD3b63fee65C8D4D4E7C4307F44826dEDc6";
        // array of arguments for contract
        string args = "[]";
        // connects to user's browser wallet to call a transaction
        string response = await EVM.Call(chain, network, contract, abi, method, args);
        // display response in game
        print(response);
        Debug.Log(response);
        Debug.Log("That better have been a 1 with 18 zeros behind it");

        // network = "testnet";

        // response = await EVM.Call(chain, network, contract, abi, method, args);
        // // display response in game
        // print(response);
        // Debug.Log(response);
        // Debug.Log("That better have been a 1 with 18 zeros behind it");
    }
}
