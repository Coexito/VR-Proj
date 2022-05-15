using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    public static SceneController instance;  // Singleton

    public UISpectatorController spectatorCanvas;
    
    // Start is called before the first frame update
    void Start()
    {
        // Set the Singleton
        instance = this;

        spectatorCanvas.SetSpectatorText("Welcome, spectator.");
    }
    


}
