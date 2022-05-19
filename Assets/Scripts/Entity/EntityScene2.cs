using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityScene2 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartScene2());
    }

    IEnumerator StartScene2()
    {

        yield return new WaitForSeconds(3);
        Debug.Log("Start");
        EntityMovement.instance.StartGame();
    }
}
