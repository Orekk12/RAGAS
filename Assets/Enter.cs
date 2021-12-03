using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enter : MonoBehaviour
{
    [SerializeField] SoundSettings soundSettings;
    // Start is called before the first frame update
    void Start()
    {
        soundSettings.musicVolume = 1;
        soundSettings.SetSFXVolume = 1;

        SceneManager.LoadScene(1);
    }
}
