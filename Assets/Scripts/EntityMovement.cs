using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EntityMovement : MonoBehaviour
{


    public CapsuleCollider collider;
    public static EntityMovement instance;

    public AudioSource usualEntity;
    public AudioSource entityHurt;


    public Transform[] places;
    int posNum = 0;
    public float moveSpeed;

    public float startTime = 2.0f;
    public float movementIteration = 2.0f;

    public float movementTime = 0.5f;

    [SerializeField] float entityHealth;

    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        collider = GetComponent<CapsuleCollider>();
    }


    void Start()
    {
        gameObject.SetActive(false);

        instance = this;
    }

    public void StartGame()
    {
        gameObject.SetActive(true);
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


    private void Move(Transform[] p)
    {
        bool isNext = Random.value > .5f;

        if (isNext)
        {
            posNum = (posNum + 1) % p.Length;
        }
        else
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

    public void EntityFound()
    {
        entityHealth -= 1;
        usualEntity.Stop();
        entityHurt.Play();
        if (entityHealth == 0)
        {
            SceneManager.LoadScene(1);
        }

        StartCoroutine(ResetEntity());

    }

    IEnumerator ResetEntity()
    {
        GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;
        usualEntity.Play();

        yield return new WaitForSeconds(5);

        GetComponent<CapsuleCollider>().enabled = true;
        GetComponentInChildren<SkinnedMeshRenderer>().enabled = true;
    }
}