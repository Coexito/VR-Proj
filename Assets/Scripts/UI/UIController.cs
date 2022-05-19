using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;

    [SerializeField] private GameObject startUI;
    [SerializeField] private GameObject settingsUI;
    [SerializeField] private Slider sliderMaster;
    [SerializeField] private Slider sliderSoundEffects;
    [SerializeField] private Slider sliderMusic;

    [SerializeField] private AudioMixer audioMixer;

    private AudioSource backgroundMusic;

    private void Start() 
    {
        startUI.SetActive(true);
        settingsUI.SetActive(false);
        backgroundMusic = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // UI always looks at player
        if (playerCamera != null)
        {
            this.gameObject.transform.LookAt(transform.position + playerCamera.transform.rotation * Vector3.forward, playerCamera.transform.rotation * Vector3.up);
        }   
    }

    #region Start UI
    public void StartGame()
    {
        startUI.SetActive(false);
        EntityMovement.instance.StartGame();
        backgroundMusic.Play();
    }

    public void OpenSettings()
    {
        startUI.SetActive(false);
        settingsUI.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    #endregion

    #region Settings UI
    public void ExitSettings()
    {
        settingsUI.SetActive(false);
        startUI.SetActive(true);
    }

    // It's important to convert the slider value to logarithmic scale
    // That's why the slider value is   log10(value) * 20

    public void SetMasterVolume()
    {
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(sliderMaster.value) * 20); 
    }

    public void SetSoundEffectsVolume(float sliderValue)
    {
        audioMixer.SetFloat("SoundEffectsVolume", Mathf.Log10(sliderSoundEffects.value) * 20); 
    }

    public void SetMusicVolume(float sliderValue)
    {
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(sliderMusic.value) * 20); 
    }

    #endregion
}
