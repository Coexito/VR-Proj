using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityMovement : MonoBehaviour
{

    public Transform[] places;
    int posNum = 0;
    public float moveSpeed;

    public float startTime = 2.0f;
    public float movementIteration = 2.0f;

    public float movementTime = 0.5f;

    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }


    void Start()
    {
        InvokeRepeating("MovePos", startTime, movementIteration);
    }


    void MovePos()
    {
        bool moveTransition = Random.value > 0.7f;
        

        if (!moveTransition)
        {
            Teleport(places);
        }
        else
        {
            Move(places);
        }

        
    }


    private void Move(Transform [] p)
    {
        bool isNext = Random.value > .5f;

        if (isNext)
        {
            posNum = (posNum + 1)%p.Length;
        }else
        {
            posNum--;
            posNum = posNum < 0 ? p.Length - 1 : posNum;
        }

        var nextPlace = p[posNum].position;

        animator.SetBool("isMoving", true);
        transform.DOLookAt(nextPlace, 0.05f);
        transform.DOMove(nextPlace, movementTime).OnComplete(() =>
        {
            animator.SetBool("isMoving", false);
            transform.DOLookAt(Vector3.zero, 0.2f);
        });
    }

    private void Teleport(Transform[] p)
    {
        posNum = Random.Range(0, places.Length);
        var newPos = p[posNum].position;
        transform.position = newPos;
        transform.LookAt(Vector3.zero);
        

    }
}
