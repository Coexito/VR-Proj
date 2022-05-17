using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class SwingingArmMotion : MonoBehaviour
{
    // GameObject references
    [SerializeField] private GameObject leftHand;
    [SerializeField] private GameObject rightHand;
    [SerializeField] private GameObject centerEyeCamera;
    [SerializeField] private GameObject forwardDirection;  // Empty gameobject used for the transform.forward parallel to the ground
    [SerializeField] private Transform spawnPosition;  // Spawning to start the game without movement error

    //Vector3 positions
    private Vector3 positionPreviousFrameLeftHand;
    private Vector3 positionPreviousFrameRightHand;
    private Vector3 positionPreviousFramePlayer;
    private Vector3 positionThisFrameLeftHand;
    private Vector3 positionThisFrameRightHand;
    private Vector3 positionThisFramePlayer;

    // Action for running
    private InputAction actionRun;
    [Space][SerializeField] private InputActionAsset myActionRunAsset;

    // Speed
    [SerializeField] private float speed = 70;
    private float handSpeed;

    // HMD UI
    [SerializeField] private TextMeshProUGUI movementTXT;


    // Testing variables (not in use)
    private float differenceNeeded = 0.1f;
    private float secondsToAllowMovement = 2f;

    void Start()
    {
        // Set original previous frame positions at start
        positionPreviousFramePlayer = transform.position;
        positionPreviousFrameLeftHand = leftHand.transform.position;
        positionPreviousFrameRightHand = rightHand.transform.position;

        actionRun = myActionRunAsset.FindAction("XRI LeftHand Interaction/Run");

        StartCoroutine(RemindMovement());
    }

    void Update()
    {
        // Move the player only when the game fully starts after x seconds
        if (Time.timeSinceLevelLoad > secondsToAllowMovement)
        {
            if(actionRun.IsPressed())  // If the button is being pressed
            {
                ArmSwingMovement();
            }
        }    
        else
            transform.position = spawnPosition.position;

    }

    IEnumerator RemindMovement()
    {
        movementTXT.SetText("Hold the left grip and swing your arms to run.");
        yield return new WaitForSeconds(13);
        movementTXT.SetText("");
    }

    private void ArmSwingMovement()
    {
        float yRotation = centerEyeCamera.transform.eulerAngles.y;
        forwardDirection.transform.eulerAngles = new Vector3(0, yRotation, 0);  // Forward direction parallel to the ground

        // Current position of hands and player
        positionThisFrameLeftHand = leftHand.transform.position;
        positionThisFrameRightHand = rightHand.transform.position;
        positionThisFramePlayer = transform.position;

        // Get distance the hands and player have moved since the last frame
        var playerDistanceMoved = Vector3.Distance(positionThisFramePlayer, positionPreviousFramePlayer);
        var leftHandDistanceMoved = Vector3.Distance(positionThisFrameLeftHand, positionPreviousFrameLeftHand);
        var rightHandDistanceMoved = Vector3.Distance(positionThisFrameRightHand, positionPreviousFrameRightHand);

        // Add them to get the handspeed from the user
        // It's neccesary to subtract the movement of the player to neglect the movement and not increment every frame
        handSpeed = ((leftHandDistanceMoved - playerDistanceMoved) + (rightHandDistanceMoved - playerDistanceMoved));

        // Calculate relative movement to know if moving forwards
        Vector3 progress = forwardDirection.transform.forward * handSpeed * speed * Time.deltaTime;
        var transformedPos = forwardDirection.transform.InverseTransformPoint(transform.position + progress); 

        // If we're moving torwards
        if(transformedPos.z > 0)  
            transform.position += progress;

        // Set previous positions for hands and player for the next frame
        positionPreviousFrameLeftHand = positionThisFrameLeftHand;
        positionPreviousFrameRightHand = positionThisFrameRightHand;
        positionPreviousFramePlayer= positionThisFramePlayer;

        
        // // ====================================================================================
        // //  INTENTO DE MOVER SOLO CUANDO LOS BRAZOS SE MUEVAN EN VERTICAL
        // //  (el movimiento no es tan bueno)
    
        // // Get difference in y
        // float differenceYLeftHand = Mathf.Abs(positionThisFrameLeftHand.y - positionPreviousFrameLeftHand.y);
        // float differenceYRightHand = Mathf.Abs(positionThisFrameRightHand.y - positionPreviousFrameRightHand.y);

        // // If the difference is enough (the player is swinging the arms vertically), do the movement 
        // if(differenceNeeded <= differenceYLeftHand || differenceNeeded <= differenceYRightHand)
        // {
        //     // Movement code
        // }

        // // ====================================================================================
        
    }
}
