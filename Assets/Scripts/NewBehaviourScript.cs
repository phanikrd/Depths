using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        //SaveManager.instance.money += 100;
        //SaveManager.instance.Save();
        Destroy(gameObject);
        Destroy(collision.gameObject);


    }
}
