using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    [SerializeField] AudioSource audioSource;

    [Header("Clips")]
    [SerializeField] AudioClip[] rocketStarts;
    [SerializeField] AudioClip[] rocketHits;
    [SerializeField] AudioClip laserBeam;
    [SerializeField] AudioClip batteryPickup;
    [SerializeField] AudioClip bzzt;

    public void PlayRocketStart()
    {
        int index = Random.Range(0, rocketStarts.Length);
        audioSource.PlayOneShot(rocketStarts[index], 1f);
    }

    public void PlayRocketHit()
    {
        int index = Random.Range(0, rocketHits.Length);
        audioSource.PlayOneShot(rocketHits[index], 0.5f);
    }

    public void PlayLaserBeam()
    {
        audioSource.PlayOneShot(laserBeam);
    }

    public void PlayerBatteryPickup()
    {
        audioSource.PlayOneShot(batteryPickup, 0.5f);
    }

    public void PlayBzzt()
    {
        audioSource.PlayOneShot(bzzt, 0.5f);
    }

    public void PlayBigExplosion()
    {
        Debug.Log("Sound");
        audioSource.PlayOneShot(rocketHits[2], 1f);
    }
}
