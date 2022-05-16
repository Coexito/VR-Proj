using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FakeEntity : MonoBehaviour
{

    public AudioSource fakeEntity;
    
    public Transform place;
    
    public float movementTime = 0.5f;

    Animator animator;

    public static FakeEntity instance;



    private void Awake()
    {
        animator = GetComponent<Animator>();
        gameObject.SetActive(false);

    }
    public void StartFakeEntity()
    {
        gameObject.SetActive(true);
        fakeEntity.Play();
        Move(place);
    }



    private void Move(Transform p)
    {

        var nextPlace = p.position;

        animator.SetBool("isMoving", true);
        transform.DOLookAt(nextPlace, 0.05f);
        transform.DOMove(nextPlace, movementTime).OnComplete(() =>
        {
            animator.SetBool("isMoving", false);
            gameObject.SetActive(false);
        });
    }

}