using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EntityMovement : MonoBehaviour
{
    public CapsuleCollider collider;
    private SkinnedMeshRenderer render;
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

    [SerializeField] private GameObject player;


    private void Awake()
    {
        animator = GetComponent<Animator>();
        collider = GetComponent<CapsuleCollider>();
        render = GetComponentInChildren<SkinnedMeshRenderer>();
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

        CheckEntityPosition();
    }



    private void Teleport(Transform[] p)
    {
        posNum = Random.Range(0, places.Length);
        var newPos = p[posNum].position;
        transform.position = newPos;
        transform.LookAt(Vector3.zero);

        CheckEntityPosition();
    }

    public void EntityFound()
    {
        entityHealth -= 1;
        usualEntity.Stop();
        entityHurt.Play();

        if(entityHealth == 2) // When two health remaining, launch the fake entities
        {
            StartCoroutine(LaunchFakeEntity());
        }
        else if (entityHealth == 0)
        {
            SceneManager.LoadScene(1);
        }

        StartCoroutine(ResetEntity());

    }



    IEnumerator ResetEntity()
    {
        render.enabled = false;
        usualEntity.Play();

        yield return new WaitForSeconds(5);

        collider.enabled = true;
        render.enabled = true;
    }

    IEnumerator LaunchFakeEntity()
    {
        // Infinite loop spawning fake entities every random seconds
        while(true)
        {
            int random = Random.Range(2, 10);
            yield return new WaitForSeconds((int)random);
            FakeEntity.instance.StartFakeEntity();
        }
        
    }

    private void CheckEntityPosition()
    {
        // Calculate the new position in relation to player's position
        //  and show it in the Spectator's UI

        var transformedPos = player.transform.InverseTransformPoint(transform.position);
        bool isRight = transformedPos.x > 2;
        bool isFront = transformedPos.z > 0;

        
        string pos = "";

        Vector3 toTarget = (transform.position - player.transform.position).normalized;

        // Front or behind
        if(transformedPos.z > 2)
            pos += "front ";
        else if(transformedPos.z < -2)
            pos += "behind ";

        // Right or left
        if(transformedPos.x > 2)
            pos += "to the right";
        else if(transformedPos.x < -2)
            pos += "to the left";

        
        UISpectatorController.instance.SetSpectatorText(pos);
    }
    
}