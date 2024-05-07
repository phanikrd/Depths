using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollisionDestroy : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        SaveManager.instance.money += 100;
        SaveManager.instance.Save();
        Destroy(collision.gameObject);
        

    }
}
