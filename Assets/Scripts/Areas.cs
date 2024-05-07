using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

public class Areas : MonoBehaviour
{
    public Transform cam;
    public float playerActivateDistance;
    bool active = false;
    public GameObject theEnemy;
    public int xPos;
    public int zPos;

    private void Update()
    {
        RaycastHit hit;
        active = Physics.Raycast(cam.position, cam.TransformDirection(Vector3.forward), out hit, playerActivateDistance);

        if (Input.GetKeyDown(KeyCode.L) && active == true)
        {
            StartCoroutine(SpawnEnemyWithDelay());
        }
    }

    IEnumerator SpawnEnemyWithDelay()
    {
        yield return new WaitForSeconds(1f);
        xPos = Random.Range(1, 40);
        zPos = Random.Range(1, 30);
        Instantiate(theEnemy, new Vector3(xPos, 30, zPos), Quaternion.identity);
    }
}
