using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FakeEntity : MonoBehaviour
{
    public static FakeEntity instance;
    public AudioSource fakeEntity;
    public Transform place;
    [SerializeField] private float movementTime = 2f;
    Animator animator;

    [SerializeField] private GameObject player;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        instance = this;

        gameObject.SetActive(false);

    }

    public void StartFakeEntity()
    {
        transform.position = player.transform.position + (player.transform.forward*2);
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