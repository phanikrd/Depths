using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MoneyCounter : MonoBehaviour
{
    private Text txt;

    private void Awake()
    {
        txt = GetComponent<Text>();
        if (txt == null)
        {
            Debug.LogError("Text component not found!");
        }
        else
        {
            Debug.Log("Text component found and initialized!");
        }
    }

    private void Update()
    {
        txt.text = SaveManager.instance.money + "$";
    }
}
