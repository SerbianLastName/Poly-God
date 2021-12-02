using System.Collections;
using System.Numerics;
using System.Collections.Generic;
using UnityEngine;

public class ERC20BalanceOfExample : MonoBehaviour
{
    public WalletUI wallet;
    async void Start()
    {
        string chain = "polygon";
        string network = "mainnet";
        string contract = "0x10cd0edc02bd69bd2d3a789df0a81eb88bd26549";
        string account = PlayerPrefs.GetString("Account");
        // string account = "0x1802426BE9a273174a25975a6d327668566b905b";

        BigInteger balanceOf = await ERC20.BalanceOf(chain, network, contract, account);
        // print(balanceOf);
        wallet.tokenBalance.text = balanceOf.ToString();
    }
}
