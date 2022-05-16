using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UISpectatorController : MonoBehaviour
{
    public static UISpectatorController instance;
    [SerializeField] private TextMeshProUGUI topTXT;

    private void Start() 
    {
        instance = this;
    }

    public void SetSpectatorText(string text)
    {
        if (text != null)
        {
            topTXT.SetText(text);
        }
    }
}
