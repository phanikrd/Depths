using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyAdd : MonoBehaviour
{

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            SaveManager.instance.money += 100;
            SaveManager.instance.Save();
        }
        else if (Input.GetKeyDown(KeyCode.O))
        {
            SaveManager.instance.money -= 100;
            SaveManager.instance.Save();
        }
    }
}
