using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityScene2 : MonoBehaviour
{


    private AudioSource scene2Music;
    // Start is called before the first frame update
    void Start()
    {

        scene2Music = GetComponent<AudioSource>();
        StartCoroutine(StartScene2());
    }

    IEnumerator StartScene2()
    {
        scene2Music.Play();
        yield return new WaitForSeconds(3);
        Debug.Log("Start");
        EntityMovement.instance.StartGame();
    }
}
