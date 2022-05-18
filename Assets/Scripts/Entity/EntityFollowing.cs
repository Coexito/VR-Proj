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

        UISpectatorController.instance.SetSpectatorPositionText("The entity is behind. Prepare to scare");
    }

    void Update() 
    {
        entityParentGizmo.transform.LookAt(playerForward.transform.position);
    }

    public void LookedAt()
    {
        lives--;
        unitsFarApart -= 5;

       UISpectatorController.instance.SetSpectatorHealthText(lives.ToString());

        switch(lives)
        {
            default:
                // Move the entity behind the player, closer each time
                Vector3 newEntityPos = new Vector3(playerForward.transform.position.x - entityParentGizmo.transform.forward.x * unitsFarApart,
                                                    playerForward.transform.position.y,
                                                    playerForward.transform.position.z - entityParentGizmo.transform.forward.z * unitsFarApart);
                
                entityParentGizmo.transform.position = newEntityPos;
                UISpectatorController.instance.SetSpectatorPositionText($"The entity is getting closer... {unitsFarApart} units apart");
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
        
        UISpectatorController.instance.SetSpectatorPositionText("Prepare the final scream! 3 seconds");
        // Little wait to create tension
        yield return new WaitForSeconds(3);
        UISpectatorController.instance.SetSpectatorPositionText("SCREAM!!!");
        entityParentGizmo.transform.DOMove(playerForward.transform.position, finalMovementTime);

        // final sound
        //audioSourceFinal.Play();
        audioSourceHurt.Play();

        // Waits three seconds before finishing the game so the player realizes the entity
        yield return new WaitForSeconds(1.5f);
        Application.Quit();
    }
}
