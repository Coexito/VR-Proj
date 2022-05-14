using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityMovement : MonoBehaviour
{

    public Vector3[] places;
    int posNum = 0;

    public float startTime = 2.0f;
    public float movementIteration = 2.0f;

    bool transition = false;
    public float movementSpeed = 0.5f;
   

    void Start()
    {
        InvokeRepeating("MovePos", startTime, movementIteration);
    }

    void MovePos()
    {
        transition = Random.value > 0.5f;

        if (!transition)
        {
            transform.position = places[posNum];
        }
        else
        {

        }
        
        posNum++;
        if(posNum == places.Length)
        {
            posNum = 0;
        }
    }

   
}
