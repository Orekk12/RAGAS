using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MenuManager : MonoBehaviour
{
    [SerializeField] GameObject pausePanel;
    [SerializeField] AudioMixer musicMixer;
    [SerializeField] AudioMixer SFXMixer;
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] gameState gameState;

    [SerializeField] GameObject energyBar;
    [SerializeField] Text txt;
    [SerializeField] SoundSettings soundSettings;
    [SerializeField] Slider musicSilder, SFXSlider;

    public bool isPaused = false;
    bool rekininAnnesininAmi = true;

    void Start()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("score", 0);
        SetSoundSettings();
        Time.timeScale =  1;
        energyBar.SetActive(true);
        isPaused = false;
        
    }
    // Update is called once per frame
    void Update()
    {
        if (gameState.get_isPlayerAlive() == false && rekininAnnesininAmi == true)
        {
            GameOver();
            rekininAnnesininAmi = false;
        }

        ProcessPauseInput();
    }

    void ProcessPauseInput()
    {
        if ((gameOverPanel.activeSelf == false) && (pausePanel.activeSelf == false) && (Input.GetKeyDown(KeyCode.Escape)))
        {
            pausePanel.SetActive(true);
            Time.timeScale = 0;
            isPaused = true;
            Debug.Log(gameState.isPlayerAlive);
        }

        else if ((pausePanel.activeSelf == true) && (Input.GetKeyDown(KeyCode.Escape)))
        {
            pausePanel.SetActive(false);
            Time.timeScale = 1;
            isPaused = false;
        }
    }

    public void Continue()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1;
        isPaused = false;
    }

    public void Restart()
    {
        SceneManager.LoadScene(2);
    }

    public void Menu()
    {
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        Application.Quit();
    }

    void SetSoundSettings()
    {
        musicMixer.SetFloat("MusicVol", soundSettings.musicVolume);
        SFXMixer.SetFloat("SFXVol", soundSettings.SetSFXVolume);
        musicSilder.value = soundSettings.musicVolume; 
        SFXSlider.value = soundSettings.SetSFXVolume;
    }

    public void SetMusicVolume(float sliderValue)
    {
        musicMixer.SetFloat("MusicVol", Mathf.Log10(sliderValue)*20);
        soundSettings.musicVolume = Mathf.Log10(sliderValue)*20;
    }

    public void SetSFXVolume(float sliderValue)
    {
        SFXMixer.SetFloat("SFXVol", Mathf.Log10(sliderValue)*20);
        soundSettings.SetSFXVolume = Mathf.Log10(sliderValue)*20;
    }

    void GameOver()
    {
        if (gameState.get_isPlayerAlive() == false)
        {
            Time.timeScale = 0;
            energyBar.SetActive(false);
            StartCoroutine(GameOverDelay());
            isPaused = true;

            if(gameState.get_score() > PlayerPrefs.GetInt("score"))
            {
                Debug.Log("HIGHSCORE");
                PlayerPrefs.SetInt("score", gameState.get_score());
                txt.text = "NEW HIGHSCORE! \n" + PlayerPrefs.GetInt("score").ToString();
            }
            else
            {
                txt.text = "YOUR SCORE\n " + gameState.get_score().ToString() + "\n\nHIGHSCORE \n" + PlayerPrefs.GetInt("score").ToString();
            }
        }
    }

    IEnumerator GameOverDelay()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        gameOverPanel.SetActive(true);
    }
}
