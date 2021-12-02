using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginScreen : MonoBehaviour
{
    [SerializeField] GameObject clickUI;
    [SerializeField] GameObject loadUI;

    public void Login()
    {
        clickUI.GetComponent<Canvas>().enabled = false;
        loadUI.GetComponent<Canvas>().enabled = true;
    }
}
