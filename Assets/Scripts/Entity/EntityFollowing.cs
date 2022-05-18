using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EntityFollowing : MonoBehaviour
{
    public static EntityFollowing instance;

    [SerializeField] private GameObject entityParentGizmo;  // object to fix the entities gizmo
    [SerializeField] private GameObject playerForward;

    private float unitsFarApart = 25f;
    private float finalMovementTime = 0.7f;

    [SerializeField] int lives = 5;
    [SerializeField] AudioSource audioSourceHurt;
    [SerializeField] AudioSource audioSourceFinal;
    

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    void Update() 
    {
        entityParentGizmo.transform.LookAt(playerForward.transform.position);
    }

    public void LookedAt()
    {
        lives--;
        unitsFarApart -= 5;

       

        switch(lives)
        {
            default:
                // Move the entity behind the player, closer each time
                entityParentGizmo.transform.position = playerForward.transform.position - entityParentGizmo.transform.forward * unitsFarApart;

                // Make a sound
                audioSourceHurt.Play();
                break;
            case 0:
                // Final scream, ends the game
                StartCoroutine(FinishGame());
                
                break;
            
        }
    }

    IEnumerator FinishGame()
    {
        //Debug.Log("Game finished");
        
        // Little wait to create tension
        yield return new WaitForSeconds(3);
        entityParentGizmo.transform.DOMove(playerForward.transform.position, finalMovementTime);

        // final sound
        //audioSourceFinal.Play();
        audioSourceHurt.Play();

        // Waits three seconds before finishing the game so the player realizes the entity
        yield return new WaitForSeconds(1.5f);
        Application.Quit();
    }
}
