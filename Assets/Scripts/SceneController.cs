using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    public static SceneController instance;  // Singleton

    public UISpectatorController spectatorCanvas;

    public Camera cameraHMD;
    public Camera cameraSpectator;
    
    // Start is called before the first frame update
    void Start()
    {
        // Set the Singleton
        instance = this;

        spectatorCanvas.SetSpectatorText("Welcome, spectator.");

        // Change the camera depths so the spectator camera show on pc monitor and HMD on HMD
        cameraHMD.depth = 0;
        cameraSpectator.depth = 1;
    }
    


}
