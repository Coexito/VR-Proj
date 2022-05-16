using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UISpectatorController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI topTXT;

    public void SetSpectatorText(string text)
    {
        if (text != null)
        {
            topTXT.SetText(text);
        }
    }
}
