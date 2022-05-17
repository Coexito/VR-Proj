using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UISpectatorController : MonoBehaviour
{
    public static UISpectatorController instance;
    [SerializeField] private TextMeshProUGUI positionTXT;
    [SerializeField] private TextMeshProUGUI healthTXT;
    [SerializeField] private TextMeshProUGUI fakeEntityTXT;

    private void Start() 
    {
        instance = this;
    }

    public void SetSpectatorPositionText(string text)
    {
        if (text != null)
        {
            positionTXT.SetText(text);
        }
    }

    public void SetSpectatorHealthText(string text)
    {
        if (text != null)
            healthTXT.SetText($"Health: {text}");
    }

    public void SetSpectatorFakeEntityText(string text)
    {
        if (text != null)
            fakeEntityTXT.SetText(text);
    }
}
