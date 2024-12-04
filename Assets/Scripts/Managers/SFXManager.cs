using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    // Despite hearing that singletons should be avoided, I decided to use one hear to get more familiar with the concept
    [SerializeField] private AudioSource audioSourcePrefab;
    public static SFXManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void PlayAudioClip(AudioClip audioClip, Transform spawnTransform, float volume, bool randomisePitch = false, float pitchRange = 0.15f)
    {
        // Spawn the audio source
        AudioSource audioSource = Instantiate(audioSourcePrefab, spawnTransform.position, Quaternion.identity);
        // Assign the audio clip
        audioSource.clip = audioClip;
        // Assign the volume
        audioSource.volume = volume;
        // Assign the pitch if it's randomised
        if (randomisePitch)
        {
            // Give the pitch slight randomness to make it less repetitive
            audioSource.pitch = Random.Range(1f - pitchRange, 1f + pitchRange);
        }
        // Play the audio clip
        audioSource.Play();
        // Get the length of the audio clip
        float clipLength = audioSource.clip.length;
        // Destroy the audio source when the clip ends
        Destroy(audioSource.gameObject, clipLength);
    }

    public void PlayGunshotAudioClip(AudioClip audioClip, Transform spawnTransform, float volume, bool hasAmmo, int ammoCount = 1)
    {
        // Spawn the audio source
        AudioSource audioSource = Instantiate(audioSourcePrefab, spawnTransform.position, Quaternion.identity);
        // Assign the audio clip
        audioSource.clip = audioClip;
        // Assign the volume
        audioSource.volume = volume;
        // Assign the pitch
        if (hasAmmo)
        {
            // If the gun's has less than 5 ammo, increase the pitch of the shot the closer the ammo count is to 0
            if (ammoCount < 5)
            {
                audioSource.pitch += (6 - ammoCount) * 0.1f;
            }
        }
        else
        {
            // Give the pitch slight randomness to make it less repetitive
            audioSource.pitch = Random.Range(1f, 1.25f);
        }
        // Play the audio clip
        audioSource.Play();
        // Get the length of the audio clip
        float clipLength = audioSource.clip.length;
        // Destroy the audio source when the clip ends
        Destroy(audioSource.gameObject, clipLength);
    }
}
